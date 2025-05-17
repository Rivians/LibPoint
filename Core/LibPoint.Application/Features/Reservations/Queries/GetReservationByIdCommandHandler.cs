using AutoMapper;
using LibPoint.Application.Abstractions;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Reservations;
using LibPoint.Domain.Models.Responses;
using LibPoint.Domain.Models.Seats;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Reservations.Queries
{
    public class GetReservationByIdCommandHandler : IRequestHandler<GetReservationByIdCommandRequest, ResponseModel<ReservationModel>>
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IMapper _mapper;
        public GetReservationByIdCommandHandler(IRepository<Reservation> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<ReservationModel>> Handle(GetReservationByIdCommandRequest request, CancellationToken cancellationToken)
        {
            var reservation = await _repository.GetAsync(s => s.Id == request.Id, false, s => s.AppUser, s => s.Seat);

            if (reservation is null)
                return new ResponseModel<ReservationModel>("Reservation is null", 404);
            else
            {
                var mappedReservation = _mapper.Map<ReservationModel>(reservation);

                return new ResponseModel<ReservationModel>(mappedReservation);
            }
        }
    }
}
