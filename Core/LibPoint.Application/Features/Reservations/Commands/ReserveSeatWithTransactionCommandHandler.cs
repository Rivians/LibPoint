using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Seats.Commands;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Entities.Enums;
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
        private readonly IRepository<Reservation> _reservationRepository;
        private readonly IRepository<Seat> _seatRepository;
        private readonly IMediator _mediator;
        public ReserveSeatWithTransactionCommandHandler(IRepository<Reservation> repository, IMediator mediator, IRepository<Seat> seatRepository)
        {
            _reservationRepository = repository;
            _mediator = mediator;
            _seatRepository = seatRepository;
        }

        public async Task<ResponseModel<bool>> Handle(ReserveSeatWithTransactionCommandRequest request, CancellationToken cancellationToken)
        {
            var transactionResult = await _reservationRepository.ExecuteTransactionAsync(async () =>
            {
                var createReservationCommand = new CreateReservationCommandRequest
                {
                    AppUserId = request.AppUserId,
                    SeatId = request.SeatId,
                    Session = Enum.Parse<Session>(request.Session.ToString()),
                    Duration = request.Duration,
                    StartTime = DateTime.UtcNow
                };

                var reservationCreateResult = await _mediator.Send(createReservationCommand);
                if (reservationCreateResult.Success is false)
                    return false;

                var setSeatReservedCommand = new SetSeatReservedCommandRequest(request.SeatId, request.AppUserId, reservationCreateResult.Data);

                var seatReservationResult = await _mediator.Send(setSeatReservedCommand);
                return seatReservationResult.Success;
            });

            return transactionResult ? new ResponseModel<bool>(transactionResult) : new ResponseModel<bool>("Transaction failed.", 400);
        }
    }
}
