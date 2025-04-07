using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.User.Commands;
using LibPoint.Domain.Entities.Identity;
using LibPoint.Domain.Models.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly ITokenService tokenService;

        public Task<UserLoginModel> LoginAsync(LoginCommandRequest loginCommandRequst)
        {
            throw new NotImplementedException();
        }

        public async Task<UserRegisterModel> RegisterAsync(RegisterCommandRequest registerCommandRequest, string? role = "User")
        {
            if (registerCommandRequest is null)
                throw new NullReferenceException($"{nameof(RegisterCommandRequest)} is null!");

            AppUser appUser = new()
            {
                Name = registerCommandRequest.Name ?? string.Empty,
                Surname = registerCommandRequest.Surname ?? string.Empty,
                Email = registerCommandRequest.Email ?? string.Empty,
                PhoneNumber = registerCommandRequest.PhoneNumber ?? string.Empty,
            };

            IdentityResult userResult = await userManager.CreateAsync(appUser, registerCommandRequest.Password);

            if (userResult.Succeeded)
            {
                bool userRoleExist = await roleManager.RoleExistsAsync(role);
                if (!userRoleExist)
                {
                    IdentityResult roleResult = await roleManager.CreateAsync(new AppRole { Name = role });
                    if (!roleResult.Succeeded)
                    {
                        return new()
                        {
                            IsSuccess = false,
                            Errors = roleResult.Errors.Select(e => e.Description).ToArray()
                        };
                    }
                }

                IdentityResult addToRoleResult = await userManager.AddToRoleAsync(appUser, role);
                if (addToRoleResult.Succeeded)
                {
                    return new()
                    {
                        IsSuccess = true,
                        Role = role,
                        UserId = appUser.Id,
                        Errors = new string[] { }
                    };
                }
                if (!addToRoleResult.Succeeded)
                {
                    return new()
                    {
                        IsSuccess = false,
                        Role = role,
                        UserId = appUser.Id,
                        Errors = addToRoleResult.Errors.Select(e => e.Description).ToArray()
                    };
                }
            }
            else
            {
                return new()
                {
                    Errors = userResult.Errors.Select(e => e.Description).ToArray(),
                    IsSuccess = false,
                    Role = role
                };
            }
        }
    }
}
