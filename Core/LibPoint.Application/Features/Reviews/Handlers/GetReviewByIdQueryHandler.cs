using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Reviews.Queries;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Responses;
using LibPoint.Domain.Models.Reviews;
using MediatR;

namespace LibPoint.Application.Features.Reviews.Handlers;

public class GetReviewByIdQueryHandler:IRequestHandler<GetReviewByIdQueryRequest,ResponseModel<ReviewModel>>
{
    private readonly IRepository<Review> _repository;

    public GetReviewByIdQueryHandler(IRepository<Review> repository)
    {
        _repository = repository;
    }
    public async Task<ResponseModel<ReviewModel>> Handle(GetReviewByIdQueryRequest request, CancellationToken cancellationToken)
    {
        //var review = await _repository.GetByIdAsync(request.Id);

        var review = await _repository.GetAsync(r => r.Id == request.Id, true, r => r.AppUser);

        if (review == null)
            return new ResponseModel<ReviewModel>("Review not found", 404);

        var reviewModel = new ReviewModel
        {
            Id = review.Id,
            Rating = review.Rating,
            FullName = review.FullName,
            Comment = review.Comment,
            AppuUserId = review.AppUserId,
            BookId = review.BookId
        };

        return new ResponseModel<ReviewModel>(reviewModel,200);
    }
}