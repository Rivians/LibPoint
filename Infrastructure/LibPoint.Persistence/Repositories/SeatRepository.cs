using LibPoint.Application.Abstractions;
using LibPoint.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Persistence.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly LibPointDbContext _context;
        public SeatRepository(LibPointDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SetFreeSeatAsync(Guid seatId)
        {
            var seat = await _context.Seats.FindAsync(seatId);

            if (seat is null || !seat.IsReserved)
                return false;

            seat.IsReserved = false;
            seat.CurrentAppUserId = null;
            seat.CurrentReservationId = null;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> SetReservedSeatAsync(Guid seatId, Guid reservationId, Guid appUserId)
        {
            var seat = await _context.Seats.FindAsync(seatId);

            if (seat is null || seat.IsReserved)
                return false;

            seat.IsReserved = true;
            seat.CurrentReservationId = reservationId;
            seat.CurrentAppUserId = appUserId;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
