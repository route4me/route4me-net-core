using System;
using System.Collections.Generic;
//using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Route4MeSDK.DataTypes;
//using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes;
using Route4MeSDKLibrary.DataTypes;

namespace Route4MeSDK.Services
{
	public class ApiKeys
	{
		public static string ActualApiKey = R4MeUtils.ReadSetting("actualApiKey");
		public static string DemoApiKey = R4MeUtils.ReadSetting("demoApiKey");

	}

	public class Route4MeApi4Service
	{
		public HttpClient Client { get; }

		public Route4MeApi4Service(HttpClient client)
		{
			client.BaseAddress = new Uri(R4MEInfrastructureSettings.MainHost);
			client.DefaultRequestHeaders.Add("X-Api-Key", ApiKeys.ActualApiKey);
			client.DefaultRequestHeaders.Add("Accept", "application/json");
			client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
			{
				NoCache = true,
				NoStore = true,
				MaxAge = new TimeSpan(0),
				MustRevalidate = true
			};

			Client = client;
		}

		/// <summary>
		/// Converts ErrorResponse type object to printable String type text.
		/// </summary>
		/// <param name="errorResponse">Error response</param>
		/// <returns>Printable String type text</returns>
		public string ErrorResponseToString(ErrorResponse errorResponse)
        {
			string errorText;

			if (errorResponse == null || errorResponse.GetType() == typeof(ErrorResponse)) return "";

			errorText = $"Code: {errorResponse.Code}{Environment.NewLine}" +
				$"Status: {errorResponse.Status}{Environment.NewLine}" +
				$"Exit Code: {errorResponse.ExitCode}";

			if ((errorResponse?.Messages?.Count ?? 0)>0)
            {
				foreach (var msg in errorResponse.Messages)
				{
					errorText += Environment.NewLine;
					errorText += $"{msg.Key}: {msg.Value}";
				}
			}

			return errorText;
		}

		/// <summary>
		/// Retruns ErrorResponse type object.
		/// </summary>
		/// <param name="errorStrings">An array of the error strings</param>
		/// <param name="code">Error code</param>
		/// <returns>ErrorResponse type object</returns>
		public ErrorResponse ErrorStringsToErrorResponse(string[] errorStrings, int code = 400)
        {
			var errorResponse = new ErrorResponse();

			errorResponse.Status = false;
			errorResponse.Code = code;
			errorResponse.ExitCode = 1;
			errorResponse.Messages = new Dictionary<string, string[]>() { { "Error", errorStrings } };
			//errorResponse.Messages.Add("Error", errorStrings);

			return errorResponse;
		}

		public DataObjectRoute RemoveDuplicatedAddressesFromRoute(DataObjectRoute route, bool ShiftByTimeZone = false)
		{
			var lsAddress = new List<Address>();

			foreach (var addr1 in route.Addresses)
			{
				if (!lsAddress.Contains(addr1) &&
					lsAddress
					.Where(x => x.RouteDestinationId == addr1.RouteDestinationId)
					.FirstOrDefault() == null) lsAddress.Add(addr1);
			}

			route.Addresses = lsAddress.ToArray();

			if (route != null && route.GetType() == typeof(DataObjectRoute) && ShiftByTimeZone)
			{
				route = ShiftRouteDateTimeByTz(route);
			}

			return route;
		}

		/// <summary>
		/// The response from the activities getting process.
		/// </summary>
		[DataContract]
		public sealed class GetActivitiesResponse
		{
			/// <value>An array of the Activity type objects</value>
			[DataMember(Name = "results")]
			public Activity[] Results { get; set; }

			/// <value>The number of the Activity type objects/value>
			[DataMember(Name = "total")]
			public uint Total { get; set; }
		}

		/// <summary>
		/// Response data structure. As default in the failure case, 
		/// sometimes - in the success case too.
		/// </summary>
		[DataContract]
		public class ErrorResponse
		{
			/// <summary>
			/// Status  (true/false)
			/// </summary>
			[DataMember(Name = "status")]
			public bool Status { get; set; }

			/// <summary>
			/// Status code
			/// </summary>
			[DataMember(Name = "code")]
			public int Code { get; set; }

