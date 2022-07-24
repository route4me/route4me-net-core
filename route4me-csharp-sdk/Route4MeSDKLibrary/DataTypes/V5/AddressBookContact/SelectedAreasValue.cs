using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.AddressBookContact
{
    /// <summary>
    /// Base class for selected area value
    /// </summary>
    [DataContract]
    [KnownType(typeof(SelectedAreasValueCircle))]
    [KnownType(typeof(SelectedAreasValuePolygon))]
    [KnownType(typeof(SelectedAreasValueRect))]
    public abstract class SelectedAreasValue
    {
    }
}