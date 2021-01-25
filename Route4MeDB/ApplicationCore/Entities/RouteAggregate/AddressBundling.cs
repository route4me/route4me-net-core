using Route4MeDB.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using static Route4MeDB.ApplicationCore.Enum;
using System.ComponentModel;

namespace Route4MeDB.ApplicationCore.Entities.RouteAggregate
{
    [Owned]
    public class AddressBundling : IAggregateRoot
    {
        /// <summary>
        /// Address bundling mode
        /// </summary>
        [Column("mode")]
        [DefaultValue(AddressBundlingMode.Address)]
        [Range((int)AddressBundlingMode.Address, (int)AddressBundlingMode.Coordinates)]
        public AddressBundlingMode Mode { get; set; }

        /// <summary>
        /// Address bundling mode parameters:
        /// <para>If Mode=3, contains an array of the field names of the Address object</para>
        /// <para>If Mode=4, contains an array of the custom fields of the Address object</para>
        /// </summary>
        [Column("mode_params")]
        public string[] ModeParams { get; set; }

        /// <summary>
        /// Address bundling merge mode
        /// </summary>
        [Column("merge_mode")]
        [DefaultValue(AddressBundlingMergeMode.KeepAsSeparateDestinations)]
        [Range((int)AddressBundlingMergeMode.KeepAsSeparateDestinations, (int)AddressBundlingMergeMode.MergeIntoSingleDestination)]
        public AddressBundlingMergeMode MergeMode { get; set; }

        /// <summary>
        /// Service time rules of the address bundling (<seealso cref="ServiceTimeRulesClass">)
        /// </summary>
        [Column("service_time_rules")]
        public ServiceTimeRulesClass ServiceTimeRules { get; set; }
    }
}
