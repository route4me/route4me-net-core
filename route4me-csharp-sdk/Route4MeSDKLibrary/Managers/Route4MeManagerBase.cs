using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Polly.CircuitBreaker;

using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes;

using Route4MeSDKLibrary.Resilience;

namespace Route4MeSDKLibrary.Managers
{
    public abstract class Route4MeManagerBase
    {
        protected readonly string ApiKey;

        protected Route4MeManagerBase(string apiKey)
        {
            ApiKey = apiKey;
        }

        public string GetStringResponseFromAPI(GenericParameters optimizationParameters,
            string url,
            HttpMethodType httpMethod,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<string>(optimizationParameters,
                url,
                httpMethod,
                true,
                false,
                out resultResponse);

            return result;
        }


        public T GetJsonObjectFromAPI<T>(GenericParameters optimizationParameters,
            string url,
            HttpMethodType httpMethod,
            out ResultResponse resultResponse,
            string[] mandatoryFields = null)
            where T : class
        {
            var result = GetJsonObjectFromAPI<T>(optimizationParameters,
                url,
                httpMethod,
                false,
                false,
                out resultResponse,
                mandatoryFields);

            return result;
        }

        public T GetJsonObjectFromAPI<T>(GenericParameters optimizationParameters,
            string url,
            HttpMethodType httpMethod,
            HttpContent httpContent,
            out ResultResponse resultResponse)
            where T : class
        {
            var result = GetJsonObjectFromAPI<T>(optimizationParameters,
                url,
                httpMethod,
                httpContent,
                false,
                false,
                out resultResponse);

            return result;
        }

        protected T GetJsonObjectFromAPI<T>(GenericParameters optimizationParameters,
            string url,
            HttpMethodType httpMethod,
            bool isString,
            bool parseWithNewtonJson,
            out ResultResponse resultResponse,
            string[] mandatoryFields = null)
            where T : class
        {
            var result = GetJsonObjectFromAPI<T>(optimizationParameters,
                url,
                httpMethod,
                null,
                isString,
                parseWithNewtonJson,
                out resultResponse,
                mandatoryFields);

            return result;
        }

        protected Task<Tuple<T, ResultResponse, string>> GetJsonObjectAndJobFromAPIAsync<T>(GenericParameters optimizationParameters,
            string url,
            HttpMethodType httpMethod)
            where T : class
        {
            var result = GetJsonObjectFromAPIAsync<T>(optimizationParameters,
                url,
                httpMethod,
                null,
                false,
                false);

            return result;
        }

        protected async Task<Tuple<T, ResultResponse>> GetJsonObjectFromAPIAsync<T>(GenericParameters optimizationParameters,
            string url,
            HttpMethodType httpMethod)
            where T : class
        {
            var result = await GetJsonObjectFromAPIAsync<T>(optimizationParameters,
                url,
                httpMethod,
                null,
                false,
                false).ConfigureAwait(false);

            return new Tuple<T, ResultResponse>(result.Item1, result.Item2);
        }

