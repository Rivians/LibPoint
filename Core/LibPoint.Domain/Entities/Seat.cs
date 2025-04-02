using LibPoint.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Domain.Entities
{
    public class Seat : BaseEntity
    {
        public string SeatNumber { get; set; }
        public bool IsAvailable { get; set; }
        public int? Floor { get; set; } // kat 1 -- kat 2 vs..
        public string? Section { get; set; } // sessiz alan -- çalışma alanı vs..

        public ICollection<Reservation> Reservations { get; set; }
    }
}
