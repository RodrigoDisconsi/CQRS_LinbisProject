using CQRSLinbis.Application.Common.Interfaces;
using CQRSLinbis.Infrastructure.Identity;
using CQRSLinbis.Infrastructure.Persistence;
using CQRSLinbis.Infrastructure.Persistence.Dapper;
using CQRSLinbis.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CQRSLinbis.Application.Common.Interfaces.Services;
using CQRSLinbis.Infrastructure.Services;

namespace Microsoft.Extensions.DependencyInjection;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("CQRSLinbisDatabase"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                  options.UseSqlServer(configuration.GetConnectionString("EFCoreDatabase"),
                      b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }


        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddPersistenceDapper(configuration);
        services.AddRepositories();

        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddTransient<IIdentityService, IdentityService>();

        services.AddTransient<IProjectService, ProjectService>();
        services.AddTransient<IDeveloperService, DeveloperService>();


        //services.AddIdentityServer()
        //    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

        //services.AddAuthentication()
        //    .AddIdentityServerJwt();

        //services.AddAuthorization(options =>
        //    options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));


        //services.AddAuthorizationPolicies(); TODO Agregar policies llegado al caso

        return services;
    }
}
