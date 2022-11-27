using Microsoft.EntityFrameworkCore;
using Shared.Entities.Model;

namespace Shared.Persistence.DatabaseContext;

public abstract class PersistenceContext : DbContext
{
    protected PersistenceContext(DbContextOptions options) : base(options)
    { }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries()
            .Where(e =>
                e.Entity is IEntity
                && (e.State == EntityState.Modified || e.State == EntityState.Added));

        foreach (var entry in entries)
        {
            var entity = entry.Entity as IEntity;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = DateTime.UtcNow;
            }

            if (entry is IUpdatableEntity updatableEntity)
            {
                updatableEntity.UpdatedAt = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
