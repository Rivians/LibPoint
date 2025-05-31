using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Review.Queries
{
    public class GetReviewByIdQuery
    {
        public Guid ReviewID { get; set; }

        public GetReviewByIdQuery(Guid reviewID)
        {
            ReviewID = reviewID;
        }
    }
}
