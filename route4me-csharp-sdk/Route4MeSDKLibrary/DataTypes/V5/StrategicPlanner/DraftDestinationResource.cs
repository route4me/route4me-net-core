using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Draft destination resource
    /// </summary>
    [DataContract]
    public sealed class DraftDestinationResource
    {
        /// <summary>
        /// Draft destination ID (hex32 format)
        /// </summary>
        [DataMember(Name = "draft_destination_id", EmitDefaultValue = false)]
        public string DraftDestinationId { get; set; }

        /// <summary>
        /// Route draft ID
        /// </summary>
        [DataMember(Name = "route_draft_id", EmitDefaultValue = false)]
        public string RouteDraftId { get; set; }

        /// <summary>
        /// Scenario ID
        /// </summary>
        [DataMember(Name = "scenario_id", EmitDefaultValue = false)]
        public string ScenarioId { get; set; }

        /// <summary>
        /// Planner location ID
        /// </summary>
        [DataMember(Name = "planner_location_id", EmitDefaultValue = false)]
        public string PlannerLocationId { get; set; }

        /// <summary>
        /// Destination name
        /// </summary>
        [DataMember(Name = "destination_name", EmitDefaultValue = false)]
        public string DestinationName { get; set; }

        /// <summary>
        /// Destination alias
        /// </summary>
        [DataMember(Name = "destination_alias", EmitDefaultValue = false)]
        public string DestinationAlias { get; set; }

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
        /// First name
        /// </summary>
        [DataMember(Name = "first_name", EmitDefaultValue = false)]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        [DataMember(Name = "last_name", EmitDefaultValue = false)]
        public string LastName { get; set; }

        /// <summary>
        /// Customer email
        /// </summary>
        [DataMember(Name = "customer_email", EmitDefaultValue = false)]
        public string CustomerEmail { get; set; }

        /// <summary>
        /// Customer phone
        /// </summary>
        [DataMember(Name = "customer_phone", EmitDefaultValue = false)]
        public string CustomerPhone { get; set; }

        /// <summary>
        /// Address stop type
        /// </summary>
        [DataMember(Name = "address_stop_type", EmitDefaultValue = false)]
        public string AddressStopType { get; set; }

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
        /// Curbside latitude
        /// </summary>
        [DataMember(Name = "curb_lat", EmitDefaultValue = false)]
        public double? CurbLat { get; set; }

        /// <summary>
        /// Curbside longitude
        /// </summary>
        [DataMember(Name = "curb_lng", EmitDefaultValue = false)]
        public double? CurbLng { get; set; }

        /// <summary>
        /// Route name
        /// </summary>
        [DataMember(Name = "route_name", EmitDefaultValue = false)]
        public string RouteName { get; set; }

        /// <summary>
        /// Is depot
        /// </summary>
        [DataMember(Name = "is_depot", EmitDefaultValue = false)]
        public string IsDepot { get; set; }

        /// <summary>
        /// Service time
        /// </summary>
        [DataMember(Name = "service_time", EmitDefaultValue = false)]
        public string ServiceTime { get; set; }

        /// <summary>
        /// Order ID
        /// </summary>
        [DataMember(Name = "order_id", EmitDefaultValue = false)]
        public string OrderId { get; set; }

        /// <summary>
        /// Order number
        /// </summary>
        [DataMember(Name = "order_no", EmitDefaultValue = false)]
        public string OrderNo { get; set; }

        /// <summary>
        /// Invoice number
        /// </summary>
        [DataMember(Name = "invoice_no", EmitDefaultValue = false)]
        public string InvoiceNo { get; set; }

        /// <summary>
        /// Reference number
        /// </summary>
        [DataMember(Name = "reference_no", EmitDefaultValue = false)]
        public string ReferenceNo { get; set; }

        /// <summary>
        /// Driving path
        /// </summary>
        [DataMember(Name = "driving_path", EmitDefaultValue = false)]
        public string DrivingPath { get; set; }
    }
}
