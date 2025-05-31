using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Books.Commands
{
    public class RemoveBookCommandRequest: IRequest<ResponseModel<Guid>>
    {
        public Guid Id { get; set; }

        public RemoveBookCommandRequest(Guid id)
        {
            Id = id;
        }
    }
}
