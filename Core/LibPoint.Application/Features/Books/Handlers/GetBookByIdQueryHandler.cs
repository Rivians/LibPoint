using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Books.Queries;
using LibPoint.Application.Features.Books.Results;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Books;
using LibPoint.Domain.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LibPoint.Application.Features.Books.Handlers
{
    public class GetBookByIdQueryHandler: IRequestHandler<GetBookByIdQueryRequest, ResponseModel<BookModel>>
    {
        private readonly IRepository<Book> _repository;

        public GetBookByIdQueryHandler(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseModel<BookModel>> Handle(GetBookByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAsync(b => b.Id == request.Id, false, b => b.Author, b => b.Categories);
            if (values == null)
            {
                return new ResponseModel<BookModel>("hata", 400);


            }
            else
            {
                var bookmodel = new BookModel
                {
                    Id = values.Id,
                    Name = values.Name,
                    ISBN = values.ISBN,
                    IsAvailable = values.IsAvailable,
                    PublishedYear = values.PublishedYear,
                    Publisher = values.Publisher,


                };

                return new ResponseModel<BookModel>(bookmodel);
               
            }
            
        }
    }
}
