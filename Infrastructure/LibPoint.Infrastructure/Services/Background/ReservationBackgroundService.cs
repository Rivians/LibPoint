using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Seats.Commands;
using LibPoint.Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    using var scope = _serviceProvider.CreateScope();

                    var reservationRepo = scope.ServiceProvider.GetRequiredService<IRepository<Reservation>>();
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                    var now = DateTime.UtcNow;

                    var activeReservations = await reservationRepo.GetAllAsync(r => r.IsActive, false, r => r.Seat);

                    foreach (var reservation in activeReservations)
                    {
                        var endTime = reservation.StartTime.AddMinutes(reservation.Duration);
                        if(now > endTime)
                        {
                            reservation.IsActive = false;
                            reservation.EndedBySession = true;
                            reservationRepo.SaveChangesAsync();

                            var setSeatFreeCommand = new SetSeatFreeCommandRequest(reservation.SeatId);
                            var setSeatFreeCommandResult = await mediator.Send(setSeatFreeCommand,stoppingToken);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                await Task.Delay(TimeSpan.FromSeconds(30));
            }
        }
    }
}
