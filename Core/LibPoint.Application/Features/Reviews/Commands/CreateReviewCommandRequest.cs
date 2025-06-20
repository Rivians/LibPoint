using LibPoint.Domain.Models.Responses;
using MediatR;

namespace LibPoint.Application.Features.Reviews.Commands;

public class CreateReviewCommandRequest:IRequest<ResponseModel<Guid>>
{
    public int Rating { get; set; }           // 1-10 arası
    public string Comment { get; set; }
    //public string FullName { get; set; }

    public Guid AppuUserId { get; set; }
    public Guid BookId { get; set; }
}