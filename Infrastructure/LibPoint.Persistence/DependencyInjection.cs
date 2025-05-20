using LibPoint.Application.Abstractions;
using LibPoint.Domain.Entities.Identity;
using LibPoint.Persistence.Data;
using LibPoint.Persistence.Repositories;
using LibPoint.Persistence.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LibPointDbContext>(options =>
            {
                // options.UseSqlServer(configuration.GetConnectionString("ConnectionString"));
                //options.UseNpgsql(configuration["SqlServerOptions:ConnectionString"]);
                options.UseSqlServer(configuration["SqlServerOptions:ConnectionString"]);
            });


            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = null;

                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<LibPointDbContext>()
            .AddDefaultTokenProviders();
   

            // -- Services --
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IAuthService, AuthService>();


            // -- Repositories --
            services.AddScoped<ISeatRepository, SeatRepository>();

            return services;
        }
        
    }    
}
