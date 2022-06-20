using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     Vehicle capacity profile data structure.
    /// </summary>
    [DataContract]
    public sealed class VehicleCapacityProfile : GenericParameters
    {
        /// <summary>
        ///     Vehicle capacity profile ID
        /// </summary>
        [DataMember(Name = "vehicle_capacity_profile_id", EmitDefaultValue = false)]
        public int? VehicleCapacityProfileId { get; set; }

        /// <summary>
        ///     Root member ID
        /// </summary>
        [DataMember(Name = "root_member_id", EmitDefaultValue = false)]
        public long? RootMemberId { get; set; }

        /// <summary>
        ///     Vehicle capacity profile name
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        ///     A maximum cargo volume the vehicle can carry.
        /// </summary>
        [DataMember(Name = "max_volume", EmitDefaultValue = false)]
        public double? MaxVolume { get; set; }

        /// <summary>
        ///     The maximum weight a vehicle can carry.
        /// </summary>
        [DataMember(Name = "max_weight", EmitDefaultValue = false)]
        public double? MaxWeight { get; set; }

        /// <summary>
        ///     The maximum number of items a vehicle can carry.
        /// </summary>
        [DataMember(Name = "max_items_number", EmitDefaultValue = false)]
        public int? MaxItemsNumber { get; set; }

        /// <summary>
        ///     The maximum revenue an owner company can gain from a vehicle.
        /// </summary>
        [DataMember(Name = "max_revenue", EmitDefaultValue = false)]
        public double? MaxRevenue { get; set; }

        /// <summary>
        ///     When the capacity profile deleted
        /// </summary>
        [DataMember(Name = "deleted_at", EmitDefaultValue = false)]
        public string DeletedAt { get; set; }

        /// <summary>
        ///     When the capacity profile created
        /// </summary>
        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public string CreatedAt { get; set; }

        /// <summary>
        ///     When the capacity profile updated
        /// </summary>
        [DataMember(Name = "updated_at", EmitDefaultValue = false)]
        public string UpdatedAt { get; set; }

        /// <summary>
        ///     A unit in which maximum volume is measured (cu ft, cu m).
        /// </summary>
        [DataMember(Name = "max_volume_unit", EmitDefaultValue = false)]
        public string MaxVolumeUnit { get; set; }

        /// <summary>
        ///     A unit in which maximum weight is measured (kg, lb).
        /// </summary>
        [DataMember(Name = "max_weight_unit", EmitDefaultValue = false)]
        public string MaxWeightUnit { get; set; }
    }
}