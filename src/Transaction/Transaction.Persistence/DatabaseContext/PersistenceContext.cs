using Microsoft.EntityFrameworkCore;
using Transaction.Domain.Model;
using Transaction.Persistence.Mapping;
using SharedPersistence = Shared.Persistence;

namespace Transaction.Persistence.DatabaseContext;

public class PersistenceContext : SharedPersistence.DatabaseContext.PersistenceContext
{
    public PersistenceContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TransactionEntry>().Map();
    }

    public DbSet<TransactionEntry> Transactions { get; set; }
}
