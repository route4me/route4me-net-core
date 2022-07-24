using System.ComponentModel;

namespace Route4MeSDKLibrary.DataTypes
{
    /// <summary>
    /// </summary>
    public enum SelectedAreasType : uint
    {
        [Description("circle")] Circle,

        [Description("polygon")] Polygon,

        [Description("rect")] Rect
    }
}