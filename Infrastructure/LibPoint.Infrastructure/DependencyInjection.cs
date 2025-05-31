using LibPoint.Application.Abstractions;
using LibPoint.Domain.Constants;
using LibPoint.Infrastructure.Middlewares;
using LibPoint.Infrastructure.Services;
using LibPoint.Infrastructure.Services.Background;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LibPoint.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    JwtOptions jwtSettings = configuration.GetSection("JwtOptions").Get<JwtOptions>();

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                    };
                });

            services.AddAuthorization();

            // -- Services --

            services.AddScoped<ITokenService, TokenService>();

            services.AddHostedService<ReservationBackgroundService>();

            return services;
        }

        public static IApplicationBuilder UseInfrastructureMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<PaginationMiddleware>();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            return app;
        }
    }
}
