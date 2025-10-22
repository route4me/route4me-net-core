using System;
using Route4MeSDKLibrary.QueryTypes.V5.Locations;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Export locations to a file (CSV, XLS, or XLSX).
        /// This example demonstrates the two-step process:
        /// 1. Get available export columns
        /// 2. Export locations with selected columns
        /// Note: Export is an asynchronous operation that returns a job ID.
        /// </summary>
        public void ExportLocations()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            Console.WriteLine("=== Location Export Example ===\n");

            // Step 1: Get available export columns
            Console.WriteLine("1. Getting available export columns...");
            var columnsRequest = new LocationExportColumnsRequest
            {
                Filters = new LocationFilters()
            };

            var columns = route4Me.GetLocationsExportColumns(columnsRequest, out var columnsResponse);

            if (columnsResponse != null && !columnsResponse.Status)
            {
                Console.WriteLine($"Error getting export columns: {string.Join(", ", columnsResponse.Messages)}");
                return;
            }

            if (columns != null && columns.Length > 0)
            {
                Console.WriteLine($"  Found {columns.Length} available columns:");
                foreach (var column in columns)
                {
                    Console.WriteLine($"    - {column.FieldTitle} ({column.FieldName})");
                    if (!string.IsNullOrEmpty(column.Group))
                    {
                        Console.WriteLine($"      Group: {column.Group}, Scope: {column.Scope}, Allowed: {column.Allowed}");
                    }
                }
            }
            Console.WriteLine();

            // Step 2: Export locations with selected columns
            Console.WriteLine("2. Initiating location export...");
            var exportRequest = new LocationExportRequest
            {
                Filters = new LocationFilters
                {
                    SearchQuery = "" // Export all locations, or add filters to export specific subset
                },
                Format = "csv", // Options: "csv", "xls", "xlsx"
                Filename = "locations_export_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"),
                Columns = new[]
                {
                    "address_id",
                    "address_1",
                    "address_alias",
                    "lat",
                    "lng",
                    "cached_lat",
                    "cached_lng"
                },
                Page = 1,
                PerPage = 100,
                Timezone = "America/New_York" // Optional: format dates/times in specific timezone
            };

            var result = route4Me.ExportLocations(exportRequest, out var exportResponse);

            if (exportResponse != null && !exportResponse.Status)
            {
                Console.WriteLine($"Error initiating export: {string.Join(", ", exportResponse.Messages)}");
                return;
            }

            // Export is asynchronous - it returns a 202 Accepted with a job ID
            Console.WriteLine("  Export job initiated successfully");
            Console.WriteLine($"  Format: {exportRequest.Format}");
            Console.WriteLine($"  Filename: {exportRequest.Filename}");
            Console.WriteLine($"  Columns: {string.Join(", ", exportRequest.Columns)}");
            Console.WriteLine();
            Console.WriteLine("  Note: Export is an asynchronous operation.");
            Console.WriteLine("  The file will be generated in the background.");
            Console.WriteLine("  Check your Route4Me account for the exported file.");

            Console.WriteLine("\n=== Export Example Complete ===");
        }
    }
}
