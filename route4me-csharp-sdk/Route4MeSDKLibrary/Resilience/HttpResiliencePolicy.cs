using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Polly;
using Polly.Extensions.Http;

namespace Route4MeSDKLibrary.Resilience
{
    internal static class HttpResiliencePolicy
    {
        private static readonly Lazy<IAsyncPolicy<HttpResponseMessage>> _asyncPolicy
            = new Lazy<IAsyncPolicy<HttpResponseMessage>>(CreateAsyncPolicy);

        private static readonly Lazy<ISyncPolicy<HttpResponseMessage>> _syncPolicy
            = new Lazy<ISyncPolicy<HttpResponseMessage>>(CreateSyncPolicy);

        private static readonly Random _jitterer = new Random();

        public static IAsyncPolicy<HttpResponseMessage> GetAsyncPolicy()
        {
            return Route4MeConfig.RetryCount > 0
                ? _asyncPolicy.Value
                : Policy.NoOpAsync<HttpResponseMessage>();
        }

        public static ISyncPolicy<HttpResponseMessage> GetSyncPolicy()
        {
            return Route4MeConfig.RetryCount > 0
                ? _syncPolicy.Value
                : Policy.NoOp<HttpResponseMessage>();
        }

        private static IAsyncPolicy<HttpResponseMessage> CreateAsyncPolicy()
        {
            var retryPolicy = CreateAsyncRetryPolicy();

            if (Route4MeConfig.EnableCircuitBreaker)
            {
                var circuitBreakerPolicy = CreateAsyncCircuitBreakerPolicy();
                return Policy.WrapAsync(retryPolicy, circuitBreakerPolicy);
            }

            return retryPolicy;
        }

        private static ISyncPolicy<HttpResponseMessage> CreateSyncPolicy()
        {
            var retryPolicy = CreateSyncRetryPolicy();

            if (Route4MeConfig.EnableCircuitBreaker)
            {
                var circuitBreakerPolicy = CreateSyncCircuitBreakerPolicy();
                return Policy.Wrap(retryPolicy, circuitBreakerPolicy);
            }

            return retryPolicy;
        }

        private static IAsyncPolicy<HttpResponseMessage> CreateAsyncRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<TaskCanceledException>()
                .OrResult(r => r.StatusCode == (HttpStatusCode)429)
                .WaitAndRetryAsync(
                    retryCount: Route4MeConfig.RetryCount,
                    sleepDurationProvider: (retryAttempt) =>
                    {
                        var exponentialDelay = Route4MeConfig.RetryInitialDelay
                            .TotalMilliseconds * Math.Pow(2, retryAttempt - 1);

                        var jitter = (_jitterer.NextDouble() * 0.4) - 0.2;
                        var delayWithJitter = exponentialDelay * (1 + jitter);

                        return TimeSpan.FromMilliseconds(delayWithJitter);
                    },
                    onRetry: (outcome, timespan, retryCount, context) =>
                    {
                        Route4MeConfig.OnRetry?.Invoke(
                            outcome.Exception,
                            timespan,
                            retryCount,
                            context
                        );
                    }
                );
        }

        private static ISyncPolicy<HttpResponseMessage> CreateSyncRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<TaskCanceledException>()
                .OrResult(r => r.StatusCode == (HttpStatusCode)429)
                .WaitAndRetry(
                    retryCount: Route4MeConfig.RetryCount,
                    sleepDurationProvider: (retryAttempt) =>
                    {
                        var exponentialDelay = Route4MeConfig.RetryInitialDelay
                            .TotalMilliseconds * Math.Pow(2, retryAttempt - 1);
                        var jitter = (_jitterer.NextDouble() * 0.4) - 0.2;
                        var delayWithJitter = exponentialDelay * (1 + jitter);
                        return TimeSpan.FromMilliseconds(delayWithJitter);
                    },
                    onRetry: (outcome, timespan, retryCount, context) =>
                    {
                        Route4MeConfig.OnRetry?.Invoke(
                            outcome.Exception,
                            timespan,
                            retryCount,
                            context
                        );
                    }
                );
        }

        private static IAsyncPolicy<HttpResponseMessage> CreateAsyncCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<TaskCanceledException>()
                .OrResult(r => r.StatusCode == (HttpStatusCode)429)
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: Route4MeConfig.CircuitBreakerFailureThreshold,
                    durationOfBreak: Route4MeConfig.CircuitBreakerDuration,
                    onBreak: (outcome, duration) =>
                    {
                        Route4MeConfig.OnCircuitBreakerOpen?.Invoke(
                            outcome.Exception,
                            duration
                        );
                    },
                    onReset: () => { },
                    onHalfOpen: () => { }
                );
        }

        private static ISyncPolicy<HttpResponseMessage> CreateSyncCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<TaskCanceledException>()
                .OrResult(r => r.StatusCode == (HttpStatusCode)429)
                .CircuitBreaker(
                    handledEventsAllowedBeforeBreaking: Route4MeConfig.CircuitBreakerFailureThreshold,
                    durationOfBreak: Route4MeConfig.CircuitBreakerDuration,
                    onBreak: (outcome, duration) =>
                    {
                        Route4MeConfig.OnCircuitBreakerOpen?.Invoke(
                            outcome.Exception,
                            duration
                        );
                    },
                    onReset: () => { },
                    onHalfOpen: () => { }
                );
        }

        internal static bool IsTransientError(HttpStatusCode statusCode)
        {
            return statusCode == HttpStatusCode.RequestTimeout
                || statusCode == (HttpStatusCode)429
                || statusCode == HttpStatusCode.ServiceUnavailable
                || statusCode == HttpStatusCode.GatewayTimeout
                || ((int)statusCode >= 500 && (int)statusCode < 600);
        }
    }
}
