using LibPoint.Application.Abstractions;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Responses;
using MediatR;

namespace LibPoint.Application.Features.Authors.Commands;

public class RemoveAuthorCommandHandler:IRequestHandler<RemoveAuthorCommandRequest, ResponseModel<Guid>>
{
    private readonly IRepository<Author> _repository;

    public RemoveAuthorCommandHandler(IRepository<Author> repository)
    {
        _repository = repository;
    }
    public async Task<ResponseModel<Guid>> Handle(RemoveAuthorCommandRequest request, CancellationToken cancellationToken)
    {
        var author =await _repository.GetByIdAsync(request.Id);

        if (author==null || author.IsDeleted)
            return new ResponseModel<Guid>("Author not found or already removed", 404);
        
        author.IsDeleted = true;
        _repository.Update(author);
        await _repository.SaveChangesAsync();
        return new ResponseModel<Guid>(author.Id);
    }
}