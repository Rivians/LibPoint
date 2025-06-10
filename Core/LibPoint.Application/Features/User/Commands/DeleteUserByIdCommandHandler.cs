using LibPoint.Application.Abstractions;
using LibPoint.Domain.Entities.Identity;
using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.User.Commands
{
    public class DeleteUserByIdCommandHandler : IRequestHandler<DeleteUserByIdCommandRequest, ResponseModel<bool>>
    {
        private readonly IRepository<AppUser> _repository;
        public DeleteUserByIdCommandHandler(IRepository<AppUser> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseModel<bool>> Handle(DeleteUserByIdCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.Id);

            if(user is not null)
            {
                user.IsDeleted = true;

                var saveResult = await _repository.SaveChangesAsync();

                if (saveResult)
                    return new ResponseModel<bool>(saveResult);

                return new ResponseModel<bool>("Delete user operation failed");
            }

            return new ResponseModel<bool>("User not found", 404);
        }
    }
}
