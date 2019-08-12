using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;
using Route4MeDB.ApplicationCore.Entities.OrderAggregate;

namespace Route4MeDB.Infrastructure.Data
{
    public class Route4MeDbContext : DbContext
    {
        public Route4MeDbContext(DbContextOptions<Route4MeDbContext> options) : base(options)
        {
        }

        public DbSet<AddressBookContact> AddressBookContacts { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AddressBookContact>(ConfigureAddressBookContact);

            builder.Entity<Order>(ConfigureOrder);
        }

        private void ConfigureAddressBookContact(EntityTypeBuilder<AddressBookContact> builder)
        {
            builder.ToTable("AddressBookContacts");

            builder.HasKey(ci => ci.AddressId);

            builder.Property(ci => ci.AddressId)
               .ForSqlServerUseSequenceHiLo("address_book_contact_hilo")
               .IsRequired();

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
        }

        private void ConfigureOrder(EntityTypeBuilder<Order> builder)
        {
            builder.Property(a => a.Address1)
               .HasMaxLength(120)
               .IsRequired();

            builder.Property(a => a.Address2)
               .HasMaxLength(120);

            builder.Property(a => a.AddressAlias)
               .HasMaxLength(60);

            builder.Property(a => a.AddressCity)
               .HasMaxLength(75);

            builder.Property(a => a.AddressCountryId)
               .HasMaxLength(3);

            builder.Property(a => a.EXT_FIELD_custom_data)
               .HasMaxLength(255);
        }
    }
}
