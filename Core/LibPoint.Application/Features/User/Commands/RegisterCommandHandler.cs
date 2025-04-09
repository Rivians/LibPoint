using LibPoint.Application.Abstractions;
using LibPoint.Domain.Models.Responses;
using LibPoint.Domain.Models.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.User.Commands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, ResponseModel<UserRegisterModel>>
    {
        private readonly IAuthService _authService;
        public RegisterCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<ResponseModel<UserRegisterModel>> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            UserRegisterModel registerResult = await _authService.RegisterAsync(request);

            if (registerResult.Errors.Any())
                return new ResponseModel<UserRegisterModel>(registerResult.Errors, 400);

            return new ResponseModel<UserRegisterModel>(registerResult);
        }
    }
}
