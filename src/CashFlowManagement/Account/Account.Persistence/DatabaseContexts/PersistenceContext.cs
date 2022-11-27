using Account.Domain.Model;
using Account.Persistence.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Account.Persistence.DatabaseContext;

public class PersistenceContext : DbContext
{
    public PersistenceContext(DbContextOptions<PersistenceContext> options)
        : base(options)
    { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>().Map();
        modelBuilder.Entity<UserClaim>().Map();
    }
}
