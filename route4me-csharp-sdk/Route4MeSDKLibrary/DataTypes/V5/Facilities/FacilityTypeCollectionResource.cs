using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Facilities
{
    /// <summary>
    /// This is just an alias for array of FacilityTypeResource
    /// The API returns an array directly, not wrapped in an object
    /// </summary>
    public class FacilityTypeCollectionResource : System.Collections.Generic.List<FacilityTypeResource>
    {
    }
}
