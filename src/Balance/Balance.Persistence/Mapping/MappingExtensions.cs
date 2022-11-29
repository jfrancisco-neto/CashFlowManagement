using Balance.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balance.Persistence.Mapping;

public static class MappingExtensions
{
    public static void Map(this EntityTypeBuilder<BalanceEntry> builder)
    {
        builder.ToTable("Balance");

        builder.HasIndex(p => p.Date).IsUnique();
    }
}