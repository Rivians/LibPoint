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
    public class GetBorrowingsByUserIdQueryHandler : IRequestHandler<GetBorrowingByUserIdQueryRequest, ResponseModel<List<BorrowingModel>>>
    {
        private readonly IRepository<Borrowing> _repository;

        public GetBorrowingsByUserIdQueryHandler(IRepository<Borrowing> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseModel<List<BorrowingModel>>> Handle(GetBorrowingByUserIdQueryRequest request, CancellationToken cancellationToken)
        {
            var borrowing = await _repository.GetAllAsync(b => b.AppUserId == request.UserId, false);
            if (borrowing != null)
            {
                var borrowingModels = borrowing.Select(b => new BorrowingModel
                {
                    Id = b.Id,
                    BookId = b.BookId,
                    AppUserId = b.AppUserId,
                    IsActive = b.IsActive,
                    Code = b.Code
                }).ToList();
                return new ResponseModel<List<BorrowingModel>>(borrowingModels, 200);
            }
            else
            {
                return new ResponseModel<List<BorrowingModel>>("No borrowings found for this user.", 404);
            }
        }
    }
}
