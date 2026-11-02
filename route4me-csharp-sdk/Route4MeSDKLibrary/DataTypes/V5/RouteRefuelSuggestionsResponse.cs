using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     The response of the route refuel suggestions (POST /api/v5.0/routes/refuel-suggestions).
    /// </summary>
    [DataContract]
    public class RouteRefuelSuggestionsResponse
    {
        /// <summary>
        ///     One entry per checked route: in request order for named routes,
        ///     in the Routes list order for filters.
        /// </summary>
        [DataMember(Name = "routes", EmitDefaultValue = false)]
        public RouteRefuelRoute[] Routes { get; set; }

        /// <summary>
        ///     Routes the selection matched across all pages. For named routes, the number of routes sent.
        /// </summary>
        [DataMember(Name = "matched_routes_count", EmitDefaultValue = false)]
        public int? MatchedRoutesCount { get; set; }

        /// <summary>
        ///     Entries in <see cref="Routes"/>, skipped ones included.
        /// </summary>
        [DataMember(Name = "checked_routes_count", EmitDefaultValue = false)]
        public int? CheckedRoutesCount { get; set; }

        /// <summary>
        ///     The page checked; 1 for named routes.
        /// </summary>
        [DataMember(Name = "page", EmitDefaultValue = false)]
        public int? Page { get; set; }
    }

    /// <summary>
    ///     Refuel suggestions for one route, in the account units.
    /// </summary>
    [DataContract]
    public class RouteRefuelRoute
    {
        /// <summary>
        ///     Route ID.
        /// </summary>
        [DataMember(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <summary>
        ///     'ok' when the route was planned; 'skipped' when it could not be, with the cause in <see cref="Reason"/>.
        ///     A skipped route has only route ID, status, reason, vehicle ID, suggestions and warnings.
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        ///     Why a skipped route could not be planned: 'route_not_found', 'route_has_no_directions',
        ///     'starting_fuel_level_unknown', 'tank_capacity_unknown', 'fuel_consumption_unknown',
        ///     'electric_vehicle_not_supported' or 'internal_error'.
        /// </summary>
        [DataMember(Name = "reason", EmitDefaultValue = false)]
        public string Reason { get; set; }

        /// <summary>
        ///     Vehicle assigned to the route; null when none is.
        /// </summary>
        [DataMember(Name = "vehicle_id", EmitDefaultValue = false)]
        public string VehicleId { get; set; }

        /// <summary>
        ///     Units of every value in the route.
        /// </summary>
        [DataMember(Name = "units", EmitDefaultValue = false)]
        public RouteRefuelUnits Units { get; set; }

        /// <summary>
        ///     Fuel values the plan used, each with its source.
        /// </summary>
        [DataMember(Name = "fuel", EmitDefaultValue = false)]
        public RouteRefuelFuel Fuel { get; set; }

        /// <summary>
        ///     Total driving distance of the route, in <see cref="RouteRefuelUnits.Distance"/>.
        /// </summary>
        [DataMember(Name = "route_distance", EmitDefaultValue = false)]
        public double? RouteDistance { get; set; }

        /// <summary>
        ///     Fuel the whole route consumes, in <see cref="RouteRefuelUnits.Volume"/>.
        /// </summary>
        [DataMember(Name = "fuel_needed", EmitDefaultValue = false)]
        public double? FuelNeeded { get; set; }

        /// <summary>
        ///     Estimated tank level at the end of the route, percent, after the suggested and existing refuels.
        ///     It can be below the reserve, or negative, when a leg is longer than a full tank allows.
        /// </summary>
        [DataMember(Name = "estimated_level_at_finish_percent", EmitDefaultValue = false)]
        public double? EstimatedLevelAtFinishPercent { get; set; }

        /// <summary>
        ///     Refuel stops already on the route; the plan assumes the tank is refilled there.
        /// </summary>
        [DataMember(Name = "existing_refuels", EmitDefaultValue = false)]
        public RouteRefuelExistingRefuel[] ExistingRefuels { get; set; }

        /// <summary>
        ///     As many refuels as the route needs, in route order; empty when the starting fuel covers it.
        /// </summary>
        [DataMember(Name = "suggestions", EmitDefaultValue = false)]
        public RouteRefuelSuggestion[] Suggestions { get; set; }

        /// <summary>
        ///     'stale_fuel_reading', 'leg_exceeds_range', 'station_search_failed', 'station_search_skipped',
        ///     'no_stations_found' or 'fuel_type_defaulted'.
        /// </summary>
        [DataMember(Name = "warnings", EmitDefaultValue = false)]
        public string[] Warnings { get; set; }
    }

    /// <summary>
    ///     Units of every value in a route. They follow the account settings, not the request units.
    /// </summary>
    [DataContract]
    public class RouteRefuelUnits
    {
        /// <summary>
        ///     'km' or 'mi'.
        /// </summary>
        [DataMember(Name = "distance", EmitDefaultValue = false)]
        public string Distance { get; set; }

        /// <summary>
        ///     'L' or 'gal'.
        /// </summary>
        [DataMember(Name = "volume", EmitDefaultValue = false)]
        public string Volume { get; set; }

        /// <summary>
        ///     'l/100km', 'km/l', 'mpg.us', 'mpg.uk' or 'mi/l'.
        /// </summary>
        [DataMember(Name = "fuel_consumption", EmitDefaultValue = false)]
        public string FuelConsumption { get; set; }
    }

    /// <summary>
    ///     Fuel values a refuel plan used. Each source is 'request', 'vehicle', 'vehicle_profile',
    ///     'telematics', 'account_config' or 'default'.
    /// </summary>
    [DataContract]
    public class RouteRefuelFuel
    {
        /// <summary>
        ///     Tank level at the route start, percent.
        /// </summary>
        [DataMember(Name = "starting_level_percent", EmitDefaultValue = false)]
        public double? StartingLevelPercent { get; set; }

        /// <summary>
        ///     Source of <see cref="StartingLevelPercent"/>.
        /// </summary>
        [DataMember(Name = "starting_level_source", EmitDefaultValue = false)]
        public string StartingLevelSource { get; set; }

        /// <summary>
        ///     When the telematics fuel level was read (ISO 8601); null for a request value.
        /// </summary>
        [DataMember(Name = "reading_at", EmitDefaultValue = false)]
        public string ReadingAt { get; set; }

        /// <summary>
        ///     Tank capacity, in <see cref="RouteRefuelUnits.Volume"/>.
        /// </summary>
        [DataMember(Name = "tank_capacity", EmitDefaultValue = false)]
        public double? TankCapacity { get; set; }

        /// <summary>
        ///     Source of <see cref="TankCapacity"/>.
        /// </summary>
        [DataMember(Name = "tank_capacity_source", EmitDefaultValue = false)]
        public string TankCapacitySource { get; set; }

        /// <summary>
        ///     Fuel consumption, in <see cref="RouteRefuelUnits.FuelConsumption"/>;
        ///     the worse of the city and highway values.
        /// </summary>
        [DataMember(Name = "fuel_consumption", EmitDefaultValue = false)]
        public double? FuelConsumption { get; set; }

        /// <summary>
        ///     Source of <see cref="FuelConsumption"/>.
        /// </summary>
        [DataMember(Name = "consumption_source", EmitDefaultValue = false)]
        public string ConsumptionSource { get; set; }

        /// <summary>
        ///     Share of the tank never planned below, percent.
        /// </summary>
        [DataMember(Name = "reserve_percent", EmitDefaultValue = false)]
        public double? ReservePercent { get; set; }

        /// <summary>
        ///     Source of <see cref="ReservePercent"/>.
        /// </summary>
        [DataMember(Name = "reserve_source", EmitDefaultValue = false)]
        public string ReserveSource { get; set; }
    }

    /// <summary>
    ///     A Refuel stop already on the route.
    /// </summary>
    [DataContract]
    public class RouteRefuelExistingRefuel
    {
        /// <summary>
        ///     Route destination ID of the Refuel stop.
        /// </summary>
        [DataMember(Name = "route_destination_id", EmitDefaultValue = false)]
        public long? RouteDestinationId { get; set; }

        /// <summary>
        ///     Sequence number of the Refuel stop.
        /// </summary>
        [DataMember(Name = "sequence_no", EmitDefaultValue = false)]
        public int? SequenceNo { get; set; }

        /// <summary>
        ///     Estimated tank level on arrival, percent. Negative after a leg longer than a full tank allows.
        /// </summary>
        [DataMember(Name = "level_on_arrival_percent", EmitDefaultValue = false)]
        public double? LevelOnArrivalPercent { get; set; }
    }

    /// <summary>
    ///     One refuel the route needs.
    /// </summary>
    [DataContract]
    public class RouteRefuelSuggestion
    {
        /// <summary>
        ///     1-based position among the route's suggestions.
        /// </summary>
        [DataMember(Name = "index", EmitDefaultValue = false)]
        public int? Index { get; set; }

        /// <summary>
        ///     The latest stop the vehicle can reach above the reserve; it must refuel before going past it.
        ///     <see cref="Stations"/> lie on the legs leading to this stop.
        /// </summary>
        [DataMember(Name = "refuel_before_destination", EmitDefaultValue = false)]
        public RouteRefuelStop RefuelBeforeDestination { get; set; }

        /// <summary>
        ///     Distance from the route start to <see cref="RefuelBeforeDestination"/>, in <see cref="RouteRefuelUnits.Distance"/>.
        /// </summary>
        [DataMember(Name = "distance_from_start", EmitDefaultValue = false)]
        public double? DistanceFromStart { get; set; }

        /// <summary>
        ///     Estimated tank level on arrival at <see cref="RefuelBeforeDestination"/>, percent.
        ///     Negative after a leg longer than a full tank allows.
        /// </summary>
        [DataMember(Name = "estimated_level_on_arrival_percent", EmitDefaultValue = false)]
        public double? EstimatedLevelOnArrivalPercent { get; set; }

        /// <summary>
        ///     Fuel stations for the vehicle's fuel type, ordered by detour.
        ///     Empty when the search found none, failed or was skipped; the route warnings say which.
        /// </summary>
        [DataMember(Name = "stations", EmitDefaultValue = false)]
        public RouteRefuelStation[] Stations { get; set; }
    }

    /// <summary>
    ///     A route stop referenced by a refuel suggestion.
    ///     As <see cref="RouteRefuelStation.InsertAfterDestination"/>, only the route destination ID and sequence number are set.
    /// </summary>
    [DataContract]
    public class RouteRefuelStop
    {
        /// <summary>
        ///     Route destination ID.
        /// </summary>
        [DataMember(Name = "route_destination_id", EmitDefaultValue = false)]
        public long? RouteDestinationId { get; set; }

        /// <summary>
        ///     Sequence number.
        /// </summary>
        [DataMember(Name = "sequence_no", EmitDefaultValue = false)]
        public int? SequenceNo { get; set; }

        /// <summary>
        ///     Stop alias.
        /// </summary>
        [DataMember(Name = "alias", EmitDefaultValue = false)]
        public string Alias { get; set; }

        /// <summary>
        ///     Stop address.
        /// </summary>
        [DataMember(Name = "address", EmitDefaultValue = false)]
        public string Address { get; set; }

        /// <summary>
        ///     True when the stop is the depot.
        /// </summary>
        [DataMember(Name = "is_depot", EmitDefaultValue = false)]
        public bool? IsDepot { get; set; }
    }

    /// <summary>
    ///     A fuel station from the Fueling API station search.
    /// </summary>
    [DataContract]
    public class RouteRefuelStation
    {
        /// <summary>
        ///     Provider station ID.
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        ///     Route4Me station ID: the same physical station gets the same ID across searches.
        ///     Null when the station registry could not be reached.
        /// </summary>
        [DataMember(Name = "fuel_station_id", EmitDefaultValue = false)]
        public string FuelStationId { get; set; }

        /// <summary>
        ///     Station name.
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        ///     Station brand.
        /// </summary>
        [DataMember(Name = "brand", EmitDefaultValue = false)]
        public string Brand { get; set; }

        /// <summary>
        ///     Station position.
        /// </summary>
        [DataMember(Name = "position", EmitDefaultValue = false)]
        public FuelStationGeoPoint Position { get; set; }

        /// <summary>
        ///     The point to navigate to in order to enter the station.
        /// </summary>
        [DataMember(Name = "access", EmitDefaultValue = false)]
        public FuelStationGeoPoint Access { get; set; }

        /// <summary>
        ///     Station address.
        /// </summary>
        [DataMember(Name = "address", EmitDefaultValue = false)]
        public FuelStationAddress Address { get; set; }

        /// <summary>
        ///     The provider's 24x7 flag. False can still come with <see cref="OpeningHours"/>
        ///     covering every day around the clock, so check those.
        /// </summary>
        [DataMember(Name = "open_24x7", EmitDefaultValue = false)]
        public bool? Open24x7 { get; set; }

        /// <summary>
        ///     Opening hours; empty when unknown.
        /// </summary>
        [DataMember(Name = "opening_hours", EmitDefaultValue = false)]
        public FuelStationOpeningHours[] OpeningHours { get; set; }

        /// <summary>
        ///     The account's fuel card discount at this station; null when the station is not on its discount site list.
        /// </summary>
        [DataMember(Name = "discount", EmitDefaultValue = false)]
        public FuelStationDiscount Discount { get; set; }

        /// <summary>
        ///     Fuel prices.
        /// </summary>
        [DataMember(Name = "prices", EmitDefaultValue = false)]
        public FuelStationPrice[] Prices { get; set; }

        /// <summary>
        ///     Straight-line distance to the station, meters.
        /// </summary>
        [DataMember(Name = "air_distance_meters", EmitDefaultValue = false)]
        public int? AirDistanceMeters { get; set; }

        /// <summary>
        ///     Road distance to the station, meters.
        /// </summary>
        [DataMember(Name = "road_distance_meters", EmitDefaultValue = false)]
        public int? RoadDistanceMeters { get; set; }

        /// <summary>
        ///     Driving time to the station, seconds.
        /// </summary>
        [DataMember(Name = "drive_time_seconds", EmitDefaultValue = false)]
        public int? DriveTimeSeconds { get; set; }

        /// <summary>
        ///     Extra distance added to the route by visiting this station, meters.
        /// </summary>
        [DataMember(Name = "detour_distance_meters", EmitDefaultValue = false)]
        public int? DetourDistanceMeters { get; set; }

        /// <summary>
        ///     Extra driving time added to the route by visiting this station, seconds.
        /// </summary>
        [DataMember(Name = "detour_time_seconds", EmitDefaultValue = false)]
        public int? DetourTimeSeconds { get; set; }

        /// <summary>
        ///     The stop after which to insert a Refuel stop at this station: the station lies on the leg that starts there.
        ///     Null when the search did not place the station on the route.
        /// </summary>
        [DataMember(Name = "insert_after_destination", EmitDefaultValue = false)]
        public RouteRefuelStop InsertAfterDestination { get; set; }
    }

    /// <summary>
    ///     A fuel station coordinate.
    /// </summary>
    [DataContract]
    public class FuelStationGeoPoint
    {
        /// <summary>
        ///     Latitude.
        /// </summary>
        [DataMember(Name = "latitude", EmitDefaultValue = false)]
        public double? Latitude { get; set; }

        /// <summary>
        ///     Longitude.
        /// </summary>
        [DataMember(Name = "longitude", EmitDefaultValue = false)]
        public double? Longitude { get; set; }
    }

    /// <summary>
    ///     A fuel station address.
    /// </summary>
    [DataContract]
    public class FuelStationAddress
    {
        /// <summary>
        ///     The full address.
        /// </summary>
        [DataMember(Name = "label", EmitDefaultValue = false)]
        public string Label { get; set; }

        /// <summary>
        ///     Street.
        /// </summary>
        [DataMember(Name = "street", EmitDefaultValue = false)]
        public string Street { get; set; }

        /// <summary>
        ///     House number.
        /// </summary>
        [DataMember(Name = "house_number", EmitDefaultValue = false)]
        public string HouseNumber { get; set; }

        /// <summary>
        ///     Postal code.
        /// </summary>
        [DataMember(Name = "postal_code", EmitDefaultValue = false)]
        public string PostalCode { get; set; }

        /// <summary>
        ///     City.
        /// </summary>
        [DataMember(Name = "city", EmitDefaultValue = false)]
        public string City { get; set; }

        /// <summary>
        ///     State.
        /// </summary>
        [DataMember(Name = "state", EmitDefaultValue = false)]
        public string State { get; set; }

        /// <summary>
        ///     Country code (e.g. 'USA').
        /// </summary>
        [DataMember(Name = "country_code", EmitDefaultValue = false)]
        public string CountryCode { get; set; }
    }

    /// <summary>
    ///     Fuel station opening hours for a set of days.
    /// </summary>
    [DataContract]
    public class FuelStationOpeningHours
    {
        /// <summary>
        ///     'mo', 'tu', 'we', 'th', 'fr', 'sa' or 'su', in no fixed order.
        /// </summary>
        [DataMember(Name = "days", EmitDefaultValue = false)]
        public string[] Days { get; set; }

        /// <summary>
        ///     Opening time (e.g. '06:00:00').
        /// </summary>
        [DataMember(Name = "from", EmitDefaultValue = false)]
        public string From { get; set; }

        /// <summary>
        ///     Closing time. '24:00:00' is the end of the day; earlier than <see cref="From"/> when the hours run past midnight.
        /// </summary>
        [DataMember(Name = "to", EmitDefaultValue = false)]
        public string To { get; set; }
    }

    /// <summary>
    ///     The account's fuel card discount at a station.
    /// </summary>
    [DataContract]
    public class FuelStationDiscount
    {
        /// <summary>
        ///     The discount site the discount comes from.
        /// </summary>
        [DataMember(Name = "fuel_discount_site_id", EmitDefaultValue = false)]
        public string FuelDiscountSiteId { get; set; }

        /// <summary>
        ///     'flat_cents' (cents off per gallon) or 'percent' (percent off the price).
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        /// <summary>
        ///     Discount on gasoline; null when none.
        /// </summary>
        [DataMember(Name = "gas", EmitDefaultValue = false)]
        public double? Gas { get; set; }

        /// <summary>
        ///     Discount on diesel; null when none.
        /// </summary>
        [DataMember(Name = "diesel", EmitDefaultValue = false)]
        public double? Diesel { get; set; }

        /// <summary>
        ///     Cents per gallon off after the gas or diesel discount; null when none.
        /// </summary>
        [DataMember(Name = "extra_cents", EmitDefaultValue = false)]
        public double? ExtraCents { get; set; }
    }

    /// <summary>
    ///     A fuel price at a station.
    /// </summary>
    [DataContract]
    public class FuelStationPrice
    {
        /// <summary>
        ///     'unleaded 87', 'unleaded 89', 'unleaded 91', 'unleaded 93', 'diesel', 'electric' or 'hybrid';
        ///     null when the provider fuel type maps to none.
        /// </summary>
        [DataMember(Name = "fuel_type", EmitDefaultValue = false)]
        public string FuelType { get; set; }

        /// <summary>
        ///     Provider fuel type ID.
        /// </summary>
        [DataMember(Name = "here_fuel_type_id", EmitDefaultValue = false)]
        public int? HereFuelTypeId { get; set; }

        /// <summary>
        ///     Price per <see cref="Unit"/>.
        /// </summary>
        [DataMember(Name = "price", EmitDefaultValue = false)]
        public double? Price { get; set; }

        /// <summary>
        ///     The price after the account's fuel card discount; null when the station has no discount for this fuel.
        /// </summary>
        [DataMember(Name = "discounted_price", EmitDefaultValue = false)]
        public double? DiscountedPrice { get; set; }

        /// <summary>
        ///     Currency code (e.g. 'USD').
        /// </summary>
        [DataMember(Name = "currency", EmitDefaultValue = false)]
        public string Currency { get; set; }

        /// <summary>
        ///     Volume unit of the price (e.g. 'gal').
        /// </summary>
        [DataMember(Name = "unit", EmitDefaultValue = false)]
        public string Unit { get; set; }

        /// <summary>
        ///     When the provider last updated the price (ISO 8601).
        /// </summary>
        [DataMember(Name = "modified", EmitDefaultValue = false)]
        public string Modified { get; set; }
    }
}