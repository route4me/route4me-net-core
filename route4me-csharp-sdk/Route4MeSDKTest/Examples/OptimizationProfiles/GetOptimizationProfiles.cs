using Route4MeSDKLibrary.Managers;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void GetOptimizationProfiles()
        {
            var route4Me = new OptimizationProfileManagerV5(ActualApiKey);

            var optimizationProfiles = route4Me.GetOptimizationProfiles(out _);
        }
    }
}