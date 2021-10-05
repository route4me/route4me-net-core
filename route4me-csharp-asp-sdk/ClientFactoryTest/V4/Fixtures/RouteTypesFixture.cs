using Route4MeSDK.DataTypes;
using System;
using System.Collections.Generic;
using Xunit;

namespace ClientFactoryTest.V4.Fixtures
{
    public class RouteTypesFixture : FactAttribute, IDisposable
    {
        static readonly string c_ApiKey = ApiKeys.ActualApiKey; // The optimizations with the Trucking, Multiple Depots, Multiple Drivers allowed only for business and higher account types --- put in the parameter an appropriate API key
        static readonly string c_ApiKey_1 = ApiKeys.DemoApiKey;

        public TestDataRepository tdr = new TestDataRepository();

        public DataObject dataObject, dataObjectMDMD24;

        public bool hasCommercialCapabalities;

        public List<string> removeOptimizationsId;

        public RouteTypesFixture()
        {
            var r4me = new Route4MeSDK.Route4MeManager(c_ApiKey);
            hasCommercialCapabalities = r4me.MemberHasCommercialCapability(c_ApiKey, c_ApiKey_1, out string errorString);

            removeOptimizationsId = new List<string>();
        }


        public void Dispose()
        {
            if (removeOptimizationsId.Count>0)
            {
                bool result = tdr.RemoveOptimization(removeOptimizationsId.ToArray());
                dataObjectMDMD24 = null;
                dataObject = null;
                tdr = null;
                Assert.True(result, "Removing of the optimizations failed.");
            }

            //if ((dataObjectMDMD24?.OptimizationProblemId ?? null) != null) removeOptIDs.Add(dataObjectMDMD24.OptimizationProblemId);
            //if ((dataObject?.OptimizationProblemId ?? null) != null) removeOptIDs.Add(dataObject.OptimizationProblemId);

            //if (removeOptIDs.Count > 0)
            //{
            //    bool result = tdr.RemoveOptimization(removeOptIDs.ToArray());
            //    dataObjectMDMD24 = null;
            //    dataObject = null;
            //    tdr = null;
            //    Assert.True(result, "Removing of the optimization with 24 stops failed.");
            //}
        }
    }
}
