using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Categories.Queries;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Categories;
using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Categories.Handlers
{
    class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQueryRequest, ResponseModel<CategoryModel>>
    {
        private readonly IRepository<Category> _repository;

        public GetCategoryByIdQueryHandler(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseModel<CategoryModel>> Handle(GetCategoryByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var value = _repository.GetAsync(c => c.Id == request.Id, false, c => c.Books).Result;
            if (value == null)
            {
                return new ResponseModel<CategoryModel>("Category not found", 404);
            }
            else
            {
                var categorymodel = new CategoryModel
                {
                    Name = value.Name,
                    Description = value.Description,
                };
                return new ResponseModel<CategoryModel>(categorymodel);
            }
        }
    }
}
