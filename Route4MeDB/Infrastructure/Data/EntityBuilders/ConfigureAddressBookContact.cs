using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;

namespace Route4MeDB.Infrastructure.Data
{
    public partial class ConfigureEntity
    {
        public void ConfigureAddressBookContact(EntityTypeBuilder<AddressBookContact> builder)
        {
            builder.HasKey(ci => ci.AddressDbId);

            builder.Property(ci => ci.AddressDbId)
               .IsConcurrencyToken(true)
               .IsRowVersion()
               .ValueGeneratedOnAdd()
               .UseSqlServerIdentityColumn();

            builder.Property(ci => ci.AddressId)
               .IsRowVersion();

            builder.Property(a => a.Address1)
               .HasColumnName("address_1")
               .HasMaxLength(120)
               .IsRequired();

            builder.Property(a => a.Address2)
               .HasColumnName("address_2")
               .HasMaxLength(120);

            builder.Property(a => a.AddressAlias)
               .HasColumnName("address_alias")
               .HasMaxLength(60);

            builder.Property(a => a.AddressCity)
               .HasColumnName("address_city")
               .HasMaxLength(75);

            builder.Property(a => a.AddressCountryId)
               .HasMaxLength(3);

            builder.Property(a => a.AddressCustomData)
               .HasMaxLength(255);

            builder.ToTable("AddressBookContacts");
        }
    }
}
