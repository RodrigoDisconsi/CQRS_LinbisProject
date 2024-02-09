using Microsoft.Extensions.DependencyInjection;

namespace CRUDCleanArchitecture.Infrastructure.Repositories;
public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services;
    }
}
