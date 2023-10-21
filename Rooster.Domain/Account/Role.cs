using Rooster.Domain.Common;

namespace Rooster.Domain.Account;

public record Role : Entity<int>
{
    public string Name { get; init; }
}