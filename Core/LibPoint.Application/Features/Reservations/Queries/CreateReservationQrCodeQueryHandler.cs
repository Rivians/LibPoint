using LibPoint.Application.Abstractions;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Reservations.Queries
{
    public class CreateReservationQrCodeQueryHandler : IRequestHandler<CreateReservationQrCodeQueryRequest, ResponseModel<string>>
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IGenerateQrService _qrService;
        public CreateReservationQrCodeQueryHandler(IRepository<Reservation> repository, IGenerateQrService qrService)
        {
            _repository = repository;
            _qrService = qrService;
        }

        public async Task<ResponseModel<string>> Handle(CreateReservationQrCodeQueryRequest request, CancellationToken cancellationToken)
        {
            var reservation = await _repository.GetByIdAsync(request.ReservationId);
            if (reservation is null)
                return new ResponseModel<string>("Reservation can not found", 404);

            var qrBase64 = _qrService.GenerateReservationQrCode(request.ReservationId);
            return new ResponseModel<string>(qrBase64)
            {
                Success = true,
                Data = qrBase64,
                Messages = null,
                StatusCode = 200,
            };
        }
    }
}
