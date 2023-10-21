using Rooster.Domain.Common;

namespace Rooster.Domain.Apartment;

public record ApartmentType : Entity<int>
{
    public string Name { get; init; }
    public decimal Price { get; init; }
}