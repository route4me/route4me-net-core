using Route4MeDB.ApplicationCore.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Route4MeDB.ApplicationCore.Entities.RouteAggregate
{
    public class Path : IAggregateRoot
    {
        [Key, Column("path_id")]
        public long PathId { get; set; }

        [Column("route_id", TypeName = "varchar(32)")]
        public string RouteId { get; set; }

        //[Column("route_destination_id")]
        //public int? RouteDestinationId { get; set; }

        //[Column("sequence_no")]
        //public int? SequenceNo { get; set; }

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

        public string RouteDbId { get; set; }
        [ForeignKey("RouteDbId")]
        public Route Route { get; set; }
        //public string RouteDbId { get; set; }
        //[ForeignKey("FK_Path_Routes_RouteDbId")]
        //public Route Route { get; set; }
    }
}
