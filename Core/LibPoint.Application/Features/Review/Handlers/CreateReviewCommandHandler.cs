using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Review.Commands;
using LibPoint.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Review.Handlers
{
    public class CreateReviewCommandHandler
    {
        private readonly IBookRepository<LibPoint.Domain.Entities.Review> _repository;

        public CreateReviewCommandHandler(IBookRepository<LibPoint.Domain.Entities.Review> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateReviewCommand command)
        {
            await _repository.CreateAsync(new LibPoint.Domain.Entities.Review
            {
                Comment = command.Comment,
                Rating = command.Rating
            });
        }
    }
}
