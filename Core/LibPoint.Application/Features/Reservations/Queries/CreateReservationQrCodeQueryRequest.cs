using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Reservations.Queries
{
    public class CreateReservationQrCodeQueryRequest : IRequest<ResponseModel<string>>
    {
        public Guid ReservationId { get; set; }
        public CreateReservationQrCodeQueryRequest(Guid reservationId)
        {
            ReservationId = reservationId;
        }
    }
}
