using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Reservations.Commands
{
    public class CheckInReservationCommandRequest : IRequest<ResponseModel<string>>
    {
        public Guid ReservationId { get; set; }
        public CheckInReservationCommandRequest(Guid reservationId)
        {
            ReservationId = reservationId;
        }
    }
}
