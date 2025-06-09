using LibPoint.Domain.Models.Responses;
using MediatR;

namespace LibPoint.Application.Features.Reviews.Queries;

public class GetAverageRatingByBookQueryRequest:IRequest<ResponseModel<double>>
{
    public Guid BookId { get; set; }

    public GetAverageRatingByBookQueryRequest(Guid bookId)
    {
        BookId = bookId;
    }
}