using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Review.Commands
{
    public class UpdateReviewCommand
    {
        public Guid ReviewID { get; set; }
        public int Rating { get; set; }   // 1 - 10 arasında olmalı
        public string Comment { get; set; }
    }
}
