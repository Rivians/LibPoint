using LibPoint.Application.Abstractions;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Reservations.Commands
{
    public class CheckInReservationCommandHandler : IRequestHandler<CheckInReservationCommandRequest, ResponseModel<string>>
    {
        private readonly IRepository<Reservation> _repository;
        public CheckInReservationCommandHandler(IRepository<Reservation> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseModel<string>> Handle(CheckInReservationCommandRequest request, CancellationToken cancellationToken)
        {
            var reservation = await _repository.GetByIdAsync(request.ReservationId);
            if (reservation is null)
                return new ResponseModel<string>("Reservation not fount", 404);

            if (reservation.CheckInTime.HasValue)
                return new ResponseModel<string>("Reservation already checked in");

            var minutesElapsed = DateTime.UtcNow - reservation.CreatedTime;
            if (minutesElapsed > TimeSpan.FromMinutes(15))
                return new ResponseModel<string>("Check-in time expired");

            reservation.CheckIn = true;
            reservation.CheckInTime = DateTime.UtcNow;
            var saveResult = await _repository.SaveChangesAsync();

            return new ResponseModel<string>
            {
                Success = true,
                Data = null,
                Messages = ["Check-in successful"],
                StatusCode = 200
            };

        }
    }
}
