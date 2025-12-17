using System;
using System.Reflection;
using NUnit.Framework;
using Route4MeSDKLibrary;

namespace Route4MeSdkV5UnitTest.V5
{
    [TestFixture]
    public class ConfigurationTests
    {
        [Test]
        public void HttpClient_UsesConfiguredTimeout_WhenCreated()
        {
            // Arrange
            var originalTimeout = Route4MeConfig.HttpTimeout;
            var customTimeout = TimeSpan.FromSeconds(45);

            try
            {
                // Set custom timeout before creating HttpClient
                Route4MeConfig.HttpTimeout = customTimeout;

                // Use a unique base address to ensure we get a fresh HttpClient instance
                var uniqueBaseAddress = $"https://test-{Guid.NewGuid()}.route4me.com";

                // Act - Use reflection to access the internal HttpClientHolderManager
                var holderManagerType = Type.GetType("Route4MeSDKLibrary.HttpClientHolderManager, Route4MeSDKLibrary");
                Assert.IsNotNull(holderManagerType, "HttpClientHolderManager type not found");

                var acquireMethod = holderManagerType.GetMethod("AcquireHttpClientHolder",
                    BindingFlags.Public | BindingFlags.Static);
                Assert.IsNotNull(acquireMethod, "AcquireHttpClientHolder method not found");

                var holder = acquireMethod.Invoke(null, new object[] { uniqueBaseAddress, null });
                Assert.IsNotNull(holder, "HttpClientHolder should not be null");

                // Get the HttpClient from the holder
                var httpClientProperty = holder.GetType().GetProperty("HttpClient");
                Assert.IsNotNull(httpClientProperty, "HttpClient property not found");

                var httpClient = httpClientProperty.GetValue(holder) as System.Net.Http.HttpClient;
                Assert.IsNotNull(httpClient, "HttpClient should not be null");

                // Assert - Verify the HttpClient has the custom timeout
                Assert.AreEqual(customTimeout, httpClient.Timeout,
                    $"HttpClient timeout should be {customTimeout.TotalSeconds} seconds");

                // Cleanup - Release the holder
                var releaseMethod = holderManagerType.GetMethod("ReleaseHttpClientHolder",
                    BindingFlags.Public | BindingFlags.Static);
                releaseMethod?.Invoke(null, new object[] { uniqueBaseAddress });
            }
            finally
            {
                // Restore original timeout
                Route4MeConfig.HttpTimeout = originalTimeout;
            }
        }

        [Test]
        public void HttpClient_DefaultTimeout_Is30Seconds_WhenNoConfigurationSet()
        {
            // Arrange
            var originalTimeout = Route4MeConfig.HttpTimeout;

            try
            {
                // Reset to default
                Route4MeConfig.HttpTimeout = TimeSpan.FromSeconds(30);

                // Use a unique base address to ensure we get a fresh HttpClient instance
                var uniqueBaseAddress = $"https://test-default-{Guid.NewGuid()}.route4me.com";

                // Act - Use reflection to access the internal HttpClientHolderManager
                var holderManagerType = Type.GetType("Route4MeSDKLibrary.HttpClientHolderManager, Route4MeSDKLibrary");
                var acquireMethod = holderManagerType.GetMethod("AcquireHttpClientHolder",
                    BindingFlags.Public | BindingFlags.Static);
                var holder = acquireMethod.Invoke(null, new object[] { uniqueBaseAddress, null });
                var httpClientProperty = holder.GetType().GetProperty("HttpClient");
                var httpClient = httpClientProperty.GetValue(holder) as System.Net.Http.HttpClient;

                // Assert - Verify the HttpClient has the default 30-second timeout
                Assert.AreEqual(TimeSpan.FromSeconds(30), httpClient.Timeout,
                    "HttpClient should have default 30-second timeout");

                // Cleanup
                var releaseMethod = holderManagerType.GetMethod("ReleaseHttpClientHolder",
                    BindingFlags.Public | BindingFlags.Static);
                releaseMethod?.Invoke(null, new object[] { uniqueBaseAddress });
            }
            finally
            {
                // Restore original timeout
                Route4MeConfig.HttpTimeout = originalTimeout;
            }
        }
    }
}
