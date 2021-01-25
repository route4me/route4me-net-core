using Route4MeDB.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Route4MeDB.ApplicationCore.Enum;

namespace Route4MeDB.ApplicationCore.Entities.RouteAggregate
{
    [Owned]
    public class ServiceTimeRulesClass : IAggregateRoot
    {
        /// <summary>
        /// Mode of a first item of the bundled addresses.
        /// </summary>
        [Column("first_item_mode")]
        [DefaultValue(AddressBundlingFirstItemMode.KeepOriginal)]
        [Range((int)AddressBundlingFirstItemMode.KeepOriginal, (int)AddressBundlingFirstItemMode.CustomTime)]
        public AddressBundlingFirstItemMode FirstItemMode { get; set; }

        /// <summary>
        /// First item mode parameters.
        /// If FirstItemMode=AddressBundlingFirstItemMode.CustomTime, contains custom service time in seconds.
        /// </summary>
        [Column("first_item_mode_params")]
        public int[] FirstItemModeParams { get; set; }

        /// <summary>
        /// Mode of the non-first items of the bundled addresses.
        /// </summary>
        [Column("additional_items_mode")]
        [DefaultValue(AddressBundlingAdditionalItemsMode.KeepOriginal)]
        [Range((int)AddressBundlingAdditionalItemsMode.KeepOriginal, (int)AddressBundlingAdditionalItemsMode.InheritFromPrimary)]
        public AddressBundlingAdditionalItemsMode AdditionalItemsMode { get; set; }

        /// <summary>
        /// Additional items mode parameters:
        /// <para>if AdditionalItemsMode=AddressBundlingAdditionalItemsMode.CustomTime, contains an array of the custom service times</para>
        /// </summary>
        [Column("additional_items_mode_params")]
        public int[] AdditionalItemsModeParams { get; set; }
    }
}
