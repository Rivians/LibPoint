using LibPoint.Domain.Models.Books;
using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Books.Queries
{
    public class GetBookByIdQueryRequest: IRequest<ResponseModel<BookModel>>
    {
        public Guid Id { get; set; }

        public GetBookByIdQueryRequest(Guid id)
        {
            Id = id;
        }
    }
}
