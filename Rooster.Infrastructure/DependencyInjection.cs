using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rooster.Application.Common;
using Rooster.Application.Common.Common.Data;
using Rooster.Application.Common.Data;
using Rooster.Infrastructure.Persistence;
using Rooster.Infrastructure.Persistence.Common.Data;

namespace Rooster.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ISettings settings)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(settings.ConnectionString, x => { x.MigrationsAssembly(AboutMe.Assembly.FullName); })
                .EnableSensitiveDataLogging()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        services.AddScoped<IDataMigrator, EfDataMigrator>();
        services.AddScoped<IDataSeeder, EfDataSeeder>();
        services.AddScoped<IDataWriter, EfDataWriter>();

        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });

        return services;
    }
}