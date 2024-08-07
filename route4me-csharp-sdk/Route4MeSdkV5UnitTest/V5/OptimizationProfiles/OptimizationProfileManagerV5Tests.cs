using System.Linq;
using Newtonsoft.Json.Linq;
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

        [Test]
        public void SaveEntitiesTest()
        {
            var route4Me = new OptimizationProfileManagerV5(CApiKey);

            var result = route4Me.SaveEntities(
                new OptimizationProfileSaveEntities()
                {
                    Items = new OptimizationProfileSaveEntitiesItem[1]
                    {
                        new()
                        {
                            Guid = "eaa", Id = "f09e3d22-c1d6-473c-8494-41563034b85b",
                            Parts = new OptimizationProfileSaveEntitiesItemPart[1]
                            {
                                new()
                                {
                                    Guid = "pav",
                                    Data = JObject.Parse("{\"append_date_to_route_name\":true}")
                                }
                            }
                        }
                    }
                }, out var resultResponse);

            Assert.That(result.GetType(), Is.EqualTo(typeof(OptimizationProfileSaveEntities)));
        }

        [Test]
        public void DeleteEntitiesTest()
        {
            var route4Me = new OptimizationProfileManagerV5(CApiKey);

            var saveResult = route4Me.SaveEntities(
                new OptimizationProfileSaveEntities()
                {
                    Items = new OptimizationProfileSaveEntitiesItem[1]
                    {
                        new()
                        {
                            Guid = "eaa", Id = "f09e3d22-c1d6-473c-8494-41563034b85b",
                            Parts = new OptimizationProfileSaveEntitiesItemPart[1]
                            {
                                new()
                                {
                                    Guid = "pav",
                                    Data = JObject.Parse("{\"append_date_to_route_name\":true}")
                                }
                            }
                        }
                    }
                }, out var resultResponse);


            var result = route4Me.DeleteEntities(new OptimizationProfileDeleteEntitiesRequest()
            {
                Items = new OptimizationProfileDeleteEntitiesRequestItem[1]
                {
                    new OptimizationProfileDeleteEntitiesRequestItem() { Id = saveResult.Items.First().Id }
                }
            }, out resultResponse);

            Assert.That(result.GetType(), Is.EqualTo(typeof(OptimizationProfileSaveEntities)));
        }
    }
}
