using LibPoint.Domain.Entities.Base;
using LibPoint.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Domain.Entities
{
    public class Notification : BaseEntity
    {
        public string Title { get; set; }
        public string Message { get; set; }


        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}
