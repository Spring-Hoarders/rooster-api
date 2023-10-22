using MediatR;

namespace Rooster.Application.Floor;

public record GetFloorsByBuildingIdQuery : IRequest<IList<FloorResponse>>
{
    public Guid BuildingId { get; init; }
}

public record FloorResponse
{
    public Guid Id { get; init; }
    public int Number { get; init; }
}