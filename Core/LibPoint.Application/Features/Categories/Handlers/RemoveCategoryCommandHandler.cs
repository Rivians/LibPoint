using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Categories.Commands;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Categories.Handlers
{
    class RemoveCategoryCommandHandler : IRequestHandler<RemoveCategoryCommandRequest, ResponseModel<Guid>>
    {
        private readonly IRepository<Category> _repository;
        public RemoveCategoryCommandHandler(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseModel<Guid>> Handle(RemoveCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var deletingCategory = await _repository.GetAsync(x=>x.Name == request.Name);
            if (deletingCategory ==null)
            {
                return new ResponseModel<Guid> { };
            }
            
            var deleteResult = _repository.Delete(deletingCategory);
            if (!deleteResult)
            {
                return new ResponseModel<Guid>
                {
                    Success = false,
                    Data = Guid.Empty,
                    Messages = new[] { "Failed to delete the category." },
                };
            }
            await _repository.SaveChangesAsync();

            return new ResponseModel<Guid>
            {
                Success = true,
                Data = deletingCategory.Id,
                Messages = new[] { "Category deleted successfully." },
                StatusCode = 200
            };
        }
    }
}
