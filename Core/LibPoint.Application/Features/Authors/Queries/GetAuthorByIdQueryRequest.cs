using LibPoint.Domain.Models.Authors;
using LibPoint.Domain.Models.Responses;
using MediatR;

namespace LibPoint.Application.Features.Authors.Queries;

public class GetAuthorByIdQueryRequest:IRequest<ResponseModel<AuthorModel>>
{
    public Guid Id { get; set; }
    
    public GetAuthorByIdQueryRequest(Guid id)
    {
        Id = id;
    }
        
}