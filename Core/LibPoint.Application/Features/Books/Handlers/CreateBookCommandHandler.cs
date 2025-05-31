using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Books.Commands;
using LibPoint.Application.Features.Review.Commands;
using LibPoint.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Books.Handlers
{
    public class CreateBookCommandHandler
    {
        private readonly IBookRepository<Book> _repository;

        public CreateBookCommandHandler(IBookRepository<Book> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateBookCommand command)
        {
            await _repository.CreateAsync(new Book
            {
                IsAvailable = command.IsAvailable,
                ISBN = command.ISBN,
                Name = command.Name,
                PublishedYear = command.PublishedYear,
                Publisher = command.Publisher,
                
            });
        }
    }
}
