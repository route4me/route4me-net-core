using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Route4MeDB.ApplicationCore;
using Route4MeDB.ApplicationCore.Entities.RouteAddressAggregate;

namespace Route4MeDB.Infrastructure.Data
{
    public partial class ConfigureEntity
    {
        public void ConfigureAddresses(EntityTypeBuilder<Address> builder)
        {

            builder.HasKey(ci => ci.RouteDestinationDbId);

            //builder.Property(ci => ci.RouteDestinationDbId)
            //   .IsConcurrencyToken(true)
            //   .IsRowVersion()
            //   .ValueGeneratedOnAdd()
            //   .UseSqlServerIdentityColumn();

            //builder
            //    .HasMany(n => n.Notes)
            //    .WithOne(a => a.address);

            //builder
            //    .HasOne(a => a.route)
            //    .WithMany(r => r.Addresses);

            var converter = new EnumToStringConverter<Enum.AddressStopType>();

            builder.Property(c => c.AddressStopType)
                .HasConversion(converter)
                .HasMaxLength(16)
                .IsUnicode(false);

            //builder.Property(a => a.RouteId)
            //    .HasMaxLength(32)
            //    .IsUnicode(false);

            //builder.Property(a => a.RouteDbId)
            //    .HasMaxLength(32)
            //    .IsUnicode(false);

            builder.Property(a => a.OriginalRouteId)
                .HasMaxLength(32)
                .IsUnicode(false);

            //builder.Property(a => a.OptimizationProblemId)
            //    .HasMaxLength(32)
            //    .IsUnicode(false);

            builder.Property(a => a.Group)
                .HasMaxLength(128)
                .IsUnicode(false);

            builder.Property(a => a.CustomerPo)
                .HasMaxLength(128)
                .IsUnicode(false);

            builder.Property(a => a.InvoiceNo)
                .HasMaxLength(128)
                .IsUnicode(false);

            builder.Property(a => a.ReferenceNo)
                .HasMaxLength(128)
                .IsUnicode(false);

            builder.Property(a => a.OrderNo)
                .HasMaxLength(128)
                .IsUnicode(false);

            builder.Property(a => a.Email)
                .HasMaxLength(128)
                .IsUnicode(false);

            builder.Property(a => a.Phone)
                .HasMaxLength(128)
                .IsUnicode(false);

            builder.Property(a => a.ChannelName)
                .HasMaxLength(250)
                .IsUnicode(false);

            builder.Property(a => a.CustomFieldsStrJson)
                .HasMaxLength(250);

            builder.Property(a => a.CustomFieldsConfigStrJson)
                .HasMaxLength(250);

            builder.Property(a => a.TrackingNumber)
                .HasMaxLength(32)
                .IsUnicode(false);

            //builder.HasData(
            //    new Address()
            //    {
            //        RouteDestinationDbId = -1,
            //        RouteDestinationId = 363044949,
            //        AddressString = "Huston",
            //        Alias = "Huston City",
            //        Latitude = 40,
            //        Longitude = -120,
            //        AddressStopType = Enum.AddressStopType.Delivery
            //    }
            //    );

        }
    }
}
