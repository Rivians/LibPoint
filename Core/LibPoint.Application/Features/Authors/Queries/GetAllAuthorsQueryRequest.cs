using LibPoint.Domain.Models.Author;
using LibPoint.Domain.Models.Responses;
using MediatR;

namespace LibPoint.Application.Features.Authors.Queries;

public class GetAllAuthorsQueryRequest:IRequest<ResponseModel<List<AuthorModel>>>
{
    
}