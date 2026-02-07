using System.Runtime.Serialization;

using Route4MeSDKLibrary.QueryTypes;

namespace Route4MeSDKLibrary.QueryTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Request for accepting a scenario
    /// </summary>
    [DataContract]
    public sealed class AcceptScenarioRequest : GenericParameters
    {
        /// <summary>
        /// Routes type: "regular" or "master"
        /// </summary>
        [DataMember(Name = "routes_type", EmitDefaultValue = false)]
        public string RoutesType { get; set; }
    }

    /// <summary>
    /// Request for completing scenario import
    /// </summary>
    [DataContract]
    public sealed class CompleteScenarioRequest : GenericParameters
    {
        /// <summary>
        /// Total created routes
        /// </summary>
        [DataMember(Name = "total_created_routes", EmitDefaultValue = false)]
        public int? TotalCreatedRoutes { get; set; }

        /// <summary>
        /// Total failed routes
        /// </summary>
        [DataMember(Name = "total_failed_routes", EmitDefaultValue = false)]
        public int? TotalFailedRoutes { get; set; }

        /// <summary>
        /// Is master routes flag
        /// </summary>
        [DataMember(Name = "is_master_routes", EmitDefaultValue = false)]
        public bool? IsMasterRoutes { get; set; }
    }

    /// <summary>
    /// Request for generating scenario with AI
    /// </summary>
    [DataContract]
    public sealed class GenerateScenarioRequest : GenericParameters
    {
        /// <summary>
        /// Prompt for generative AI
        /// </summary>
        [DataMember(Name = "prompt", EmitDefaultValue = false)]
        public string Prompt { get; set; }
    }

    /// <summary>
    /// Request for adding scenarios to an optimization
    /// </summary>
    [DataContract]
    public sealed class AddScenariosRequest : GenericParameters
    {
        /// <summary>
        /// Array of JSON objects containing scenario parameters
        /// </summary>
        [DataMember(Name = "paramsJson", EmitDefaultValue = false)]
        public Route4MeSDK.DataTypes.V5.StrategicPlanner.OptimizationParameters[] ParamsJson { get; set; }
    }
}
