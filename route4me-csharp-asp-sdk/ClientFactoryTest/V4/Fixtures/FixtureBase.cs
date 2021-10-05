using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.TestHost;
using Route4MeSDK.Controllers;
using Route4MeSDK.DataTypes;
using Route4MeSDK.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Route4MeSDK.Services.Route4MeApi4Service;

namespace ClientFactoryTest.V4.Fixtures
{
    public class FixtureBase : IDisposable
    {
        //public Route4MeApi4Controller r4mController;

        //static string skip;
        static readonly string c_ApiKey = ApiKeys.ActualApiKey; // The optimizations with the Trucking, Multiple Depots, Multiple Drivers allowed only for business and higher account types --- put in the parameter an appropriate API key
        static readonly string c_ApiKey_1 = ApiKeys.DemoApiKey;

        public TestDataRepository tdr = new TestDataRepository();

        public DataObject dataObject, dataObjectMDMD24;

        public bool hasCommercialCapabalities;

        public FixtureBase()
        {
            var r4me = new Route4MeSDK.Route4MeManager(c_ApiKey);
            hasCommercialCapabalities = r4me.MemberHasCommercialCapability(c_ApiKey, c_ApiKey_1, out string errorString);
        }

        public void Dispose()
        {
            var removeOptIDs = new List<string>();
            if ((dataObjectMDMD24?.OptimizationProblemId ?? null) != null) removeOptIDs.Add(dataObjectMDMD24.OptimizationProblemId);
            if ((dataObject?.OptimizationProblemId ?? null) != null) removeOptIDs.Add(dataObject.OptimizationProblemId);

            if (removeOptIDs.Count > 0)
            {
                bool result = tdr.RemoveOptimization(removeOptIDs.ToArray());

                Assert.True(result, "Removing of the optimization with 24 stops failed.");
            }
        }
    }
}
