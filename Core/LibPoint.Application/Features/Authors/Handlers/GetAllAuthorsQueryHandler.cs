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

    public GetAllAuthorsQueryHandler(IRepository<Author> repository)
    {
        _repository = repository;
    }
    
    public async Task<ResponseModel<List<AuthorModel>>> Handle(GetAllAuthorsQueryRequest request, CancellationToken cancellationToken)
    {
        var authors = await _repository.GetAllAsync();
        
        if(authors == null)
         return new ResponseModel<List<AuthorModel>>("No authors found", 404 );

        var authorModels = authors.Select(author => new AuthorModel
        {
            Id = author.Id,
            Name = author.Name,
            Surname = author.Surname,
            Bio = author.Bio,
            DateOfBirth = author.DateOfBirth,
            DateOfDeath = author.DateOfDeath,
        }).ToList();
        
        return new ResponseModel<List<AuthorModel>>(authorModels, 200);
    }
}