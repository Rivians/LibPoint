using LibPoint.Domain.Models.Responses;
using MediatR;

namespace LibPoint.Application.Features.Authors.Commands;

public class RemoveAuthorCommandRequest:IRequest<ResponseModel<Guid>>
{ 
    public RemoveAuthorCommandRequest(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }

   
}