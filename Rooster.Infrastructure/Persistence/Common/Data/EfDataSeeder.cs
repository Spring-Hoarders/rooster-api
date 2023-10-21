using System.Reflection;
using Rooster.Application.Account;
using Rooster.Application.Common.Common.Data;
using Rooster.Application.Common.Data;
using Rooster.Infrastructure.Persistence.Common.Data.Common;

namespace Rooster.Infrastructure.Persistence.Common.Data;

public class EfDataSeeder : IDataSeeder
{
    private const string SeederMethodName = "Run";

    private readonly AppDbContext _db;
    private readonly IPasswordHasher _passwordHasher;
    private readonly Guid _adminUserId = Guid.Parse("2a800b51-8c2c-4c1a-8ce1-6cf4290e7eb6");

    public EfDataSeeder(AppDbContext db, IPasswordHasher passwordHasher)
    {
        _db = db;
        _passwordHasher = passwordHasher;
    }

    public async Task SeedAsync()
    {
        var seeders = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Select(
                t => new
                {
                    Class = t,
                    SeederAttribute = t.GetCustomAttribute<EfSeederAttribute>(true),
                    RunMethod = t.GetMethod(SeederMethodName, BindingFlags.Public | BindingFlags.Static)
                }
            ).Where(t => t.SeederAttribute != null && t.RunMethod != null);

        foreach (var seeder in seeders)
        {
            if (seeder.SeederAttribute!.Enabled)
            {
                await (Task)seeder.RunMethod!.Invoke(null, GenerateParamSet(seeder.RunMethod))!;
            }
        }
    }
    
    private object[] GenerateParamSet(MethodInfo runMethod)
    {
        List<object> parameters = new();

        foreach (var parameter in runMethod.GetParameters())
        {
            if (parameter.ParameterType == typeof(AppDbContext))
            {
                parameters.Add(_db);
            }
            else if (parameter.ParameterType == typeof(IPasswordHasher))
            {
                parameters.Add(_passwordHasher);
            }
            else if (string.Equals(parameter.Name!, "adminUserId", StringComparison.CurrentCultureIgnoreCase))
            {
                parameters.Add(_adminUserId);
            }
        }

        return parameters.ToArray();
    }
}