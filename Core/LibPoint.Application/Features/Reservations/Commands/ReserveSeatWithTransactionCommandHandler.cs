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
            bool transactionResult = await _reservationRepository.ExecuteTransactionAsync(async () =>
            {
                var utcNow = DateTime.UtcNow;
                var session = DetermineSession(utcNow);

                if (session == -1)
                    return false;

                var createReservationCommand = new CreateReservationCommandRequest
                {
                    AppUserId = request.AppUserId,
                    SeatId = request.SeatId,
                    Session = (Session)session,
                    Duration = request.Duration,
                    StartTime = utcNow
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

        private int DetermineSession(DateTime utcTime)
        {
            var localTime = utcTime.AddHours(3); // tr saatine denk oluyor

            var time = localTime.TimeOfDay;

            if (time >= TimeSpan.FromHours(8) && time < TimeSpan.FromHours(13))
                return 0;
            else if (time >= TimeSpan.FromHours(13) && time < TimeSpan.FromHours(18))
                return 1;
            else if (time >= TimeSpan.FromHours(18) && time < TimeSpan.FromHours(23))
                return 2;

            return -1;            
        }
    }
}

//Morning,    // 08:00 - 13:00
//Afternoon,  // 13:00 - 18:00
//Evening     // 18:00 - 23:00
