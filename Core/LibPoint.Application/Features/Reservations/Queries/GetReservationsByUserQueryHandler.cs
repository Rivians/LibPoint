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
    public class GetReservationsByUserQueryHandler : IRequestHandler<GetReservationsByUserQueryRequest, ResponseModel<List<ReservationModel>>>
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IMapper _mapper;
        public GetReservationsByUserQueryHandler(IRepository<Reservation> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<ReservationModel>>> Handle(GetReservationsByUserQueryRequest request, CancellationToken cancellationToken)
        {
            var reservations = await _repository.GetAllAsync(r => r.AppUserId == request.AppUserId, false, r => r.Seat);
            if (reservations is null)
                return new ResponseModel<List<ReservationModel>>("Reservations is null");
            
            var mappedReservations = _mapper.Map<List<ReservationModel>>(reservations);

            return new ResponseModel<List<ReservationModel>>(mappedReservations);
        }
    }
}
