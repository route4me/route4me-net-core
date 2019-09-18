using Route4MeDB.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Route4MeDB.ApplicationCore.Entities.GeocodingAggregate
{
    [Owned]
    public class GeoPoint : IAggregateRoot
    {
        /// <summary>
        /// Latitude
        /// </summary>
        [Range(-90, 90)]
        [Column("lat")]
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude
        /// </summary>
        [Range(-180, 180)]
        [Column("lng")]
        public double Longitude { get; set; }
    }
}
