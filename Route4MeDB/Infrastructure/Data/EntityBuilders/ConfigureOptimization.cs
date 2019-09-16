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
        public void ConfigureOptimizations(EntityTypeBuilder<OptimizationProblem> builder)
        {
            builder.HasKey(o => o.OptimizationProblemDbId);

            builder.Property(o => o.OptimizationProblemDbId)
               .IsUnicode(false)
               .ValueGeneratedOnAdd();

            var converter = new EnumToNumberConverter<Enum.OptimizationState, int>();

            builder.Property(o => o.State)
                .HasConversion(converter);

            //builder.Property(o => o.CreatedTimestamp)
            //    .HasConversion<uint>();

            builder
                .HasMany(o => o.Routes)
                .WithOne(r => r.optimizationProblem)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(o => o.Addresses)
                .WithOne(a => a.optimizationProblem)
                .OnDelete(DeleteBehavior.Cascade);

            // TO DO: Adjust later
            //builder
            //    .HasMany(o => o.Directions)
            //    .WithOne(d => d.optimizationProblem)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
