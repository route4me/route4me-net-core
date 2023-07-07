using System.ComponentModel;

namespace Route4MeSDKLibrary.DataTypes.V5.RouteStatus
{
    public enum RouteStatus : uint
    {
        [Description("planned")] Planned,

        [Description("started")] Started,

        [Description("paused")] Paused,

        [Description("completed")] Completed
    }
}