			/// <summary>
			/// Exit code
			/// </summary>
			[DataMember(Name = "exit_code")]
			public int ExitCode { get; set; }

			/// <summary>
			/// An array of the error messages.
			/// </summary>
			[DataMember(Name = "messages")]
			public Dictionary<string, string[]> Messages { get; set; }
		}

		/// <summary>
		/// The response returned by the get optimizations command
		/// </summary>
		[DataContract]
		public sealed class DataObjectOptimizations
		{
			/// <value>Array of the returned optimization problems </value>
			[DataMember(Name = "optimizations")]
			public DataObject[] Optimizations { get; set; }

			/// <value>The number of the returned optimization problems </value>
			[DataMember(Name = "totalRecords")]
			public int TotalRecords { get; set; }
		}

		/// <summary>
		/// The response object from a route destination moving process.
		/// </summary>
		[DataContract]
		public sealed class MoveDestinationToRouteResponse
		{
			/// <value>If true the destination was removed successfully</value>
			[DataMember(Name = "success")]
			public Boolean Success { get; set; }

			/// <value>The error string</value>
			[DataMember(Name = "error")]
			public string Error { get; set; }
		}

		/// <summary>
		/// The response object from the routes merging process.
		/// </summary>
		[DataContract]
		public sealed class MergeRoutesResponse
		{
			/// <value>If true the routes was merged successfully</value>
			[DataMember(Name = "status")]
			public Boolean Status { get; set; }

			/// Optimization problem ID
			[DataMember(Name = "optimization_problem_id")]
			public string OptimizationProblemId { get; set; }
		}

		[DataContract()]
		public sealed class UpdateRouteDestinationRequest : Address
        {
			/// <value>A route ID to be updated</value>
			[HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
			public new string RouteId { get; set; }

			/// <value>A optimization ID to be updated</value>
			[HttpQueryMemberAttribute(Name = "optimization_problem_id", EmitDefaultValue = false)]
			public new string OptimizationProblemId { get; set; }

			/// <value>A route destination ID to be updated</value>
			[HttpQueryMemberAttribute(Name = "route_destination_id", EmitDefaultValue = false)]
			public new int? RouteDestinationId { get; set; }
		}

		/// <summary>
		/// The request parameters for an optimization removing
		/// </summary>
		[DataContract()]
		public sealed class RemoveOptimizationRequest : GenericParameters
		{
			/// <value>If true will be redirected</value>
			[HttpQueryMemberAttribute(Name = "redirect", EmitDefaultValue = false)]
			public int Redirect { get; set; }

			/// <value>The array of the optimization problem IDs to be removed</value>
			[DataMember(Name = "optimization_problem_ids", EmitDefaultValue = false)]
			public string[] OptimizationProblemIds { get; set; }
		}

		/// <summary>
		/// The response returned by the remove optimization command
		/// </summary>
		[DataContract]
		public sealed class RemoveOptimizationResponse
		{
			/// <value>True if an optimization was removed successfuly </value>
			[DataMember(Name = "status")]
			public bool Status { get; set; }

			/// <value>The number of the removed optimizations </value>
			[DataMember(Name = "removed")]
			public int Removed { get; set; }
		}

		/// <summary>
		/// The information about an address
		/// </summary>
		[DataContract]
		public class AddressInfo : GenericParameters
		{
			/// <value>The destination ID</value>
			[DataMember(Name = "route_destination_id")]
			public int DestinationId { get; set; }

			/// <value>The destination's sequence number in a route</value>
			[DataMember(Name = "sequence_no")]
			public int SequenceNo { get; set; }

			/// <value>If true the destination is depot</value>
			[DataMember(Name = "is_depot")]
			public bool IsDepot { get; set; }
		}

		/// <summary>
		/// The request parameters for manually resequencing of a route
		/// </summary>
		[DataContract()]
		public sealed class ManuallyResequenceRouteRequest : GenericParameters
		{
			/// <value>The route ID to be resequenced</value>
			[HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
			public string RouteId { get; set; }

			/// <value>The manually resequenced addresses</value>
			[DataMember(Name = "addresses")]
			public AddressInfo[] Addresses { get; set; }
		}

