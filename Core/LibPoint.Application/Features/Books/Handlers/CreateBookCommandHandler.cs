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
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommandRequest, ResponseModel<Guid>>
    {
        private readonly IRepository<Book> _repository;

        public CreateBookCommandHandler(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseModel<Guid>> Handle(CreateBookCommandRequest request, CancellationToken cancellationToken)
        {
            var newBook = new Book
            {
                IsAvailable = request.IsAvailable,
                ISBN = request.ISBN,
                Name = request.Name,
                PublishedYear = request.PublishedYear,
                Publisher = request.Publisher,

            };

            var result = await _repository.AddAsync(newBook);
            
            var saveResult = await _repository.SaveChangesAsync();

            if(saveResult == false)
            {
                return new ResponseModel<Guid>("Book could not be created", 500);
            }
            else
            {
                return new ResponseModel<Guid>(newBook.Id);
            }


                
            

        }




    }
}
