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
        ///     Find the best position for a new stop with an asynchronous job:
        ///     dispatch the lookup, poll the job status, then read the job result.
        ///     Use it when the lookup covers many routes and the synchronous call may time out.
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
                RouteIds = new[] { SD10Stops_route_id },
                Latitude = 33.1296514,
                Longitude = -83.2485687,
                InsertMode = DynamicInsertMode.OptimalAfterLastVisited.Description(),
                RecommendBy = DynamicInsertRecomendBy.Distance.Description(),
                TimeWindowStart = 9 * 3600,
                TimeWindowEnd = 17 * 3600
            };

            // 1. Dispatch the job. The API answers immediately with a job ID.
            var job = route4Me.DispatchDynamicInsertRouteAddresses(dynamicInsertParams, out ResultResponse resultResponse);

            if (job == null)
            {
                PrintFailResponse(resultResponse, "DynamicInsertStopAsyncJob");
                RemoveTestOptimizations();
                return;
            }

            Console.WriteLine($"Job {job.JobId} dispatched.");

            // 2. Poll the status once per second, for up to 30 seconds, while the job is processing.
            var status = route4Me.GetDynamicInsertJobStatus(job.JobId, out _);

            for (var i = 0; i < 30 && status?.Status == "processing"; i++)
            {
                Thread.Sleep(1000);
                status = route4Me.GetDynamicInsertJobStatus(job.JobId, out _);
            }

            // "processed" when the job is done; "failed" or "canceled" otherwise.
            Console.WriteLine($"Job status: {status?.Status}");

            // 3. Read the result: the same insertion options as the synchronous lookup.
            var result = route4Me.GetDynamicInsertJobResult(job.JobId, out resultResponse);

            if (result?.Result == null)
            {
                PrintFailResponse(resultResponse, "DynamicInsertStopAsyncJob");
            }
            else
            {
                foreach (var route in result.Result)
                {
                    Console.WriteLine($"Route {route.RouteId}: insert at stop {route.RecommendedInsertionStopNumber}, " +
                                      $"distance {route.OldDistance} -> {route.NewDistance}, " +
                                      $"late stops: {route.TimeWindowViolations?.Count ?? 0}");
                }
            }

            RemoveTestOptimizations();
        }
    }
}