using LibPoint.Domain.Entities.Base;
using LibPoint.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Domain.Entities
{
    public class Borrowing : BaseEntity
    {

        public Guid AppUserId { get; set; }
        public Guid BookId { get; set; }
        public bool IsActive { get; set; } = false;

        public AppUser AppUser { get; set; }
        public Book Book { get; set; }
    }
}
