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
    public class EndReservationEarlyCommandHandler : IRequestHandler<EndReservationEarlyCommandRequest, ResponseModel<bool>>
    {
        private readonly IRepository<Reservation> _repository;
        public EndReservationEarlyCommandHandler(IRepository<Reservation> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseModel<bool>> Handle(EndReservationEarlyCommandRequest request, CancellationToken cancellationToken)
        {
            //var reservation = await _repository.GetByIdAsync(request.ReservationId);
            var reservation = await _repository.GetAsync(r => r.Id == request.ReservationId, true, r => r.Seat);

            if (reservation == null || reservation.IsActive == false)
                return new ResponseModel<bool>("Reservation can not found", 404);

            if (reservation.AppUserId != request.AppUserId)
                return new ResponseModel<bool>("This reservation does not belong to you.", 403);  // 403 yasak erişim. 

            reservation.IsActive = false;
            reservation.Seat.IsReserved = false;   // seat'in IsReserved değerini false yapcaz.
            reservation.Seat.CurrentReservationId = null;
            reservation.Seat.CurrentAppUserId = null;

            reservation.EndedByUser = true;

            var saveResult = await _repository.SaveChangesAsync();

            return new ResponseModel<bool>(saveResult);
        }
    }
}