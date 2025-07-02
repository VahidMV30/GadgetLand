using GadgetLand.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GadgetLand.Infrastructure.Persistence.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.Property(x => x.Authority).HasMaxLength(256).IsRequired();

        builder.HasMany(x => x.PaymentItems).WithOne(x => x.Payment).HasForeignKey(x => x.PaymentId);
    }
}
