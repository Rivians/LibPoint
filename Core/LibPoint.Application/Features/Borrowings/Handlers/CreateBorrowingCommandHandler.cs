using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Borrowings.Commands;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Borrowings.Handlers
{
    public class CreateBorrowingCommandHandler : IRequestHandler<CreateBorrowingCommandRequest, ResponseModel<Guid>>
    {
        private readonly IRepository<Borrowing> _repository;

        public CreateBorrowingCommandHandler(IRepository<Borrowing> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseModel<Guid>> Handle(CreateBorrowingCommandRequest request, CancellationToken cancellationToken)
        {
            var borrowing = new Borrowing
            {
                Id = Guid.NewGuid(),
                AppUserId = request.AppUserId,
                BookId = request.BookId,
                BorrowDate = request.BorrowDate,
                DueDate = request.DueDate,
                IsReturned = request.IsReturned
            };
            var createResult = await _repository.AddAsync(borrowing);
            if (!createResult)
                return new ResponseModel<Guid>("Borrowing could not be created", 400);
            await _repository.SaveChangesAsync();
            return new ResponseModel<Guid>(borrowing.Id);
        }
            
    }
}
