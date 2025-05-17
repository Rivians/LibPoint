using LibPoint.Application.Abstractions;
using LibPoint.Persistence.Data;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> SetSeatFreeAsync(Guid seatId)
        {
            var seat = await _context.Seats.FindAsync(seatId);

            if (seat is null || !seat.IsReserved)
                return false;

            seat.IsReserved = false;
            seat.CurrentAppUserId = null;
            seat.CurrentReservationId = null;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> SetSeatReservedAsync(Guid appUserId, Guid seatId, Guid reservationId)
        {
            //var seatData = await _context.Seats
            //    .Where(s => s.Id == seatId && !s.IsReserved)
            //    .Select(s => new
            //    {
            //        s,
            //        appUser = _context.AppUsers.FirstOrDefault(u => u.Id == appUserId),
            //        reservation = _context.Reservations.FirstOrDefault(r => r.Id == reservationId)
            //    }).FirstOrDefaultAsync();

            var seat = await _context.Seats.FindAsync(seatId);
            var appUser = await _context.AppUsers.FindAsync(appUserId);
            var reservation = await _context.Reservations.FindAsync(reservationId);

            if (seat is null || seat.IsReserved || appUser is null || reservation is null)
                return false;

            seat.IsReserved = true;
            seat.CurrentReservationId = reservationId;
            seat.CurrentAppUserId = appUserId;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
