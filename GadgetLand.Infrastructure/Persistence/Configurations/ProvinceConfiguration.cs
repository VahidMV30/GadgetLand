using GadgetLand.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GadgetLand.Infrastructure.Persistence.Configurations;

public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(256).IsRequired();

        builder.HasMany(x => x.Cities).WithOne(x => x.Province).HasForeignKey(x => x.ProvinceId);
    }
}
