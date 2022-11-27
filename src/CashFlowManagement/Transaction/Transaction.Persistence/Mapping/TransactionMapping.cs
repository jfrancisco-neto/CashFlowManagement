using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transaction.Domain.Model;

namespace Transaction.Persistence.Mapping;

public static class TransactionMapping
{
    public static void Map(this EntityTypeBuilder<TransactionEntry> builder)
    {
        builder.ToTable("Transaction");
    }
}
