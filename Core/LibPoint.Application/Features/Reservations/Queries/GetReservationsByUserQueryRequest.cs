using LibPoint.Domain.Models.Reservations;
using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Reservations.Queries
{
    public class GetReservationsByUserQueryRequest : IRequest<ResponseModel<List<ReservationModel>>>
    {
        public Guid AppUserId { get; set; }
        public GetReservationsByUserQueryRequest(Guid appUserId)
        {
            AppUserId = appUserId;
        }
    }
}
