using LibPoint.Domain.Entities.Identity;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Borrowings.Commands
{
    public class CreateBorrowingCommandRequest: IRequest<ResponseModel<Guid>>
    {
        public Guid AppUserId { get; set; }
        public Guid BookId { get; set; }
    }
}
