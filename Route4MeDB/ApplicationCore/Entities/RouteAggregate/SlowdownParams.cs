using Route4MeDB.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Route4MeDB.ApplicationCore.Entities.RouteAggregate
{
    [Owned]
    public class SlowdownParams : IAggregateRoot
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="serviceTime">Service time slowdown (percent)</param>
        /// <param name="travelTime">Travel time slowdown (percent)</param>
        public SlowdownParams(int? serviceTime, int? travelTime)
        {
            ServiceTime = serviceTime;
            TravelTime = travelTime;
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        public SlowdownParams()
        {

        }

        /// <summary>
        /// Service time slowdowon (percent)
        /// </summary>
        [Column("service_time")]
        public int? ServiceTime { get; set; }

        /// <summary>
        /// Travel time slowdowon (percent)
        /// </summary>
        [Column("travel_time")]
        public int? TravelTime { get; set; }
    }
}
