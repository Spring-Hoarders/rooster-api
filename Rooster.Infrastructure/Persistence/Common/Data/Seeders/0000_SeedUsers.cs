using Rooster.Application.Account;
using Rooster.Domain.Account;
using Rooster.Infrastructure.Persistence.Common.Data.Common;

namespace Rooster.Infrastructure.Persistence.Common.Data.Seeders;

[EfSeeder]
public class AddUsers
{
    private const string AdminEmail = "admin@rooster.com"; 
    
    public static async Task Run(AppDbContext db, IPasswordHasher passwordHasher, Guid adminUserId)
    {
        if (db.Users.Any(x => x.Email.Equals(AdminEmail))) return;

        db.Add(new User
        {
            Id = adminUserId,
            Username = AdminEmail,
            Email = AdminEmail,
            Password = passwordHasher.HashPassword("P@ssword1"),
            CreatedAt = DateTimeOffset.UtcNow
        });

        await db.SaveChangesAsync();
    }
}