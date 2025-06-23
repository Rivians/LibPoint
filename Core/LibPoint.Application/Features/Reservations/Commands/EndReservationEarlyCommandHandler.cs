using LibPoint.Application.Abstractions;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Entities.Identity;
using LibPoint.Domain.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
        private readonly IRepository<AppUser> _appUserRepository;
        private readonly UserManager<AppUser> _userManager;
        public EndReservationEarlyCommandHandler(IRepository<Reservation> repository, IRepository<AppUser> appUserRepository, UserManager<AppUser> userManager)
        {
            _repository = repository;
            _appUserRepository = appUserRepository;
            _userManager = userManager;
        }

        public async Task<ResponseModel<bool>> Handle(EndReservationEarlyCommandRequest request, CancellationToken cancellationToken)
        {
            var reservation = await _repository.GetAsync(r => r.Id == request.ReservationId, true, r => r.Seat, r => r.AppUser);
            if (reservation == null || reservation.IsActive == false)
                return new ResponseModel<bool>("Reservation can not found", 404);

            var user = await _appUserRepository.GetAsync(u => u.Id == reservation.AppUser.Id, true);
            if(user == null)
                return new ResponseModel<bool>("User can not found", 404);

            var requestUser = await _appUserRepository.GetAsync(u => u.Id == request.UserId, true);
            if(requestUser is null)
                return new ResponseModel<bool>("UserId is null. It is required data to request", 400);

            if (await _userManager.IsInRoleAsync(requestUser, "Admin") || reservation.AppUserId == request.UserId)
            {
                reservation.IsActive = false;
                reservation.Seat.IsReserved = false;   // seat'in IsReserved değerini false yapcaz.
                reservation.Seat.CurrentReservationId = null;
                reservation.Seat.CurrentAppUserId = null;
                reservation.EndedByUser = true;
            }
                

            if (reservation.AppUserId != request.UserId && await _userManager.IsInRoleAsync(requestUser,"Admin") == false)
                return new ResponseModel<bool>("This reservation does not belong to you.", 403);  // 403 yasak erişim. 

            var saveResult = await _repository.SaveChangesAsync();

            return new ResponseModel<bool>(saveResult);
        }
    }
}