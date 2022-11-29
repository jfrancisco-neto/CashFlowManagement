using Balance.Domain.Model;
using Balance.Persistence.Mapping;
using Microsoft.EntityFrameworkCore;
using SharedPersistence = Shared.Persistence.DatabaseContext;

namespace Balance.Persistence.DatabaseContext;

public class PersistenceContext : SharedPersistence.PersistenceContext
{
    public PersistenceContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<BalanceEntry> BalanceEntries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BalanceEntry>().Map();
    }
}
