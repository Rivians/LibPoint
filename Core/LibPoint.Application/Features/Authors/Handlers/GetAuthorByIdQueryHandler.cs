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
    public GetAuthorByIdQueryHandler(IRepository<Author> repository)
    {
        _repository = repository;
    }


    public async Task<ResponseModel<AuthorModel>> Handle(GetAuthorByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var author = await _repository.GetByIdAsync(request.Id);
        if (author == null)
         return new ResponseModel<AuthorModel>("Author not found", 404 );

        var authorModel = new AuthorModel
        {
            Id = author.Id,
            Name = author.Name,
            Surname = author.Surname,
            Bio = author.Bio,
            DateOfBirth = author.DateOfBirth,
            DateOfDeath = author.DateOfDeath,
        };
        
        return new ResponseModel<AuthorModel>(authorModel,  200);
    }
}