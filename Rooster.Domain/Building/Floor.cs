using Rooster.Domain.Common;

namespace Rooster.Domain.Building;

public record Floor : EntityWithGuid
{
    public Guid BuildingId { get; init; }
    public Building Building { get; init; }
    
    public int Number { get; init; }
}