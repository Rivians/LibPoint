using LibPoint.Domain.Entities.Identity;
using LibPoint.Persistence.Data;
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
    public static class PersistenceServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LibPointDbContext>(options =>
            {
                //options.UseSqlServer(configuration.GetConnectionString("ConnectionString"));
                options.UseSqlServer(configuration["SqlServerOptions:ConnectionString"]);
            });

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                services.AddIdentity<AppUser, AppRole>(options =>
                {
                    options.User.RequireUniqueEmail = true;

                    options.SignIn.RequireConfirmedAccount = false;
                    options.SignIn.RequireConfirmedEmail = false;

                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 3;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<LibPointDbContext>()
                .AddDefaultTokenProviders();
            });
        }
    }
}
