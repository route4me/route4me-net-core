using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

using Route4MeSDKLibrary;

namespace Route4MeSDK.Examples
{
    /// <summary>
    ///     Example demonstrating how to configure the Route4Me SDK globally.
    /// </summary>
    public sealed partial class Route4MeExamples
    {
        public void CustomHttpHandlerConfigurationExample()
        {
            const string filePath = "http-log.json";
            Route4MeConfig.CustomHttpMessageHandler = new JsonPayloadLoggingHandler(filePath)
            {
                InnerHandler = new HttpClientHandler()
            };
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var routeParameters = new RouteParametersQuery { Limit = 1, Offset = 15 };

            // Run the query
            var result = route4Me.GetRoutes(routeParameters, out ResultResponse resultResponse);
            // Check log
            ShowLog(filePath);
        }

        private void ShowLog(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Log file not found.");
                return;
            }

            var content = File.ReadAllText(filePath);
            Console.WriteLine(content);
        }

        private class JsonPayloadLoggingHandler : DelegatingHandler
        {
            private readonly string _logFilePath;

            public JsonPayloadLoggingHandler(string logFilePath)
            {
                _logFilePath = logFilePath;
            }

            protected override async Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
                string requestBody = null;
                string responseBody = null;

                if (request.Content != null)
                {
                    requestBody = await request.Content.ReadAsStringAsync();
                }

                var response = await base.SendAsync(request, cancellationToken);

                if (response.Content != null)
                {
                    responseBody = await response.Content.ReadAsStringAsync();

                    // Re-create content so it can be read later
                    response.Content = new StringContent(
                        responseBody,
                        Encoding.UTF8,
                        response.Content.Headers.ContentType?.MediaType
                    );
                }

                var logEntry = new
                {
                    timestamp = DateTime.UtcNow,
                    request = new
                    {
                        method = request.Method.Method,
                        url = request.RequestUri?.ToString(),
                        headers = request.Headers,
                        body = requestBody
                    },
                    response = new
                    {
                        statusCode = (int)response.StatusCode,
                        reasonPhrase = response.ReasonPhrase,
                        headers = response.Headers,
                        body = responseBody
                    }
                };

                var json = JsonSerializer.Serialize(
                    logEntry,
                    new JsonSerializerOptions { WriteIndented = true }
                );

                await File.AppendAllTextAsync(_logFilePath, json + Environment.NewLine);

                return response;
            }
        }
    }
}