using LibPoint.Domain.Models.Responses;
using LibPoint.Domain.Models.Reviews;
using MediatR;

namespace LibPoint.Application.Features.Reviews.Queries;

public class GetReviewByIdQueryRequest:IRequest<ResponseModel<ReviewModel>>
{
    public Guid Id { get; set; }

    public GetReviewByIdQueryRequest(Guid id)
    {
        Id = id;
    }
}