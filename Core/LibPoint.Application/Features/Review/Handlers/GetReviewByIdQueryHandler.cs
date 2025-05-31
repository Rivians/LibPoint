using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Review.Queries;
using LibPoint.Application.Features.Review.Results;
using LibPoint.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Review.Handlers
{
    public class GetReviewByIdQueryHandler
    {
        private readonly IBookRepository<LibPoint.Domain.Entities.Review> _repository;

        public GetReviewByIdQueryHandler(IBookRepository<LibPoint.Domain.Entities.Review> repository)
        {
            _repository = repository;
        }

        public async Task<GetReviewByIdQueryResult> Handle(GetReviewByIdQuery query)
        {
            var values = await _repository.GetByIdAsync(query.ReviewID);
            return new GetReviewByIdQueryResult
            {
                ReviewID = values.ReviewID,
                Comment = values.Comment,
                Rating = values.Rating
            };
        }
    }
}
