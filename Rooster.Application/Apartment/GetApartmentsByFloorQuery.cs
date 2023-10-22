using MediatR;
using Rooster.Domain.Apartment;
using Rooster.Domain.Building;

namespace Rooster.Apartament
{
    public record GetApartmentsByFloorQuery : IRequest<IList<ApartmentResponse>>
    {
        public Guid FloorId { get; set; }

    }
    public record ApartmentResponse
    {
        public int ApartmentTypeId { get; init; }
        public string Name { get; init; }
        public ApartmentStatus Status { get; init; }
        public int Capacity { get; init; }
        public int Occupied { get; init; }
        public decimal Price { get; init; } 

    }
}
