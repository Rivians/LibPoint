using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Reservations.Commands
{
    public class ReserveSeatWithTransactionCommandRequest : IRequest<ResponseModel<bool>>
    {
        public Guid AppUserId { get; set; }
        public Guid SeatId { get; set; }
        public Guid ReservationId { get; set; }
        public ReserveSeatWithTransactionCommandRequest(Guid appUserId, Guid seatId, Guid reservationId)
        {
            AppUserId = appUserId;
            SeatId = seatId;
            ReservationId = reservationId;
        }
    }
}
