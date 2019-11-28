using Route4MeDB.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Route4MeDB.ApplicationCore.Entities.RouteAggregate
{
    [Owned]
    public class TrackingHistory : IAggregateRoot
    {
        [Key, Column("tracking_history_id")]
        public long TrackingHistoryId { get; set; }

        /// <summary>
        /// Speed at the time of the location transaction event.
        /// </summary>
        [Column("s")]
        public string Speed { get; set; }

        /// <summary>
        /// Latitude at the time of the location transaction event.
        /// </summary>
        [Column("lt")]
        public string Latitude { get; set; }

        /// <summary>
        /// MemberID.
        /// </summary>
        [Column("m")]
        public int? MemberId { get; set; }

        /// <summary>
        /// Longitude at the time of the location transaction event.
        /// </summary>
        [Column("lg")]
        public string Longitude { get; set; }

        /// <summary>
        /// Direction/heading at the time of the location transaction event.
        /// </summary>
        [Column("d")]
        public double? Direction { get; set; }

        /// <summary>
        /// The original timestamp in unix timestamp format at the moment location transaction event.
        /// </summary>
        [Column("ts")]
        public string TimeStamp { get; set; }

        /// <summary>
        /// The original timestamp in a human readable timestamp format at the moment location transaction event.
        /// </summary>
        [Column("ts_friendly", TypeName = "varchar(24)")]
        public string TimeStampFriendly { get; set; }

        public string RouteDbId { get; set; }
        [ForeignKey("RouteDbId")]
        public Route Route { get; set; }
    }
}
