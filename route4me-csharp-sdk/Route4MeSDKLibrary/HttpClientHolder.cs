using System;
using System.Net.Http;

namespace Route4MeSDKLibrary
{
    internal class HttpClientHolder : IDisposable
    {
        private readonly string _key;

        public HttpClientHolder(HttpClient httpClient, string key)
        {
            HttpClient = httpClient;
            _key = key;
        }

        public HttpClient HttpClient { get; }

        public void Dispose()
        {
            HttpClientHolderManager.ReleaseHttpClientHolder(_key);
        }
    }
}