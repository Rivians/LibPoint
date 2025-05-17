using LibPoint.Application.Abstractions;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Reservations.Commands
{
    public class ReserveSeatWithTransactionCommandHandler : IRequestHandler<ReserveSeatWithTransactionCommandRequest, ResponseModel<bool>>
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IMediator _mediator;
        public ReserveSeatWithTransactionCommandHandler(IRepository<Reservation> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<ResponseModel<bool>> Handle(ReserveSeatWithTransactionCommandRequest request, CancellationToken cancellationToken)
        {
            using(var transaction = await _repository.Table.)
        }
    }
}
