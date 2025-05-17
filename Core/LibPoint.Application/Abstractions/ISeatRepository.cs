using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Abstractions
{
    public interface ISeatRepository
    {
        Task<bool> SetSeatReservedAsync(Guid appUserId, Guid seatId, Guid reservationId);
        Task<bool> SetSeatFreeAsync(Guid seatId);
    }
}
