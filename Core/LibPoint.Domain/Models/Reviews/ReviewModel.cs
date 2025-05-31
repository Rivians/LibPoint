using LibPoint.Domain.Entities.Identity;
using LibPoint.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Domain.Models.Reviews
{
    public class Review
    {
        public int Rating { get; set; }   // 1 - 10 arasında olmalı
        public string Comment { get; set; }


        public Guid AppuUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }
    }
}
