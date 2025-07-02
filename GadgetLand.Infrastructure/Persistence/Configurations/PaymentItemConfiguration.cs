using GadgetLand.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GadgetLand.Infrastructure.Persistence.Configurations;

public class PaymentItemConfiguration : IEntityTypeConfiguration<PaymentItem>
{
    public void Configure(EntityTypeBuilder<PaymentItem> builder)
    {
        builder.HasOne(x => x.Payment).WithMany(x => x.PaymentItems).HasForeignKey(x => x.PaymentId);
        builder.HasOne(x => x.Product).WithMany(x => x.PaymentItems).HasForeignKey(x => x.ProductId);
    }
}
