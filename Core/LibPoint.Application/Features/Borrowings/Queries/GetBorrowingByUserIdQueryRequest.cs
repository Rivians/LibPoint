using LibPoint.Domain.Models.Borrowings;
using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Borrowings.Queries
{
    public class GetBorrowingByUserIdQueryRequest: IRequest<ResponseModel<List<BorrowingModel>>>
    {
        public Guid UserId { get; set; }

        public GetBorrowingByUserIdQueryRequest(Guid id)
        {
            UserId = id;
        }
    }
}
