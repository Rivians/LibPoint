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
    public class SetSeatFreeCommandHandler : IRequestHandler<SetSeatFreeCommandRequest, ResponseModel<bool>>
    {
        private readonly ISeatRepository _seatRepository;
        private readonly IRepository<Seat> _repository;
        public SetSeatFreeCommandHandler(ISeatRepository seatRepository, IRepository<Seat> repository)
        {
            _seatRepository = seatRepository;
            _repository = repository;
        }

        public async Task<ResponseModel<bool>> Handle(SetSeatFreeCommandRequest request, CancellationToken cancellationToken)
        {
            var seat = await _repository.GetAsync(s => s.Id == request.Id);

            if (seat is null)
            {
                return new ResponseModel<bool>("Seat is null", 400);
            }
            else
            {
                var result = await _seatRepository.SetSeatFreeAsync(request.Id);

                if (result is true)
                    return new ResponseModel<bool>(result);
                else
                    return new ResponseModel<bool>("Setting seat free operation is fail.");
            }
        }
    }
}
