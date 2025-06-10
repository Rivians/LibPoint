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
    public class GetAllBorrowingsQueryRequest: IRequest<ResponseModel<List<BorrowingModel>>>
    {
    }
}
