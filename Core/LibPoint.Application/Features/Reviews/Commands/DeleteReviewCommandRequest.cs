using LibPoint.Domain.Models.Responses;
using MediatR;

namespace LibPoint.Application.Features.Reviews.Commands;
public class DeleteReviewCommandRequest: IRequest<ResponseModel<Guid>>
{
    public Guid Id { get; set; }

    public DeleteReviewCommandRequest(Guid id)
    {
        Id = id;
    }
}