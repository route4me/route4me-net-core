using Newtonsoft.Json;
using Route4MeDB.ApplicationCore.Entities.RouteAddressAggregate;
using Route4MeDB.ApplicationCore.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Route4MeDB.ApplicationCore.Entities.GeocodingAggregate
{
    public class Geocoding : IAggregateRoot
    {
        [Key, Column("geocoding_db_id")]
        public int GeocodingDbId { get; set; }

        /// <summary>
        /// A unique identifier for the geocoding
        /// </summary>
        [Column("key")]
        public string Key { get; set; }

        /// <summary>
        /// Specific description of the geocoding result
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Boundary box
        /// </summary>
        [Column("bbox")]
        public string Bbox { get; set; }

        [NotMapped]
        public double[] BboxArray
        {
            get { return Bbox == null ? null : JsonConvert.DeserializeObject<double[]>(Bbox); }
            set { Bbox = JsonConvert.SerializeObject(value); }
        }

        /// <summary>
        /// The latitude of the geocoded address
        /// </summary>
        [Column("lat")]
        public double Latitude { get; set; }

        /// <summary>
        /// The longitude of the geocoded address
        /// </summary>
        [Column("lng")]
        public double Longtude { get; set; }

        /// <summary>
        /// Confidance level in the address geocoding:
        /// <para>high, medium, low</para>
        /// </summary>
        [Column("confidence")]
        public string Confidence { get; set; }

        /// <summary>
        /// The postal code of the geocoded address
        /// </summary>
        [Column("postalCode")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Country region
        /// </summary>
        [Column("countryRegion")]
        public string CountryRegion { get; set; }

        /// <summary>
        /// The address curbside coordinates
        /// </summary>
        [Column("curbside_coordinates")]
        public string CurbsideCoordinates { get; set; }

        [NotMapped]
        public GeoPoint CurbsideCoordinatesObj
        {
            get { return CurbsideCoordinates == null ? null : JsonConvert.DeserializeObject<GeoPoint>(CurbsideCoordinates); }
            set { CurbsideCoordinates = JsonConvert.SerializeObject(value); }
        }

        /// <summary>
        /// The address without number
        /// </summary>
        [Column("address_without_number")]
        public string AaddressWithoutNumber { get; set; }

        /// <summary>
        /// The place ID
        /// </summary>
        [Column("place_id")]
        public string PlaceID { get; set; }

        public int RouteDestinationDbId { get; set; }
        [ForeignKey("RouteDestinationDbId")]
        public Address Route { get; set; }
    }
}
