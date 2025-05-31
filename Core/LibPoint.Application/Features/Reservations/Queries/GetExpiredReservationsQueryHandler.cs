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
    public class GetExpiredReservationsQueryHandler : IRequestHandler<GetExpiredReservationsQueryRequest, ResponseModel<List<ReservationModel>>>
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IMapper _mapper;
        public GetExpiredReservationsQueryHandler(IRepository<Reservation> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<ReservationModel>>> Handle(GetExpiredReservationsQueryRequest request, CancellationToken cancellationToken)
        {
            var expiredReservations = await _repository.GetAllAsync(r => r.EndTime < DateTime.UtcNow && r.EndedBySession && r.IsDeleted == false, false, r => r.Seat);

            if (expiredReservations is null)
                return new ResponseModel<List<ReservationModel>>("Expired reservations are not found", 404);

            var mappedExpiredReservations = _mapper.Map<List<ReservationModel>>(expiredReservations);

            return new ResponseModel<List<ReservationModel>>(mappedExpiredReservations);
        }
    }
}