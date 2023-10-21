using Rooster.Domain.Common;

namespace Rooster.Domain.Payment;

public record Payment : EntityWithGuid
{
    public Guid ClientId { get; init; }
    public Client.Client Client { get; init; }
    
    public Guid ContractId { get; init; }
    public Contract.Contract Contract { get; init; }
    
    public string FileUrl { get; init; }
    public decimal Amount { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
}