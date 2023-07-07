using System.ComponentModel;

namespace Route4MeSDKLibrary.DataTypes
{
    /// <summary>
    /// Display values
    /// </summary>
    public enum DisplayValues : uint
    {
        [Description("all")] All,

        [Description("routed")] Routed,

        [Description("unrouted ")] Unrouted
    }
}