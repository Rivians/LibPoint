using LibPoint.Domain.Models.Responses;
using LibPoint.Domain.Models.Reviews;
using MediatR;

namespace LibPoint.Application.Features.Reviews.Queries;

public class GetReviewsByBookIdQueryRequest:IRequest<ResponseModel<List<ReviewModel>>>
{
    public Guid BookId { get; set; }

    public GetReviewsByBookIdQueryRequest(Guid bookId)
    {
        BookId = bookId;
    }  
}