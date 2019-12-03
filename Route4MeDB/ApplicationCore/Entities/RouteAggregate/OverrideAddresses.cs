using Route4MeDB.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Route4MeDB.ApplicationCore.Entities.RouteAggregate
{
    [Owned]
    public class OverrideAddresses : IAggregateRoot
    {
        /// <summary>
        /// The service time specified or all the addresses in the route.
        /// </summary>
        [Column("time")]
        public long? Time { get; set; }
    }
}
