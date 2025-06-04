using LibPoint.Application.Abstractions;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Infrastructure.Services
{
    public class GenerateQrService : IGenerateQrService
    {
        public string GenerateReservationQrCode(Guid reservationId)
        {
            var url = $"http://35.158.197.224/api/reservation/check-in-reservation?reservationId={reservationId}";

            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrCodeData);
            var qrBytes = qrCode.GetGraphic(20);
            return Convert.ToBase64String(qrBytes);
        }
    }
}
