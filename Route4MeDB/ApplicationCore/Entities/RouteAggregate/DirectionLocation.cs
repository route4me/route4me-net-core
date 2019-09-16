using Route4MeDB.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Route4MeDB.ApplicationCore.Entities.RouteAggregate
{
    [Owned]
    public class DirectionLocation : IAggregateRoot
    {
        public DirectionLocation() {  }

        ///<summary>
        /// Direction name
        ///</summary>
        [Column("name")]
        public string Name { get; set; }

        ///<summary>
        /// Required time for passing the segment (seconds)
        ///</summary>
        [Column("time")]
        public int Time { get; set; }

        ///<summary>
        /// Segment distance
        ///</summary>
        [Column("segment_distance")]
        public double SegmentDistance { get; set; }

        ///<summary>
        /// Start Location
        ///</summary>
        [Column("start_location")]
        public string StartLocation { get; set; }

        ///<summary>
        /// End Location
        ///</summary>
        [Column("end_location")]
        public string EndLocation { get; set; }

        ///<summary>
        /// Directions Error
        ///</summary>
        [Column("directions_error")]
        public string DirectionsError { get; set; }

        ///<summary>
        /// Error Code
        ///</summary>
        [Column("error_code")]
        public int ErrorCode { get; set; }
    }
}
