using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Reviews.Commands;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Responses;
using MediatR;

namespace LibPoint.Application.Features.Reviews.Handlers;

public class CreateReviewCommandHandler: IRequestHandler<CreateReviewCommandRequest, ResponseModel<Guid>>

{
    private readonly IRepository<Review> _repository;
    public CreateReviewCommandHandler(IRepository<Review> repository)
    {
        _repository = repository;
    }
    public async Task<ResponseModel<Guid>> Handle(CreateReviewCommandRequest request, CancellationToken cancellationToken)
    {
        var review = new Review
        {
            Id = Guid.NewGuid(),
            Rating = request.Rating,
            Comment = request.Comment,
            AppUserId = request.AppuUserId,
            BookId = request.BookId,
        };

        var addResult = await _repository.AddAsync(review);

        if (!addResult)
            return new ResponseModel<Guid>("Review could not be created", 400);

        await _repository.SaveChangesAsync();

        return new ResponseModel<Guid>(review.Id);
    }
}