﻿using System;
using System.Net.Http;

namespace Route4MeSDKLibrary
{
    internal class HttpClientHolder : IDisposable
    {
        private readonly string _baseAddress;

        public HttpClient HttpClient { get; }

        public HttpClientHolder(HttpClient httpClient, string baseAddress)
        {
            HttpClient = httpClient;
            _baseAddress = baseAddress;
        }

        public void Dispose()
        {
            HttpClientHolderManager.ReleaseHttpClientHolder(_baseAddress);
        }
    }
}