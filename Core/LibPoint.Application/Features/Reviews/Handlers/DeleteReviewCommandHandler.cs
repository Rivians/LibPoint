using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Reviews.Commands;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Responses;
using MediatR;

namespace LibPoint.Application.Features.Reviews.Handlers;

public class DeleteReviewCommandHandler:IRequestHandler<DeleteReviewCommandRequest,ResponseModel<Guid>>
{   
    private readonly IRepository<Review> _repository;

    public DeleteReviewCommandHandler(IRepository<Review> repository)
    {
        _repository = repository;
    }
    
    public async Task<ResponseModel<Guid>> Handle(DeleteReviewCommandRequest request, CancellationToken cancellationToken)
    {
        var review = await _repository.GetByIdAsync(request.Id);
        if (review == null)
        {
            return new ResponseModel<Guid>("Review not found", 404);
        }

        var deleteResult = _repository.Delete(review);

        if (!deleteResult)
            return new ResponseModel<Guid>("Review could not be deleted", 400);

        await _repository.SaveChangesAsync();

        return new ResponseModel<Guid>(request.Id);
    }
}