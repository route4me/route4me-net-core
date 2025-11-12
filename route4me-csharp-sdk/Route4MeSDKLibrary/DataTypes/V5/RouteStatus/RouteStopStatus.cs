using System.ComponentModel;

namespace Route4MeSDKLibrary.DataTypes.V5.RouteStatus
{
    public enum RouteStopStatus
    {
        [Description("Skipped")] Skipped,
        [Description("Completed")] Completed,
        [Description("Failed")] Failed,
        [Description("Empty")] Empty
    }
}