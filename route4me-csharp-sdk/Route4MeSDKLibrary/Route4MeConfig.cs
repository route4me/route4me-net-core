using System;

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
    }
}
