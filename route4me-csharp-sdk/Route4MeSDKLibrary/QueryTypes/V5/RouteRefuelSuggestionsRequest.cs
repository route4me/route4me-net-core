using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes.V5
{
    /// <summary>
    ///     The request body of the route refuel suggestions (POST /api/v5.0/routes/refuel-suggestions).
    ///     Name the routes one by one in <see cref="Routes"/>, or choose them with the Routes list
    ///     filters in <see cref="Filters"/>. Exactly one of the two is required.
    ///     Values without a unit are read in the account units.
    /// </summary>
    [DataContract]
    public sealed class RouteRefuelSuggestionsRequest : GenericParameters
    {
        /// <summary>
        ///     Routes named one by one (1-10), with optional fuel values that override the vehicle data.
        ///     Not combinable with <see cref="Filters"/>.
        /// </summary>
        [DataMember(Name = "routes", EmitDefaultValue = false)]
        public RouteRefuelRouteRequest[] Routes { get; set; }

        /// <summary>
        ///     The Routes list filters, as POST /api/v5.0/routes/list/combined takes them.
        ///     An empty object selects every visible route except master routes.
        ///     Selected routes take their fuel values from telematics, the vehicle and its profile.
        ///     Not combinable with <see cref="Routes"/>. Up to 10 routes are checked per page.
        /// </summary>
        [DataMember(Name = "filters", EmitDefaultValue = false)]
        public RouteFilterParametersFilters Filters { get; set; }

        /// <summary>
        ///     Routes left out of the filter selection. Only with <see cref="Filters"/>.
        /// </summary>
        [DataMember(Name = "exclude_ids", EmitDefaultValue = false)]
        public string[] ExcludeIds { get; set; }

        /// <summary>
        ///     IANA timezone the date filters are cut in (e.g. 'America/New_York'). Only with <see cref="Filters"/>.
        /// </summary>
        [DataMember(Name = "timezone", EmitDefaultValue = false)]
        public string Timezone { get; set; }

        /// <summary>
        ///     Routes list free-text search. Only with <see cref="Filters"/>.
        /// </summary>
        [DataMember(Name = "search_query", EmitDefaultValue = false)]
        public string SearchQuery { get; set; }

        /// <summary>
        ///     Routes list exist_fields. Only with <see cref="Filters"/>.
        /// </summary>
        [DataMember(Name = "exist_fields", EmitDefaultValue = false)]
        public string[] ExistFields { get; set; }

        /// <summary>
        ///     Routes list sorting as [field, direction] pairs, which decides which routes make the first page.
        ///     When not set, the view mode's sorting is used. Only with <see cref="Filters"/>.
        /// </summary>
        [DataMember(Name = "order_by", EmitDefaultValue = false)]
        public List<string[]> OrderBy { get; set; }

        /// <summary>
        ///     Routes list grouping (e.g. 'start_date', 'vehicle', 'none'), which sorts the routes by group first.
        ///     When not set, the view mode's grouping is used. Only with <see cref="Filters"/>.
        /// </summary>
        [DataMember(Name = "group_by", EmitDefaultValue = false)]
        public string GroupBy { get; set; }

        /// <summary>
        ///     Page of 10 matched routes to check (1-1000, default 1). Only with <see cref="Filters"/>.
        /// </summary>
        [DataMember(Name = "page", EmitDefaultValue = false)]
        public int? Page { get; set; }

        /// <summary>
        ///     Share of the tank never planned below, percent (0-90). Defaults to the account setting.
        /// </summary>
        [DataMember(Name = "reserve_percent", EmitDefaultValue = false)]
        public double? ReservePercent { get; set; }

        /// <summary>
        ///     Fuel stations per suggestion (0-25, default 5). 0 skips the station search.
        /// </summary>
        [DataMember(Name = "max_stations", EmitDefaultValue = false)]
        public int? MaxStations { get; set; }
    }

    /// <summary>
    ///     A route named in <see cref="RouteRefuelSuggestionsRequest.Routes"/>,
    ///     with optional fuel values that override the vehicle data.
    /// </summary>
    [DataContract]
    public sealed class RouteRefuelRouteRequest
    {
        /// <summary>
        ///     Route ID. Required.
        /// </summary>
        [DataMember(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <summary>
        ///     Tank level at the route start, percent (0-100).
        /// </summary>
        [DataMember(Name = "starting_fuel_level_percent", EmitDefaultValue = false)]
        public double? StartingFuelLevelPercent { get; set; }

        /// <summary>
        ///     Overrides the vehicle tank capacity, in <see cref="TankCapacityUnit"/>.
        /// </summary>
        [DataMember(Name = "tank_capacity", EmitDefaultValue = false)]
        public double? TankCapacity { get; set; }

        /// <summary>
        ///     'L' or 'gal'. Defaults to the account volume unit.
        /// </summary>
        [DataMember(Name = "tank_capacity_unit", EmitDefaultValue = false)]
        public string TankCapacityUnit { get; set; }

        /// <summary>
        ///     Overrides the vehicle fuel consumption, in <see cref="FuelConsumptionUnit"/>.
        /// </summary>
        [DataMember(Name = "fuel_consumption", EmitDefaultValue = false)]
        public double? FuelConsumption { get; set; }

        /// <summary>
        ///     'l/100km', 'km/l', 'mpg.us', 'mpg.uk', 'mi/l', 'mpg' (US) or 'mpg uk'.
        ///     Defaults to the account fuel consumption unit.
        /// </summary>
        [DataMember(Name = "fuel_consumption_unit", EmitDefaultValue = false)]
        public string FuelConsumptionUnit { get; set; }
    }
}