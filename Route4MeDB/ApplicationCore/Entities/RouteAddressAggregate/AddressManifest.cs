using System;
using System.Collections.Generic;
using System.Text;
using Route4MeDB.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using static Route4MeDB.ApplicationCore.Enum;
using Route4MeDB.ApplicationCore.Entities.RouteAggregate;
using System.ComponentModel.DataAnnotations;
namespace Route4MeDB.ApplicationCore.Entities.RouteAddressAggregate
{
    [Owned]
    public class AddressManifest : IAggregateRoot
    {
        [Column("running_service_time")]
        public int? RunningServiceTime { get; set; }

        /// <summary>
        /// How much time is spent driving from the start in seconds.
        /// </summary>
        [Column("running_travel_time")]
        public int? RunningTravelTime { get; set; }

        /// <summary>
        /// Running wait time.
        /// </summary>
        [Column("running_wait_time")]
        public int? RunningWaitTime { get; set; }

        /// <summary>
        /// Distance traversed before reaching this address.
        /// </summary>
        [Column("running_distance")]
        public double? RunningDistance { get; set; }

        /// <summary>
        /// Expected fuel consumption from the start.
        /// </summary>
        [Column("fuel_from_start")]
        public double? FuelFromStart { get; set; }

        /// <summary>
        /// Expected fuel cost from start.
        /// </summary>
        [Column("fuel_cost_from_start")]
        public double? FuelCostFromStart { get; set; }

        /// <summary>
        /// Projected arrival time UTC unixtime.
        /// </summary>
        [Column("projected_arrival_time_ts")]
        public int? ProjectedArrivalTimeTs { get; set; }

        /// <summary>
        /// Estimated departure time UTC unixtime.
        /// </summary>
        [Column("projected_departure_time_ts")]
        public int? ProjectedDepartureTimeTs { get; set; }

        /// <summary>
        /// Time when the address was marked as visited UTC unixtime. 
        /// This is actually equal to timestamp_last_visited most of the time.
        /// </summary>
        [Column("actual_arrival_time_ts")]
        public int? ActualArrivalTimeTs { get; set; }

        /// <summary>
        /// Time when the address was mared as departed UTC. 
        /// This is actually equal to timestamp_last_departed most of the time.
        /// </summary>
        [Column("actual_departure_time_ts")]
        public int? ActualDepartureTimeTs { get; set; }

        /// <summary>
        /// Estimated arrival time based on the current route progress, 
        /// i.e. based on the last known actual_arrival_time.
        /// </summary>
        [Column("estimated_arrival_time_ts")]
        public int? EstimatedArrivalTimeTs { get; set; }

        /// <summary>
        /// Estimated departure time based on the current route progress.
        /// </summary>
        [Column("estimated_departure_time_ts")]
        public int? EstimatedDepartureTimeTs { get; set; }

        /// <summary>
        /// This is the difference between the originally projected arrival time and Actual Arrival Time.
        /// </summary>
        [Column("time_impact")]
        public int? TimeImpact { get; set; }
    }
}
