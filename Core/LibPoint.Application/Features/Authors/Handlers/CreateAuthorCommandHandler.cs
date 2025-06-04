using LibPoint.Application.Abstractions;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Responses;
using MediatR;

namespace LibPoint.Application.Features.Authors.Commands;

public class CreateAuthorCommandHandler:IRequestHandler<CreateAuthorCommandRequest, ResponseModel<Guid>>
{
    private readonly IRepository<Author> _repository;

    public CreateAuthorCommandHandler(IRepository<Author> repository)
    {
        _repository = repository;
    }
    
    public async Task<ResponseModel<Guid>> Handle(CreateAuthorCommandRequest request, CancellationToken cancellationToken)
    {
        var author = new Author
        {
            Name = request.Name,
            Surname = request.Surname,
            Bio = request.Bio,
            DateOfBirth = request.DateOfBirth,
            DateOfDeath = request.DateOfDeath, Books = new System.Collections.Generic.HashSet<Book>()

        };
        var addResult=await _repository.AddAsync(author);

        if (!addResult)
            return new ResponseModel<Guid>("Author could not be created", 400);

        await _repository.SaveChangesAsync();
        return new ResponseModel<Guid>(author.Id);
    }
}