using Rooster.Domain.Common;

namespace Rooster.Domain.Building;

public record Building : EntityWithGuid
{
    public string Name { get; init; }
    
    // FK
}