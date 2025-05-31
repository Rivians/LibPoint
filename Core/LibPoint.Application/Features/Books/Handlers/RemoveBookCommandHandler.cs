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
    public class RemoveBookCommandHandler
    {
        private readonly IBookRepository<Book> _repository;

        public RemoveBookCommandHandler(IBookRepository<Book> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveBookCommand command)
        {
            var value = await _repository.GetByIdAsync(command.Id);
            await _repository.RemoveAsync(value);
        }
    }
}
