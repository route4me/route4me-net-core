using Route4MeDB.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Route4MeDB.ApplicationCore.Entities.RouteAggregate
{
    [Owned]
    public class RouteAdvancedConstraints : IAggregateRoot
    {
        /// <summary>
        /// Maximum cargo volume per route
        /// </summary>
        [Column("max_cargo_volume")]
        public double? MaximumCargoVolume { get; set; }

        /// <summary>
        /// Vehicle capacity.
        /// <para>How much total cargo can be transported per route (units, e.g. cubic meters)</para>
        /// </summary>
        [Column("max_capacity")]
        public int? MaximumCapacity { get; set; }

        /// <summary>
        /// Legacy feature which permits a user to request an example number of optimized routes.
        /// </summary>
        [Column("members_count")]
        public int? MembersCount { get; set; }

        /// <summary>
        /// An array of the available time windows (e.g. [ [25200, 75000 ] )
        /// </summary>
        [Column("available_time_windows")]
        public List<int[]> AvailableTimeWindows { get; set; }

        /// <summary>
        /// The driver tags specified in a team member's custom data.
        /// (e.g. "driver skills": 
        /// ["Class A CDL", "Class B CDL", "Forklift", "Skid Steer Loader", "Independent Contractor"]
        /// </summary>
        [Column("tags")]
        public string[] Tags { get; set; }

        /// <summary>
        /// An array of the skilled driver IDs.
        /// </summary>
        [Column("route4me_members_id")]
        public int[] Route4meMembersId { get; set; }
    }
}
