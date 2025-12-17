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
    }
}
