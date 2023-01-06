using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Schedules
{
    /// <summary>
    /// Route schedule preview
    /// </summary>
    [DataContract]
    public class RouteSchedulePreview
    {
        /// <summary>
        ///     Version
        /// </summary>
        [DataMember(Name = "version", EmitDefaultValue = false)]
        public string Version { get; set; }

        /// <summary>
        ///     Dates
        /// </summary>
        [DataMember(Name = "dates", EmitDefaultValue = false)]
        public RouteSchedulePreviewDates[] Dates { get; set; }
    }

    /// <summary>
    /// Route schedule preview dates
    /// </summary>
    [DataContract]
    public class RouteSchedulePreviewDates
    {
        /// <summary>
        ///     Date
        /// </summary>
        [DataMember(Name = "date", EmitDefaultValue = false)]
        public string Date { get; set; }

        /// <summary>
        ///     Scheduled
        /// </summary>
        [DataMember(Name = "scheduled", EmitDefaultValue = false)]
        public bool Scheduled { get; set; }

        /// <summary>
        ///     Created
        /// </summary>
        [DataMember(Name = "created", EmitDefaultValue = false)]
        public bool Created { get; set; }

        /// <summary>
        ///     Date
        /// </summary>
        [DataMember(Name = "excluded", EmitDefaultValue = false)]
        public bool Excluded { get; set; }
    }
}
