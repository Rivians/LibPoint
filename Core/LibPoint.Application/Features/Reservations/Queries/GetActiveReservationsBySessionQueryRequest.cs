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
    public class GetActiveReservationsBySessionQueryRequest : IRequest<ResponseModel<List<ReservationModel>>>
    {
        public int SessionEnumNumber { get; set; }

        public GetActiveReservationsBySessionQueryRequest(int sessionEnumNumber)
        {
            SessionEnumNumber = sessionEnumNumber;
        }
    }
}
