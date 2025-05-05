using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Abstractions
{
    public interface ISeatRepository
    {
        Task<bool> SetReservedSeatAsync(Guid seatId, Guid reservationId, Guid appUserId);
        Task<bool> SetFreeSeatAsync(Guid seatId);
    }
}
