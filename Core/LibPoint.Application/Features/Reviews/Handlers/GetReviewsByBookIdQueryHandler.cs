using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Reviews.Queries;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Responses;
using LibPoint.Domain.Models.Reviews;
using MediatR;

namespace LibPoint.Application.Features.Reviews.Handlers;

public class GetReviewsByBookIdQueryHandler:IRequestHandler<GetReviewsByBookIdQueryRequest,ResponseModel<List<ReviewModel>>>
{
    private readonly IRepository<Review> _repository;

    public GetReviewsByBookIdQueryHandler(IRepository<Review> repository)
    {
        _repository = repository;
    }
    
    public async Task<ResponseModel<List<ReviewModel>>> Handle(GetReviewsByBookIdQueryRequest request, CancellationToken cancellationToken)
    {
        var reviews = await _repository.GetAllAsync(r => r.BookId == request.BookId, false);

        if (reviews == null|| !reviews.Any())
            return new ResponseModel<List<ReviewModel>>("No reviews found for this book.", 404);

        var reviewModels = reviews.Select(r => new ReviewModel
        {
            Id = r.Id,
            Rating = r.Rating,
            Comment = r.Comment,
            AppuUserId = r.AppUserId,
            BookId = r.BookId,
            FullName = r.FullName,
            CreatedTime = r.CreatedTime
        }).ToList();

        return new ResponseModel<List<ReviewModel>>(reviewModels, 200);
    }
}