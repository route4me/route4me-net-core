using System;
using System.Net.Http;

using Polly;

namespace Route4MeSDKLibrary
{
    /// <summary>
    ///     Global configuration settings for the Route4Me SDK.
    /// </summary>
    public static class Route4MeConfig
    {
        /// <summary>
        ///     Gets or sets the HTTP request timeout for all API calls made by the SDK.
        ///     Default is 30 seconds. Set this value before making any API calls to configure the timeout globally.
        /// </summary>
        /// <example>
        ///     // Set a 60-second timeout for all API calls
        ///     Route4MeConfig.HttpTimeout = TimeSpan.FromSeconds(60);
        ///
        ///     // Or use minutes
        ///     Route4MeConfig.HttpTimeout = TimeSpan.FromMinutes(2);
        /// </example>
        public static TimeSpan HttpTimeout
        {
            get => HttpClientHolderManager.RequestsTimeout;
            set => HttpClientHolderManager.RequestsTimeout = value;
        }

        /// <summary>
        ///     Gets or sets the maximum number of retry attempts for transient HTTP failures.
        ///     Default is 0 (retry disabled for backward compatibility).
        ///     Set to a value between 1-10 to enable retry logic.
        /// </summary>
        /// <example>
        ///     // Enable 3 retry attempts with exponential backoff
        ///     Route4MeConfig.RetryCount = 3;
        /// </example>
        public static int RetryCount { get; set; } = 0;

        /// <summary>
        ///     Gets or sets the initial delay for exponential backoff retry strategy.
        ///     Default is 200ms. Each subsequent retry doubles this value with jitter.
        /// </summary>
        /// <example>
        ///     // Set initial delay to 500ms
        ///     Route4MeConfig.RetryInitialDelay = TimeSpan.FromMilliseconds(500);
        /// </example>
        public static TimeSpan RetryInitialDelay { get; set; } = TimeSpan.FromMilliseconds(200);

        /// <summary>
        ///     Gets or sets whether to enable circuit breaker pattern.
        ///     Default is false. When enabled, prevents cascading failures by temporarily
        ///     halting requests after consecutive failures.
        /// </summary>
        /// <example>
        ///     // Enable circuit breaker
        ///     Route4MeConfig.EnableCircuitBreaker = true;
        /// </example>
        public static bool EnableCircuitBreaker { get; set; } = false;

        /// <summary>
        ///     Gets or sets the number of consecutive failures before circuit breaker opens.
        ///     Default is 5. Only applies when EnableCircuitBreaker is true.
        /// </summary>
        /// 
        public static int CircuitBreakerFailureThreshold { get; set; } = 5;

        /// <summary>
        ///     Gets or sets the duration the circuit breaker remains open before allowing retry.
        ///     Default is 30 seconds. Only applies when EnableCircuitBreaker is true.
        /// </summary>
        public static TimeSpan CircuitBreakerDuration { get; set; } = TimeSpan.FromSeconds(30);

        /// <summary>
        ///     Gets or sets the callback action invoked when a retry attempt occurs.
        ///     Useful for logging and monitoring retry behavior.
        /// </summary>
        /// <example>
        ///     Route4MeConfig.OnRetry = (exception, timeSpan, retryCount, context) =>
        ///     {
        ///         Console.WriteLine($"Retry {retryCount} after {timeSpan.TotalMilliseconds}ms due to {exception.Message}");
        ///     };
        /// </example>
        public static Action<Exception, TimeSpan, int, Context> OnRetry { get; set; }

        /// <summary>
        ///     Gets or sets the callback action invoked when circuit breaker opens.
        ///     Useful for alerting and monitoring.
        /// </summary>
        public static Action<Exception, TimeSpan> OnCircuitBreakerOpen { get; set; }

        /// <summary>
        ///     Gets or sets a factory function that creates <see cref="HttpMessageHandler"/> instances for HttpClient.
        ///     When set, this factory is invoked to create a new handler for each HttpClient instance.
        ///     The HttpClient will own and dispose the handler when the client is disposed.
        ///     This allows injection of custom handlers for logging, payload capture, testing, or other middleware.
        /// </summary>
        /// <example>
        ///     // Configure a logging handler factory
        ///     Route4MeConfig.HttpMessageHandlerFactory = () => new CustomLoggingHandler
        ///     {
        ///         InnerHandler = new HttpClientHandler()
        ///     };
        /// </example>
        public static Func<HttpMessageHandler> HttpMessageHandlerFactory { get; set; } = null;
    }
}