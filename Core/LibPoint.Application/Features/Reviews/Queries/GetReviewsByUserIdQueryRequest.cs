using LibPoint.Domain.Models.Responses;
using LibPoint.Domain.Models.Reviews;
using MediatR;

namespace LibPoint.Application.Features.Reviews.Queries;

public class GetReviewsByUserIdQueryRequest:IRequest<ResponseModel<List<ReviewModel>>>
{
    public Guid UserId { get; set; }

    public GetReviewsByUserIdQueryRequest(Guid userId)
    {
        UserId = userId;
    }
}