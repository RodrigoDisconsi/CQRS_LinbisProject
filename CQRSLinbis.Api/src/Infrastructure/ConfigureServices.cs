using CRUDCleanArchitecture.Application.Common.Interfaces;
using CRUDCleanArchitecture.Infrastructure.Identity;
using CRUDCleanArchitecture.Infrastructure.Persistence;
using CRUDCleanArchitecture.Infrastructure.Persistence.Dapper;
using CRUDCleanArchitecture.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CRUDCleanArchitecture.Application.Common.Interfaces.Services;

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
