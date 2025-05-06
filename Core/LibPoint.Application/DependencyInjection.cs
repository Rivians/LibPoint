using LibPoint.Application.BackgroundServices;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LibPoint.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddHostedService<ReservationBackgroundService>();

            return services;
        }
    }
}
