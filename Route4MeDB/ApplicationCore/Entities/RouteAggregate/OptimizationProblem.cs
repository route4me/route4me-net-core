using System;
using System.Collections.Generic;
using System.Text;
using Route4MeDB.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using static Route4MeDB.ApplicationCore.Enum;
using Route4MeDB.ApplicationCore.Entities.RouteAddressAggregate;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Route4MeDB.ApplicationCore.Entities.RouteAggregate
{
    public class OptimizationProblem : BaseEntity, IAggregateRoot
    {
        [Key, Column("optimization_problem_db_id", TypeName ="varchar(36)")]
        public string OptimizationProblemDbId { get; set; }

        /// <summary>
        /// Optimization problem ID
        /// </summary>
        [Column("optimization_problem_id", TypeName = "varchar(32)")]
        public string OptimizationProblemId { get; set; }

        [Column("optimization_completed_timestamp")]
        public long? OptimizationCompletedTimestamp { get; set; }

        /// <summary>
        /// An optimization problem state. See <see cref="OptimizationState"/>
        /// </summary>
        [Column("state")]
        public OptimizationState State { get; set; }

        /// <summary>
        /// An array of the user errors
        /// </summary>
        [Column("user_errors", TypeName = "varchar(250)")]
        public string UserErrors { get; set; }

        [NotMapped]
        public string[] UserErrorsArray
        {
            get { return UserErrors == null ? null : JsonConvert.DeserializeObject<string[]>(UserErrors); }
            set { UserErrors = JsonConvert.SerializeObject(value); }
        }

        /// <summary>
        /// If true it means the solution was not returned (it is being computed in the background)
        /// </summary>
        [Column("sent_to_background")]
        public bool IsSentToBackground { get; set; }

        /// <summary>
        /// When the optimization problem was created
        /// </summary>
        [Column("created_timestamp")]
        public Int64? CreatedTimestamp { get; set; }

        /// <summary>
        /// An Unix Timestamp the Optimization Problem was scheduled for
        /// </summary>
        [Column("scheduled_for")]
        public Int64? ScheduledFor { get; set; }

        /// <summary>
        /// Route Parameters. See <see cref="RouteParameters"/>
        /// </summary>
        [Column("parameters")]
        public string Parameters { get; set; }

        [NotMapped]
        public RouteParameters ParametersObj
        {
            get { return Parameters == null ? null : JsonConvert.DeserializeObject<RouteParameters>(Parameters); }
            set { Parameters = JsonConvert.SerializeObject(value); }
        }

        /// <summary>
        /// An array ot the Address type objects. See  <see cref="Address"/>
        /// </summary>
        [Column("addresses")]
        public List<Address> Addresses { get; set; }

        /// <summary>
        /// An array ot the DataObjectRoute type objects. See <see cref="DataObjectRoute"/>
        /// <para>The routes included in the optimization problem</para>
        /// </summary>
        [Column("routes")]
        public List<Route> Routes { get; set; }

        /// <summary>
        /// The links to the GET operations for the optimization problem. See <see cref="Links"/>
        /// </summary>
        [Column("links", TypeName = "varchar(768)")]
        public string Links { get; set; }

        [NotMapped]
        public Dictionary<string, string> LinksObj
        {
            get { return Links == null ? null : JsonConvert.DeserializeObject<Dictionary<string, string>>(Links.ToString()); }
            set { Links = JsonConvert.SerializeObject(value); }
        }

        /// <summary>
        /// A collection of device tracking data with coordinates, speed, and timestamps.
        /// <para>See <see cref="TrackingHistory"/></para>
        /// </summary>
        //[Column("tracking_history")]
        //public string TrackingHistory { get; set; }

        //[NotMapped]
        //public TrackingHistory[] TrackingHistoryArray
        //{
        //    get { return TrackingHistory == null ? null : JsonConvert.DeserializeObject<TrackingHistory[]>(TrackingHistory); }
        //    set { TrackingHistory = JsonConvert.SerializeObject(value); }
        //}

        /// <summary>
        /// Edge by edge turn-by-turn directions. See <see cref="Direction"/>
        /// </summary>
        // TO DO: removing this column causes error - find why.
        [Column("directions")]
        public List<Direction> Directions { get; set; }

        /// <summary>
        /// Edge-wise path to be drawn on the map See <see cref="DirectionPathPoint"/>
        /// </summary>
        //[Column("path", TypeName = "varchar(max)")]
        //public string Path { get; set; }

        //[NotMapped]
        //public DirectionPathPoint[] PathArray
        //{
        //    get { return Path == null ? null : JsonConvert.DeserializeObject<DirectionPathPoint[]>(Path); }
        //    set { Path = JsonConvert.SerializeObject(value); }
        //}

        /// <summary>
        /// Total number of the addresses included in the optimization
        /// </summary>
        [Column("total_addresses")]
        public int? TotalAddresses { get; set; }
    }
}
