using Rooster.Domain.Common;

namespace Rooster.Domain.Client;

public record Client : EntityWithGuid
{
    public Guid ApartmentId { get; init; }
    public Apartment.Apartment Apartment { get; init; }
    
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string PhoneNumber { get; init; }
}