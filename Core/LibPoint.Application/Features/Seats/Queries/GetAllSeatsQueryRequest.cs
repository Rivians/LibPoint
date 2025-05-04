using LibPoint.Domain.Models.Responses;
using LibPoint.Domain.Models.Seats;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Seats.Queries
{
    public class GetAllSeatsQueryRequest : IRequest<ResponseModel<List<SeatModel>>>
    {
    }
}
