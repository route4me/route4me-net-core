using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;

namespace Route4MeSDKLibrary.DataTypes.V5.Orders
{
    /// <summary>
    /// Filters
    /// </summary>
    [DataContract]
    public class FiltersParamRequestBody
    {
        /// <summary>
        ///     Order IDs
        /// </summary>
        [DataMember(Name = "order_ids", EmitDefaultValue = false )]
        public string[] OrderIds { get; set; }

        /// <summary>
        ///     Excluded IDs
        /// </summary>
        [DataMember(Name = "excluded_ids", EmitDefaultValue = false)]
        public string[] ExcludedIds { get; set; }

        /// <summary>
        ///     Tracking numbers
        /// </summary>
        [DataMember(Name = "tracking_numbers", EmitDefaultValue = false)]
        public string[] TrackingNumbers { get; set; }

        /// <summary>
        ///     Only geocoded
        /// </summary>
        [DataMember(Name = "only_geocoded", EmitDefaultValue = false)]
        public bool? OnlyGeocoded { get; set; }

        /// <summary>
        ///     Updated timestamp
        /// </summary>
        [DataMember(Name = "updated_timestamp", EmitDefaultValue = false)]
        public JToken UpdatedTimestamp { get; set; }

        /// <summary>
        ///     Created timestamp
        /// </summary>
        [DataMember(Name = "created_timestamp", EmitDefaultValue = false)]
        public JToken CreatedTimestamp { get; set; }

        /// <summary>
        ///     Scheduled for
        /// </summary>
        [DataMember(Name = "scheduled_for", EmitDefaultValue = false)]
        public JToken ScheduledFor { get; set; }

        /// <summary>
        ///     Only unscheduled 
        /// </summary>
        [DataMember(Name = "only_unscheduled", EmitDefaultValue = false)]
        public bool OnlyUnScheduled { get; set; }

        /// <summary>
        ///     Day added
        /// </summary>
        [DataMember(Name = "day_added", EmitDefaultValue = false)]
        public JToken DayAdded { get; set; }

        /// <summary>
        ///     Sorted on
        /// </summary>
        [DataMember(Name = "sorted_on", EmitDefaultValue = false)]
        public JToken SortedOn { get; set; }

        /// <summary>
        ///     Address stop types
        /// </summary>
        [DataMember(Name = "address_stop_types", EmitDefaultValue = false)]
        public string[] AddressStopTypes { get; set; }

        /// <summary>
        ///     Last statuses
        /// </summary>
        [DataMember(Name = "last_statuses", EmitDefaultValue = false)]
        public int[] LastStatuses { get; set; }

        /// <summary>
        ///     Territory Ids
        /// </summary>
        [DataMember(Name = "territory_ids", EmitDefaultValue = false)]
        public int[] TerritoryIds { get; set; }

        /// <summary>
        ///     Done day
        /// </summary>
        [DataMember(Name = "done_day", EmitDefaultValue = false)]
        public string DoneDay { get; set; }

        /// <summary>
        ///     Processing Day
        /// </summary>
        [DataMember(Name = "possession_day", EmitDefaultValue = false)]
        public string ProcessingDay { get; set; }

        /// <summary>
        ///     Groups
        /// </summary>
        [DataMember(Name = "groups", EmitDefaultValue = false)]
        public string[] Groups { get; set; }

        /// <summary>
        ///     Display (see <see cref="DisplayValues"/>). Default is <seealso cref="DisplayValues.All"/>
        /// </summary>
        [DataMember(Name = "display", EmitDefaultValue = false)]
        public string Display { get; set; }
    }
}