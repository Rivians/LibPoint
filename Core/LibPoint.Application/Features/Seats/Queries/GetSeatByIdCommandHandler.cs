using AutoMapper;
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
    public class GetSeatByIdCommandHandler : IRequestHandler<GetSeatByIdCommandRequest, ResponseModel<SeatModel>>
    {
        private readonly IRepository<Seat> _repository;
        private readonly IMapper _mapper;
        public GetSeatByIdCommandHandler(IRepository<Seat> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<SeatModel>> Handle(GetSeatByIdCommandRequest request, CancellationToken cancellationToken)
        {
            var seat = await _repository.GetAsync(s => s.Id == request.Id, false, s => s.Reservations);

            if (seat is null)
                return new ResponseModel<SeatModel>("Seat is null", 404);
            else
            {
                var mappedSeat = _mapper.Map<SeatModel>(seat);

                return new ResponseModel<SeatModel>(mappedSeat);
            }
        }
    }
}
