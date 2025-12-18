using System;

using NUnit.Framework;

using Route4MeSDKLibrary;

namespace Route4MeSdkV5UnitTest.V5
{
    [TestFixture]
    public class ResilienceTests
    {
        [SetUp]
        public void Setup()
        {
            // Reset to defaults before each test
            Route4MeConfig.RetryCount = 0;
            Route4MeConfig.RetryInitialDelay = TimeSpan.FromMilliseconds(200);
            Route4MeConfig.EnableCircuitBreaker = false;
            Route4MeConfig.CircuitBreakerFailureThreshold = 5;
            Route4MeConfig.CircuitBreakerDuration = TimeSpan.FromSeconds(30);
            Route4MeConfig.OnRetry = null;
            Route4MeConfig.OnCircuitBreakerOpen = null;
        }

        [Test]
        public void RetryCount_DefaultValue_IsZero()
        {
            // Assert - Verify backward compatibility
            Assert.AreEqual(0, Route4MeConfig.RetryCount,
                "RetryCount should be 0 by default for backward compatibility");
        }

        [Test]
        public void RetryInitialDelay_DefaultValue_Is200Milliseconds()
        {
            // Assert
            Assert.AreEqual(TimeSpan.FromMilliseconds(200), Route4MeConfig.RetryInitialDelay,
                "RetryInitialDelay should be 200ms by default");
        }

        [Test]
        public void EnableCircuitBreaker_DefaultValue_IsFalse()
        {
            // Assert - Verify circuit breaker disabled by default
            Assert.IsFalse(Route4MeConfig.EnableCircuitBreaker,
                "EnableCircuitBreaker should be false by default");
        }

        [Test]
        public void CircuitBreakerFailureThreshold_DefaultValue_IsFive()
        {
            // Assert
            Assert.AreEqual(5, Route4MeConfig.CircuitBreakerFailureThreshold,
                "CircuitBreakerFailureThreshold should be 5 by default");
        }

        [Test]
        public void CircuitBreakerDuration_DefaultValue_Is30Seconds()
        {
            // Assert
            Assert.AreEqual(TimeSpan.FromSeconds(30), Route4MeConfig.CircuitBreakerDuration,
                "CircuitBreakerDuration should be 30 seconds by default");
        }

        [Test]
        public void RetryCount_CanBeConfigured()
        {
            // Arrange & Act
            Route4MeConfig.RetryCount = 3;

            // Assert
            Assert.AreEqual(3, Route4MeConfig.RetryCount);
        }

        [Test]
        public void RetryInitialDelay_CanBeConfigured()
        {
            // Arrange & Act
            var customDelay = TimeSpan.FromMilliseconds(500);
            Route4MeConfig.RetryInitialDelay = customDelay;

            // Assert
            Assert.AreEqual(customDelay, Route4MeConfig.RetryInitialDelay);
        }

        [Test]
        public void EnableCircuitBreaker_CanBeEnabled()
        {
            // Arrange & Act
            Route4MeConfig.EnableCircuitBreaker = true;

            // Assert
            Assert.IsTrue(Route4MeConfig.EnableCircuitBreaker);
        }

        [Test]
        public void CircuitBreakerFailureThreshold_CanBeConfigured()
        {
            // Arrange & Act
            Route4MeConfig.CircuitBreakerFailureThreshold = 10;

            // Assert
            Assert.AreEqual(10, Route4MeConfig.CircuitBreakerFailureThreshold);
        }

        [Test]
        public void CircuitBreakerDuration_CanBeConfigured()
        {
            // Arrange & Act
            var customDuration = TimeSpan.FromMinutes(1);
            Route4MeConfig.CircuitBreakerDuration = customDuration;

            // Assert
            Assert.AreEqual(customDuration, Route4MeConfig.CircuitBreakerDuration);
        }

        [Test]
        public void OnRetry_CanBeSetToCallback()
        {
            // Arrange
            bool callbackInvoked = false;
            Action<Exception, TimeSpan, int, Polly.Context> callback = (ex, ts, count, ctx) =>
            {
                callbackInvoked = true;
            };

            // Act
            Route4MeConfig.OnRetry = callback;

            // Assert
            Assert.IsNotNull(Route4MeConfig.OnRetry);
            Route4MeConfig.OnRetry?.Invoke(new Exception(), TimeSpan.Zero, 1, new Polly.Context());
            Assert.IsTrue(callbackInvoked, "Callback should have been invoked");
        }

        [Test]
        public void OnCircuitBreakerOpen_CanBeSetToCallback()
        {
            // Arrange
            bool callbackInvoked = false;
            Action<Exception, TimeSpan> callback = (ex, duration) =>
            {
                callbackInvoked = true;
            };

            // Act
            Route4MeConfig.OnCircuitBreakerOpen = callback;

            // Assert
            Assert.IsNotNull(Route4MeConfig.OnCircuitBreakerOpen);
            Route4MeConfig.OnCircuitBreakerOpen?.Invoke(new Exception(), TimeSpan.Zero);
            Assert.IsTrue(callbackInvoked, "Callback should have been invoked");
        }

        [Test]
        public void Configuration_SupportsMultipleConfigurations()
        {
            // Arrange & Act - Configure multiple settings
            Route4MeConfig.RetryCount = 5;
            Route4MeConfig.RetryInitialDelay = TimeSpan.FromMilliseconds(100);
            Route4MeConfig.EnableCircuitBreaker = true;
            Route4MeConfig.CircuitBreakerFailureThreshold = 10;
            Route4MeConfig.CircuitBreakerDuration = TimeSpan.FromMinutes(2);

            // Assert - All configurations should be maintained
            Assert.AreEqual(5, Route4MeConfig.RetryCount);
            Assert.AreEqual(100, Route4MeConfig.RetryInitialDelay.TotalMilliseconds);
            Assert.IsTrue(Route4MeConfig.EnableCircuitBreaker);
            Assert.AreEqual(10, Route4MeConfig.CircuitBreakerFailureThreshold);
            Assert.AreEqual(120, Route4MeConfig.CircuitBreakerDuration.TotalSeconds);
        }

        [Test]
        public void Configuration_CanBeResetToDefaults()
        {
            // Arrange - Set custom values
            Route4MeConfig.RetryCount = 10;
            Route4MeConfig.EnableCircuitBreaker = true;

            // Act - Reset to defaults
            Route4MeConfig.RetryCount = 0;
            Route4MeConfig.RetryInitialDelay = TimeSpan.FromMilliseconds(200);
            Route4MeConfig.EnableCircuitBreaker = false;
            Route4MeConfig.CircuitBreakerFailureThreshold = 5;
            Route4MeConfig.CircuitBreakerDuration = TimeSpan.FromSeconds(30);

            // Assert - Verify defaults
            Assert.AreEqual(0, Route4MeConfig.RetryCount);
            Assert.AreEqual(200, Route4MeConfig.RetryInitialDelay.TotalMilliseconds);
            Assert.IsFalse(Route4MeConfig.EnableCircuitBreaker);
            Assert.AreEqual(5, Route4MeConfig.CircuitBreakerFailureThreshold);
            Assert.AreEqual(30, Route4MeConfig.CircuitBreakerDuration.TotalSeconds);
        }
    }
}