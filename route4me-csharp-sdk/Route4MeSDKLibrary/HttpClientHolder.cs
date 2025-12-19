using System;
using System.Net.Http;

using Microsoft.Extensions.Logging;

namespace Route4MeSDKLibrary
{
    internal class HttpClientHolder : IDisposable
    {
        private readonly string _baseAddress;

        public HttpClientHolder(HttpClient httpClient, string baseAddress, ILogger logger = null)
        {
            HttpClient = httpClient;
            _baseAddress = baseAddress;
            Logger = logger;
        }

        public HttpClient HttpClient { get; }

        /// <summary>
        ///     Logger instance for this HTTP client holder.
        ///     Thread-safe as each holder instance has its own logger reference.
        /// </summary>
        public ILogger Logger { get; }

        public void Dispose()
        {
            HttpClientHolderManager.ReleaseHttpClientHolder(_baseAddress);
        }
    }
}