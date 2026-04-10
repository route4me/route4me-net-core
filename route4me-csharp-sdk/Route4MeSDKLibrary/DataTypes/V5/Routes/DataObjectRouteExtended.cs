using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     Response wrapper for POST /api/v5.0/routes/list (and list-style routes endpoints).
    ///     The API returns { "data": [...], "links": {...}, "meta": {...} }; this type matches that shape.
    /// </summary>
    [DataContract]
    public sealed class RoutesListResponse
    {
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public DataObjectRouteExtended[] Data { get; set; }

        [DataMember(Name = "links", EmitDefaultValue = false)]
        public PageLinks Links { get; set; }

        [DataMember(Name = "meta", EmitDefaultValue = false)]
        public PageMeta Meta { get; set; }
    }

    /// <summary>
    ///     Response wrapper for GET /api/v5.0/routes/{route_id} endpoint.
    ///     The API returns the route data wrapped in a "data" property.
    /// </summary>
    [DataContract]
    public sealed class GetRouteResponse
    {
        /// <summary>
        ///     The route data with extended details
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public DataObjectRouteExtended Data { get; set; }
    }

    /// <summary>
    ///     Extended route data structure with full details including addresses and parameters.
    ///     This is returned from the GET /api/v5.0/routes/{route_id} endpoint.
    ///     Only adds fields that are unique to the extended route response.
    /// </summary>
    [DataContract]
    public sealed class DataObjectRouteExtended : DataObjectRoute
    {
        /// <summary>
        ///     Route cost
        /// </summary>
        [DataMember(Name = "route_cost", EmitDefaultValue = false)]
        public double? RouteCost { get; set; }

        /// <summary>
        ///     Route weight
        /// </summary>
        [DataMember(Name = "route_weight", EmitDefaultValue = false)]
        public double? RouteWeight { get; set; }

        /// <summary>
        ///     Route cube
        /// </summary>
        [DataMember(Name = "route_cube", EmitDefaultValue = false)]
        public double? RouteCube { get; set; }

        /// <summary>
        ///     Route pieces
        /// </summary>
        [DataMember(Name = "route_pieces", EmitDefaultValue = false)]
        public long? RoutePieces { get; set; }

        /// <summary>
        ///     Route revenue
        /// </summary>
        [DataMember(Name = "route_revenue", EmitDefaultValue = false)]
        public double? RouteRevenue { get; set; }

        /// <summary>
        ///     Net revenue per distance unit
        /// </summary>
        [DataMember(Name = "net_revenue_per_distance_unit", EmitDefaultValue = false)]
        public double? NetRevenuePerDistanceUnit { get; set; }

        /// <summary>
        ///     Tracking history
        /// </summary>
        [DataMember(Name = "tracking_history", EmitDefaultValue = false)]
        public object[] TrackingHistory { get; set; }
    }
}