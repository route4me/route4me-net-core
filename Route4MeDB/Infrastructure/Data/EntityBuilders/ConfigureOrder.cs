using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route4MeDB.ApplicationCore.Entities.OrderAggregate;

namespace Route4MeDB.Infrastructure.Data
{
    public partial class ConfigureEntity
    {
        public void ConfigureOrder(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.OrderDbId);

            builder.Property(a => a.Address1)
               .HasMaxLength(120)
               .IsRequired();

            builder.Property(a => a.OrderDbId)
               .IsConcurrencyToken(true)
               .IsRowVersion()
               .ValueGeneratedOnAdd();

            builder.Property(a => a.OrderId)
               .IsRowVersion();

            builder.Property(a => a.Address2)
               .HasMaxLength(120);

            builder.Property(a => a.AddressAlias)
               .HasMaxLength(60);

            builder.Property(a => a.AddressCity)
               .HasMaxLength(75);

            builder.Property(a => a.AddressCountryId)
               .HasMaxLength(3);

            builder.Property(a => a.ExtFieldCustomData)
               .HasMaxLength(255);

            builder.ToTable("Order");
        }
    }
}
