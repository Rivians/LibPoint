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
    public class UpdateReviewCommandHandler
    {
        private readonly IBookRepository<LibPoint.Domain.Entities.Review> _repository;

        public UpdateReviewCommandHandler(IBookRepository<LibPoint.Domain.Entities.Review> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateReviewCommand command)
        {
            var values = await _repository.GetByIdAsync(command.ReviewID);
            
            await _repository.UpdateAsync(values);
        }
    }
}
