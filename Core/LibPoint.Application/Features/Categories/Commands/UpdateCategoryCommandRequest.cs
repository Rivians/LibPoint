using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Categories.Commands
{
    public class UpdateCategoryCommandRequest: IRequest<ResponseModel<Guid>>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
