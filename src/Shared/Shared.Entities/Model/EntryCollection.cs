namespace Shared.Entities.Model;

public class EntryCollection<T> where T : IEntity
{
    public long Total { get; set; }
    public ICollection<T> Items { get; set; }
}
