using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Borrowings.Commands;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Entities.Identity;
using LibPoint.Domain.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
        private readonly IRepository<AppUser> _userRepository;
        private readonly UserManager<AppUser> _userManager;
        public CompleteBorrowingCommandHandler(IRepository<Borrowing> repository, IRepository<Book> bookRepository, IRepository<AppUser> userRepository, UserManager<AppUser> userManager)
        {
            _repository = repository;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<ResponseModel<bool>> Handle(CompleteBorrowingCommandRequest request, CancellationToken cancellationToken)
        {
            var borrowing = await _repository.GetAsync(br => br.AppUserId == request.AppUserId && br.BookId == request.BookId && br.IsActive, true);
            var book = await _bookRepository.GetAsync(bk => bk.Id == request.BookId, true);
            var admin = await _userRepository.GetAsync(a => a.Id == request.AdminId, true);

            if (borrowing is null)
                return new ResponseModel<bool>("Active borrowing not found for this book and user, 404");

            if (book is null)
                return new ResponseModel<bool>("Book not found", 404);

            if (admin is null)
                return new ResponseModel<bool>("Admin user not found", 404);

            bool isAdmin = await _userManager.IsInRoleAsync(admin, "Admin");
            if(!isAdmin)
                return new ResponseModel<bool>("Unauthorized: Only admins can complete borrowings", 403);

            borrowing.IsActive = false;
            book.IsAvailable = true;

            var saveResult = await _repository.SaveChangesAsync();
            return new ResponseModel<bool>(saveResult);
        }
    }
}
