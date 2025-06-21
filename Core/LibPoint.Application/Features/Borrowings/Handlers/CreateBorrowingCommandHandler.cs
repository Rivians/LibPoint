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
        private readonly IRepository<Book> _bookRepository;

        public CreateBorrowingCommandHandler(IRepository<Borrowing> repository, IRepository<Book> bookRepository)
        {
            _repository = repository;
            _bookRepository = bookRepository;
        }

        public async Task<ResponseModel<Guid>> Handle(CreateBorrowingCommandRequest request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetAsync(bk => bk.Id == request.BookId, true);
            if (book is null)
                return new ResponseModel<Guid>("Borrowing operation failed, because book is not found", 404);

            var borrowing = new Borrowing
            {
                Id = Guid.NewGuid(),
                AppUserId = request.AppUserId,
                BookId = request.BookId,
                IsActive = true
            };            

            var createResult = await _repository.AddAsync(borrowing);
            if (!createResult)
                return new ResponseModel<Guid>("Borrowing could not be created", 400);

            var borrowingSaveResult = await _repository.SaveChangesAsync();

            if (borrowingSaveResult)
            {
                book.IsAvailable = false;
                await _bookRepository.SaveChangesAsync();

                return new ResponseModel<Guid>(borrowing.Id);
            }
            else
            {
                return new ResponseModel<Guid>("Borrowing saving entity failed.", 400);
            }            
        }            
    }
}
