using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Draft visit resource
    /// </summary>
    [DataContract]
    public sealed class DraftVisitResource
    {
        /// <summary>
        /// Visit draft ID (hex32 format)
        /// </summary>
        [DataMember(Name = "visit_draft_id", EmitDefaultValue = false)]
        public string VisitDraftId { get; set; }

        /// <summary>
        /// Scenario ID
        /// </summary>
        [DataMember(Name = "scenario_id", EmitDefaultValue = false)]
        public string ScenarioId { get; set; }

        /// <summary>
        /// Customer name
        /// </summary>
        [DataMember(Name = "customer_name", EmitDefaultValue = false)]
        public string CustomerName { get; set; }

        /// <summary>
        /// Customer ID
        /// </summary>
        [DataMember(Name = "customer_id", EmitDefaultValue = false)]
        public string CustomerId { get; set; }

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
        /// Number of destinations
        /// </summary>
        [DataMember(Name = "destinations", EmitDefaultValue = false)]
        public int? Destinations { get; set; }

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
        /// Average days between destinations
        /// </summary>
        [DataMember(Name = "avg_days_between_destinations", EmitDefaultValue = false)]
        public double? AvgDaysBetweenDestinations { get; set; }

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
        /// Route draft IDs
        /// </summary>
        [DataMember(Name = "route_draft_id", EmitDefaultValue = false)]
        public string[] RouteDraftId { get; set; }

        /// <summary>
        /// Days when visits occur
        /// </summary>
        [DataMember(Name = "days", EmitDefaultValue = false)]
        public string[][] Days { get; set; }
    }
}
