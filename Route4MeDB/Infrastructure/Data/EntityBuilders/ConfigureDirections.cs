using System;
using System.Collections.Generic;
using Route4MeDB.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using static Route4MeDB.ApplicationCore.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route4MeDB.ApplicationCore.Entities.RouteAggregate;

namespace Route4MeDB.Infrastructure.Data
{
    public partial class ConfigureEntity
    {
        public void ConfigureDirections(EntityTypeBuilder<Direction> builder)
        {
            //builder.Property(d => d.RouteId)
            //    .HasMaxLength(32)
            //    .IsUnicode(false);
            builder.Property(d => d.Location)
                .IsUnicode(false);

            builder.Property(d => d.Steps)
                .IsUnicode(false);

        }
    }
}
