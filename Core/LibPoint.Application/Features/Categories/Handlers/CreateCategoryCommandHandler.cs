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
    class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest, ResponseModel<Guid>>
    {
        private readonly IRepository<Category> _repository;

        public CreateCategoryCommandHandler(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseModel<Guid>> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var newcategory = new Category
            {
                Name = request.Name,
                Description = request.Description
            };

            var result = await _repository.AddAsync(newcategory);
            if (!result)
            {
                return new ResponseModel<Guid>("Category could not be created", 500);
            }
            var saveResult = await _repository.SaveChangesAsync();
            if (saveResult == false)
            {
                return new ResponseModel<Guid>("Category could not be created", 500);
            }
            else
            {
                return new ResponseModel<Guid>(newcategory.Id, 200);
            }
        }
    }
}
