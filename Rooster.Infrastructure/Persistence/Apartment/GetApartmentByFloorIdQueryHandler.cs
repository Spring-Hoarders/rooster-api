using MediatR;
using Microsoft.EntityFrameworkCore;
using Rooster.Apartament;
using Rooster.Application.Building;
using System.Linq;

namespace Rooster.Infrastructure.Persistence.Building
{
    public class GetApartmentByFloorId : IRequestHandler<GetApartmentsByFloorQuery, IList<ApartmentResponse>>
    {
        private readonly AppDbContext _db;

        public GetApartmentByFloorId(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IList<ApartmentResponse>> Handle(GetApartmentsByFloorQuery request, CancellationToken cancellationToken)
        {
            return await _db.Apartment
                .Where(x => x.FloorId == request.FloorId)
                .Select(x => new ApartmentResponse
                {
                    Capacity = x.ApartmentType.Capacity,
                    ApartmentTypeId = x.ApartmentTypeId,
                    Name = x.Name,
                    Occupied = 0,
                    Price = x.ApartmentType.Price,
                    Status = x.Status
                    //occupied needs query
                })
                .ToListAsync(cancellationToken);
        }
    }
}
