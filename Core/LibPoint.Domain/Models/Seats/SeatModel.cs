using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Domain.Models.Seats
{
    public class SeatModel
    {
        public Guid Id { get; set; }
        public string SeatNumber { get; set; }
        public bool IsReserved { get; set; } = false; 
        public Guid? CurrentAppUserId { get; set; }  
        public Guid? CurrentReservationId { get; set; }

        //public ICollection<ReservationModel> Reservations { get; set; }
    }
}
