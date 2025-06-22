using AutoMapper;
using LibPoint.Application.Abstractions;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Reservations;
using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Reservations.Queries
{
    public class GetActiveReservationsQueryHandler : IRequestHandler<GetActiveReservationsQueryRequest, ResponseModel<List<ReservationModel>>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Reservation> _repository;
        public GetActiveReservationsQueryHandler(IMapper mapper, IRepository<Reservation> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }       

        public async Task<ResponseModel<List<ReservationModel>>> Handle(GetActiveReservationsQueryRequest request, CancellationToken cancellationToken)
        {
            var activeReservations = await _repository.GetAllAsync(r => r.IsActive, true, r => r.Seat);

            if (activeReservations is null)
                return new ResponseModel<List<ReservationModel>>("Reservations are null", 404);

            var mappedActiveReservations = _mapper.Map<List<ReservationModel>>(activeReservations);

            return new ResponseModel<List<ReservationModel>>(mappedActiveReservations);
        }
    }
}