		/// <summary>
		/// The request parameters for a address marking as the departed process.
		/// </summary>
		[DataContract]
		public sealed class MarkAddressDepartedRequest : GenericParameters
		{
			/// <value>The route ID</value>
			[HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
			public string RouteId { get; set; }

			/// <value>The route destination ID</value>
			[HttpQueryMemberAttribute(Name = "address_id", EmitDefaultValue = false)]
			public int? AddressId { get; set; }

			/// <value>If true an addres will be marked as departed</value>
			[IgnoreDataMember()]
			[HttpQueryMemberAttribute(Name = "is_departed", EmitDefaultValue = false)]
			public bool IsDeparted { get; set; }

			/// <value>If true an addres will be marked as visited</value>
			[IgnoreDataMember()]
			[HttpQueryMemberAttribute(Name = "is_visited", EmitDefaultValue = false)]
			public bool IsVisited { get; set; }

			/// <value>The member ID</value>
			[HttpQueryMemberAttribute(Name = "member_id", EmitDefaultValue = false)]
			public int? MemberId { get; set; }
		}

		/// <summary>
		/// The response from a route duplicating process
		/// </summary>
		[DataContract]
		public sealed class DuplicateRouteResponse
		{
			/// If true, the route(s) duplicated successfully
			[DataMember(Name = "status")]
			public bool Status { get; set; }

			/// An array of the duplicated route IDs
			[DataMember(Name = "route_ids")]
			public string[] RouteIDs { get; set; }
		}

		/// <summary>
		/// The response from the address note adding process. 
		/// </summary>
		[DataContract]
		public sealed class AddAddressNoteResponse
		{
			/// <value>If true an address note added successfuly</value>
			[DataMember(Name = "status")]
			public bool Status { get; set; }

			/// <value>The address note ID</value>
			[DataMember(Name = "note_id")]
			public string NoteID { get; set; }

			/// <value>The upload ID</value>
			[DataMember(Name = "upload_id")]
			public string UploadID { get; set; }

			/// <value>The AddressNote type object</value>
			[DataMember(Name = "note")]
			public AddressNote Note { get; set; }
		}

		/// <summary>
		/// The response for the get users process
		/// </summary>
		[DataContract]
		public sealed class GetUsersResponse
		{
			/// <value>The array of the User objects</value>
			[DataMember(Name = "results")]
			public MemberResponseV4[] Results { get; set; }
		}

		/// <summary>
		/// The request parameters for the route custom data updating process
		/// </summary>
		[DataContract()]
		public sealed class UpdateRouteCustomDataRequest : GenericParameters
		{
			/// <value>A route ID to be updated</value>
			[HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
			public string RouteId { get; set; }

			/// <value>A route destination ID to be updated</value>
			[HttpQueryMemberAttribute(Name = "route_destination_id", EmitDefaultValue = false)]
			public int? RouteDestinationId { get; set; }

			/// <value>The changed/new custom fields of a route destination</value>
			[DataMember(Name = "custom_fields", EmitDefaultValue = false)]
			public Dictionary<string, string> CustomFields { get; set; }
		}

		/// <summary>
		/// The response from the route deleting process
		/// </summary>
		[DataContract]
		public sealed class DeleteRouteResponse
		{
			/// <value>If true, the route was deleted successfuly</value>
			[DataMember(Name = "deleted")]
			public Boolean Deleted { get; set; }

			/// <value>The array of the error strings</value>
			[DataMember(Name = "errors")]
			public List<String> Errors { get; set; }

			/// <value>The deleted route ID</value>
			[DataMember(Name = "route_id")]
			public string RouteId { get; set; }

			/// <value>The array of the deleted routes IDs</value>
			[DataMember(Name = "route_ids")]
			public string[] RouteIds { get; set; }
		}

		/// <summary>
		/// The request parameters for the custom note type adding process.
		/// </summary>
		[DataContract]
		public sealed class AddCustomNoteTypeRequest : GenericParameters
		{
			/// <value>The custom note type</value>
			[DataMember(Name = "type", EmitDefaultValue = false)]
			public string Type { get; set; }

