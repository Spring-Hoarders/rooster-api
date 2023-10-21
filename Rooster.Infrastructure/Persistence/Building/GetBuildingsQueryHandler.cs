using MediatR;
using Microsoft.EntityFrameworkCore;
using Rooster.Application.Building;

namespace Rooster.Infrastructure.Persistence.Building;

public class GetBuildingsQueryHandler : IRequestHandler<GetBuildingsQuery, IList<BuildingResponse>>
{
    private readonly AppDbContext _db;

    public GetBuildingsQueryHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IList<BuildingResponse>> Handle(GetBuildingsQuery request, CancellationToken cancellationToken)
    {
        return await _db.Buildings.Select(x => new BuildingResponse
        {
            Id = x.Id,
            Name = x.Name
        }).ToListAsync(cancellationToken);
    }
}