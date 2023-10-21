using Rooster.Domain.Common;

namespace Rooster.Domain.Account;

public record User : EntityWithGuid
{
    public string Username { get; init; }
    public string Password { get; init; }
    public string Email { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? LastModified { get; init; } 
}