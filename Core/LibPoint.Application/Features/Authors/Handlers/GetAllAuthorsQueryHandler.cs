using AutoMapper;
using LibPoint.Application.Abstractions;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Author;
using LibPoint.Domain.Models.Responses;
using MediatR;

namespace LibPoint.Application.Features.Authors.Queries;

public class GetAllAuthorsQueryHandler:IRequestHandler<GetAllAuthorsQueryRequest, ResponseModel<List<AuthorModel>>>
{
    private readonly IRepository<Author> _repository;
    private readonly IMapper _mapper;

    public GetAllAuthorsQueryHandler(IRepository<Author> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ResponseModel<List<AuthorModel>>> Handle(GetAllAuthorsQueryRequest request, CancellationToken cancellationToken)
    {
        var authors = await _repository.GetAllAsync();
        
        var mappedAuthors = _mapper.Map<List<AuthorModel>>(authors);
        return new ResponseModel<List<AuthorModel>>(mappedAuthors, 200);
    }
}