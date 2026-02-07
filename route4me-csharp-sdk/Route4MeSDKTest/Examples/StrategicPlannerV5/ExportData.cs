using System;
using System.Threading;

using Route4MeSDKLibrary.QueryTypes.V5.StrategicPlanner;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Example of exporting optimization data to CSV
        /// </summary>
        public void ExportOptimizationData()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            // 1. Get available export columns
            Console.WriteLine("Getting available export columns...");
            var columns = route4Me.StrategicPlanner.GetExportColumns("optimizations", null, out var resultResponse);

            if (resultResponse == null && columns != null)
            {
                Console.WriteLine($"Available columns: {columns.Length}");
                foreach (var column in columns)
                {
                    if (column.Allowed == true)
                    {
                        Console.WriteLine($"  - {column.FieldTitle} ({column.FieldName})");
                    }
                }
            }

            // 2. Submit export job
            Console.WriteLine("\nSubmitting export job...");
            var exportRequest = new ExportOptimizationsRequest
            {
                Format = "csv",
                Columns = new[] { "name", "locations_count", "scenarios_count", "created" },
                Filters = new StrategicOptimizationFilters
                {
                    SearchQuery = ""
                },
                Page = 1,
                PerPage = 100
            };

            var exportJob = route4Me.StrategicPlanner.ExportOptimizations(exportRequest, out resultResponse);

            if (resultResponse != null && !resultResponse.Status)
            {
                Console.WriteLine("Failed to submit export: " + 
                    string.Join(", ", resultResponse.Messages?.Values ?? Array.Empty<string[]>()));
                return;
            }

            Console.WriteLine($"Export job submitted! Job ID: {exportJob.JobId}");

            // 3. Check export status
            Console.WriteLine("\nChecking export status...");
            var maxAttempts = 30;
            var attempt = 0;

            while (attempt < maxAttempts)
            {
                Thread.Sleep(2000); // Wait 2 seconds between checks

                var status = route4Me.StrategicPlanner.GetExportStatus(exportJob.JobId.Value, out resultResponse);

                if (status != null)
                {
                    Console.WriteLine($"Status: {status.Status}");

                    if (status.Status == "finished")
                    {
                        Console.WriteLine("Export completed successfully!");
                        
                        // 4. Download the file
                        Console.WriteLine("\nDownloading export file...");
                        var fileContent = route4Me.StrategicPlanner.DownloadExport(exportJob.JobId.Value, out resultResponse);

                        if (resultResponse == null && fileContent != null)
                        {
                            var fileName = status.Filename ?? "export.csv";
                            System.IO.File.WriteAllBytes(fileName, fileContent);
                            Console.WriteLine($"File downloaded: {fileName} ({fileContent.Length} bytes)");
                        }
                        
                        break;
                    }
                    else if (status.Status == "failed")
                    {
                        Console.WriteLine("Export failed!");
                        break;
                    }
                }

                attempt++;
            }

            if (attempt >= maxAttempts)
            {
                Console.WriteLine("Export timed out");
            }
        }

        /// <summary>
        /// Example of exporting optimizations with nested entities
        /// </summary>
        public void ExportNestedOptimizationData()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            // Get totals first
            Console.WriteLine("Getting export totals...");
            var totalsRequest = new NestedOptimizationsExportRequest
            {
                Optimizations = new ExportOptimizationsRequest
                {
                    Filters = new StrategicOptimizationFilters
                    {
                        SearchQuery = ""
                    }
                },
                Scenarios = new ExportScenariosRequest
                {
                    Filters = new ScenarioFilters()
                },
                Routes = new ExportRoutesRequest
                {
                    Filters = new RouteFilters()
                },
                Visits = new ExportVisitsRequest
                {
                    Filters = new VisitFilters()
                },
                Locations = new ExportLocationsRequest
                {
                    Filters = new LocationFilters()
                }
            };

            var totals = route4Me.StrategicPlanner.GetNestedOptimizationsExportTotals(totalsRequest, out var resultResponse);

            if (resultResponse == null)
            {
                Console.WriteLine("Export will include:");
                // Note: totals.Totals is a dynamic object, cast appropriately
                Console.WriteLine("  Optimization totals retrieved");
            }

            // Submit nested export
            Console.WriteLine("\nSubmitting nested export job...");
            var exportJob = route4Me.StrategicPlanner.ExportOptimizationsNested(totalsRequest, out resultResponse);

            if (resultResponse != null && !resultResponse.Status)
            {
                Console.WriteLine("Failed to submit export: " + 
                    string.Join(", ", resultResponse.Messages?.Values ?? Array.Empty<string[]>()));
                return;
            }

            Console.WriteLine($"Nested export job submitted! Job ID: {exportJob.JobId}");
        }
    }
}
