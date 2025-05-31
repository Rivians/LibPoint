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
    class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest, ResponseModel<Guid>>
    {
        private readonly IRepository<Category> _repository;
        public UpdateCategoryCommandHandler(IRepository<Category> repository)
        {
            _repository = repository;
        }
        public async Task<ResponseModel<Guid>> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var updatingCategory = await _repository.GetAsync(x=>x.Name == request.Name, tracking: true);
            if (updatingCategory == null)
            {
                return await Task.FromResult(new ResponseModel<Guid>
                {
                    Success = false,
                    Data = Guid.Empty,
                    Messages = new[] { "Category not found." },
                });
            }
            else
            {
                updatingCategory.Name = request.Name;
                updatingCategory.Description = request.Description;

                var updateResult = _repository.Update(updatingCategory);
                if (!updateResult)
                {
                    return await Task.FromResult(new ResponseModel<Guid>
                    {
                        Success = false,
                        Data = Guid.Empty,
                        Messages = new[] { "Failed to update the category." },
                    });
                }

                await _repository.SaveChangesAsync();
                return await Task.FromResult(new ResponseModel<Guid>
                {
                    Success = true,
                    Data = updatingCategory.Id,
                    Messages = new[] { "Category updated successfully." },
                    StatusCode = 200
                });
            }

        }
    }
}
