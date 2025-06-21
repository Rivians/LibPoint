using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Borrowings.Commands
{
    public class CompleteBorrowingCommandRequest : IRequest<ResponseModel<bool>>
    {
        public Guid AppUserId { get; set; }
        public Guid AdminId { get; set; }
        public Guid BookId { get; set; }

        public CompleteBorrowingCommandRequest(Guid appUserId, Guid adminId, Guid bookId)
        {
            AppUserId = appUserId;
            AdminId = adminId;
            BookId = bookId;
        }

    }
}
