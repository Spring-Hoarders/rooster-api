using Microsoft.EntityFrameworkCore;
using Rooster.Application.Common.Common.Data;
using Rooster.Application.Common.Data;

namespace Rooster.Infrastructure.Persistence.Common.Data;

public class EfDataMigrator : IDataMigrator
{
    private readonly AppDbContext _db;

    public EfDataMigrator(AppDbContext db)
    {
        _db = db;
    }

    public async Task Migrate()
    {
        if ((await _db.Database.GetPendingMigrationsAsync()).Any())
            await _db.Database.MigrateAsync();
    }
}