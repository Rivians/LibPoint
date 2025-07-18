﻿using Microsoft.AspNetCore.Identity;

namespace LibPoint.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsMalformed { get; set; } = false;
        public bool IsAdmin { get; set; } = false;

        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedTime { get; set; }
        public bool IsDeleted { get; set; } = false;


        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Borrowing> Borrowings { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
