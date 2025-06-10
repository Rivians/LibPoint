using AutoMapper;
using LibPoint.Application.Abstractions;
using LibPoint.Domain.Entities.Identity;
using LibPoint.Domain.Models.Responses;
using LibPoint.Domain.Models.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.User.Queries
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQueryRequest, ResponseModel<List<UserModel>>>
    {
        private readonly IRepository<AppUser> _repository;
        private readonly IMapper _mapper;
        public GetUserListQueryHandler(IRepository<AppUser> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<UserModel>>> Handle(GetUserListQueryRequest request, CancellationToken cancellationToken)
        {
            var users = await _repository.GetAllAsync(u => u.IsDeleted == false);

            if (users is null)
                return new ResponseModel<List<UserModel>>("Users are empty");

            var mappedUsers = _mapper.Map<List<UserModel>>(users);

            return new ResponseModel<List<UserModel>>(mappedUsers);
        }
    }
}
