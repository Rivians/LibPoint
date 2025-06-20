using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Reviews.Queries;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Responses;
using LibPoint.Domain.Models.Reviews;
using MediatR;

namespace LibPoint.Application.Features.Reviews.Handlers;

public class GetReviewsByUserIdQueryHandler:IRequestHandler<GetReviewsByUserIdQueryRequest,ResponseModel<List<ReviewModel>>>
{
    private readonly IRepository<Review> _repository;

    public GetReviewsByUserIdQueryHandler(IRepository<Review> repository)
    {
        _repository = repository;
    }
    
    public async Task<ResponseModel<List<ReviewModel>>> Handle(GetReviewsByUserIdQueryRequest request, CancellationToken cancellationToken)
    {
        var reviews = await _repository.GetAllAsync(r => r.AppUserId == request.UserId, true, r => r.AppUser);

        if (reviews == null|| !reviews.Any())
            return new ResponseModel<List<ReviewModel>>("No reviews found for this user.", 404);

        var reviewModels = reviews.Select(review => new ReviewModel
        {
            Id = review.Id,
            Rating = review.Rating,
            Comment = review.Comment,
            AppuUserId = review.AppUserId,
            BookId = review.BookId,
            FullName = review.FullName,
            CreatedTime = review.CreatedTime            
        }).ToList();

        return new ResponseModel<List<ReviewModel>>(reviewModels, 200);
    }
}