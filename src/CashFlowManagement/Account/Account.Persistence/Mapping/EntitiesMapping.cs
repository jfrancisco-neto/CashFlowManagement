using Account.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Account.Persistence.Mapping;

internal static class EntitiesMapping
{
    public static void Map(this EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        builder.Property(p => p.Name).HasMaxLength(2048).IsRequired();
        builder.Property(p => p.Login).HasMaxLength(100).IsRequired();
        builder.Property(p => p.Password).HasMaxLength(100).IsRequired();
        builder.Property(p => p.Salt).HasMaxLength(100).IsRequired();
        builder.Property(p => p.CreatedAt).ValueGeneratedOnAdd();
        builder.Property(p => p.UpdatedAt).ValueGeneratedOnAddOrUpdate();
    }

    public static void Map(this EntityTypeBuilder<UserClaim> builder)
    {
        builder.ToTable("UserClaim");
        builder.Property(p => p.Type).HasMaxLength(100).IsRequired();
        builder.Property(p => p.Value).HasMaxLength(100).IsRequired();
        builder.Property(p => p.CreatedAt).ValueGeneratedOnAdd();
        builder.HasOne(p => p.User)
            .WithMany(p =>p.Claims)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}
