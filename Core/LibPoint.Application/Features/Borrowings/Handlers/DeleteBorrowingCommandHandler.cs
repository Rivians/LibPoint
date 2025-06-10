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
    public class DeleteBorrowingCommandHandler : IRequestHandler<DeleteBorrowingCommandRequest, ResponseModel<Guid>>
    {
        private readonly IRepository<Borrowing> repository;

        public DeleteBorrowingCommandHandler(IRepository<Borrowing> repository)
        {
            this.repository = repository;
        }

        public async Task<ResponseModel<Guid>> Handle(DeleteBorrowingCommandRequest request, CancellationToken cancellationToken)
        {
            var deletingBorrowing = await repository.GetByIdAsync(request.Id);
            if (deletingBorrowing != null)
            {
                repository.Delete(deletingBorrowing);
                await repository.SaveChangesAsync();

            }
            else
            {
                throw new Exception("Borrowing not found");
            }
            return new ResponseModel<Guid>(request.Id);
        }
            
    }
}
