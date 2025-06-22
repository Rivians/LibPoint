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
    public class GetActiveReservationsQueryRequest : IRequest<ResponseModel<List<ReservationModel>>>
    {
    }
}
