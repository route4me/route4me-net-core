using Route4MeSDK;

namespace Route4MeSDKUnitTest.Types
{
    public class ApiKeys
    {
        public static string ActualApiKey = R4MeUtils.ReadSetting("actualApiKey");
        public static string DemoApiKey = R4MeUtils.ReadSetting("demoApiKey");
    }
}