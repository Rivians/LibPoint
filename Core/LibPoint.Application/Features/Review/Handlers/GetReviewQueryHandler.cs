using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Review.Results;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Review.Handlers
{
    public class GetReviewQueryHandler
    {
        private readonly IBookRepository<Domain.Entities.Review> _repository;

        public GetReviewQueryHandler(IBookRepository<Domain.Entities.Review> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetReviewQueryResult>> Handle()
        {
            var values = await _repository.GetAllAsync();
            return values.Select(x => new GetReviewQueryResult
            {
                Comment = x.Comment,
                Rating =x.Rating,
                ReviewID = x.ReviewID
            }).ToList();
        }
    }
}
