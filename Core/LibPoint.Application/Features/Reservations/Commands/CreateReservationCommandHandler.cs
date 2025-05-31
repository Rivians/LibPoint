using LibPoint.Application.Abstractions;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Responses;
using MediatR;

namespace LibPoint.Application.Features.Reservations.Commands
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommandRequest, ResponseModel<Guid>>
    {
        private readonly IRepository<Reservation> _repository;
        public CreateReservationCommandHandler(IRepository<Reservation> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseModel<Guid>> Handle(CreateReservationCommandRequest request, CancellationToken cancellationToken)
        {
            // yollanan session'un güncel session ile eşleşip eşleşmediği kontrol edilmeli.

            // yollanan duration 1-3 saat aralığında olup olmadığı kontrol edilmeli.

            var sessionEndTime = request.StartTime.AddMinutes(request.Duration);

            var reservation = new Reservation
            {
                AppUserId = request.AppUserId,
                SeatId = request.SeatId,
                StartTime = request.StartTime,
                EndTime = sessionEndTime,
                Duration = request.Duration,
                Session = request.Session,
                CheckIn = false,
                EndedBySession = false,
                IsActive = true
            };

            var addResult = await _repository.AddAsync(reservation);

            if (addResult is false)
                return new ResponseModel<Guid>("Reservation could not be created", 400);

            await _repository.SaveChangesAsync();
            return new ResponseModel<Guid>(reservation.Id);            
        }
    }
}
