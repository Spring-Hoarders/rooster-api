using Rooster.Domain.Client;
using Rooster.Domain.Common;

namespace Rooster.Domain.Contract;

public record Contract : EntityWithGuid
{
    public Guid ApartmentId { get; init; }
    public Apartment.Apartment Apartment { get; init; }
    
    public Guid ClientId { get; init; }
    public Client.Client Client { get; init; }
    
    public ContractStatus Status { get; init; }
    public DateTimeOffset? AccommodationDate { get; init; }
    public DateTimeOffset? EvictionDate { get; init; }
    public DateTimeOffset StartDate { get; init; }
    public DateTimeOffset EndDate { get; init; }
    public GuaranteeType Guarantee { get; init; }
}