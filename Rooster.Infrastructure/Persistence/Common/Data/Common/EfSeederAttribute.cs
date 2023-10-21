namespace Rooster.Infrastructure.Persistence.Common.Data.Common;

[AttributeUsage(AttributeTargets.Class)]
public class EfSeederAttribute : Attribute
{
    public bool Enabled = true;
}