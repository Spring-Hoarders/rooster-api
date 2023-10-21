using Rooster.Domain.Apartment;
using Rooster.Domain.Building;
using Rooster.Infrastructure.Persistence.Common.Data.Common;

namespace Rooster.Infrastructure.Persistence.Common.Data.Seeders;

[EfSeeder]
public class SeedBuildings
{
    public static async Task Run(AppDbContext db)
    {
        if (db.Buildings.Any()) return;
        var buildings = new List<(Guid, string)>
        {
            (Guid.NewGuid(), "Section A"),
            (Guid.NewGuid(), "Section B"),
            (Guid.NewGuid(), "Section C")
        };


        foreach (var (buildingId, buildingName) in buildings)
        {
            db.Add(new Domain.Building.Building
            {
                Id = buildingId,
                Name = buildingName
            });
        }

        if (!db.Floor.Any())
        {
            foreach (var (buildingId, buildingName) in buildings)
            {
                for (var i = 0; i < 3; i++)
                {
                    var floorId = Guid.NewGuid();
                    db.Add(new Floor
                    {
                        Id = floorId,
                        BuildingId = buildingId,
                        Number = i + 1
                    });
                    for (var j = 0; j < 4; j++)
                    {
                        db.Add(new Apartment
                        {
                            Id = Guid.NewGuid(),
                            FloorId = floorId,
                            Name = $"{buildingName.Last()}{i + 1}0{j + 1}",
                            ApartmentTypeId = 2,
                            Status = ApartmentStatus.Free
                        });
                    }
                }
            }
        }

        await db.SaveChangesAsync();
    }
}