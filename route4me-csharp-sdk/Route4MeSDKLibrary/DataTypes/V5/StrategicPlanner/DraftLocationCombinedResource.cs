using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Draft location combined resource with formatted display values
    /// </summary>
    [DataContract]
    public sealed class DraftLocationCombinedResource
    {
        /// <summary>
        /// Location ID (hex32 format)
        /// </summary>
        [DataMember(Name = "location_id", EmitDefaultValue = false)]
        public string LocationId { get; set; }

        /// <summary>
        /// Strategic optimization ID
        /// </summary>
        [DataMember(Name = "strategic_optimization_id", EmitDefaultValue = false)]
        public string StrategicOptimizationId { get; set; }

        /// <summary>
        /// Contact ID
        /// </summary>
        [DataMember(Name = "contact_id", EmitDefaultValue = false)]
        public int? ContactId { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        [DataMember(Name = "address", EmitDefaultValue = false)]
        public string Address { get; set; }

        /// <summary>
        /// Alias
        /// </summary>
        [DataMember(Name = "alias", EmitDefaultValue = false)]
        public string Alias { get; set; }

        /// <summary>
        /// Service time (formatted string)
        /// </summary>
        [DataMember(Name = "time", EmitDefaultValue = false)]
        public string Time { get; set; }

        /// <summary>
        /// Days (formatted string)
        /// </summary>
        [DataMember(Name = "days", EmitDefaultValue = false)]
        public string Days { get; set; }

        /// <summary>
        /// Starting cycle
        /// </summary>
        [DataMember(Name = "starting_cycle", EmitDefaultValue = false)]
        public int? StartingCycle { get; set; }

        /// <summary>
        /// Days between cycle
        /// </summary>
        [DataMember(Name = "days_between_cycle", EmitDefaultValue = false)]
        public int? DaysBetweenCycle { get; set; }

        /// <summary>
        /// Days range
        /// </summary>
        [DataMember(Name = "days_range", EmitDefaultValue = false)]
        public int? DaysRange { get; set; }

        /// <summary>
        /// Weight
        /// </summary>
        [DataMember(Name = "weight", EmitDefaultValue = false)]
        public double? Weight { get; set; }

        /// <summary>
        /// Weight unit
        /// </summary>
        [DataMember(Name = "weight_unit", EmitDefaultValue = false)]
        public string WeightUnit { get; set; }

        /// <summary>
        /// Cost
        /// </summary>
        [DataMember(Name = "cost", EmitDefaultValue = false)]
        public double? Cost { get; set; }

        /// <summary>
        /// Cost unit
        /// </summary>
        [DataMember(Name = "cost_unit", EmitDefaultValue = false)]
        public string CostUnit { get; set; }

        /// <summary>
        /// Revenue
        /// </summary>
        [DataMember(Name = "revenue", EmitDefaultValue = false)]
        public double? Revenue { get; set; }

        /// <summary>
        /// Revenue unit
        /// </summary>
        [DataMember(Name = "revenue_unit", EmitDefaultValue = false)]
        public string RevenueUnit { get; set; }

        /// <summary>
        /// Cube
        /// </summary>
        [DataMember(Name = "cube", EmitDefaultValue = false)]
        public double? Cube { get; set; }

        /// <summary>
        /// Cube unit
        /// </summary>
        [DataMember(Name = "cube_unit", EmitDefaultValue = false)]
        public string CubeUnit { get; set; }

        /// <summary>
        /// Pieces
        /// </summary>
        [DataMember(Name = "pieces", EmitDefaultValue = false)]
        public int? Pieces { get; set; }

        /// <summary>
        /// Time window (formatted)
        /// </summary>
        [DataMember(Name = "time_window", EmitDefaultValue = false)]
        public int? TimeWindow { get; set; }

        /// <summary>
        /// Latitude
        /// </summary>
        [DataMember(Name = "lat", EmitDefaultValue = false)]
        public double? Lat { get; set; }

        /// <summary>
        /// Longitude
        /// </summary>
        [DataMember(Name = "lng", EmitDefaultValue = false)]
        public double? Lng { get; set; }

        /// <summary>
        /// Is depot (formatted string)
        /// </summary>
        [DataMember(Name = "is_depot", EmitDefaultValue = false)]
        public string IsDepot { get; set; }
    }
}
