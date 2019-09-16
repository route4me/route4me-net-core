using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Route4MeDB.ApplicationCore;
using Route4MeDB.ApplicationCore.Entities.RouteAddressAggregate;

namespace Route4MeDB.Infrastructure.Data
{
    public partial class ConfigureEntity
    {
        public void ConfigureAddressNotes(EntityTypeBuilder<AddressNote> builder)
        {
            builder.HasKey(ci => ci.NoteDbId);

            builder.Property(ci => ci.NoteDbId)
               .IsConcurrencyToken(true)
               .IsRowVersion()
               .ValueGeneratedOnAdd()
               .UseSqlServerIdentityColumn();

            builder
                .HasOne(n => n.address)
                .WithMany(a => a.Notes);
            //.HasForeignKey(c => c.RouteDestinationId);

            //builder
            //    .HasOne(n => n.route)
            //    .WithMany(r => r.Notes);


            //builder.Property(n => n.RouteId)
            //    .HasMaxLength(32)
            //    .IsUnicode(false);

            builder.Property(n => n.UploadId)
                .HasMaxLength(32)
                .IsUnicode(false);

            var statusUpdateTypeConverter = new EnumToStringConverter<Enum.StatusUpdateType>();

            builder.Property(n => n.StatusUpdateType)
                .HasConversion(statusUpdateTypeConverter)
                .HasMaxLength(32)
                .IsUnicode(false);

            var uploadTypeConverter = new EnumToStringConverter<Enum.UploadType>();

            builder.Property(n => n.UploadType)
                .HasConversion(uploadTypeConverter)
                .HasMaxLength(32)
                .IsUnicode(false);

            builder.Property(n => n.UploadUrl)
                .HasMaxLength(250)
                .IsUnicode(false);

            builder.Property(n => n.UploadExtension)
                .HasMaxLength(16)
                .IsUnicode(false);

            var deviceTypeConverter = new EnumToStringConverter<Enum.DeviceType>();

            builder.Property(n => n.DeviceType)
                .HasConversion(deviceTypeConverter)
                .HasMaxLength(32)
                .IsUnicode(false);

            //builder.HasData(
            //    new AddressNote()
            //    {
            //        NoteDbId = -1,
            //        Contents = "Note example",
            //        DeviceType = Enum.DeviceType.Web,
            //        UploadType = Enum.UploadType.AnyFile,
            //        StatusUpdateType = Enum.StatusUpdateType.DropOff,
            //        IsInactive = true,
            //        Latitude = 95.85441,
            //        Longitude = -250.65418,
            //        UploadExtension = "jpg",
            //        NoteId = 111111,
            //        RouteDestinationId = 363044949
            //        //RouteId = "76DF97A0AB51EAC8B90D75700A3BBFBF"
            //    }
            //    );
        }
    }
}
