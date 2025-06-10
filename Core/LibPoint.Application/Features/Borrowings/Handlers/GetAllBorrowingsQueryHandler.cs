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
    public class GetAllBorrowingsQueryHandler : IRequestHandler<GetAllBorrowingsQueryRequest, ResponseModel<List<BorrowingModel>>>
    {
        private readonly IRepository<Borrowing> _repository;

        public GetAllBorrowingsQueryHandler(IRepository<Borrowing> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseModel<List<BorrowingModel>>> Handle(GetAllBorrowingsQueryRequest request, CancellationToken cancellationToken)
        {
            var borrowings = await _repository.GetAllAsync();
            if (borrowings != null)
            {
                var borrowingModels = borrowings.Select(borrowing => new BorrowingModel
                {
                    Id = borrowing.Id,
                    AppUserId = borrowing.AppUserId,
                    BookId = borrowing.BookId,
                    BorrowDate = borrowing.BorrowDate,
                    DueDate = borrowing.DueDate,
                    IsReturned = borrowing.IsReturned

                }).ToList();
                return new ResponseModel<List<BorrowingModel>>(borrowingModels, 200);
            }
            else
            {
                return new ResponseModel<List<BorrowingModel>>("Herhangi bir ödünç alma bulunamadı!", 404);
            }
        }
    }
}
