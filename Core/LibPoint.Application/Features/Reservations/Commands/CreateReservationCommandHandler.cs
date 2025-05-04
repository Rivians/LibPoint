using LibPoint.Application.Abstractions;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Responses;
using MediatR;

namespace LibPoint.Application.Features.Reservations.Commands
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommandRequest, ResponseModel<bool>>
    {
        private readonly IRepository<Reservation> _repository;
        public CreateReservationCommandHandler(IRepository<Reservation> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseModel<bool>> Handle(CreateReservationCommandRequest request, CancellationToken cancellationToken)
        {
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
            };

            var addResult = await _repository.AddAsync(reservation);

            if (addResult is false)
                return new ResponseModel<bool>("Reservation could not be created", 400);

            return new ResponseModel<bool>(await _repository.SaveChangesAsync());
            
        }
    }
}
