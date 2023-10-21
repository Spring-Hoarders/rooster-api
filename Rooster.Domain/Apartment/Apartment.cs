using Rooster.Domain.Building;
using Rooster.Domain.Common;

namespace Rooster.Domain.Apartment;

public record Apartment : EntityWithGuid
{
    public Guid FloorId { get; init; }
    public Floor Floor { get; init; }
    
    public string Name { get; init; }
    public ApartmentStatus Status { get; init; }
    public ApartmentType Type { get; init; }
}