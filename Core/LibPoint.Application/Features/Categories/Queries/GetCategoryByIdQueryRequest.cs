using LibPoint.Domain.Models.Categories;
using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Categories.Queries
{
    public class GetCategoryByIdQueryRequest: IRequest<ResponseModel<CategoryModel>>
    {
        public Guid Id { get; set; }

        public GetCategoryByIdQueryRequest(Guid id)
        {
            Id = id;
        }
    }
    
}
