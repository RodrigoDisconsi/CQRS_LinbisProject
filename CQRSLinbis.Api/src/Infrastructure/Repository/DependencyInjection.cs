using CQRSLinbis.Application.Common.Interfaces.Repository;
using CQRSLinbis.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace CQRSLinbis.Infrastructure.Repositories;
public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(DatabaseRepository<>));

        return services;
    }
}
