using Account.Model;
using Microsoft.EntityFrameworkCore;

namespace Account.Persistence;

public class PersistenceContext : DbContext
{
    public PersistenceContext(DbContextOptions<PersistenceContext> options) : base(options)
    { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        var userEntity = modelBuilder.Entity<User>();

        userEntity.ToTable("User");
        userEntity.Property(p => p.Name).HasMaxLength(2048).IsRequired();
        userEntity.Property(p => p.Login).HasMaxLength(2048).IsRequired();
        userEntity.Property(p => p.Password).HasMaxLength(2048).IsRequired();
        userEntity.Property(p => p.Salt).HasMaxLength(2048).IsRequired();
    }
}
