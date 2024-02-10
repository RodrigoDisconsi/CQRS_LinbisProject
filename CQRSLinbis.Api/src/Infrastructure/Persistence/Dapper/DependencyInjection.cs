using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.SqlClient;
using System.Data;
using static Confluent.Kafka.ConfigPropertyNames;

namespace CQRSLinbis.Infrastructure.Persistence.Dapper;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceDapper(this IServiceCollection services, IConfiguration configuration)
    {
        //var connectionString = configuration.GetConnectionString("DapperDatabase");

        //services.AddTransient<IDbConnection>((sp) => new SqlConnection(connectionString));

        using (var serviceProvider = services.BuildServiceProvider())
        {
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // Garantizamos que la base de datos se cree y se llene con datos iniciales
            dbContext.Database.EnsureCreated();
        }

        // Ahora configuramos Dapper para que pueda acceder a la misma base de datos en memoria
        services.AddTransient<IDbConnection>((sp) =>
        {
            var dbContext = sp.GetRequiredService<ApplicationDbContext>();
            return dbContext.Database.GetDbConnection();
        });

        return services;
    }
}
