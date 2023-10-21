using Rooster.Domain.Common;

namespace Rooster.Domain.Account;

public class UserRole : IAuditableEntity
{
    public Guid UserId { get; init; }
    public Role RoleId { get; init; }
    
    public Guid CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public DateTimeOffset? LastModifiedAt { get; set; }
    
    public User User { get; init; }
}