using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Reviews.Queries;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Responses;
using MediatR;

namespace LibPoint.Application.Features.Reviews.Handlers;

public class GetAverageRatingByBookQueryHandler:IRequestHandler<GetAverageRatingByBookQueryRequest,ResponseModel<double>>
{
    private readonly IRepository<Review> _repository;

    public GetAverageRatingByBookQueryHandler(IRepository<Review> repository)
    {
        _repository = repository;
    }
    
    public async Task<ResponseModel<double>> Handle(GetAverageRatingByBookQueryRequest request, CancellationToken cancellationToken)
    {
        var reviews = await _repository.GetAllAsync(r => r.BookId == request.BookId);

        if (reviews == null || !reviews.Any())
            return new ResponseModel<double>("No reviews found for this book.", 404);

        var averageRating = reviews.Average(r => r.Rating);

        return new ResponseModel<double>(averageRating, 200);
    }
}