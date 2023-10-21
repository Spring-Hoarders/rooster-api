using Rooster.Domain.Common;

namespace Rooster.Domain.Apartment;

public record RoomType : Entity<int>
{
    public string Name { get; init; }
    public int Capacity { get; init; }
}