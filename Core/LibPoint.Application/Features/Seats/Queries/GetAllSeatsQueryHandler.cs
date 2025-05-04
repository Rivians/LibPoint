using LibPoint.Application.Abstractions;
using LibPoint.Domain.Entities;
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
    public class GetAllSeatsQueryHandler : IRequestHandler<GetAllSeatsQueryRequest, ResponseModel<List<SeatModel>>>
    {
        private readonly IRepository<Seat> _repository;
        public GetAllSeatsQueryHandler(IRepository<Seat> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseModel<List<SeatModel>>> Handle(GetAllSeatsQueryRequest request, CancellationToken cancellationToken)
        {
            var seats = await _repository.GetAllAsync();

            // mapleme

            // return
        }
    }
}
