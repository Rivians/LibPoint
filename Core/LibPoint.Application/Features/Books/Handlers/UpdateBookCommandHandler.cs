using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Books.Commands;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Books.Handlers
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommandRequest, ResponseModel<Guid>>
    {
        private readonly IRepository<Book> _repository;

        public UpdateBookCommandHandler(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseModel<Guid>> Handle(UpdateBookCommandRequest request, CancellationToken cancellationToken)
        {
            var updatingBook = await _repository.GetAsync(x => x.Id == request.Id, tracking: true);
            if (updatingBook == null)
            {
                return new ResponseModel<Guid>
                {
                    Success = false,
                    Messages = new[] { "Book not found" },
                    Data = Guid.Empty
                };
            }
            else
            {
                updatingBook.Name = request.Name;
                updatingBook.ISBN = request.ISBN;
                updatingBook.IsAvailable = request.IsAvailable;
                updatingBook.Publisher = request.Publisher;
                updatingBook.PublishedYear = request.PublishedYear;

                var updateResult = _repository.Update(updatingBook);
                if (!updateResult)
                {
                    return new ResponseModel<Guid>
                    {
                        Success = false,
                        Messages = new[] { "Failed to update the book" },
                        Data = Guid.Empty
                    };
                }

                await _repository.SaveChangesAsync();

                return new ResponseModel<Guid>
                {
                    Success = true,
                    Messages = new[] { "Book updated successfully" },
                    Data = updatingBook.Id
                };
            }
        }
    }
}
