using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Books.Commands;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace LibPoint.Application.Features.Books.Handlers
{
    public class RemoveBookCommandHandler : IRequestHandler<RemoveBookCommandRequest, ResponseModel<Guid>>
    {
        private readonly IRepository<Book> _repository;

        public RemoveBookCommandHandler(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseModel<Guid>> Handle(RemoveBookCommandRequest request, CancellationToken cancellationToken)
        {
            var deletingBook = await _repository.GetAsync(x => x.Id == request.Id,tracking:true);
            if (deletingBook == null)
            {
                return new ResponseModel<Guid>
                {
                    Success = false,
                    Data = Guid.Empty,
                    Messages = new[] { "Book not found." },
                    
                };
            }

            var deleteResult = _repository.Delete(deletingBook);
            if (!deleteResult)
            {
                return new ResponseModel<Guid>
                {
                    Success = false,
                    Data = Guid.Empty,
                    Messages = new[] { "Failed to delete the book." },
                    
                };
            }

            await _repository.SaveChangesAsync();

            return new ResponseModel<Guid>
            {
                Success = true,
                Data = deletingBook.Id,
                Messages = new[] { "Book deleted successfully." },
                StatusCode = 200
            };
        }
    }
}
