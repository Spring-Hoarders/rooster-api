using Rooster.Domain.Apartment;
using Rooster.Infrastructure.Persistence.Common.Data.Common;

namespace Rooster.Infrastructure.Persistence.Common.Data.Seeders;

[EfSeeder]
public class SeedApartmentTypes {
    public static async Task Run(AppDbContext db)
    {
        if(db.ApartmentTypes.Any()) return;

        db.Add(new ApartmentType
        {
            Id = (int)ApartmentTypes.OneRoom,
            Name = "Single Room",
            Capacity = 1,
            Price = 25_000
        });
        
        db.Add(new ApartmentType
        {
            Id = (int)ApartmentTypes.TwoRoom,
            Name = "Two Room",
            Capacity = 2,
            Price = 30_000
        });
        
        db.Add(new ApartmentType
        {
            Id = (int)ApartmentTypes.ThreeRoom,
            Name = "Three People",
            Capacity = 3,
            Price = 35_000
        });
        
        db.Add(new ApartmentType
        {
            Id = (int)ApartmentTypes.FourRoom,
            Name = "Four People",
            Capacity = 4,
            Price = 40_000
        });

        await db.SaveChangesAsync();
    }
}