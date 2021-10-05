using ClientFactoryTest.IgnoreTests;
using System;
using System.Collections.Generic;
using Xunit;

namespace ClientFactoryTest.V4.Fixtures
{
    public class OptimizationFixture : IDisposable
    {
        public TestDataRepository tdr = new TestDataRepository();

        public List<string> removeOptimizationsId;

        public OptimizationFixture()
        {
            removeOptimizationsId = new List<string>();

            if (ApiKeys.ActualApiKey != ApiKeys.DemoApiKey)
            {
                bool result = tdr.RunOptimizationSingleDriverRoute10Stops();

                if (result) removeOptimizationsId.Add(tdr.SD10Stops_optimization_problem_id);

                bool result2 = tdr.RunSingleDriverRoundTrip();
                if (result2) removeOptimizationsId.Add(tdr.SDRT_optimization_problem_id);
            }

            //this.CheckApiKey(ApiKeys.ActualApiKey, ApiKeys.DemoApiKey);
        }

        public void Dispose()
        {
            if (ApiKeys.ActualApiKey != ApiKeys.DemoApiKey && removeOptimizationsId.Count > 0)
            {
                bool result = tdr.RemoveOptimization(removeOptimizationsId.ToArray());
                
                Assert.True(result, "Removing of the optimizations failed.");
            }

            tdr = null;
        }
    }
}
