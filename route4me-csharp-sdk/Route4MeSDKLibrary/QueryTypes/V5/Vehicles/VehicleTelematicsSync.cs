using System.Runtime.Serialization;

using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.QueryTypes.V5
{
    public sealed class VehicleTelematicsSync : Vehicle
    {
        /// <summary>
        ///     Telematics gateway connection ID
        /// </summary>
        [DataMember(Name = "r4m_telematics_gateway_connection_id", EmitDefaultValue = false)]
        public int? TelematicsGatewayCnnectionId { get; set; }

        /// <summary>
        ///     Telematics gateway vehicle ID
        /// </summary>
        [DataMember(Name = "r4m_telematics_gateway_vehicle_id", EmitDefaultValue = false)]
        public int? TelematicsGatewayVehicleId { get; set; }
    }
}