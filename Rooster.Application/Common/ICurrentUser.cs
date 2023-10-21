namespace Rooster.Application.Common.Common;

public interface ICurrentUser
{
    Guid UserId { get; }
    int RoleId { get; }
}

public class CurrentUser : ICurrentUser
{
    public Guid UserId { get; set; }
    public int RoleId { get; set; }
}