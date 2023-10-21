using MediatR;

namespace Rooster.Application.Building;

public record GetBuildingsQuery : IRequest<IList<BuildingResponse>>;

public record BuildingResponse 
{
    public Guid Id { get; init; }
    public string Name { get; init; }
}