using LibPoint.Application.Features.User.Commands;
using LibPoint.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Abstractions
{
    public interface IAuthService
    {
        Task<UserRegisterModel> RegisterAsync(RegisterCommandRequest registerCommandRequest, string? role = "User");
        Task<UserLoginModel> LoginAsync(LoginCommandRequest loginCommandRequst);
    }
}
