using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Books.Results;
using LibPoint.Application.Features.Review.Results;
using LibPoint.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Books.Handlers
{
    public class GetBookQueryHandler
    {
        private readonly IBookRepository<Book> _repository;

        public GetBookQueryHandler(IBookRepository<Book> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetBookQueryResult>> Handle()
        {
            var values = await _repository.GetAllAsync();
            return values.Select(x => new GetBookQueryResult
            {
                Id = x.Id,
                IsAvailable = x.IsAvailable,
                ISBN = x.ISBN,
                Name = x.Name,
                PublishedYear = x.PublishedYear,
                Publisher = x.Publisher
            }).ToList();
        }
    }
}
