using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Rooster.Application.Account;
using Rooster.Application.Common;
using Rooster.Application.Common.Common.Dates;

namespace Rooster.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, ISettings settings)
    {
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(Assembly.GetEntryAssembly()!); });
        
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IDateTimeFactory, DateTimeFactory>();
        
        return services;
    }
}