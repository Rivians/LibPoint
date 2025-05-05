using AutoMapper;
using LibPoint.Application.Abstractions;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Responses;
using LibPoint.Domain.Models.Seats;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Seats.Queries
{
    public class GetAllSeatsQueryHandler : IRequestHandler<GetAllSeatsQueryRequest, ResponseModel<List<SeatModel>>>
    {
        private readonly IRepository<Seat> _repository;
        private readonly IMapper _mapper;
        public GetAllSeatsQueryHandler(IRepository<Seat> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<SeatModel>>> Handle(GetAllSeatsQueryRequest request, CancellationToken cancellationToken)
        {
            var seatList = await _repository.GetAllAsync();

            if (seatList is null)
                return new ResponseModel<List<SeatModel>>("Seats are not found", 404);

            var mappedSeatList = _mapper.Map<List<SeatModel>>(seatList);

            return new ResponseModel<List<SeatModel>>(mappedSeatList);            
        }
    }
}
