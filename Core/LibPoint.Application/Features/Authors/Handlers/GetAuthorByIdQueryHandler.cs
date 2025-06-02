using AutoMapper;
using LibPoint.Application.Abstractions;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Author;
using LibPoint.Domain.Models.Responses;
using MediatR;

namespace LibPoint.Application.Features.Authors.Queries;

public class GetAuthorByIdQueryHandler:IRequestHandler<GetAuthorByIdQueryRequest, ResponseModel<AuthorModel>>
{
    private readonly IRepository<Author> _repository;
    private readonly IMapper _mapper;

    public GetAuthorByIdQueryHandler(IRepository<Author> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<ResponseModel<AuthorModel>> Handle(GetAuthorByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var author = await _repository.GetByIdAsync(request.Id);
        if (author == null)
         return new ResponseModel<AuthorModel>("Author not found", 404 );
        
        var mappedAuthor = _mapper.Map<AuthorModel>(author);
        return new ResponseModel<AuthorModel>(mappedAuthor, 200);
    }
}