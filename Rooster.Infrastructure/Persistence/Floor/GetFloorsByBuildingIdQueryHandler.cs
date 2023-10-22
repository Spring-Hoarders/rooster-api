using MediatR;
using Microsoft.EntityFrameworkCore;
using Rooster.Application.Floor;

namespace Rooster.Infrastructure.Persistence.Floor;

public class GetFloorsByBuildingIdQueryHandler: IRequestHandler<GetFloorsByBuildingIdQuery, IList<FloorResponse>>
{
    private readonly AppDbContext _db;

    public GetFloorsByBuildingIdQueryHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IList<FloorResponse>> Handle(GetFloorsByBuildingIdQuery request, CancellationToken cancellationToken)
    {
        return await _db.Floor
            .Where(x => x.BuildingId == request.BuildingId)
            .Select(x => new FloorResponse
            {
                Id = x.Id,
                Number = x.Number
            }).ToListAsync(cancellationToken);
    }
}