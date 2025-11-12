using System.Linq;

using Newtonsoft.Json.Linq;

using Route4MeSDKLibrary.DataTypes.V5.OptimizationProfiles;
using Route4MeSDKLibrary.Managers;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void OptimizationProfileEntities()
        {
            var route4Me = new OptimizationProfileManagerV5(ActualApiKey);

            var data = new JObject();
            data["append_date_to_route_name"] = true;

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
                                    Data = data
                                }
                            }
                        }
                    }
                }, out var resultResponse);


            var deleteResult = route4Me.DeleteEntities(new OptimizationProfileDeleteEntitiesRequest()
            {
                Items = new OptimizationProfileDeleteEntitiesRequestItem[1]
                {
                    new() { Id = saveResult.Items.First().Id }
                }
            }, out resultResponse);
        }
    }
}