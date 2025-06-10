using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Borrowings.Commands
{
    public class DeleteBorrowingCommandRequest: IRequest<ResponseModel<Guid>>
    {
        public Guid Id { get; set; }

        public DeleteBorrowingCommandRequest(Guid id)
        {
            Id = id;
        }
    }
}
