namespace Rooster.Domain.Common;

public interface IAuditableEntity
{
    Guid CreatedBy { get; set; }
    DateTimeOffset CreatedAt { get; set; }
    Guid? LastModifiedBy { get; set; }
    DateTimeOffset? LastModifiedAt { get; set; }
}