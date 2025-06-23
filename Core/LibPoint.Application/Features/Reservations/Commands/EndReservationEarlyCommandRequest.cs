using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Reservations.Commands
{
    public class EndReservationEarlyCommandRequest : IRequest<ResponseModel<bool>>
    {
        public Guid UserId { get; set; }         // adminde olabilir, normal kullanıcı da --- kontrol edilmesi lazım
        public Guid ReservationId { get; set; }
    }
}
