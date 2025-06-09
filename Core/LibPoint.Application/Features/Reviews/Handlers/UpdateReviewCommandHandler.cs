using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Reviews.Commands;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Responses;
using MediatR;

namespace LibPoint.Application.Features.Reviews.Handlers;

public class UpdateReviewCommandHandler:IRequestHandler<UpdateReviewCommandRequest,ResponseModel<Guid>>
{ 
    private readonly IRepository<Review> _repository;

    public UpdateReviewCommandHandler(IRepository<Review> repository)
    {
        _repository = repository;
    }
    public async Task<ResponseModel<Guid>> Handle(UpdateReviewCommandRequest request, CancellationToken cancellationToken)
    {
        var review = await _repository.GetByIdAsync(request.Id);
        if (review == null)
        {
            return new ResponseModel<Guid>("Review not found", 404);
        }

        review.Rating = request.Rating;
        review.Comment = request.Comment;
        review.AppuUserId = request.AppuUserId;
        review.BookId = request.BookId;

        var updateResult = _repository.Update(review);

        if (!updateResult)
            return new ResponseModel<Guid>("Review could not be updated", 400);

        await _repository.SaveChangesAsync();

        return new ResponseModel<Guid>(review.Id);
    }
}