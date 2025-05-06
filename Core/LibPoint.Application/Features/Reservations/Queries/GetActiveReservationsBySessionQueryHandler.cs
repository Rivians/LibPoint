using AutoMapper;
using LibPoint.Application.Abstractions;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Entities.Enums;
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
    public class GetActiveReservationsBySessionQueryHandler : IRequestHandler<GetActiveReservationsBySessionQueryRequest, ResponseModel<List<ReservationModel>>>
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IMapper _mapper;
        public GetActiveReservationsBySessionQueryHandler(IRepository<Reservation> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<ReservationModel>>> Handle(GetActiveReservationsBySessionQueryRequest request, CancellationToken cancellationToken)
        {
            var activeReservations = await _repository.GetAllAsync(r => r.Session == (Session)request.SessionEnumNumber && r.IsActive, false, r => r.Seat);

            if (activeReservations == null)
                return new ResponseModel<List<ReservationModel>>("Active reservations are not found", 404);

            var mappedActiveReservations = _mapper.Map<List<ReservationModel>>(activeReservations);

            return new ResponseModel<List<ReservationModel>>(mappedActiveReservations);
        }
    }
}
