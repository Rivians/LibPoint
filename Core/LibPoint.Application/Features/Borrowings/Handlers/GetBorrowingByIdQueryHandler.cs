using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Borrowings.Queries;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Borrowings;
using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Borrowings.Handlers
{
    public class GetBorrowingByIdQueryHandler : IRequestHandler<GetBorrowingByIdQueryRequest, ResponseModel<BorrowingModel>>
    {
        private readonly IRepository<Borrowing> _repository;

        public GetBorrowingByIdQueryHandler(IRepository<Borrowing> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseModel<BorrowingModel>> Handle(GetBorrowingByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var borrowing = await _repository.GetByIdAsync(request.Id);
            if (borrowing != null)
            {
                var borrowingModel = new BorrowingModel
                {
                    Id = borrowing.Id,
                    AppUserId = borrowing.AppUserId,
                    BookId = borrowing.BookId,
                    BorrowDate = borrowing.BorrowDate,
                    IsReturned = borrowing.IsReturned,
                    DueDate = borrowing.DueDate

                };
                return new ResponseModel<BorrowingModel>(borrowingModel, 200);
            }
            else
            {
                return new ResponseModel<BorrowingModel>("Borrowing not found", 404);
            }
        }
    }
}
