namespace Rooster.Domain.Common;

public interface IEntity<T>
{
    T Id { get; init; }
}