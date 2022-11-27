namespace Shared.Entities.Model;

public interface IEntity
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
}

public interface IUpdatableEntity : IEntity
{
    public DateTime? UpdatedAt { get; set; }
}

public interface ISoftDeletableEntity : IUpdatableEntity
{
    public bool Active { get; set; }
}
