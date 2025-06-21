using LibPoint.Domain.Entities.Identity;
using LibPoint.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Domain.Models.Borrowings
{
    public class BorrowingModel
    {
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; }
        public bool IsActive { get; set; }
        public string Code { get; set; }

        //public AppUser AppUser { get; set; }
        public Guid BookId { get; set; }
        //public Book Book { get; set; }
    }
}
