using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Books.Queries;
using LibPoint.Application.Features.Books.Results;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Books;
using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Books.Handlers
{
    public class GetBookQueryHandler: IRequestHandler<GetBooksQueryRequest, ResponseModel<List<BookModel>>>
    {
        private readonly IRepository<Book> _repository;
        public GetBookQueryHandler(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseModel<List<BookModel>>> Handle(GetBooksQueryRequest request, CancellationToken cancellationToken )
        {
            var values = await _repository.GetAllAsync();
            if (values == null)
            { 
                return new ResponseModel<List<BookModel>>("No books found", 404);
            }
            else
            {
                var bookmodel = values.Select(Book => new BookModel
                {
                    Id = Book.Id,
                    Name = Book.Name,
                    ISBN = Book.ISBN,
                    IsAvailable = Book.IsAvailable,
                    PublishedYear = Book.PublishedYear,
                    Publisher = Book.Publisher,
                    Categories = Book.Categories,
                    AuthorName = Book.AuthorName,
                    ImageUrl = Book.ImageUrl
                }).ToList();
                return new ResponseModel<List<BookModel>>(bookmodel);
                
            }
        }

    }
}
