using CQRSLinbis.Application.Common.Interfaces;
using CQRSLinbis.Infrastructure.Identity;
using CQRSLinbis.Infrastructure.Persistence;
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

        using (var serviceProvider = services.BuildServiceProvider())
        {
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

            dbContext.Database.EnsureCreated();
        }


        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddRepositories();
        services.AddServices();

        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();


        return services;
    }
}