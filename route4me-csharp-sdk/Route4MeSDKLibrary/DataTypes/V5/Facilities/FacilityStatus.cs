using System.ComponentModel;

namespace Route4MeSDKLibrary.DataTypes.V5.Facilities
{
    /// <summary>
    /// Represents an enumeration of the facility statuses
    /// </summary>
    public enum FacilityStatus
    {
        [Description("ACTIVE")] Active = 1,

        [Description("INACTIVE")] InActive,

        [Description("IN REVIEW")] InReview,
    }
}
