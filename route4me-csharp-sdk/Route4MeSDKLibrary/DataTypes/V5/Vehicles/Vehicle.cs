using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     Response from the vehicle request
    /// </summary>
    [DataContract]
    public class Vehicle : VehicleBase
    {
        /// <summary>
        ///     Member ID assigned to the vehicle.
        /// </summary>
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public long? MemberId { get; set; }

        /// <summary>
        ///     If true, the vehicle is marked as deleted.
        /// </summary>
        [DataMember(Name = "is_deleted", EmitDefaultValue = false)]
        public bool? IsDeleted { get; set; }

        /// <summary>
        ///     Vehicle registration country ID.
        /// </summary>
        [DataMember(Name = "vehicle_reg_state_id", EmitDefaultValue = false)]
        public int? VehicleRegStateId { get; set; }

        /// <summary>
        ///     Vehicle registration country ID.
        /// </summary>
        [DataMember(Name = "vehicle_reg_country_id", EmitDefaultValue = false)]
        public int? VehicleRegCountryId { get; set; }

        /// <summary>
        ///     Vehicle type.
        ///     <para>Availbale values:</para>
        ///     sedan', 'suv', 'pickup_truck', 'van', '18wheeler', 'cabin', 'hatchback',
        ///     '
        ///     <para>motorcyle', 'waste_disposal', 'tree_cutting', 'bigrig', 'cement_mixer', </para>
        ///     'livestock_carrier', 'dairy','tractor_trailer'.
        /// </summary>
        [DataMember(Name = "vehicle_type_id", EmitDefaultValue = false)]
        public string VehicleTypeId { get; set; }

        /// <summary>
        ///     When the vehicle was added.
        /// </summary>
        [DataMember(Name = "timestamp_added", EmitDefaultValue = false)]
        public string TimestampAdded { get; set; }

        /// <summary>
        ///     A type of the fuel
        ///     enum: ['unleaded 87','unleaded 89','unleaded 91','unleaded 93','diesel','electric','hybrid']
        /// </summary>
        [DataMember(Name = "fuel_type", EmitDefaultValue = false)]
        public string FuelType { get; set; }

        /// <summary>
        ///     External telematics vehicle IDs
        /// </summary>
        [DataMember(Name = "external_telematics_vehicle_ids", EmitDefaultValue = false)]
        public long? ExternalTelematicsVehicleIDs { get; set; }

        /// <summary>
        ///     When the vehcile was marked as deleted.
        /// </summary>
        [DataMember(Name = "timestamp_removed", EmitDefaultValue = false)]
        public string TimestampRemoved { get; set; }

        /// <summary>
        ///     Vehicle profile ID
        /// </summary>
        [DataMember(Name = "vehicle_profile_id", EmitDefaultValue = false)]
        public int? VehicleProfileId { get; set; }

        /// <summary>
        ///     Fuel consumption city
        /// </summary>
        [DataMember(Name = "fuel_consumption_city", EmitDefaultValue = false)]
        public double? FuelConsumptionCity { get; set; }

        /// <summary>
        ///     Fuel consumption in the highway area
        /// </summary>
        [DataMember(Name = "fuel_consumption_highway", EmitDefaultValue = false)]
        public double? FuelConsumptionHighway { get; set; }

        /// <summary>
        ///     Fuel consumption units in the city area (e.g. mi/l)
        /// </summary>
        [DataMember(Name = "fuel_consumption_city_unit", EmitDefaultValue = false)]
        public string FuelConsumptionCityUnit { get; set; }

        /// <summary>
        ///     Fuel consumption units in the highway area (e.g. mi/l)
        /// </summary>
        [DataMember(Name = "fuel_consumption_highway_unit", EmitDefaultValue = false)]
        public string FuelConsumptionHighwayUnit { get; set; }

        /// <summary>
        ///     Miles per gallon in the city area
        /// </summary>
        [DataMember(Name = "mpg_city", EmitDefaultValue = false)]
        public double? MpgCity { get; set; }

        /// <summary>
        ///     Miles per gallon in the highway area
        /// </summary>
        [DataMember(Name = "mpg_highway", EmitDefaultValue = false)]
        public double? MpgHighway { get; set; }

        /// <summary>
        ///     Fuel consumption UF value in the city area (e.g. '20.01 mi/l')
        /// </summary>
        [DataMember(Name = "fuel_consumption_city_uf_value", EmitDefaultValue = false)]
        public string FuelConsumptionCityUfValue { get; set; }

        /// <summary>
        ///     Fuel consumption UF value in the highway area (e.g. '2,000.01 mpg')
        /// </summary>
        [DataMember(Name = "fuel_consumption_highway_uf_value", EmitDefaultValue = false)]
        public string FuelConsumptionHighwayUfValue { get; set; }
    }
}