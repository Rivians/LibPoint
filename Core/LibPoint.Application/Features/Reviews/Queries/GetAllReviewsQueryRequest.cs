using LibPoint.Domain.Models.Responses;
using LibPoint.Domain.Models.Reviews;
using MediatR;

namespace LibPoint.Application.Features.Reviews.Queries;

public class GetAllReviewsQueryRequest:IRequest<ResponseModel<List<ReviewModel>>>
{
    
}