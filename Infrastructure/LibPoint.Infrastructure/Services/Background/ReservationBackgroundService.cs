using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Notifications.Commands;
using LibPoint.Application.Features.Seats.Commands;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Entities.Enums;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LibPoint.Infrastructure.Services.Background
{
    public class ReservationBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ReservationBackgroundService> _logger;
        public ReservationBackgroundService(IServiceProvider serviceProvider, ILogger<ReservationBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation($"rezevasyon background servisi başlatıldı. - {DateTime.UtcNow}");

                    using var scope = _serviceProvider.CreateScope();

                    var reservationRepo = scope.ServiceProvider.GetRequiredService<IRepository<Reservation>>();
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                    var now = DateTime.UtcNow;

                    var activeReservations = await reservationRepo.GetAllAsync(r => r.IsActive, true, r => r.Seat);

                    foreach (var reservation in activeReservations)
                    {
                        var endTime = reservation.StartTime.AddMinutes(reservation.Duration);
                        if (now > endTime && !reservation.EndedByUser)
                        {
                            //_logger.LogInformation($"döngü içerisine girildi - {DateTime.UtcNow}");

                            reservation.IsActive = false;
                            reservation.EndedBySession = true;
                            await reservationRepo.SaveChangesAsync();

                            //_logger.LogInformation($"savechanges cağırıldı - {DateTime.UtcNow}");

                            var setSeatFreeCommand = new SetSeatFreeCommandRequest(reservation.SeatId);
                            var setSeatFreeCommandResult = await mediator.Send(setSeatFreeCommand, stoppingToken);
                        }

                        var checkInEndTime = reservation.StartTime.AddMinutes(2);
                        if(now > checkInEndTime && !reservation.CheckIn && !reservation.EndedByUser)
                        {
                            reservation.IsActive = false;
                            reservation.EndedBySession = true;
                            await reservationRepo.SaveChangesAsync();

                            var setSeatFreeCommand2 = new SetSeatFreeCommandRequest(reservation.SeatId);
                            var setSeatFreeCommandResult2 = await mediator.Send(setSeatFreeCommand2, stoppingToken);

                            var createNotificationCommand = new CreateNofiticationCommandRequest
                            {
                                AppUserId = reservation.AppUserId,
                                Title = "Rezevasyon İptali",
                                Message = $"Saat {reservation.StartTime:HH:mm} tarihinde başlayan rezervasyonunuz için belirtilen check-in süresi dolduğu için rezervasyon iptal edilmiştir.",
                                Type = (int)NotificationType.EmailAndSystem
                            };

                            await mediator.Send(createNotificationCommand, stoppingToken);
                            _logger.LogInformation($"Check-in süresi dolduğu için rezervasyon iptal edildi ve bildirim gönderildi.");
                            _logger.LogInformation($"AppUserId ==> {reservation.AppUserId} ----- RezervasyonId ==> {reservation.Id}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "ReservationBackgroundService failed.");
                }

                await Task.Delay(TimeSpan.FromSeconds(30));
            }
        }
    }
}
