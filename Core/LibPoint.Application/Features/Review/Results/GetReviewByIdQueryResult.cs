using LibPoint.Domain.Entities.Identity;
using LibPoint.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Review.Results
{
    public class GetReviewByIdQueryResult
    {
        public Guid ReviewID { get; set; }
        public int Rating { get; set; }   // 1 - 10 arasında olmalı
        public string Comment { get; set; }


        
    }
}
