using Route4MeDB.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using static Route4MeDB.ApplicationCore.Enum;
using Route4MeDB.ApplicationCore.Entities.RouteAggregate;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Route4MeDB.ApplicationCore.Entities.RouteAddressAggregate
{
    [Owned]
    public class BundledItemResponse : IAggregateRoot
    {
        /// <summary>
        /// Summary cube value of the bundled addresses
        /// </summary>
        [Column("cube")]
        public double Cube { get; set; }

        /// <summary>
        /// Summary revenue value of the bundled addresses
        /// </summary>
        [Column("revenue")]
        public double Revenue { get; set; }

        /// <summary>
        /// Summary pieces value of the bundled addresses
        /// </summary>
        [Column("pieces")]
        public int Pieces { get; set; }

        /// <summary>
        /// Summary weight value of the bundled addresses
        /// </summary>
        [Column("weight")]
        public double Weight { get; set; }

        /// <summary>
        /// Summary cost value of the bundled addresses
        /// </summary>
        [Column("cost")]
        public double Cost { get; set; }

        /// <summary>
        /// Service time of the bundled addresses
        /// </summary>
        [Column("service_time")]
        public int? ServiceTime { get; set; }

        /// <summary>
        /// Time window start of the bundled addresses
        /// </summary>
        [Column("time_window_start")]
        public int? TimeWindowStart { get; set; }

        /// <summary>
        /// Time window emd of the bundled addresses
        /// </summary>
        [Column("time_window_end")]
        public int? TimeWindowEnd { get; set; }

        /// <summary>
        /// TO DO: Adjust description
        /// </summary>
        [Column("custom_data")]
        public object CustomData { get; set; }

        /// <summary>
        /// Array of the IDs of the bundeld addresses.
        /// </summary>
        [Column("addresses_id")]
        public string AddressesId { get; set; }

        [NotMapped]
        public int[] AddressesIdArray
        {
            get { return AddressesId == null ? null : JsonConvert.DeserializeObject<int[]>(AddressesId); }
            set { AddressesId = JsonConvert.SerializeObject(value); }
        }
    }
}
