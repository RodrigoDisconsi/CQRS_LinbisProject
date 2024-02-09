using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace CRUDCleanArchitecture.Infrastructure.Persistence.Dapper;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceDapper(this IServiceCollection services, IConfiguration configuration)
    {
        //var connectionString = configuration.GetConnectionString("DapperDatabase");

        //services.AddTransient<IDbConnection>((sp) => new SqlConnection(connectionString));

        services.AddTransient<IDbConnection>((sp) =>
        {
            var dbContext = sp.GetRequiredService<ApplicationDbContext>();
            return dbContext.Database.GetDbConnection();
        });

        return services;
    }
}
