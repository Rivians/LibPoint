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
        public bool IsReserved { get; set; } = false;
        public Guid? CurrentAppUserId { get; set; }  // eğerki sandalye dolu ise 
        public Guid? CurrentReservationId { get; set; }  

        public ICollection<Reservation> Reservations { get; set; }
    }
}
