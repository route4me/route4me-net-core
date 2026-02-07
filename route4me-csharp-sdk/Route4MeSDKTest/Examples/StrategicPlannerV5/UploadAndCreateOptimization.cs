using System;

using Route4MeSDK.DataTypes.V5.StrategicPlanner;
using Route4MeSDKLibrary.QueryTypes.V5.StrategicPlanner;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Complete example of uploading a CSV file and creating a strategic optimization
        /// </summary>
        public void UploadAndCreateOptimization()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            // Step 1: Upload CSV file
            Console.WriteLine("Step 1: Uploading CSV file...");
            var filePath = "path/to/your/customers.csv"; // Replace with actual file path

            var uploadResult = route4Me.StrategicPlanner.UploadFile(filePath, out var resultResponse);

            if (resultResponse != null && !resultResponse.Status)
            {
                Console.WriteLine("Failed to upload file: " +
                    string.Join(", ", resultResponse.Messages?.Values ?? Array.Empty<string[]>()));
                return;
            }

            Console.WriteLine($"File uploaded successfully! Upload ID: {uploadResult.UploadId}");
            Console.WriteLine($"Available encodings: {uploadResult.Encodings?.Length ?? 0}");

            // Step 2: Preview the uploaded file
            Console.WriteLine("\nStep 2: Previewing uploaded data...");
            var previewRequest = new UploadPreviewRequest
            {
                StrUploadID = uploadResult.UploadId,
                Limit = 10,
                Sheet = 0,
                IntFromEncodingIndex = 0,
                ArrValidCSVColumns = new[] { "address", "alias", "lat", "lng" }
            };

            var preview = route4Me.StrategicPlanner.GetUploadPreview(previewRequest, out resultResponse);

            if (resultResponse == null)
            {
                Console.WriteLine($"Found {preview.AddressCount} addresses");
                Console.WriteLine($"Headers: {string.Join(", ", preview.CsvHeader ?? Array.Empty<string>())}");

                if (preview.Warnings != null)
                {
                    Console.WriteLine("Warnings found in the data - review before proceeding");
                }
            }

            // Step 3: Create optimization from uploaded file
            Console.WriteLine("\nStep 3: Creating optimization...");
            var createRequest = new CreateOptimizationRequest
            {
                StrUploadID = uploadResult.UploadId,
                FileName = "customers.csv",
                Name = "Monthly Delivery Optimization " + DateTime.Now.ToString("yyyyMMddHHmmss"),
                ParamsJson = new[]
                {
                    new OptimizationParameters
                    {
                        SchedulerName = "Weekly Schedule",
                        Scheduler = new SchedulerConfiguration
                        {
                            Cycles = 4,
                            CycleLength = 7,
                            StartDate = DateTime.Today.ToString("yyyy-MM-dd"),
                            BlackoutDays = new[] { "sun" }
                        },
                        Timezone = "America/New_York",
                        RouteTime = 28800, // 8:00 AM
                        Optimize = "Distance",
                        DistanceUnit = "mi",
                        TravelMode = "Driving",
                        VehicleCapacity = 100
                    }
                },
                DepotAddresses = new[]
                {
                    new DepotAddress
                    {
                        Address = "Main Depot, 789 Warehouse Blvd, New York, NY",
                        Lat = 40.7589,
                        Lng = -73.9851
                    }
                },
                ArrValidCSVColumns = new[] { "address", "alias", "lat", "lng" },
                IsUi = false
            };

            var optimization = route4Me.StrategicPlanner.CreateOptimizationFromUpload(createRequest, out resultResponse);

            if (resultResponse != null && !resultResponse.Status)
            {
                Console.WriteLine("Failed to create optimization: " +
                    string.Join(", ", resultResponse.Messages?.Values ?? Array.Empty<string[]>()));
                return;
            }

            Console.WriteLine("Optimization created successfully!");
            Console.WriteLine($"Optimization ID: {optimization.OptimizationId}");
            Console.WriteLine("Scenarios are being generated in the background...");
        }

        /// <summary>
        /// Example of uploading and creating optimization asynchronously
        /// </summary>
        public async void UploadAndCreateOptimizationAsync()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var filePath = "path/to/your/customers.csv";

            // Step 1: Upload
            var (uploadResult, resultResponse) = await route4Me.StrategicPlanner.UploadFileAsync(filePath);

            if (resultResponse != null && !resultResponse.Status)
            {
                Console.WriteLine("Upload failed");
                return;
            }

            Console.WriteLine($"Upload ID: {uploadResult.UploadId}");

            // Step 2: Preview
            var previewRequest = new UploadPreviewRequest
            {
                StrUploadID = uploadResult.UploadId,
                Limit = 10
            };

            var (preview, previewResponse) = await route4Me.StrategicPlanner.GetUploadPreviewAsync(previewRequest);

            if (previewResponse == null)
            {
                Console.WriteLine($"Preview shows {preview.AddressCount} addresses");
            }

            // Step 3: Create optimization
            var createRequest = new CreateOptimizationRequest
            {
                StrUploadID = uploadResult.UploadId,
                FileName = "customers.csv",
                Name = "Optimization " + DateTime.Now.ToString("yyyyMMddHHmmss"),
                ParamsJson = new[]
                {
                    new OptimizationParameters
                    {
                        SchedulerName = "Weekly",
                        Scheduler = new SchedulerConfiguration
                        {
                            Cycles = 4,
                            CycleLength = 7,
                            StartDate = DateTime.Today.ToString("yyyy-MM-dd")
                        },
                        Timezone = "America/New_York"
                    }
                }
            };

            var (optimization, createResponse) = await route4Me.StrategicPlanner.CreateOptimizationFromUploadAsync(createRequest);

            if (createResponse == null)
            {
                Console.WriteLine($"Optimization created: {optimization.OptimizationId}");
            }
        }
    }
}
