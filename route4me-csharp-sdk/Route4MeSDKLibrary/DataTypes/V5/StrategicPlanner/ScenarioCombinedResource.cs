using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Scenario combined resource (same as ScenarioResource but used in combined list context)
    /// </summary>
    [DataContract]
    public sealed class ScenarioCombinedResource : ScenarioResource
    {
        // This class inherits all properties from ScenarioResource
        // It's used in combined list responses for consistency with API structure
    }
}
