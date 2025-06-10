using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.User.Commands
{
    public class DeleteUserByIdCommandRequest : IRequest<ResponseModel<bool>>
    {
        public Guid Id { get; set; }
        public DeleteUserByIdCommandRequest(Guid ıd)
        {
            Id = ıd;
        }
    }
}
