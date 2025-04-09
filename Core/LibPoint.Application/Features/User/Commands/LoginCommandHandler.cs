using LibPoint.Application.Abstractions;
using LibPoint.Domain.Models.Responses;
using LibPoint.Domain.Models.User;
using MediatR;

namespace LibPoint.Application.Features.User.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, ResponseModel<UserLoginModel>>
    {
        private readonly IAuthService authService;
        public LoginCommandHandler(IAuthService authService)
        {
            this.authService = authService;
        }

        public async Task<ResponseModel<UserLoginModel>> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            UserLoginModel loginResult = await authService.LoginAsync(request);

            if (loginResult.Errors.Any())
                return new ResponseModel<UserLoginModel>(loginResult.Errors, 400);

            return new ResponseModel<UserLoginModel>(loginResult);           
        }
    }
}
