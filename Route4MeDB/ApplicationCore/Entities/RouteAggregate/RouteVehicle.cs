using Route4MeDB.ApplicationCore.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Route4MeDB.ApplicationCore.Entities.RouteAggregate
{
    public class RouteVehicle : IAggregateRoot
    {
        [Column("vehicle_id")]
        public string VehicleId { get; set; }

        /// <summary>
        /// Member ID assigned to the vehicle.
        /// </summary>
        [Column("member_id")]
        public string MemberId { get; set; }

        /// <summary>
        /// <c>true</c> if the vehicle is deleted.
        /// </summary>
        [Column("is_deleted")]
        public string IsDeleted { get; set; }

        /// <summary>
        /// Vehicle alias.
        /// </summary>
        [Column("vehicle_alias")]
        public string VehicleAlias { get; set; }

        /// <summary>
        /// Vehicle VIN.
        /// </summary>
        [Column("vehicle_vin")]
        public string VehicleVin { get; set; }

        /// <summary>
        /// When the vehicle was created.
        /// </summary>
        [Column("created_time")]
        public string CreatedTime { get; set; }

        /// <summary>
        /// Vehicle registration state ID.
        /// </summary>
        [Column("vehicle_reg_state_id")]
        public string VehicleRegStateId { get; set; }

        /// <summary>
        /// Vehicle registration country ID.
        /// </summary>
        [Column("vehicle_reg_country_id")]
        public string VehicleRegCountryId { get; set; }

        /// <summary>
        /// A license plate of the vehicle.
        /// </summary>
        [Column("vehicle_license_plate")]
        public string VehicleLicensePlate { get; set; }

        /// <summary>
        /// Vehicle type.
        /// <para>Availbale values:</para>
        /// <value>
        /// 'sedan', 'suv', 'pickup_truck', 'van', '18wheeler', 'cabin', 'hatchback', 
        /// '<para>motorcyle', 'waste_disposal', 'tree_cutting', 'bigrig', 'cement_mixer', </para>
        /// 'livestock_carrier', 'dairy','tractor_trailer'.
        /// </value>
        /// </summary>
        [Column("vehicle_type_id")]
        public string VehicleTypeID { get; set; }

        /// <summary>
        /// When the vehicle was added.
        /// </summary>
        [Column("timestamp_added")]
        public string TimestampAdded { get; set; }

        /// <summary>
        /// Vehicle maker brend. 
        /// <para>Available values:</para>
        /// <value>
        /// "american coleman", "bmw", "chevrolet", "ford", "freightliner", "gmc", 
        /// <para>"hino", "honda", "isuzu", "kenworth", "mack", "mercedes-benz", "mitsubishi", </para>
        /// "navistar", "nissan", "peterbilt", "renault", "scania", "sterling", "toyota", 
        /// <para>"volvo", "western star" </para>
        /// </value>"
        /// </summary>
        [Column("vehicle_make")]
        public string VehicleMake { get; set; }

        /// <summary>
        /// Vehicle model year.
        /// </summary>
        [Column("vehicle_model_year")]
        public string VehicleModelYear { get; set; }

        /// <summary>
        /// Vehicle model.
        /// </summary>
        [Column("vehicle_model")]
        public string VehicleModel { get; set; }

        /// <summary>
        /// The year, vehicle was acquired.
        /// </summary>
        [Column("vehicle_year_acquired")]
        public string VehicleYearAcquired { get; set; }

        /// <summary>
        /// A cost of the new vehicle.
        /// </summary>
        [Column("vehicle_cost_new")]
        public string VehicleCostNew { get; set; }

        /// <summary>
        /// If true, the vehicle was purchased new.
        /// </summary>
        [Column("purchased_new")]
        public string PurchasedNew { get; set; }

        /// <summary>
        /// Start date of the license.
        /// </summary>
        [Column("license_start_date")]
        public string LicenseStartDate { get; set; }

        /// <summary>
        /// End date of the license.
        /// </summary>
        [Column("license_end_date")]
        public string LicenseEndDate { get; set; }

        /// <summary>
        /// A number of the vecile's axles.
        /// </summary>
        [Column("vehicle_axle_count")]
        public string VehicleAxleCount { get; set; }

        /// <summary>
        /// Miles per gallon in the city area.
        /// </summary>
        [Column("mpg_city")]
        public string MpgCity { get; set; }

        /// <summary>
        /// Miles per gallon in the highway area.
        /// </summary>
        [Column("mpg_highway")]
        public string MpgHighway { get; set; }

        /// <summary>
        /// A type of the fuel.
        /// <para>Available values:</para>
        /// <value>unleaded 87, unleaded 89, unleaded 91, unleaded 93, diesel, electric, hybrid</value>
        /// </summary>
        [Column("fuel_type")]
        public string FuelType { get; set; }

        /// <summary>
        /// Height of the vehicle in the inches.
        /// </summary>
        [Column("height_inches")]
        public string HeightInches { get; set; }

        /// <summary>
        /// Weight of the vehicle in the pounds.
        /// </summary>
        [Column("weight_lb")]
        public string WeightLb { get; set; }

        /// <summary>
        /// If "1", the vehicle is operational.
        /// </summary>
        [Column("is_operational")]
        public string IsOperational { get; set; }

        /// <summary>
        /// External telematics vehicle ID.
        /// </summary>
        [Column("External telematics vehicle ID")]
        public string ExternalTelematicsVehicleID { get; set; }

        /// <summary>
        /// If "1", the vehicle has trailer.
        /// </summary>
        [Column("has_trailer")]
        public string HasTrailer { get; set; }

        /// <summary>
        /// Vehicle height in inches.
        /// </summary>
        [Column("heightInInches")]
        public string HeightInInches { get; set; }

        /// <summary>
        /// Vehicle length in inches.
        /// </summary>
        [Column("lengthInInches")]
        public string LengthInInches { get; set; }

        /// <summary>
        /// Vehicle width in inches.
        /// </summary>
        [Column("widthInInches")]
        public string WidthInInches { get; set; }

        /// <summary>
        /// Maximum weight per axle group in pounds.
        /// </summary>
        [Column("maxWeightPerAxleGroupInPounds")]
        public string MaxWeightPerAxleGroupInPounds { get; set; }

        /// <summary>
        /// Number of the axles.
        /// </summary>
        [Column("numAxles")]
        public string NumAxles { get; set; }

        /// <summary>
        /// Weight in pounds.
        /// </summary>
        [Column("weightInPounds")]
        public string WeightInPounds { get; set; }

        /// <summary>
        /// Hazardous materials type.
        /// <para>Available values:</para>
        /// <value>'INVALID', 'NONE', 'GENERAL', 'EXPLOSIVE', 
        /// 'INHALANT', 'RADIOACTIVE', 'CAUSTIC', 'FLAMMABLE', 'HARMFUL_TO_WATER'</value>
        /// </summary>
        [Column("HazmatType")]
        public string HazmatType { get; set; }

        /// <summary>
        /// Low emission zone preference.
        /// </summary>
        [Column("LowEmissionZonePref")]
        public string LowEmissionZonePref { get; set; }

        /// <summary>
        /// If equal to 'YES', optimization algorithm will use 53 foot trailer routing.
        /// <para>Available values:</para>
        /// <value>'YES', 'NO'</value>
        /// </summary>
        [Column("Use53FootTrailerRouting")]
        public string Use53FootTrailerRouting { get; set; }

        /// <summary>
        /// If equal to 'YES', optimization algorithm will use national network.
        /// <para>Available values:</para>
        /// <value>'YES', 'NO'</value>
        /// </summary>
        [Column("UseNationalNetwork")]
        public string UseNationalNetwork { get; set; }

        /// <summary>
        /// If equal to 'YES', optimization algorithm will use truck restrictions.
        /// <para>Available values:</para>
        /// <value>'YES', 'NO'</value>
        /// </summary>
        [Column("UseTruckRestrictions")]
        public string UseTruckRestrictions { get; set; }

        /// <summary>
        /// If equal to 'YES', optimization algorithm will avoid ferries.
        /// <para>Available values:</para>
        /// <value>'YES', 'NO'</value>.
        /// </summary>
        [Column("AvoidFerries")]
        public string AvoidFerries { get; set; }

        /// <summary>
        /// Divided highway avoid preference (e.g. NEUTRAL).
        /// </summary>
        [Column("DividedHighwayAvoidPreference")]
        public string DividedHighwayAvoidPreference { get; set; }

        /// <summary>
        /// Freeway avoid preference.
        /// <para>Available values:</para>
        /// <value>'STRONG_AVOID', 'AVOID', 'NEUTRAL', 'FAVOR', 'STRONG_FAVOR'</value>
        /// </summary>
        [Column("FreewayAvoidPreference")]
        public string FreewayAvoidPreference { get; set; }

        /// <summary>
        /// If equal to 'YES', optimization algorithm will use 'International borders open' option.
        /// <para>Available values:</para>
        /// <value>'YES', 'NO'</value>
        /// </summary>
        [Column("InternationalBordersOpen")]
        public string InternationalBordersOpen { get; set; }

        /// <summary>
        /// Toll road usage.
        /// <para>Available values:</para>
        /// <value>'STRONG_AVOID', 'AVOID', 'NEUTRAL', 'FAVOR', 'STRONG_FAVOR'</value>
        /// </summary>
        [Column("TollRoadUsage")]
        public string TollRoadUsage { get; set; }

        /// <summary>
        /// If true, the vehicle uses only highway.
        /// </summary>
        [Column("hwy_only")]
        public string HwyOnly { get; set; }

        /// <summary>
        /// If true, the vehicle is long combination.
        /// </summary>
        [Column("long_combination_vehicle")]
        public string LongCombinationVehicle { get; set; }

        /// <summary>
        /// If true, the vehicle should avoid highways
        /// </summary>
        [Column("avoid_highways")]
        public string AvoidHighways { get; set; }

        /// <summary>
        /// Side street adherence.
        /// <para>Available values:</para>
        /// <value>'OFF', 'MINIMAL', 'MODERATE', 'AVERAGE', 'STRICT', 'ADHERE', 'STRONGLYHERE'</value>
        /// </summary>
        [Column("side_street_adherence")]
        public string SideStreetAdherence { get; set; }

        /// <summary>
        /// Truck configuration.
        /// <para>Available values:</para>
        /// <value>'NONE', 'PASSENGER', '28_DOUBLETRAILER', '48_STRAIGHT_TRUCK', '48_SEMI_TRAILER', 
        /// '53_SEMI_TRAILER', 'FULLSIZEVAN', '26_STRAIGHT_TRUCK'</value>
        /// </summary>
        [Column("truck_config")]
        public string TruckConfig { get; set; }

        /// <summary>
        /// Vehicle height in metric unit.
        /// </summary>
        [Column("height_metric")]
        public string HeightMetric { get; set; }

        /// <summary>
        /// Vehicle length in metric unit.
        /// </summary>
        [Column("length_metric")]
        public string LengthMetric { get; set; }

        /// <summary>
        /// Vehicle width in metric unit.
        /// </summary>
        [Column("width_metric")]
        public string WidthMetric { get; set; }

        /// <summary>
        /// Vehicle weight in metric unit.
        /// </summary>
        [Column("weight_metric")]
        public string WeightMetric { get; set; }

        /// <summary>
        /// Maximum weight per axle group in metric unit.
        /// </summary>
        [Column("max_weight_per_axle_group_metric")]
        public string MaxWeightPerAxleGroupMetric { get; set; }

        /// <summary>
        /// When the vehicle was removed.
        /// </summary>
        [Column("timestamp_removed")]
        public string TimestampRemoved { get; set; }
    }
}
