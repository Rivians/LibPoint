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
    public class CompleteBorrowingCommandHandler : IRequestHandler<CompleteBorrowingCommandRequest, ResponseModel<bool>>
    {
        private readonly IRepository<Borrowing> _repository;
        private readonly IRepository<Book> _bookRepository;
        public CompleteBorrowingCommandHandler(IRepository<Borrowing> repository, IRepository<Book> bookRepository)
        {
            _repository = repository;
            _bookRepository = bookRepository;
        }

        public async Task<ResponseModel<bool>> Handle(CompleteBorrowingCommandRequest request, CancellationToken cancellationToken)
        {
            var borrowing = await _repository.GetAsync(br => br.AppUserId == request.AppUserId && br.BookId == request.BookId && br.IsActive, true);
            var book = await _bookRepository.GetAsync(bk => bk.Id == request.BookId, true);

            if (borrowing is null)
                return new ResponseModel<bool>("Active borrowing not found for this book and user, 404");

            borrowing.IsActive = false;
            book.IsAvailable = true;

            var saveResult = await _repository.SaveChangesAsync();
            return new ResponseModel<bool>(saveResult);
        }
    }
}
