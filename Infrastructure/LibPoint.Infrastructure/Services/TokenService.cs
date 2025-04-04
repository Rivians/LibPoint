using LibPoint.Application.Abstractions;
using LibPoint.Domain.Constants;
using LibPoint.Domain.Entities.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions jwtOptions;
        public TokenService(IOptions<JwtOptions> jwtOptions)
        {
            this.jwtOptions = jwtOptions.Value;
        }

        public Token GenerateToken(AppUser appUser)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()),
                new Claim("Id", appUser.Id.ToString()),
                new Claim("Email", appUser.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddMinutes(jwtOptions.ExpiryInMinutes);

            var securityToken = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: signingCredentials
            );

            Token token = new();
            token.Expiration = expires;
            token.AccessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }
    }
}
