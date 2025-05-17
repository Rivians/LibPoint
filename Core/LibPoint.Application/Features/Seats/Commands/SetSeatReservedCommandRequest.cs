using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Seats.Commands
{
    public class SetSeatReservedCommandRequest : IRequest<ResponseModel<bool>>
    {
        public Guid SeatId { get; set; }
        public Guid AppUserId { get; set; }
        public Guid ReservationId { get; set; }
        public SetSeatReservedCommandRequest(Guid seatId, Guid appUserId, Guid reservationId)
        {
            SeatId = seatId;
            AppUserId = appUserId;
            ReservationId = reservationId;
        }

    }
}