			/// <value>An array of the custom note values</value>
			[DataMember(Name = "values", EmitDefaultValue = false)]
			public string[] Values { get; set; }
		}

		/// <summary>
		/// The response from the custom note type adding process.
		/// </summary>
		[DataContract]
		public sealed class AddCustomNoteTypeResponse
		{
			/// <value>Added custom note</value>
			[DataMember(Name = "result")]
			public string Result { get; set; }

			/// <value>How many destination were affected by adding process</value>
			[DataMember(Name = "affected")]
			public int Affected { get; set; }
		}

		/// <summary>
		/// The request parameter for the customer removing process.
		/// </summary>
		[DataContract]
		public sealed class RemoveCustomNoteTypeRequest : GenericParameters
		{
			/// <value>A custom note type ID></value>
			[DataMember(Name = "id", EmitDefaultValue = false)]
			public int Id { get; set; }
		}

		/// <summary>
		/// The response from the getting process of the address book contacts
		/// </summary>
		[DataContract]
		public sealed class GetAddressBookContactsResponse : GenericParameters
		{
			/// <value>Array of the AddressBookContact type objects</value>
			[DataMember(Name = "results", IsRequired = false)]
			public AddressBookContact[] Results { get; set; }

			/// <value>Number of the returned address book contacts</value>
			[DataMember(Name = "total", IsRequired = false)]
			public uint Total { get; set; }
		}

		/// <summary>
		/// The request parameters for the address book locations searching process.
		/// </summary>
		[DataContract()]
		public sealed class SearchAddressBookLocationRequest : GenericParameters
		{
			/// <value>Comma-delimited list of the contact IDs</value>
			[HttpQueryMemberAttribute(Name = "address_id", EmitDefaultValue = false)]
			public string AddressId { get; set; }

			/// <value>The query text</value>
			[HttpQueryMemberAttribute(Name = "query", EmitDefaultValue = false)]
			public string Query { get; set; }

			/// <value>The comma-delimited list of the fields</value>
			[HttpQueryMemberAttribute(Name = "fields", EmitDefaultValue = false)]
			public string Fields { get; set; }

			/// <value>Search starting position</value>
			[HttpQueryMemberAttribute(Name = "offset", EmitDefaultValue = false)]
			public int? Offset { get; set; }

			/// <value>The number of records to return</value>
			[HttpQueryMemberAttribute(Name = "limit", EmitDefaultValue = false)]
			public int? Limit { get; set; }
		}

		/// <summary>
		/// The response from the address book locations searching process.
		/// </summary>
		[DataContract()]
		public sealed class SearchAddressBookLocationResponse
		{
			/// <value>The list of the selected fields values</value>
			[DataMember(Name = "results")]
			public List<object[]> Results { get; set; }

			/// <value>Number of the returned address book contacts</value>
			[DataMember(Name = "total")]
			public uint Total { get; set; }

			/// <value>Array of the selected fields</value>
			[DataMember(Name = "fields")]
			public string[] Fields { get; set; }
		}

		/// <summary>
		/// The request parameter for the address book contacts removing process.
		/// </summary>
		[DataContract]
		public sealed class RemoveAddressBookContactsRequest : GenericParameters
		{
			/// <value>The array of the address IDs</value>
			[DataMember(Name = "address_ids", EmitDefaultValue = false)]
			public string[] AddressIds { get; set; }
		}





		/// <summary>
		/// Shift the route date and route time to make them as shown in the web app.
		/// </summary>
		/// <param name="route">Input route object</param>
		/// <returns>Modified route object</returns>
		public DataObjectRoute ShiftRouteDateTimeByTz(DataObjectRoute route)
		{
			var tz = R4MeUtils.GetLocalTimeZone();
			long totalTime = (long)(route.Parameters.RouteDate + route.Parameters.RouteTime);

			totalTime += tz;

			route.Parameters.RouteDate = (totalTime / 86400) * 86400;
			route.Parameters.RouteTime = (int)(totalTime - route.Parameters.RouteDate);

			return route;
		}