        protected async Task<Tuple<T, ResultResponse, string>> GetJsonObjectFromAPIAsync<T>(
            GenericParameters optimizationParameters,
            string url,
            HttpMethodType httpMethod,
            HttpContent httpContent,
            bool parseWithNewtonJson,
            bool isString,
            string[] mandatoryFields = null,
            bool serializeBodyWithNewtonJson = false)
            where T : class
        {
            var result = default(T);
            var resultResponse = default(ResultResponse);
            string jobId = default(string);

            bool v5 = R4MeUtils.IsV5(url);

            var parametersURI = optimizationParameters.Serialize(v5 ? null : ApiKey);
            var uri = new Uri($"{url}{parametersURI}");

            try
            {
                using (var httpClientHolder =
                       HttpClientHolderManager.AcquireHttpClientHolder(uri.GetLeftPart(UriPartial.Authority), v5 ? ApiKey : null))
                {
                    switch (httpMethod)
                    {
                        case HttpMethodType.Get:
                            {
                                var httpResponse = await HttpResiliencePolicy.GetAsyncPolicy()
                                    .ExecuteAsync(async () =>
                                        await httpClientHolder.HttpClient.GetAsync(uri.PathAndQuery)
                                            .ConfigureAwait(false))
                                    .ConfigureAwait(false);

                                httpResponse.EnsureSuccessStatusCode();
                                var response = await httpResponse.Content.ReadAsStreamAsync().ConfigureAwait(false);

                                if (isString)
                                {
                                    result = response.ReadString() as T;
                                }
                                else
                                {
                                    result = parseWithNewtonJson
                                        ? response.ReadObjectNew<T>()
                                        : response.ReadObject<T>();
                                }
                                break;
                            }
                        case HttpMethodType.Post:
                        case HttpMethodType.Put:
                        case HttpMethodType.Patch:
                        case HttpMethodType.Delete:
                            {
                                var isPut = httpMethod == HttpMethodType.Put;
                                var isPatch = httpMethod == HttpMethodType.Patch;
                                var isDelete = httpMethod == HttpMethodType.Delete;
                                HttpContent content;
                                if (httpContent != null)
                                {
                                    content = httpContent;
                                }
                                else
                                {
                                    var jsonString = ((mandatoryFields?.Length ?? 0) > 0) || serializeBodyWithNewtonJson
                                        ? R4MeUtils.SerializeObjectToJson(optimizationParameters, mandatoryFields)
                                        : R4MeUtils.SerializeObjectToJson(optimizationParameters);
                                    content = new StringContent(jsonString);
                                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                                }

                                HttpResponseMessage response = await HttpResiliencePolicy.GetAsyncPolicy()
                                    .ExecuteAsync(async () =>
                                    {
                                        if (isPut)
                                        {
                                            return await httpClientHolder.HttpClient.PutAsync(uri.PathAndQuery, content).ConfigureAwait(false);
                                        }
                                        else if (isPatch)
                                        {
                                            content.Headers.ContentType = new MediaTypeHeaderValue("application/json-patch+json");
                                            return await httpClientHolder.HttpClient.PatchAsync(uri.PathAndQuery, content).ConfigureAwait(false);
                                        }
                                        else if (isDelete)
                                        {
                                            var request = new HttpRequestMessage
                                            {
                                                Content = content,
                                                Method = HttpMethod.Delete,
                                                RequestUri = new Uri(uri.PathAndQuery, UriKind.Relative)
                                            };
                                            return await httpClientHolder.HttpClient.SendAsync(request).ConfigureAwait(false);
                                        }
                                        else
                                        {
                                            return await httpClientHolder.HttpClient.PostAsync(uri.PathAndQuery, content)
                                                .ConfigureAwait(false);
                                        }
                                    })
                                    .ConfigureAwait(false);

                                // Check if successful
                                if (response.Content is StreamContent)
                                {
                                    var streamTask = await ((StreamContent)response.Content).ReadAsStreamAsync().ConfigureAwait(false);

                                    if (isString)
                                    {
                                        result = streamTask.ReadString() as T;
                                    }
                                    else
                                    {
                                        result = parseWithNewtonJson
                                            ? streamTask.ReadObjectNew<T>()
                                            : streamTask.ReadObject<T>();
                                    }
                                }
                                else if (response.Content
                                         .GetType().ToString().ToLower()
                                         .Contains("httpconnectionresponsecontent"))
                                {
                                    var content2 = response.Content;

                                    jobId = ExtractJobId(response);

                                    if (isString)
                                    {
                                        result = content2.ReadAsStreamAsync().Result.ReadString() as T;
                                    }
                                    else
                                    {
                                        result = parseWithNewtonJson
                                            ? content2.ReadAsStreamAsync().Result.ReadObjectNew<T>()
                                            : content2.ReadAsStreamAsync().Result.ReadObject<T>();
                                    }

                                    if (typeof(T) == typeof(StatusResponse))
                                    {
                                        if (result == null)
                                        {
                                            result = JsonConvert.DeserializeObject<T>("{}");
                                        }
                                        result.GetType().GetProperty("StatusCode")
                                            ?.SetValue(result, (int)response.StatusCode);

                                        result.GetType().GetProperty("IsSuccessStatusCode")
                                            ?.SetValue(result, response.IsSuccessStatusCode);

                                        result.GetType().GetProperty("Status")
                                            ?.SetValue(result, response.IsSuccessStatusCode);
                                    }
                                }
                                else
                                {
                                    Task<string> errorMessageContent = null;

                                    if (response.Content.GetType() != typeof(StreamContent))
                                        errorMessageContent = response.Content.ReadAsStringAsync();

                                    if (errorMessageContent != null)
                                    {
                                        resultResponse = new ResultResponse
                                        {
                                            Status = false,
                                            Messages = new Dictionary<string, string[]>
                                        {
                                            {"ErrorMessageContent", new[] {errorMessageContent.Result}}
                                        }
                                        };
                                    }
                                    else
                                    {
                                        var responseStream = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                                        var responseString = responseStream;

                                        resultResponse = new ResultResponse
                                        {
                                            Status = false,
                                            Messages = new Dictionary<string, string[]>
                                        {
                                            {"Response", new[] {responseString}}
                                        }
                                        };
                                    }
                                }

                                break;
                            }
                    }
                }
            }
            catch (BrokenCircuitException)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"CircuitBreakerError", new[] {
                            "Circuit breaker is open due to consecutive failures. " +
                            "Requests are temporarily blocked. Please try again later."
                        }}
                    }
                };
                result = null;
            }
            catch (HttpRequestException hre) when (Route4MeConfig.RetryCount > 0)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"RetryExhausted", new[] {
                            $"Request failed after {Route4MeConfig.RetryCount} retry attempts: {hre.Message}"
                        }}
                    }
                };
                result = null;
            }
            catch (HttpListenerException e)
            {
                resultResponse = new ResultResponse
                {
                    Status = false
                };

                if (e.Message != null)
                    resultResponse.Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {e.Message}}
                    };

                if ((e.InnerException?.Message ?? null) != null)
                {
                    if (resultResponse.Messages == null)
                        resultResponse.Messages = new Dictionary<string, string[]>();

                    resultResponse.Messages.Add("Error", new[] { e.InnerException.Message });
                }

                result = null;
            }
            catch (Exception e)
            {
                resultResponse = new ResultResponse
                {
                    Status = false
                };

                if (e.Message != null)
                    resultResponse.Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {e.Message}}
                    };

                if ((e.InnerException?.Message ?? null) != null)
                {
                    if (resultResponse.Messages == null)
                        resultResponse.Messages = new Dictionary<string, string[]>();

                    resultResponse.Messages.Add("InnerException Error", new[] { e.InnerException.Message });
                }

                result = default;
            }

            return new Tuple<T, ResultResponse, string>(result, resultResponse, jobId);
        }

        protected string ExtractJobId(HttpResponseMessage response)
        {

            var jobLocation = response?.Headers?.Location;

            string jobId = (jobLocation?.OriginalString?.Length ?? 0) > 32
                ? jobLocation.OriginalString.Split('/').Last()
                : null;

            return (jobId?.Length ?? 0) == 36 ? jobId : null;
        }


        protected T GetJsonObjectFromAPI<T>(GenericParameters optimizationParameters,
            string url,
            HttpMethodType httpMethod,
            HttpContent httpContent,
            bool isString,
            bool parseWithNewtonJson,
            out ResultResponse resultResponse,
            string[] mandatoryFields = null,
            bool serializeBodyWithNewtonJson = false
        )
            where T : class
        {
            var result = default(T);
            resultResponse = default;
            bool v5 = R4MeUtils.IsV5(url);
            var parametersUri = optimizationParameters.Serialize(v5 ? null : ApiKey);
            var uri = new Uri($"{url}{parametersUri}");

            try
            {
                using (var httpClientHolder =
                       HttpClientHolderManager.AcquireHttpClientHolder(uri.GetLeftPart(UriPartial.Authority), v5 ? ApiKey : null))
                {
                    switch (httpMethod)
                    {
                        case HttpMethodType.Get:
                            {
                                var httpResponse = HttpResiliencePolicy.GetSyncPolicy()
                                    .Execute(() =>
                                    {
                                        var task = httpClientHolder.HttpClient.GetAsync(uri.PathAndQuery);
                                        task.Wait();
                                        return task.Result;
                                    });

                                httpResponse.EnsureSuccessStatusCode();
                                var streamTask = httpResponse.Content.ReadAsStreamAsync();
                                streamTask.Wait();
                                var response = streamTask.Result;

                                if (isString)
                                {
                                    result = response.ReadString() as T;
                                }
                                else
                                {
                                    result = parseWithNewtonJson
                                        ? response.ReadObjectNew<T>()
                                        : response.ReadObject<T>();
                                }
                                break;
                            }
                        case HttpMethodType.Post:
                        case HttpMethodType.Put:
                        case HttpMethodType.Patch:
                        case HttpMethodType.Delete:
                            {
                                var isPut = httpMethod == HttpMethodType.Put;
                                var isPatch = httpMethod == HttpMethodType.Patch;
                                var isDelete = httpMethod == HttpMethodType.Delete;
                                HttpContent content;
                                if (httpContent != null)
                                {
                                    content = httpContent;
                                }
                                else
                                {
                                    var jsonString = ((mandatoryFields?.Length ?? 0) > 0) || serializeBodyWithNewtonJson
                                        ? R4MeUtils.SerializeObjectToJson(optimizationParameters, mandatoryFields)
                                        : R4MeUtils.SerializeObjectToJson(optimizationParameters);
                                    content = new StringContent(jsonString);
                                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                                }

                                var response = HttpResiliencePolicy.GetSyncPolicy()
                                    .Execute(() =>
                                    {
                                        Task<HttpResponseMessage> task;
                                        if (isPut)
                                        {
                                            task = httpClientHolder.HttpClient.PutAsync(uri.PathAndQuery, content);
                                        }
                                        else if (isPatch)
                                        {
                                            //content.Headers.ContentType = new MediaTypeHeaderValue("application/json-patch+json");
                                            task = httpClientHolder.HttpClient.PatchAsync(uri.PathAndQuery, content);
                                        }
                                        else if (isDelete)
                                        {
                                            var request = new HttpRequestMessage
                                            {
                                                Content = content,
                                                Method = HttpMethod.Delete,
                                                RequestUri = new Uri(uri.PathAndQuery, UriKind.Relative)
                                            };
                                            task = httpClientHolder.HttpClient.SendAsync(request);
                                        }
                                        else
                                        {
                                            var cts = new CancellationTokenSource();
                                            cts.CancelAfter(1000 * 60 * 5); // 3 seconds
                                            task = httpClientHolder.HttpClient.PostAsync(uri.PathAndQuery, content, cts.Token);
                                        }

                                        task.Wait();
                                        return task.Result;
                                    });

                                // Check if successful
                                if (response.IsSuccessStatusCode &&
                                    response.Content is StreamContent)
                                {
                                    var streamTask = ((StreamContent)response.Content).ReadAsStreamAsync();
                                    streamTask.Wait();

                                    if (isString)
                                    {
                                        result = streamTask.Result.ReadString() as T;
                                    }
                                    else
                                    {
                                        result = parseWithNewtonJson
                                            ? streamTask.Result.ReadObjectNew<T>()
                                            : streamTask.Result.ReadObject<T>();
                                    }
                                }
                                else if (response.IsSuccessStatusCode &&
                                         response.Content
                                             .GetType().ToString().ToLower()
                                             .Contains("httpconnectionresponsecontent"))
                                {
                                    var streamTask2 = response.Content.ReadAsStreamAsync();
                                    streamTask2.Wait();

                                    if (streamTask2.IsCompleted)
                                    {
                                        var content2 = response.Content;

                                        if (isString)
                                        {
                                            result = content2.ReadAsStreamAsync().Result.ReadString() as T;
                                        }
                                        else
                                        {
                                            result = parseWithNewtonJson
                                                ? content2.ReadAsStreamAsync().Result.ReadObjectNew<T>()
                                                : content2.ReadAsStreamAsync().Result.ReadObject<T>();
                                        }
                                    }

                                    if (typeof(T) == typeof(StatusResponse))
                                    {
                                        if (result == null)
                                        {
                                            result = JsonConvert.DeserializeObject<T>("{}");
                                        }
                                        result.GetType().GetProperty("StatusCode")
                                            ?.SetValue(result, (int)response.StatusCode);

                                        result.GetType().GetProperty("IsSuccessStatusCode")
                                            ?.SetValue(result, response.IsSuccessStatusCode);

                                        result.GetType().GetProperty("Status")
                                            ?.SetValue(result, response.IsSuccessStatusCode);
                                    }

                                }
                                else
                                {
                                    Task<Stream> streamTask = null;
                                    Task<string> errorMessageContent = null;

                                    if (response.Content.GetType() == typeof(StreamContent))
                                        streamTask = ((StreamContent)response.Content).ReadAsStreamAsync();
                                    else
                                        errorMessageContent = response.Content.ReadAsStringAsync();

                                    streamTask?.Wait();
                                    errorMessageContent?.Wait();

                                    try
                                    {
                                        resultResponse = streamTask?.Result.ReadObject<ResultResponse>();
                                    }
                                    catch
                                    {
                                        resultResponse = default;
                                    }


                                    if (errorMessageContent != null)
                                    {
                                        resultResponse = new ResultResponse
                                        {
                                            Status = false,
                                            Messages = new Dictionary<string, string[]>
                                        {
                                            {"ErrorMessageContent", new[] {errorMessageContent.Result}}
                                        }
                                        };
                                    }
                                    else
                                    {
                                        var responseStream = response.Content.ReadAsStringAsync();
                                        responseStream.Wait();
                                        var responseString = responseStream.Result;

                                        resultResponse = new ResultResponse
                                        {
                                            Status = false,
                                            Messages = new Dictionary<string, string[]>
                                        {
                                            {"Response", new[] {responseString}}
                                        }
                                        };
                                    }
                                }

                                break;
                            }
                    }
                }
            }
            catch (BrokenCircuitException)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"CircuitBreakerError", new[] {
                            "Circuit breaker is open due to consecutive failures. " +
                            "Requests are temporarily blocked. Please try again later."
                        }}
                    }
                };
                result = null;
            }
            catch (HttpRequestException hre) when (Route4MeConfig.RetryCount > 0)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"RetryExhausted", new[] {
                            $"Request failed after {Route4MeConfig.RetryCount} retry attempts: {hre.Message}"
                        }}
                    }
                };
                result = null;
            }
            catch (HttpListenerException e)
            {
                resultResponse = new ResultResponse
                {
                    Status = false
                };

                if (e.Message != null)
                    resultResponse.Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {e.Message}}
                    };

                if ((e.InnerException?.Message ?? null) != null)
                {
                    if (resultResponse.Messages == null)
                        resultResponse.Messages = new Dictionary<string, string[]>();

                    resultResponse.Messages.Add("Error", new[] { e.InnerException.Message });
                }

                result = null;
            }
            catch (Exception e)
            {
                resultResponse = new ResultResponse
                {
                    Status = false
                };

                if (e.Message != null)
                    resultResponse.Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {e.Message}}
                    };

                if ((e.InnerException?.Message ?? null) != null)
                {
                    resultResponse.Messages.Add("InnerException Error", new[] { e.InnerException.Message });
                }

                result = default;
            }

            return result;
        }
    }
}