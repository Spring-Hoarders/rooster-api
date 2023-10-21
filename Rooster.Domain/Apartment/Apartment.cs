using Rooster.Domain.Building;
using Rooster.Domain.Common;

namespace Rooster.Domain.Apartment;

public record Apartment : EntityWithGuid
{
    public Guid FloorId { get; init; }
    public Floor Floor { get; init; }
    
    public Guid ApartmentTypeId { get; init; }
    public ApartmentType ApartmentType { get; init; }
    
    public int RoomTypeId { get; init; }
    public RoomType RoomType { get; init; }
    
    public string Name { get; init; }
    public ApartmentStatus Status { get; init; }
}