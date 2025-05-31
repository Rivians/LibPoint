using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Categories.Queries;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Books;
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
    class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQueryRequest, ResponseModel<List<CategoryModel>>>
    {
        private readonly IRepository<Category> _repository;

        public GetAllCategoriesQueryHandler(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseModel<List<CategoryModel>>> Handle(GetAllCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAllAsync();
            if (values == null)
            {
                return new ResponseModel<List<CategoryModel>>("No categories found", 404);
            }
            else
            {
                var categorymodel = values.Select(Category => new CategoryModel
                {
                    Name = Category.Name,
                    Description = Category.Description,

                }).ToList();
                return new ResponseModel<List<CategoryModel>>(categorymodel);
            }
        }
    }
}
