using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKUnitTest.Tests
{
    [TestFixture]
    public class RunOptimizationWithRetryTest
    {
        private class TestLogger : ILogger
        {
            public List<string> Logs = new List<string>();

            public IDisposable BeginScope<TState>(TState state) => null;

            public bool IsEnabled(LogLevel logLevel) => true;

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                var message = formatter(state, exception);
                Logs.Add($"{logLevel}: {message}");
                if (exception != null)
                {
                    Logs.Add($"{logLevel} Exception: {exception.Message}");
                }
            }
        }

        [Test]
        public void RunOptimizationWithRetry_LogsAndRetries_OnTimeout()
        {
            // Arrange
            var logger = new TestLogger();
            // Set a very short timeout to force a timeout
            var manager = new Route4MeManager("11111111111111111111111111111111", TimeSpan.FromMilliseconds(1), logger);

            var parameters = new OptimizationParameters
            {
                Parameters = new RouteParameters
                {
                    AlgorithmType = AlgorithmType.TSP,
                    RouteName = "Test Route"
                },
                Addresses = new Address[]
                {
                    new Address { AddressString = "754 5th Ave New York, NY 10019", Alias = "Bergdorf Goodman", IsDepot = true, Latitude = 40.7636197, Longitude = -73.9744388, Time = 0 },
                    new Address { AddressString = "717 5th Ave New York, NY 10022", Alias = "Giorgio Armani", Latitude = 40.7669692, Longitude = -73.9754042, Time = 0 }
                }
            };

            // Act
            var result = manager.RunOptimizationWithRetry(parameters, out var failureResponse, maxRetries: 2);

            // Assert
            Assert.IsNull(result, "Result should be null on failure.");
            Assert.IsNotNull(failureResponse, "Failure response should not be null.");
            
            // Verify logs
            // We expect at least one "attempt failed" log and one "all attempts failed" log
            Assert.IsTrue(logger.Logs.Exists(l => l.Contains("Attempt 1 failed")), "Should log attempt 1 failure.");
            Assert.IsTrue(logger.Logs.Exists(l => l.Contains("All attempts to run optimization failed")), "Should log final failure.");
            
            // Verify specific timeout message
            Assert.IsTrue(logger.Logs.Exists(l => l.Contains("API request timed out")), "Should log timeout specific message.");
        }

        [Test]
        public void RunOptimizationWithRetry_RealApiCall_Success()
        {
            // Arrange
            var logger = new TestLogger();
            // Use real API key
            var manager = new Route4MeManager(Route4MeSDKUnitTest.Types.ApiKeys.ActualApiKey, logger: logger);

            var parameters = new OptimizationParameters
            {
                Parameters = new RouteParameters
                {
                    AlgorithmType = AlgorithmType.TSP,
                    RouteName = "Real API Test Route",
                    RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.Now.AddDays(1))
                },
                Addresses = new Address[]
                {
                    new Address { AddressString = "754 5th Ave New York, NY 10019", Alias = "Bergdorf Goodman", IsDepot = true, Latitude = 40.7636197, Longitude = -73.9744388, Time = 0 },
                    new Address { AddressString = "717 5th Ave New York, NY 10022", Alias = "Giorgio Armani", Latitude = 40.7669692, Longitude = -73.9754042, Time = 0 }
                }
            };

            // Act
            var result = manager.RunOptimizationWithRetry(parameters, out var failureResponse, maxRetries: 3);

            // Assert
            Assert.IsNotNull(result, "Result should NOT be null on success.");
            Assert.IsNull(failureResponse, "Failure response should be null on success.");
            Assert.IsNotNull(result.OptimizationProblemId, "OptimizationProblemId should be set.");

            // Cleanup (Optional but good practice)
            if (result != null && !string.IsNullOrEmpty(result.OptimizationProblemId))
            {
                manager.RemoveOptimization(new[] { result.OptimizationProblemId }, out _);
            }
        }

        [Test]
        public void RunOptimizationWithRetry_SimulateNetworkFailure_RetriesAndFails()
        {
            // Arrange
            var logger = new TestLogger();
            // Use dummy key, we want to fail connection anyway
            var manager = new Route4MeManager("11111111111111111111111111111111", TimeSpan.FromSeconds(1), logger);
            
            // Set invalid base URL to force DNS/Connection error (simulate network issue)
            manager.BaseUrlOverride = "https://non-existent-domain-12345.com/api";

            var parameters = new OptimizationParameters
            {
                Parameters = new RouteParameters { AlgorithmType = AlgorithmType.TSP, RouteName = "Network Fail Test" },
                Addresses = new Address[] { new Address { AddressString = "Test" } }
            };

            // Act
            var result = manager.RunOptimizationWithRetry(parameters, out var failureResponse, maxRetries: 2);

            // Assert
            Assert.IsNull(result, "Result should be null on network failure.");
            Assert.IsNotNull(failureResponse, "Failure response should be present.");
            
            // Verify logs show network issue
            Assert.IsTrue(logger.Logs.Exists(l => l.Contains("API request failed due to network issue")), "Should log network failure.");
            Assert.IsTrue(logger.Logs.Exists(l => l.Contains("Attempt 1 failed")), "Should log retry attempt.");
        }

        [Test]
        public void RunOptimizationWithRetry_InvalidGeocoding_ReturnsError()
        {
            // Arrange
            var logger = new TestLogger();
            // Use real API key to reach the API and get a Business Rule error (Validation)
            var manager = new Route4MeManager(Route4MeSDKUnitTest.Types.ApiKeys.ActualApiKey, logger: logger);

            var parameters = new OptimizationParameters
            {
                Parameters = new RouteParameters
                {
                    AlgorithmType = AlgorithmType.TSP,
                    RouteName = "Invalid Geocoding Test"
                },
                Addresses = new Address[]
                {
                    // Invalid address data intended to provoke a validation error e.g. High Confidence Geocoding error
                    // or minimum requirements. 
                    // Providing an address with Alias but NO address string and NO lat/lng might trigger 400 Bad Request
                    new Address { Alias = "Invalid Entry", Time = 0 } 
                }
            };

            // Act
            // We expect the API to return a 200 OK with error OR 400 Bad Request. 
            // The SDK might return null with errorMessage or throw depending on how the server replies.
            // RunOptimizationWithRetry captures exceptions and returns ResultResponse.
            var result = manager.RunOptimizationWithRetry(parameters, out var failureResponse, maxRetries: 1);

            // Assert
            // If the key is dummy/invalid (403), this will also fail but with Auth error.
            // If key is valid, it should fail with content validation error.
            
            Assert.IsNull(result, "Result should be null for invalid input.");
            Assert.IsNotNull(failureResponse, "Should have failure response.");
            
            if (failureResponse != null && failureResponse.Messages != null)
            {
                foreach(var kvp in failureResponse.Messages)
                {
                     Console.WriteLine($"Error Key: {kvp.Key}, Value: {string.Join(",", kvp.Value)}");
                }
            }
        }
    }
}
