using GadgetLand.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GadgetLand.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.FullName).HasMaxLength(256).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(256).IsRequired();
        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.Mobile).HasMaxLength(256);
        builder.Property(x => x.PostalCode).HasMaxLength(256);
        builder.Property(x => x.Address).HasMaxLength(512);

        builder.HasOne(x => x.Role).WithMany(x => x.Users).HasForeignKey(x => x.RoleId);
        builder.HasMany(x => x.Reviews).WithOne(x => x.User).HasForeignKey(x => x.UserId);
        builder.HasOne(x => x.City).WithMany(x => x.Users).HasForeignKey(x => x.CityId);
    }
}
