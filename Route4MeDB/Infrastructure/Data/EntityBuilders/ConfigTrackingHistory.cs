using System;
using System.Collections.Generic;
using Route4MeDB.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using static Route4MeDB.ApplicationCore.Enum;
using Route4MeDB.ApplicationCore.Entities.GeocodingAggregate;
using Route4MeDB.ApplicationCore.Entities.RouteAddressAggregate;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route4MeDB.ApplicationCore.Entities.RouteAggregate;

namespace Route4MeDB.Infrastructure.Data
{
    public partial class ConfigureEntity
    {
        public void ConfigureTrackingHistory(EntityTypeBuilder<TrackingHistory> builder)
        {

        }
    }
}
