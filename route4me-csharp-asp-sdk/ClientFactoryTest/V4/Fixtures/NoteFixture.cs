using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;
using Xunit;

namespace ClientFactoryTest.V4.Fixtures
{
    public class NoteFixture : IDisposable
    {
        static readonly string c_ApiKey = ApiKeys.ActualApiKey; // The optimizations with the Trucking, Multiple Depots, Multiple Drivers allowed only for business and higher account types --- put in the parameter an appropriate API key
        static readonly string c_ApiKey_1 = ApiKeys.DemoApiKey;

        public TestDataRepository tdr = new TestDataRepository();

        public List<string> removeOptimizationsId;

        public List<string> removeCustomNoteTypes;

        public int lastCustomNoteTypeID;

        public NoteFixture()
        {
            var r4me = new Route4MeManager(c_ApiKey);
            //hasCommercialCapabalities = r4me.MemberHasCommercialCapability(c_ApiKey, c_ApiKey_1, out string errorString);

            bool result = tdr.RunSingleDriverRoundTrip();
            Assert.True(result, "Single Driver Round Trip generation failed.");
            Assert.True((tdr.SDRT_route?.Addresses?.Length ?? 0) > 0, "The route has no addresses.");

            removeOptimizationsId = new List<string>();

            removeOptimizationsId.Add(tdr.SDRT_optimization_problem_id);

            string routeIdToMoveTo = tdr.SDRT_route_id;
            Assert.True(routeIdToMoveTo!=null, "routeId_SingleDriverRoundTrip is null.");

            int addressId = (tdr.DataObjectSDRT != null &&
                                tdr.SDRT_route != null &&
                                tdr.SDRT_route.Addresses.Length > 1 &&
                                tdr.SDRT_route.Addresses[1].RouteDestinationId != null)
                             ? tdr.SDRT_route.Addresses[1].RouteDestinationId.Value
                             : 0;

            double lat = tdr.SDRT_route.Addresses.Length > 1
                            ? tdr.SDRT_route.Addresses[1].Latitude
                            : 33.132675170898;
            double lng = tdr.SDRT_route.Addresses.Length > 1
                            ? tdr.SDRT_route.Addresses[1].Longitude
                            : -83.244743347168;

            var noteParameters = new NoteParameters()
            {
                RouteId = routeIdToMoveTo,
                AddressId = addressId,
                Latitude = lat,
                Longitude = lng,
                DeviceType = DeviceType.Web.Description(),
                ActivityType = StatusUpdateType.DropOff.Description(),
                Format = "json"
            };

            // Run the query
            string contents = "Test Note Contents " + DateTime.Now.ToString();
            AddressNote note = r4me.AddAddressNote(
                                            noteParameters,
                                            contents,
                                            out string errorString);

            Assert.True(note!=null, "AddAddressNoteTest failed. " + errorString);

            var response = r4me.GetAllCustomNoteTypes(out errorString);
            Assert.True(response.GetType() == typeof(CustomNoteType[]), errorString);

            removeCustomNoteTypes = new List<string>();

            if (((CustomNoteType[])response).Length < 2)
            {
                AddCustomNoteType("Conditions at Site",
                                  new string[] { "safe", "mild", "dangerous", "slippery" }
                );
                AddCustomNoteType("To Do",
                                  new string[] { "Pass a package", "Pickup package", "Do a service" }
                );

                removeCustomNoteTypes.Add("Conditions at Site");
                removeCustomNoteTypes.Add("To Do");

                response = r4me.GetAllCustomNoteTypes(out errorString);
            }

            Assert.True(((CustomNoteType[])response).Length > 0,
            "Can not find custom note type in the account. " + errorString);

            lastCustomNoteTypeID = ((CustomNoteType[])response)[((CustomNoteType[])response).Length - 1].NoteCustomTypeID;
        }

        public void Dispose()
        {
            if (removeOptimizationsId.Count > 0)
            {
                bool result = tdr.RemoveOptimization(removeOptimizationsId.ToArray());
                Assert.True(result, "Removing of the optimizations failed.");
            }

            tdr = null;
            removeOptimizationsId = null;
        }

        public Object AddCustomNoteType(string customType, string[] customValues)
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            // Run the query
            var response = route4Me.AddCustomNoteType(customType, customValues, out string errorString);

            return response ?? errorString;
        }
    }
}
