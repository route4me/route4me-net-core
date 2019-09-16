using Route4MeDB.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Route4MeDB.ApplicationCore.Entities.RouteAggregate
{
    [Owned]
    public class DirectionStep : IAggregateRoot
    {
        public DirectionStep() { }

        ///<summary>
        /// Name (detailed)
        ///</summary>
        [Column("direction")]
        public string Direction { get; set; }

        ///<summary>
        /// Name (brief)
        ///</summary>
        [Column("directions")]
        public string Directions { get; set; }

        ///<summary>
        /// Distance
        ///</summary>
        [Column("distance")]
        public double Distance { get; set; }

        ///<summary>
        /// Distance unit
        ///</summary>
        [Column("distance_unit")]
        public string DistanceUnit { get; set; }

        ///<summary>
        /// Maneuver Type. Available values:
        /// <para>Head,Go Straight,Turn Left,Turn Right,Turn Slight Left,</para>
        /// <para>Turn Slight Right,Turn Sharp Left,Turn Sharp Right,</para>
        /// <para>Roundabout Left,Roundabout Right,Uturn Left,Uturn Right,</para>
        /// <para>Ramp Left,Ramp Right,Fork Left,Fork Right,Keep Left,</para>
        /// <para>Keep Right,Ferry,Ferry Train,Merge,Reached Your Destination</para>
        ///</summary>
        [Column("maneuverType")]
        public string ManeuverType { get; set; }

        ///<summary>
        /// Compass Direction. Available values:
        /// <para> N, S, W, E, NW, NE, SW, SE</para>
        ///</summary>
        [Column("compass_direction")]
        public string CompassDirection { get; set; }

        ///<summary>
        /// Direction step duration(seconds)
        ///</summary>
        [Column("duration_sec")]
        public int DurationSec { get; set; }

        ///<summary>
        /// Maneuver Point. See <see cref="DirectionPathPoint"/>
        ///</summary>
        [Column("maneuverPoint")]
        public string ManeuverPoint { get; set; }

        [NotMapped]
        public DirectionPathPoint ManeuverPointObject
        {
            get { return ManeuverPoint == null ? null : JsonConvert.DeserializeObject<DirectionPathPoint>(ManeuverPoint); }
            set { ManeuverPoint = JsonConvert.SerializeObject(value); }
        }
    }
}
