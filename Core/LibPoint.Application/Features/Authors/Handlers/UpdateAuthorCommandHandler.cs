using LibPoint.Application.Abstractions;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Responses;
using MediatR;

namespace LibPoint.Application.Features.Authors.Commands;

public class UpdateAuthorCommandHandler:IRequestHandler<UpdateAuthorCommandRequest, ResponseModel<Guid>>
{
    private readonly IRepository<Author> _repository;

    public UpdateAuthorCommandHandler(IRepository<Author> repository)
    {
        _repository = repository;
    }
    
    public async Task<ResponseModel<Guid>> Handle(UpdateAuthorCommandRequest request, CancellationToken cancellationToken)
    {
        var author = await _repository.GetByIdAsync(request.Id);
        if (author == null)
            return new ResponseModel<Guid>("Author not found",404);
        
        author.Name = request.Name;
        author.Surname = request.Surname;
        author.Bio = request.Bio;
        author.DateOfBirth = request.DateOfBirth;
        author.DateOfDeath = request.DateOfDeath;
        
        await _repository.SaveChangesAsync();
        return new ResponseModel<Guid>(author.Id);
    }
    
}