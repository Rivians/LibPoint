using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Books.Queries;
using LibPoint.Application.Features.Books.Results;
using LibPoint.Application.Features.Review.Queries;
using LibPoint.Application.Features.Review.Results;
using LibPoint.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Books.Handlers
{
    public class GetBookByIdQueryHandler
    {
        private readonly IBookRepository<Book> _repository;

        public GetBookByIdQueryHandler(IBookRepository<Book> repository)
        {
            _repository = repository;
        }

        public async Task<GetBookByIdQueryResult> Handle(GetBookByIdQuery query)
        {
            var values = await _repository.GetByIdAsync(query.Id);
            return new GetBookByIdQueryResult
            {
                Id = values.Id,
                Publisher = values.Publisher,
                PublishedYear = values.PublishedYear,
                Name = values.Name,
                ISBN = values.ISBN,
                IsAvailable = values.IsAvailable
            };
        }
    }
}
