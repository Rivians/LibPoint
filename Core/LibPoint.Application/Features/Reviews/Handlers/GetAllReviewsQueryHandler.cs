using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Reviews.Queries;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Responses;
using LibPoint.Domain.Models.Reviews;
using MediatR;

namespace LibPoint.Application.Features.Reviews.Handlers;

public class GetAllReviewsQueryHandler:IRequestHandler<GetAllReviewsQueryRequest,ResponseModel<List<ReviewModel>>>
{
    private readonly IRepository<Review> _repository;

    public GetAllReviewsQueryHandler(IRepository<Review> repository)
    {
        _repository = repository;
    }
    
    public async Task<ResponseModel<List<ReviewModel>>> Handle(GetAllReviewsQueryRequest request, CancellationToken cancellationToken)
    {
        var reviews = await _repository.GetAllAsync();

        if (reviews == null|| !reviews.Any())
            return new ResponseModel<List<ReviewModel>>("No reviews found", 404);

        var reviewModels = reviews.Select(review => new ReviewModel
        {
            Id = review.Id,
            Rating = review.Rating,
            Comment = review.Comment,
            AppuUserId = review.AppuUserId,
            BookId = review.BookId
        }).ToList();

        return new ResponseModel<List<ReviewModel>>(reviewModels, 200);
    }
}