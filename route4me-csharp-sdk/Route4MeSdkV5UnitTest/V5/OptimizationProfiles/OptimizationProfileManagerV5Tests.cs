using NUnit.Framework;
using Route4MeSDKLibrary.DataTypes.V5.OptimizationProfiles;
using Route4MeSDKLibrary.Managers;

namespace Route4MeSdkV5UnitTest.V5.OptimizationProfiles
{
    [TestFixture]
    public class OptimizationProfileManagerV5Tests
    {
        private static readonly string CApiKey = ApiKeys.ActualApiKey;

        [Test]
        public void GetGetOptimizationProfilesTest()
        {
            var route4Me = new OptimizationProfileManagerV5(CApiKey);

            var optimizationProfiles = route4Me.GetOptimizationProfiles(out _);

            Assert.That(optimizationProfiles.GetType(), Is.EqualTo(typeof(OptimizationProfilesResponse)));
        }
    }
}