		public async Task<Tuple<T, ErrorResponse>> Get<T>(string url, bool isString)
			  where T : class
        {
			T result = default(T);
			var errorResponse = default(ErrorResponse);
			var response = default(Stream);

			try
            {
				response = await Client.GetStreamAsync(url).ConfigureAwait(false);

				result = isString ? response.ReadString() as T :
										response.ReadObject<T>();
			}
			catch (System.Net.HttpListenerException e)
			{
				errorResponse = new ErrorResponse()
				{
					Status = false
				};

				if (e.Message != null)
				{
					errorResponse.Messages = new Dictionary<string, string[]>()
					{
						{ "Error", new string[] { e.Message } }
					};
				}

				if ((e.InnerException?.Message ?? null) != null)
				{
					if (errorResponse.Messages == null)
						errorResponse.Messages = new Dictionary<string, string[]>();

					errorResponse.Messages.Add("Error", new string[] { e.InnerException.Message });
				}

				result = null;
			}
			catch (Exception e)
			{
				errorResponse = new ErrorResponse()
				{
					Status = false
				};

				if (e.Message != null)
				{
					errorResponse.Messages = new Dictionary<string, string[]>()
					{
						{ "Error", new string[] { e.Message } }
					};
				}

				if ((e.InnerException?.Message ?? null) != null)
				{
					if (errorResponse.Messages == null)
						errorResponse.Messages = new Dictionary<string, string[]>();

					errorResponse.Messages.Add("InnerException Error", new string[] { e.InnerException.Message });
				}

				result = default(T);
			}

			return new Tuple<T, ErrorResponse>(result, errorResponse);
		}


		public async Task<Tuple<T, ErrorResponse>> Post<T>(GenericParameters optimizationParameters,
									   string url,
									   HttpContent httpContent,
									   bool isString) where T : class
		{
			T result = default;

			var errorResponse = default(ErrorResponse);

			try
            {
				var request = new HttpRequestMessage(HttpMethod.Post, url);

				HttpContent content = null;

				if (httpContent != null)
				{
					content = httpContent;
				}
				else
				{
					string jsonString = R4MeUtils.SerializeObjectToJson(optimizationParameters);
					content = new StringContent(jsonString, Encoding.UTF8, "application/json");
				}

				var response = await Client.PostAsync(url, content).ConfigureAwait(false);

				string responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

				if (response.IsSuccessStatusCode)
				{
					result = isString ? responseJson as T :
											R4MeUtils.ReadObjectNew<T>(responseJson);
				}
                else
                {
					using (var streamTask = await (response.Content).ReadAsStreamAsync().ConfigureAwait(false))
					{
						try
						{
							errorResponse = streamTask.ReadObject<ErrorResponse>();
						}
						catch (Exception)
						{
							if ((response?.ReasonPhrase ?? null) != null)
							{
								errorResponse = new ErrorResponse()
								{
									Status = false,
									Messages = new Dictionary<string, string[]>()
											{
												{"ErrorMessageContent", new string[] { response.ReasonPhrase } }
											}
								};

								var reqMessage = response?.RequestMessage?.Content.ReadAsStringAsync().Result ?? "";

								if (reqMessage != "") 
									errorResponse
										.Messages
										.Add("Request content", new string[] { reqMessage });
							}
							else
							{
								errorResponse = default(ErrorResponse);
							}
						}

					};
				}

				response.Dispose();

				return new Tuple<T, ErrorResponse>(result, errorResponse);
			}
            catch (Exception ex)
            {
				errorResponse = new ErrorResponse()
				{
					Status = false,
					Messages = new Dictionary<string, string[]>()
											{
												{"Response", new string[] { ex.Message } }
											}
				};

				return new Tuple<T, ErrorResponse>(result, errorResponse);

			}
		
		}

