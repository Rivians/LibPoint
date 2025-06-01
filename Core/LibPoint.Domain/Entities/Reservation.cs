using LibPoint.Domain.Entities.Base;
using LibPoint.Domain.Entities.Enums;
using LibPoint.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Domain.Entities
{
    public class Reservation : BaseEntity
    {        
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Session Session { get; set; }
        public int Duration { get; set; }           // dakika veya saat olucak belirlemedik. dakika olucak.
        public bool IsActive { get; set; } = false;
        public bool CheckIn { get; set; } = false;
        public bool EndedBySession { get; set; } = false;
        public bool EndedByUser { get; set; } = false;

        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid SeatId { get; set; }
        public Seat Seat { get; set; }
    }
}
