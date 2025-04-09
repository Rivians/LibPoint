using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.User.Commands;
using LibPoint.Domain.Constants;
using LibPoint.Domain.Entities.Identity;
using LibPoint.Domain.Models.User;
using Microsoft.AspNetCore.Identity;

namespace LibPoint.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly ITokenService tokenService;

        public async Task<UserLoginModel> LoginAsync(LoginCommandRequest loginCommandRequst)
        {
            if (loginCommandRequst is null)
                throw new NullReferenceException($"{nameof(LoginCommandRequest)} is null!");

            AppUser? user = await userManager.FindByEmailAsync(loginCommandRequst.Email);
            
            if(user is null || user.IsDeleted is true)
            {
                return new()
                {
                    Errors = new string[] { "User not found" },
                    Role = string.Empty,
                    Token = string.Empty,
                    UserModel = new()
                };                
            }

            bool checkPasswordResult = await userManager.CheckPasswordAsync(user, loginCommandRequst.Password);

            if(checkPasswordResult is false)
            {
                return new()
                {
                    Errors = new[] { "Invalid email or password!" },
                    Role = string.Empty,
                    Token = string.Empty,
                    UserModel = new()
                };
            }

            IList<string> userRole = await userManager.GetRolesAsync(user);

            Token token = tokenService.GenerateToken(user);

            return new()
            {
                UserModel = new()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    CreatedAtUtc = user.CreatedTime
                },
                Role = userRole.FirstOrDefault(),
                Errors = new string[] {},
                Token = token.AccessToken
            };
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
                else
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
