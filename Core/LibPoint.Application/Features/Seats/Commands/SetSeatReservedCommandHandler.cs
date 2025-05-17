using LibPoint.Application.Abstractions;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Seats.Commands
{
    public class SetSeatReservedCommandHandler : IRequestHandler<SetSeatReservedCommandRequest, ResponseModel<bool>>
    {
        private readonly ISeatRepository _seatRepository;
        private readonly IRepository<Seat> _repository;
        public SetSeatReservedCommandHandler(ISeatRepository seatRepository, IRepository<Seat> repository)
        {
            _seatRepository = seatRepository;
            _repository = repository;
        }

        public async Task<ResponseModel<bool>> Handle(SetSeatReservedCommandRequest request, CancellationToken cancellationToken)
        {
            var seat = await _repository.GetAsync(s => s.Id == request.SeatId);

            if(seat is null)
            {
                return new ResponseModel<bool>("Seat is null");
            }
            else
            {
                var result = await _seatRepository.SetSeatReservedAsync(request.AppUserId, request.SeatId, request.ReservationId);

                if (result is true)
                    return new ResponseModel<bool>(result);
                else
                    return new ResponseModel<bool>("Setting seat reserved operation is fail");
            }
        }
    }
}
