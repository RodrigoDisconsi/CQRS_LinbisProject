using CQRSLinbis.Application.Common.Interfaces.Services;
using CQRSLinbis.Infrastructure.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CQRSLinbis.Infrastructure.Services
{
    public static  class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IDeveloperService, DeveloperService>();

            return services;
        }
    }
}
