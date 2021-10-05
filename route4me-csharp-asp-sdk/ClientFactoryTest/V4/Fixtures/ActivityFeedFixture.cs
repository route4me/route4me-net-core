using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ClientFactoryTest.V4.Fixtures
{
    public class ActivityFeedFixture : IDisposable
    {
        public TestDataRepository tdr = new TestDataRepository();

        public List<string> removeOptimizationsId;

        public string memberId;

        public ActivityFeedFixture()
        {
            removeOptimizationsId = new List<string>();

            bool result = tdr.RunOptimizationSingleDriverRoute10Stops();

            if (result) removeOptimizationsId.Add(tdr.SD10Stops_optimization_problem_id);

            var r4me = new Route4MeSDK.Route4MeManager(ApiKeys.ActualApiKey);

            var response = r4me.GetUsers(new GenericParameters(), out string userErrorString);

            if ((response?.Results?.Length ?? 0) > 0) memberId = response.Results[0].MemberId;
        }

        public void Dispose()
        {
            if (removeOptimizationsId.Count > 0)
            {
                bool result = tdr.RemoveOptimization(removeOptimizationsId.ToArray());

                tdr = null;
                Assert.True(result, "Removing of the optimizations failed.");
            }
        }
    }
}