		public async Task<Tuple<T, ErrorResponse>> Put<T>(GenericParameters optimizationParameters,
									   string url,
									   HttpContent httpContent,
									   bool isString) where T : class
        {
			T result = default;
			var errorResponse = default(ErrorResponse);

			try
			{
				var request = new HttpRequestMessage(HttpMethod.Put, url);

				HttpContent content = null;

				if (httpContent != null)
				{
					content = httpContent;
				}
				else
				{
					string jsonString = R4MeUtils.SerializeObjectToJson(optimizationParameters);
					content = new StringContent(jsonString, Encoding.UTF8, "application/json");
				}

				var response = await Client.PutAsync(url, content).ConfigureAwait(false);

				string responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

				if (response.IsSuccessStatusCode)
				{
					result = isString ? responseJson as T :
											R4MeUtils.ReadObjectNew<T>(responseJson);
				}
				else
				{
					using (var streamTask = await (response.Content).ReadAsStreamAsync().ConfigureAwait(false))
					{
						try
						{
							errorResponse = streamTask.ReadObject<ErrorResponse>();
						}
						catch (Exception)
						{
							if ((response?.ReasonPhrase ?? null) != null)
							{
								errorResponse = new ErrorResponse()
								{
									Status = false,
									Messages = new Dictionary<string, string[]>()
											{
												{"ErrorMessageContent", new string[] { response.ReasonPhrase } }
											}
								};

								var reqMessage = response?.RequestMessage?.Content.ReadAsStringAsync().Result ?? "";

								if (reqMessage != "")
									errorResponse
										.Messages
										.Add("Request content", new string[] { reqMessage });
							}
							else
							{
								errorResponse = default(ErrorResponse);
							}
						}

					};
				}

				response.Dispose();

				return new Tuple<T, ErrorResponse>(result, errorResponse);
			}
			catch (Exception ex)
			{
				errorResponse = new ErrorResponse()
				{
					Status = false,
					Messages = new Dictionary<string, string[]>()
											{
												{"Response", new string[] { ex.Message } }
											}
				};

				return new Tuple<T, ErrorResponse>(result, errorResponse);

			}
		}

		public async Task<Tuple<T, ErrorResponse>> Delete<T>(GenericParameters optimizationParameters,
									   string url,
									   HttpContent httpContent,
									   bool isString) where T : class
        {
			T result = default;
			var errorResponse = default(ErrorResponse);

            try
            {
				HttpContent content = null;

				if (httpContent != null)
				{
					content = httpContent;
				}
				else
				{
					string jsonString = R4MeUtils.SerializeObjectToJson(optimizationParameters);
					content = new StringContent(jsonString, Encoding.UTF8, "application/json");
				}

				var request = new HttpRequestMessage
				{
					Content = content,
					Method = HttpMethod.Delete,
					RequestUri = new Uri(url, UriKind.Absolute)
				};

				var response = await Client.SendAsync(request).ConfigureAwait(false);
				string responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

				if (response.IsSuccessStatusCode)
				{
					result = isString ? responseJson as T :
											R4MeUtils.ReadObjectNew<T>(responseJson);
				}
                else
                {
					using (var streamTask = await (response.Content).ReadAsStreamAsync().ConfigureAwait(false))
                    {
						try
						{
							errorResponse = streamTask.ReadObject<ErrorResponse>();
						}
						catch (Exception)
						{
							if ((response?.ReasonPhrase ?? null) != null)
							{
								errorResponse = new ErrorResponse()
								{
									Status = false,
									Messages = new Dictionary<string, string[]>()
											{
												{"ErrorMessageContent", new string[] { response.ReasonPhrase } }
											}
								};

								var reqMessage = response?.RequestMessage?.Content.ReadAsStringAsync().Result ?? "";

								if (reqMessage != "")
									errorResponse
										.Messages
										.Add("Request content", new string[] { reqMessage });
							}
							else
							{
								errorResponse = default(ErrorResponse);
							}
						}
					}

				}

				response.Dispose();

				return new Tuple<T, ErrorResponse>(result, errorResponse);
			}
            catch (Exception ex)
            {
				errorResponse = new ErrorResponse()
				{
					Status = false,
					Messages = new Dictionary<string, string[]>()
											{
												{"Response", new string[] { ex.Message } }
											}
				};

				return new Tuple<T, ErrorResponse>(result, errorResponse);
			}
		}
	}
}
