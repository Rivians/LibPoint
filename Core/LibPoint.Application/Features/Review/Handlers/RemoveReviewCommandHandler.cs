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
    public class RemoveReviewCommandHandler
    {
        private readonly IBookRepository<LibPoint.Domain.Entities.Review> _repository;

        public RemoveReviewCommandHandler(IBookRepository<LibPoint.Domain.Entities.Review> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveReviewCommand command)
        {
            var value = await _repository.GetByIdAsync(command.ReviewID);
            await _repository.RemoveAsync(value);
        }
    }
}
