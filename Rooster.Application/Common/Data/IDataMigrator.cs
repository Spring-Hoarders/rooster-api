namespace Rooster.Application.Common.Data;

public interface IDataMigrator
{
    Task Migrate();
}