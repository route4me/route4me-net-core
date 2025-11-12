using System.Runtime.Serialization;

using Route4MeSDK.DataTypes;

namespace Route4MeSDKUnitTest.Types
{
    [DataContract]
    internal class MyDataObjectGeneric
    {
        [DataMember(Name = "optimization_problem_id")]
        public string OptimizationProblemId { get; set; }

        [DataMember(Name = "state")] public int MyState { get; set; }

        [DataMember(Name = "addresses")] public Address[] Addresses { get; set; }

        [DataMember(Name = "parameters")] public RouteParameters Parameters { get; set; }
    }
}