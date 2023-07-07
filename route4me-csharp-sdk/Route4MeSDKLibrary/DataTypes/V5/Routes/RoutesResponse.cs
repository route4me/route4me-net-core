using System.Runtime.Serialization;
using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDKLibrary.DataTypes.V5.Routes
{
    [DataContract]
    public class RoutesResponse
    {
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public DataObjectRoute[] Data { get; set; }
    }
}
