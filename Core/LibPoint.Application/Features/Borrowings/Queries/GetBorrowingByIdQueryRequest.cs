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
    public class GetBorrowingByIdQueryRequest: IRequest<ResponseModel<BorrowingModel>>
    {
        public Guid Id { get; set; }
        public GetBorrowingByIdQueryRequest(Guid id)
        {
            Id = id;
        }
    }
   
}
