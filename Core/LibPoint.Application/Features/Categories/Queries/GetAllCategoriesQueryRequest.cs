using LibPoint.Domain.Models.Responses;
using LibPoint.Domain.Models.Categories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Categories.Queries
{
    public class GetAllCategoriesQueryRequest : IRequest<ResponseModel<List<CategoryModel>>>
    {
    }
}
