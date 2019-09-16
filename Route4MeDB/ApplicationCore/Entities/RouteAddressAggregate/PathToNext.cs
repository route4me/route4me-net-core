using Route4MeDB.ApplicationCore.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Route4MeDB.ApplicationCore.Entities.RouteAddressAggregate
{
    public class PathToNext : IAggregateRoot
    {
        [Key, Column("path_to_next_id")]
        public long PathToNextId { get; set; }

        [Column("route_id", TypeName = "varchar(32)")]
        public string RouteId { get; set; }

        [Column("route_destination_id")]
        public int? RouteDestinationId { get; set; }

        [Column("sequence_no")]
        public int? SequenceNo { get; set; }

        ///<summary>
        /// Latitude
        ///</summary>
        [Column("lat")]
        public double Lat { get; set; }

        ///<summary>
        /// Longitude
        ///</summary>
        [Column("lng")]
        public double Lng { get; set; }

        public int RouteDestinationDbId { get; set; }
        //[ForeignKey("FK_PathToNext_Addresses_RouteDestinationDbId")]
        public Address address { get; set; }
    }
}
