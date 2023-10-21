using Rooster.Application.Account;
using Rooster.Domain.Account;
using Rooster.Infrastructure.Persistence.Common.Data.Common;

namespace Rooster.Infrastructure.Persistence.Common.Data.Seeders;

[EfSeeder]
public class AddUsers
{
    public static async Task Run(AppDbContext db)
    {
        if (db.Roles.Any()) return;

        db.Add(new Role
        {
            Id = (int)Roles.SuperAdmin,
            Name = "SuperAdmin"
        });

        db.Add(new Role
        {
            Id = (int)Roles.BuildingAdmin,
            Name = "BuildingAdmin"
        });

        db.Add(new Role
        {
            Id = (int)Roles.FloorAdmin,
            Name = "FloorAdmin"
        });

        await db.SaveChangesAsync();
    }
}