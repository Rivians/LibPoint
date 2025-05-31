using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Review.Commands
{
    public class CreateReviewCommand
    {
        public int Rating { get; set; }   
        public string Comment { get; set; }
    }
}
