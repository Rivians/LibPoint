using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Borrowings.Commands;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Borrowings.Handlers
{
    public class UpdateBorrowingCommandHandler : IRequestHandler<UpdateBorrowingCommandRequest, ResponseModel<Guid>>
    {
        private readonly IRepository<Borrowing> repository;

        public UpdateBorrowingCommandHandler(IRepository<Borrowing> repository)
        {
            this.repository = repository;
        }

        public async Task<ResponseModel<Guid>> Handle(UpdateBorrowingCommandRequest request, CancellationToken cancellationToken)
        {
            var updatingBorrowing = await repository.GetByIdAsync(request.Id);
            if (updatingBorrowing == null)
            {
                return new ResponseModel<Guid>("Borrowing not found", 404);
            }
            else
            {
                updatingBorrowing.AppUserId = request.AppUserId;
                updatingBorrowing.BookId = request.BookId;
                var updateResult = repository.Update(updatingBorrowing);
                if (!updateResult)
                {
                    return new ResponseModel<Guid>("Borrowing could not be updated", 400);
                }
                await repository.SaveChangesAsync();
            }
            return new ResponseModel<Guid>(updatingBorrowing.Id);
        }
    }
}
