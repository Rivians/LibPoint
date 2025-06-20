using LibPoint.Domain.Entities.Base;
using LibPoint.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Domain.Entities
{
    public class Review : BaseEntity
    {
        public int Rating { get; set; }   // 1 - 10 arasında olmalı
        public string Comment { get; set; }
        public string FullName => $"{AppUser?.Name} {AppUser?.Surname}";


        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }
    }
}
