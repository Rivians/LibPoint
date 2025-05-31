using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Review.Commands
{
    public class RemoveReviewCommand
    {
        public Guid ReviewID { get; set; }

        public RemoveReviewCommand(Guid reviewID)
        {
            ReviewID = reviewID;
        }
    }
}
