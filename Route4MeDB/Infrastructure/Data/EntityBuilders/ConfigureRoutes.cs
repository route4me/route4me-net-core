using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Route4MeDB.ApplicationCore;
using Route4MeDB.ApplicationCore.Entities.RouteAddressAggregate;
using Route4MeDB.ApplicationCore.Entities.RouteAggregate;

namespace Route4MeDB.Infrastructure.Data
{
    public partial class ConfigureEntity
    {
        public void ConfigureRoutes(EntityTypeBuilder<Route> builder)
        {
            builder.HasKey(r => r.RouteDbId);

            //builder.Property(r => r.RouteDbId)
            //   .HasMaxLength(32)
            //   .ValueGeneratedOnAdd();

            //builder.Property(r => r.RouteId)
            //    .HasMaxLength(32)
            //    .IsUnicode(false);

            builder.Property(r => r.MemberEmail)
                .HasMaxLength(128)
                .IsUnicode(false);

            builder.Property(r => r.MemberFirstName)
                .HasMaxLength(64);

            builder.Property(r => r.MemberLastName)
                .HasMaxLength(64);

            builder.Property(r => r.MemberPicture)
                .HasMaxLength(250)
                .IsUnicode(false);

            builder.Property(r => r.MemberTrackingSubheadline)
                .HasMaxLength(250)
                .IsUnicode(false);

            builder.Property(r => r.VehicleAlias)
                .HasMaxLength(250)
                .IsUnicode(false);

            builder.Property(r => r.DriverAlias)
                .HasMaxLength(250)
                .IsUnicode(false);

            var converter = new EnumToStringConverter<Enum.TerritoryType>();

            builder.Property(r => r.GeofencePolygonType)
                .HasConversion(converter)
                .HasMaxLength(16)
                .IsUnicode(false);

            //builder
            //    .HasMany(r => r.Notes)
            //    .WithOne(a => a.route);

            //builder
            //    .HasMany(r => r.Directions)
            //    .WithOne(d => d.route);

            //builder.
            //    HasMany(r => r.Addresses)
            //    .WithOne(a => a.route);

            //builder
            //    .HasData(
            //        new Route()
            //        {
            //            RouteDbId = "1111111111111111111111111111111",
            //            RouteId = "76DF97A0AB51EAC8B90D75700A3BBFBF",
            //            ParametersObject = new RouteParameters()
            //            {
            //                AlgorithmType = Enum.AlgorithmType.TSP,
            //                DeviceType = Enum.DeviceType.Web,
            //                DistanceUnit = "mi",
            //                Optimize = "Distance",
            //                RouteName = "Route Name 080919"
            //            }
            //        }
            //    );
        }
    }
}
