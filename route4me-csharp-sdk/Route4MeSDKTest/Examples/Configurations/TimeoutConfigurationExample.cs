using System;

using Route4MeSDKLibrary;

namespace Route4MeSDK.Examples
{
    /// <summary>
    ///     Example demonstrating how to configure the Route4Me SDK globally.
    /// </summary>
    public sealed partial class Route4MeExamples
    {
        public void TimeoutConfigurationExample()
        {
            Console.WriteLine("=== Route4Me SDK Configuration Examples ===\n");

            // Example 1: Check default timeout
            Console.WriteLine($"Default HTTP timeout: {Route4MeConfig.HttpTimeout.TotalSeconds} seconds");

            // Example 2: Set a custom timeout (60 seconds)
            Console.WriteLine("\nSetting custom timeout to 60 seconds...");
            Route4MeConfig.HttpTimeout = TimeSpan.FromSeconds(60);
            Console.WriteLine($"New HTTP timeout: {Route4MeConfig.HttpTimeout.TotalSeconds} seconds");

            // Example 3: Set timeout using minutes
            Console.WriteLine("\nSetting timeout to 2 minutes...");
            Route4MeConfig.HttpTimeout = TimeSpan.FromMinutes(2);
            Console.WriteLine($"New HTTP timeout: {Route4MeConfig.HttpTimeout.TotalMinutes} minutes");

            // Example 4: Reset to default (30 seconds)
            Console.WriteLine("\nResetting to default timeout (30 seconds)...");
            Route4MeConfig.HttpTimeout = TimeSpan.FromSeconds(30);
            Console.WriteLine($"HTTP timeout: {Route4MeConfig.HttpTimeout.TotalSeconds} seconds");

            Console.WriteLine("\n=== Important Notes ===");
            Console.WriteLine("- Set the timeout BEFORE making any API calls");
            Console.WriteLine("- The timeout applies to all subsequent HTTP requests");
            Console.WriteLine("- Default timeout is 30 seconds");
            Console.WriteLine("- Increase for long-running operations (e.g., large optimizations)");
            Console.WriteLine("- Decrease for faster failure detection in time-sensitive scenarios");
        }

        public void ErrorHandlingConfigurationExample()
        {
            Console.WriteLine("=== Route4Me SDK Error Handling Configuration ===\n");

            // Example 1: Check default error handling behavior
            Console.WriteLine($"Default UseImprovedErrorHandling: {Route4MeConfig.UseImprovedErrorHandling}");
            Console.WriteLine("(Default is false for backward compatibility)\n");

            // Example 2: Demonstrate the difference with a real API call
            Console.WriteLine("=== Demonstrating Error Handling Difference ===");
            Console.WriteLine("Making an intentional error call to show error message differences...\n");

            var route4Me = new Route4MeManager(ActualApiKey);

            // Create parameters that will cause an error (invalid route/address IDs)
            var invalidParams = new Route4MeSDK.QueryTypes.AddressParameters
            {
                RouteId = "INVALID_ROUTE_ID",
                AddressId = -1,
                IsDeparted = true
            };

            // First call: With legacy error handling (disabled)
            Console.WriteLine("--- Test 1: Legacy Error Handling (UseImprovedErrorHandling = false) ---");
            Route4MeConfig.UseImprovedErrorHandling = false;
            var result1 = route4Me.MarkAddressDeparted(invalidParams, out var error1);
            Console.WriteLine($"Result: {result1}");
            Console.WriteLine($"Error Message: {(string.IsNullOrEmpty(error1) ? "(No error message)" : error1)}");
            Console.WriteLine();

            // Second call: With improved error handling (enabled)
            Console.WriteLine("--- Test 2: Improved Error Handling (UseImprovedErrorHandling = true) ---");
            Route4MeConfig.UseImprovedErrorHandling = true;
            var result2 = route4Me.MarkAddressDeparted(invalidParams, out var error2);
            Console.WriteLine($"Result: {result2}");
            Console.WriteLine($"Error Message: {(string.IsNullOrEmpty(error2) ? "(No error message)" : error2)}");
            Console.WriteLine();

            // Reset to default
            Route4MeConfig.UseImprovedErrorHandling = false;

            Console.WriteLine("=== Comparison ===");
            Console.WriteLine("Notice how the improved error handling provides more detailed");
            Console.WriteLine("HTTP error information from the API response.\n");

            Console.WriteLine("=== Usage Example ===");
            Console.WriteLine("// Enable improved error handling globally");
            Console.WriteLine("Route4MeConfig.UseImprovedErrorHandling = true;");
            Console.WriteLine("");
            Console.WriteLine("// Create manager and make API calls");
            Console.WriteLine("var route4Me = new Route4MeManager(apiKey);");
            Console.WriteLine("var result = route4Me.GetOptimization(parameters, out string errorString);");
            Console.WriteLine("");
            Console.WriteLine("// If an error occurs, errorString will contain detailed HTTP error messages");
            Console.WriteLine("if (!string.IsNullOrEmpty(errorString))");
            Console.WriteLine("{");
            Console.WriteLine("    Console.WriteLine($\"Error: {errorString}\");");
            Console.WriteLine("}\n");

            Console.WriteLine("=== Important Notes ===");
            Console.WriteLine("- Set UseImprovedErrorHandling BEFORE making API calls");
            Console.WriteLine("- Default is false (legacy behavior for backward compatibility)");
            Console.WriteLine("- When enabled (true):");
            Console.WriteLine("  * Checks HTTP status codes before processing responses");
            Console.WriteLine("  * Extracts detailed error messages from failed HTTP responses");
            Console.WriteLine("  * Properly disposes HTTP response objects");
            Console.WriteLine("- When disabled (false):");
            Console.WriteLine("  * Uses legacy error handling behavior");
            Console.WriteLine("  * Maintains compatibility with existing code");
            Console.WriteLine("- Recommended: Enable for new projects to get better error diagnostics");
        }
    }
}