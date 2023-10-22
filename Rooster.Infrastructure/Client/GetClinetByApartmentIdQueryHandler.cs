
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rooster.Apartament;
using Rooster.Application.Building;
using Rooster.Application.Clients;

namespace Rooster.Infrastructure.Persistence.Building;

public class GetClinetByApartmentIdQueryHandler : IRequestHandler<GetClientsByApartmentIdQuery , IList<ClientResponse>>
{
    private readonly AppDbContext _db;

    public GetClinetByApartmentIdQueryHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IList<ClientResponse>> Handle(GetClientsByApartmentIdQuery request, CancellationToken cancellationToken)
    {
        return await _db.Clients
            .Where(x => x.ApartmentId == request.ApartmentId)
            .Select(x => new ClientResponse
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber
            })
            .ToListAsync(cancellationToken);
    }
}
