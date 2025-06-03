using LibPoint.Domain.Entities.Enums;
using LibPoint.Domain.Entities.Identity;
using LibPoint.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibPoint.Domain.Models.User;
using LibPoint.Domain.Models.Seats;

namespace LibPoint.Domain.Models.Reservations
{
    public class ReservationModel
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Session Session { get; set; }
        public int Duration { get; set; }           // dakika veya saat olucak belirlemedik. update: dakika türünden olucak.
        public bool IsActive { get; set; }
        public bool CheckIn { get; set; } = false;
        public DateTime? CheckInTime { get; set; }
        public bool? EndedBySession { get; set; } = false;
        public bool? EndedByUser { get; set; } = false;

        public Guid AppUserId { get; set; }
        //public UserModel AppUser { get; set; }
        public Guid SeatId { get; set; }
        public SeatModel Seat { get; set; }
    }
}
