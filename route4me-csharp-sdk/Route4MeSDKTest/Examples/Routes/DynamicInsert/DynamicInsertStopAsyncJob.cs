using System;
using System.Collections.Generic;
using System.Threading;

using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        ///     Dispatch an asynchronous (tracked-batch) dynamic insert lookup, poll the
        ///     module-level job-tracker status, then fetch the result. The result payload matches
        ///     the synchronous DynamicInsertRouteAddresses response.
        /// </summary>
        public void DynamicInsertStopAsyncJob()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();
            OptimizationsToRemove = new List<string>()
            {
                SD10Stops_optimization_problem_id
            };

            var dynamicInsertParams = new DynamicInsertRequest()
            {
                InsertMode = DynamicInsertMode.OptimalAfterLastVisited.Description(),
                ScheduledFor = DateTime.Now.Date.AddDays(1).ToString("yyyy-MM-dd"),
                Latitude = 33.1296514,
                Longitude = -83.2485687,
                LookupResultsLimit = 5,
                RecommendBy = DynamicInsertRecomendBy.Distance.Description(),
                MaxIncreasePercentAllowed = 500
            };

            // 1. Dispatch the job
            var dispatch = route4Me.DynamicInsertRouteAddressesJob(
                dynamicInsertParams,
                out ResultResponse resultResponse);

            if (dispatch?.JobId == null)
            {
                Console.WriteLine("DynamicInsertStopAsyncJob dispatch failed.");
                RemoveTestOptimizations();
                return;
            }

            Console.WriteLine($"Job dispatched: {dispatch.JobId}");
            Console.WriteLine($"  status_url: {dispatch.StatusUrl}");
            Console.WriteLine($"  result_url: {dispatch.ResultUrl}");

            // 2. Poll the module-level status endpoint until the job is terminal
            DynamicInsertJobResult jobResult = null;

            for (var attempt = 0; attempt < 30; attempt++)
            {
                route4Me.GetDynamicInsertJobStatus(dispatch.JobId, out resultResponse);

                // The result endpoint returns 200 with the payload once the job is terminal.
                jobResult = route4Me.GetDynamicInsertJobResult(dispatch.JobId, out resultResponse);

                if (jobResult?.Result != null)
                {
                    break;
                }

                Thread.Sleep(2000);
            }

            // 3. Report the result
            if (jobResult?.Result == null)
            {
                Console.WriteLine("DynamicInsertStopAsyncJob did not produce a result in time.");
            }
            else
            {
                Console.WriteLine("The address can be inserted at the routes: ");

                foreach (var route in jobResult.Result)
                {
                    Console.WriteLine($"     {route.RouteId}");
                }
            }

            RemoveTestOptimizations();
        }
    }
}
