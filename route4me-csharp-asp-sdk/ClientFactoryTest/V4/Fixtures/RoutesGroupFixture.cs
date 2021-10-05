using System;
using System.Collections.Generic;
using Xunit;

namespace ClientFactoryTest.V4.Fixtures
{
    public class RoutesGroupFixture : IDisposable
    {
        static readonly string c_ApiKey = ApiKeys.ActualApiKey; // The optimizations with the Trucking, Multiple Depots, Multiple Drivers allowed only for business and higher account types --- put in the parameter an appropriate API key
        static readonly string c_ApiKey_1 = ApiKeys.DemoApiKey;

        public TestDataRepository tdr = new TestDataRepository();
        public TestDataRepository tdr2 = new TestDataRepository();

        static List<string> removeOptimizationsId;
        //static List<string> removeVehicleIDs;

        public bool hasCommercialCapabalities;

        public RoutesGroupFixture()
        {
            removeOptimizationsId = new List<string>();
            //removeVehicleIDs = new List<string>();

            bool result = tdr.RunOptimizationSingleDriverRoute10Stops();
            bool result2 = tdr2.RunOptimizationSingleDriverRoute10Stops();
            bool result3 = tdr2.RunSingleDriverRoundTrip();

            Assert.True(result, "Single Driver 10 Stops generation failed.");
            Assert.True(result2, "Single Driver 10 Stops generation failed.");

            Assert.True(tdr.SD10Stops_route.Addresses.Length > 0, "The route has no addresses.");
            Assert.True(tdr2.SD10Stops_route.Addresses.Length > 0, "The route has no addresses.");

            removeOptimizationsId.Add(tdr.SD10Stops_optimization_problem_id);
            removeOptimizationsId.Add(tdr2.SD10Stops_optimization_problem_id);
            removeOptimizationsId.Add(tdr2.SDRT_optimization_problem_id);

            bool result1 = tdr.RunSingleDriverRoundTrip();

            Assert.True(result1, "Single Driver Round Trip generation failed.");

            Assert.True(tdr.SDRT_route.Addresses.Length > 0, "The route has no addresses.");

            removeOptimizationsId.Add(tdr.SDRT_optimization_problem_id);
        }

        public void Dispose()
        {
            if (removeOptimizationsId.Count > 0)
            {
                bool result = tdr.RemoveOptimization(removeOptimizationsId.ToArray());
                Assert.True(result, "Removing of the optimizations failed.");
            }

            tdr = null;
            tdr2 = null;
            removeOptimizationsId = null;
            //removeVehicleIDs = null;
        }
    }
}
