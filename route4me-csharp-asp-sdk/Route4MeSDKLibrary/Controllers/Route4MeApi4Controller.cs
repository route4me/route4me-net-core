using Microsoft.AspNetCore.Mvc;
using Route4MeSDK.DataTypes;
//using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes;
using Route4MeSDK.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Route4MeSDK.Services.Route4MeApi4Service;

namespace Route4MeSDK.Controllers
{
    [ApiController]
    public class Route4MeApi4Controller : Controller
    {
        private readonly Route4MeApi4Service _r4mApi4Service;

		private HttpClient _client;

		private string[] mandatoryFields;

		public Route4MeApi4Controller(Route4MeApi4Service r4mApi4Service, HttpClient client)
        {
            _r4mApi4Service = r4mApi4Service;
			_client = client;

		}

        #region Activities
        public async Task<Tuple<GetActivitiesResponse, ErrorResponse>> GetActivityFeed(ActivityParameters activityParameters)
        {
			var result = await Get<GetActivitiesResponse>(
					activityParameters,
					R4MEInfrastructureSettings.GetActivitiesHost,
					false
				).ConfigureAwait(false);

			return result;
		}

		public async Task<Tuple<StatusResponse, ErrorResponse>> LogCustomActivity(Activity activity)
		{
			var result = await Post<StatusResponse>(activity,
												R4MEInfrastructureSettings.ActivityFeedHost,
												null,
												false).ConfigureAwait(false);

			return result;
		}

		#endregion

		#region Optimizations

		public async Task<Tuple<DataObjectOptimizations, ErrorResponse>> GetOptimizations(OptimizationParameters optimizationParameters)
		{
			var result = await Get<DataObjectOptimizations>(
					optimizationParameters,
					R4MEInfrastructureSettings.ApiHost,
					false
				).ConfigureAwait(false);

			return result;
		}

		public async Task<Tuple<DataObject, ErrorResponse>> GetOptimization(OptimizationParameters optimizationParameters)
		{
			var result = await Get<DataObject>(
					optimizationParameters,
					R4MEInfrastructureSettings.ApiHost,
					false
				).ConfigureAwait(false);

			return result;
		}


		public async Task<Tuple<DataObject, ErrorResponse>> RunOptimization(OptimizationParameters optimizationParameters)
		{
			var result = await Post<DataObject>(optimizationParameters,
												R4MEInfrastructureSettings.ApiHost,
												null,
                                                false).ConfigureAwait(false);

            return result;
		}

		public async Task<Tuple<DataObject[], ErrorResponse>> RunOptimizationByOrderTerritories(OptimizationParameters optimizationParameters)
		{
			var result = await Post<DataObject[]>(optimizationParameters,
												R4MEInfrastructureSettings.ApiHost,
												null,
												false).ConfigureAwait(false);

			return result;
		}

		public async Task<Tuple<DataObject, ErrorResponse>> UpdateOptimization(OptimizationParameters optimizationParameters)
		{
			var result = await Put<DataObject>(optimizationParameters,
												R4MEInfrastructureSettings.ApiHost,
												null,
												false).ConfigureAwait(false);

			return result;
		}

		public async Task<Tuple<Address, ErrorResponse>> UpdateOptimizationDestination(Address addressParameters)
        {
			var request = new OptimizationParameters()
			{
				OptimizationProblemID = addressParameters.OptimizationProblemId,
				Addresses = new Address[] { addressParameters }
			};

			var result = await Put<DataObject>(request,
												R4MEInfrastructureSettings.ApiHost,
												null,
												false).ConfigureAwait(false);

			var updatedAddress = result?.Item1?.Addresses?
				.Where(x => x.RouteDestinationId == addressParameters.RouteDestinationId)
				.FirstOrDefault() ?? null;

			var resultTuple = Tuple.Create(updatedAddress, result.Item2);

			return resultTuple;
		}

		public async Task<Tuple<RemoveOptimizationResponse, ErrorResponse>> RemoveOptimization(string[] optimizationProblemIDs)
		{
			var remParameters = new RemoveOptimizationRequest()
			{
				Redirect = 0,
				OptimizationProblemIds = optimizationProblemIDs
			};

			var result = await Delete<RemoveOptimizationResponse>(remParameters,
												R4MEInfrastructureSettings.ApiHost,
												null,
												false).ConfigureAwait(false);

			return result;
		}

		#endregion

		#region Routes

		public async Task<Tuple<DataObjectRoute[], ErrorResponse>> GetRoutes(RouteParametersQuery routeParameters)
		{
			var result = await Get<DataObjectRoute[]>(
					routeParameters,
					R4MEInfrastructureSettings.RouteHost,
					false
				).ConfigureAwait(false);

			return result;
		}

		public async Task<Tuple<DataObjectRoute, ErrorResponse>> GetRoute(RouteParametersQuery routeParameters)
		{
			var result = await Get<DataObjectRoute>(
					routeParameters,
					R4MEInfrastructureSettings.RouteHost,
					false
				).ConfigureAwait(false);

			return result;
		}

		public async Task<Tuple<MoveDestinationToRouteResponse, ErrorResponse>> MoveDestinationToRoute(string toRouteId, 
																							int routeDestinationId, 
																							int afterDestinationId)
		{
			var keyValues = new List<KeyValuePair<string, string>>()
			{
				new KeyValuePair<string, string>("to_route_id", toRouteId),
				new KeyValuePair<string, string>("route_destination_id", Convert.ToString(routeDestinationId)),
				new KeyValuePair<string, string>("after_destination_id", Convert.ToString(afterDestinationId))
			};

			using (HttpContent httpContent = new FormUrlEncodedContent(keyValues))
            {
				var result = await Post<MoveDestinationToRouteResponse>(new GenericParameters(),
												R4MEInfrastructureSettings.MoveRouteDestination,
												httpContent,
												false).ConfigureAwait(false);

				return result;
			};
		}

		public async Task<Tuple<MergeRoutesResponse, ErrorResponse>> MergeRoutes(MergeRoutesQuery mergeRoutesParameters)
		{
			var keyValues = new List<KeyValuePair<string, string>>()
			{
				new KeyValuePair<string, string>("route_ids", mergeRoutesParameters.RouteIds),
				new KeyValuePair<string, string>("depot_address", mergeRoutesParameters.DepotAddress),
				new KeyValuePair<string, string>("remove_origin", mergeRoutesParameters.RemoveOrigin.ToString()),
				new KeyValuePair<string, string>("depot_lat", mergeRoutesParameters.DepotLat.ToString()),
				new KeyValuePair<string, string>("depot_lng", mergeRoutesParameters.DepotLng.ToString())
			};

			using (HttpContent httpContent = new FormUrlEncodedContent(keyValues))
			{
				var result = await Post<MergeRoutesResponse>(new GenericParameters(),
												R4MEInfrastructureSettings.MergeRoutes,
												httpContent,
												false).ConfigureAwait(false);

				return result;
			};
		}

		public async Task<Tuple<DataObjectRoute, ErrorResponse>> ManuallyResequenceRoute(RouteParametersQuery rParams, Address[] addresses)
		{
			var request = new ManuallyResequenceRouteRequest()
			{
				RouteId = rParams.RouteId,
			};

			var lsAddresses = new List<AddressInfo>();

			int iMaxSequenceNumber = 0;

			foreach (var address in addresses)
			{
				var aInfo = new AddressInfo()
				{
					DestinationId = address.RouteDestinationId != null ? (int)address.RouteDestinationId : -1,
					SequenceNo = address.SequenceNo != null ? (int)address.SequenceNo : iMaxSequenceNumber
				};

				lsAddresses.Add(aInfo);

				iMaxSequenceNumber++;
			}

			request.Addresses = lsAddresses.ToArray();

			var result = await Put<DataObjectRoute>(request,
												R4MEInfrastructureSettings.RouteHost,
												null,
												false).ConfigureAwait(false);

			return result;
		}

		public async Task<Tuple<DataObjectRoute, ErrorResponse>> ReoptimizeRoute(RouteParametersQuery queryParams)
		{
			var result = await Put<DataObjectRoute>(queryParams,
												R4MEInfrastructureSettings.RouteHost,
												null,
												false).ConfigureAwait(false);

			return result;
		}

		public async Task<Tuple<ScheduleCalendarResponse, ErrorResponse>>  GetScheduleCalendar(ScheduleCalendarQuery scheduleCalendarParams)
		{
			var response = await Get<ScheduleCalendarResponse>(
				scheduleCalendarParams,
				R4MEInfrastructureSettings.ScheduleCalendar,
				false).ConfigureAwait(false);

			return response;
		}

		public async Task<Tuple<DataObjectRoute, ErrorResponse>> MarkAddressVisited(AddressParameters aParams)
		{
			var request = new MarkAddressDepartedRequest
			{
				RouteId = aParams.RouteId,
				AddressId = aParams.AddressId,
				IsVisited = aParams.IsVisited,
				MemberId = 1
			};

			var result = await Get<DataObjectRoute>(
					request,
					R4MEInfrastructureSettings.MarkAddressVisited,
					false
				).ConfigureAwait(false);

			return result;
		}

		public async Task<Tuple<DataObjectRoute, ErrorResponse>> UpdateRoute(RouteParametersQuery routeParameters)
		{
			var result = await Put<DataObjectRoute>(routeParameters,
												R4MEInfrastructureSettings.RouteHost,
												null,
												false).ConfigureAwait(false);

			if ((result?.Item1 ?? null) != null && result.Item1.GetType() == typeof(DataObjectRoute) && routeParameters.ShiftByTimeZone)
			{
				result = new Tuple<DataObjectRoute, ErrorResponse>(_r4mApi4Service.ShiftRouteDateTimeByTz(result.Item1), result.Item2);
			}

			return result;
		}

		public async Task<Tuple<DuplicateRouteResponse, ErrorResponse>> DuplicateRoute(RouteParametersQuery queryParameters)
		{
			var result = await Post<DuplicateRouteResponse>(queryParameters,
												R4MEInfrastructureSettings.RouteHost,
												null,
												false).ConfigureAwait(false);

			return result;
		}

		public async Task<Tuple<DataObjectRoute, ErrorResponse>> UpdateRoute(DataObjectRoute route, DataObjectRoute initialRoute)
		{
			string errorString = "";
			//bool parseWithNewtonJson = true;

			var errorResponse = new ErrorResponse();

			if (initialRoute == null)
			{
				errorResponse = _r4mApi4Service.ErrorStringsToErrorResponse(
					new string[] { "An initial route should be specified" },
					400);

				return new Tuple<DataObjectRoute, ErrorResponse>(null, errorResponse);
			}

			route = _r4mApi4Service.RemoveDuplicatedAddressesFromRoute(route);
			initialRoute = _r4mApi4Service.RemoveDuplicatedAddressesFromRoute(initialRoute);

			var initialRouteResult = new Tuple<DataObjectRoute, ErrorResponse>(initialRoute, null);

			#region // ApprovedForExecution
			string approvedForExecution = "";

			if (initialRoute.ApprovedForExecution != route.ApprovedForExecution)
			{
				approvedForExecution = String
					.Concat("{\"approved_for_execution\": ", 
					route.ApprovedForExecution ? "true" : "false", "}");

				var genParams = new RouteParametersQuery()
				{
					RouteId = initialRoute.RouteId
				};

				var content = new StringContent(approvedForExecution, Encoding.UTF8, "application/json");

				initialRouteResult = await Put<DataObjectRoute>(
					genParams,
					R4MEInfrastructureSettings.RouteHost,
					content,
					false).ConfigureAwait(false);

				if ((initialRouteResult?.Item1 ?? null) == null)
				{
					errorResponse = _r4mApi4Service.ErrorStringsToErrorResponse(
						new string[] { "Cannot update the route with the field approved_for_execution" },
						400);

					return new Tuple<DataObjectRoute, ErrorResponse>(null, errorResponse);
				}

				initialRoute = initialRouteResult.Item1;
			}

			#endregion

			#region // Resequence if sequence was changed
			string resequenceJson = "";

			foreach (var addr1 in initialRoute.Addresses)
			{
				var addr = route.Addresses.Where(x => x.RouteDestinationId == addr1.RouteDestinationId).FirstOrDefault();

				if (addr1 != null && (addr.SequenceNo != addr1.SequenceNo || addr.IsDepot != addr1.IsDepot))
				{
					resequenceJson += "{\"route_destination_id\":" + addr1.RouteDestinationId;

					if (addr.SequenceNo != addr1.SequenceNo)
					{
						resequenceJson += "," + "\"sequence_no\":" + addr.SequenceNo;
					}
					else if (addr.IsDepot != addr1.IsDepot)
					{
						resequenceJson += "," + "\"is_depot\":" + addr.IsDepot.ToString().ToLower();
					}

					resequenceJson += "},";
				}
			}

			if (resequenceJson.Length > 10)
			{
				resequenceJson = resequenceJson.TrimEnd(',');
				resequenceJson = "{\"addresses\": [" + resequenceJson + "]}";

				var genParams = new RouteParametersQuery()
				{
					RouteId = initialRoute.RouteId
				};

				var content = new StringContent(resequenceJson, Encoding.UTF8, "application/json");

				initialRouteResult = await Put<DataObjectRoute>(
					genParams,
					R4MEInfrastructureSettings.RouteHost,
					content,
					false).ConfigureAwait(false);

				if ((initialRouteResult?.Item1 ?? null) == null)
				{
					errorResponse = _r4mApi4Service.ErrorStringsToErrorResponse(
						new string[] { "An initial route should be specified" },
						400);

					return new Tuple<DataObjectRoute, ErrorResponse>(null, errorResponse);
				}

				initialRoute = initialRouteResult.Item1;
			}

			#endregion

			#region // Update Route Parameters

			if ((route?.Parameters ?? null) != null)
			{
				var updatableRouteParametersProperties = R4MeUtils
					.GetPropertiesWithDifferentValues(route.Parameters, initialRoute.Parameters, out errorString);

				if (updatableRouteParametersProperties != null && updatableRouteParametersProperties.Count > 0)
				{
					var dynamicRouteProperties = new Route4MeDynamicClass();

					dynamicRouteProperties.CopyPropertiesFromClass(route.Parameters, updatableRouteParametersProperties, out string errorString0);

					var routeParamsJsonString = R4MeUtils.SerializeObjectToJson(dynamicRouteProperties.DynamicProperties, true);

					routeParamsJsonString = String.Concat("{\"parameters\":", routeParamsJsonString, "}");

					var genParams = new RouteParametersQuery()
					{
						RouteId = initialRoute.RouteId
					};

					var content = new StringContent(routeParamsJsonString, Encoding.UTF8, "application/json");

					initialRouteResult = await Put<DataObjectRoute>(
						genParams,
						R4MEInfrastructureSettings.RouteHost,
						content,
						false).ConfigureAwait(false);
				}
			}

			if ((initialRouteResult?.Item1 ?? null) == null)
			{
				errorResponse = _r4mApi4Service.ErrorStringsToErrorResponse(
					new string[] { "Cannot update the route with the field approved_for_execution" },
					400);

				return new Tuple<DataObjectRoute, ErrorResponse>(null, errorResponse);
			}

			initialRoute = initialRouteResult.Item1;

			#endregion

			#region // Update Route Addresses

			var lsUpdatedAddresses = new List<Address>();

			if ((route?.Addresses ?? null) != null && route.Addresses.Length > 0)
			{
				foreach (var address in route.Addresses)
				{
					var initialAddress = initialRoute.Addresses
						.Where(x => x.RouteDestinationId == address.RouteDestinationId)
						.FirstOrDefault();

					if (initialAddress == null)
					{
						initialAddress = initialRoute.Addresses
						.Where(x => x.AddressString == address.AddressString)
						.FirstOrDefault();
					}

					if (initialAddress == null) continue;

					var updatableAddressProperties = R4MeUtils
					.GetPropertiesWithDifferentValues(address, initialAddress, out errorString);

					if (updatableAddressProperties.Contains("IsDepot")) updatableAddressProperties.Remove("IsDepot");
					if (updatableAddressProperties.Contains("SequenceNo")) updatableAddressProperties.Remove("SequenceNo");
					if (updatableAddressProperties.Contains("OptimizationProblemId")
						&& updatableAddressProperties.Count == 1) updatableAddressProperties.Remove("OptimizationProblemId");

					if (updatableAddressProperties != null && updatableAddressProperties.Count > 0)
					{
						var dynamicAddressProperties = new Route4MeDynamicClass();

						dynamicAddressProperties.CopyPropertiesFromClass(address, updatableAddressProperties, out string errorString0);

						var addressParamsJsonString = R4MeUtils.SerializeObjectToJson(dynamicAddressProperties.DynamicProperties, true);

						var genParams = new RouteParametersQuery()
						{
							RouteId = initialRoute.RouteId,
							RouteDestinationId = address.RouteDestinationId
						};

						var content = new StringContent(addressParamsJsonString, Encoding.UTF8, "application/json");

						var updatedAddress = await Put<Address>(
							genParams,
							R4MEInfrastructureSettings.GetAddress,
							content,
							false).ConfigureAwait(false);

						updatedAddress.Item1.IsDepot = initialAddress.IsDepot;
						updatedAddress.Item1.SequenceNo = initialAddress.SequenceNo;

						if ((address?.Notes?.Length ?? -1) > 0)
						{
							var addressNotes = new List<AddressNote>();

							foreach (AddressNote note1 in address.Notes)
							{
								if ((note1?.NoteId ?? null) == -1)
								{
									var noteParameters = new NoteParameters()
									{
										RouteId = initialRoute.RouteId,
										AddressId = updatedAddress.Item1.RouteDestinationId != null 
														? (int)updatedAddress.Item1.RouteDestinationId 
														: note1.RouteDestinationId,
										Latitude = note1.Latitude,
										Longitude = note1.Longitude
									};

									if (note1.ActivityType != null) noteParameters.ActivityType = note1.ActivityType;
									if (note1.DeviceType != null) noteParameters.DeviceType = note1.DeviceType;

									string noteContent = note1.Contents != null ? note1.Contents : null;

									if (noteContent != null) noteParameters.StrNoteContents = noteContent;

									Dictionary<string, string> customNotes = null;

									if ((note1?.CustomTypes?.Length ?? -1) > 0)
									{
										customNotes = new Dictionary<string, string>();

										foreach (AddressCustomNote customNote in note1.CustomTypes)
										{
											customNotes.Add("custom_note_type[" + customNote.NoteCustomTypeID + "]", customNote.NoteCustomValue);
										}
									}

									if (customNotes != null) noteParameters.CustomNoteTypes = customNotes;

									if (note1.UploadUrl != null) noteParameters.StrFileName = note1.UploadUrl;

									var addedNoteResult = await AddAddressNote(noteParameters);

									AddressNote addedNote = (addedNoteResult?.Item1?.Note ?? null)!=null 
										? addedNoteResult.Item1.Note 
										: null;

									if (addedNote != null) 
										addressNotes.Add(addedNote);
								}
								else
								{
									addressNotes.Add(note1);
								}
							}

							address.Notes = addressNotes.ToArray();
							updatedAddress.Item1.Notes = addressNotes.ToArray();
						}

						if ((updatedAddress?.Item1 ?? null) != null && updatedAddress.Item1.GetType() == typeof(Address))
						{
							int addressIndex = Array.IndexOf(initialRoute.Addresses, initialAddress);
							if (addressIndex > -1) initialRoute.Addresses[addressIndex] = updatedAddress.Item1;
						}
					}
				}
			}

			#endregion

			return new Tuple<DataObjectRoute, ErrorResponse>(initialRoute, null);
		}

		/// <summary>
		/// Updates a route's custom data
		/// </summary>
		/// <param name="routeParameters">The RouteParametersQuery object contains parameters:
		/// <para>RouteId: a route ID to be updated</para>
		/// <para>RouteDestinationId: the ID of the route destination to be updated</para>
		/// </param>
		/// <param name="customData">The keyvalue pairs of the type Dictionary<string, string></param>
		/// <param name="errorString">Returned error string in case of the processs failing</param>
		/// <returns>Updated route destination</returns>
		public async Task<Tuple<Address, ErrorResponse>> UpdateRouteCustomData(
													RouteParametersQuery routeParameters, 
													Dictionary<string, string> customData)
		{
			var request = new UpdateRouteCustomDataRequest
			{
				RouteId = routeParameters.RouteId,
				RouteDestinationId = routeParameters.RouteDestinationId,
				CustomFields = customData
			};

			var result = await Put<Address>(
						request,
						R4MEInfrastructureSettings.GetAddress,
						null,
						false).ConfigureAwait(false);

			return result;
		}

		/// <summary>
		/// Removes the routes from a user's account
		/// </summary>
		/// <param name="routeIds">The array of the route IDs to be removed</param>
		/// <param name="errorString">Returned error string in case of the processs failing</param>
		/// <returns>Array of the removed routes IDs</returns>
		public async Task<Tuple<DeleteRouteResponse, ErrorResponse>> DeleteRoutes(string[] routeIds)
		{
			string str_route_ids = "";

			foreach (string routeId in routeIds)
			{
				if (str_route_ids.Length > 0) str_route_ids += ",";
				str_route_ids += routeId;
			}

			var genericParameters = new GenericParameters();

			genericParameters.ParametersCollection.Add("route_id", str_route_ids);

			var result = await Delete<DeleteRouteResponse>(genericParameters,
												R4MEInfrastructureSettings.RouteHost,
												null,
												false).ConfigureAwait(false);

			return result;
		}

		/// <summary>
		/// Shares a route by an email
		/// </summary>
		/// <param name="roParames">The RouteParametersQuery parameters object contains parameters:
		/// <para>RouteId: a route ID to be shared</para>
		/// <para>ResponseFormat: the response format</para>
		/// </param>
		/// <param name="Email">Recipient email</param>
		/// <param name="errorString">Returned error string in case of the processs failing</param>
		/// <returns>True if a route was shared</returns>
		public async Task<Tuple<StatusResponse, ErrorResponse>> RouteSharing(RouteParametersQuery roParames, string Email)
		{
			var keyValues = new List<KeyValuePair<string, string>>()
			{
				new KeyValuePair<string, string>("recipient_email", Email)
			};

			using (HttpContent httpContent = new FormUrlEncodedContent(keyValues))
			{
				var result = await Post<StatusResponse>(roParames,
												R4MEInfrastructureSettings.RouteSharing,
												httpContent,
												false).ConfigureAwait(false);

				return result;
			};
		}

		#endregion

		#region Destinations

		/// <summary>
		/// Returns an Address type object as the response
		/// </summary>
		/// <param name="addressParameters">An AddressParameters type object containing the route ID as the input parameter.</param>
		/// <param name="errorString">Error message text</param>
		/// <returns>The Address type object</returns>
		public async Task<Tuple<Address, ErrorResponse>> GetAddress(AddressParameters addressParameters)
		{
			var result = await Get<Address>(addressParameters,
					R4MEInfrastructureSettings.GetAddress,
					false
				).ConfigureAwait(false);

			return result;

			//return GetJsonObjectFromAPI<Address>(addressParameters,
			//													 R4MEInfrastructureSettings.GetAddress,
			//													 HttpMethodType.Get,
			//													 out errorString);
		}

		#endregion

		#region Users

		public async Task<Tuple<MemberCapabilities, ErrorResponse>> GetMemberCapabilities(string apiKey)
        {
			var parameters = new GenericParameters();

			parameters.ParametersCollection.Add("ApiKey", apiKey);

			var result = await Get<MemberCapabilities>(
								new GenericParameters(),
								R4MEInfrastructureSettings.MemberCapabilities,
								false).ConfigureAwait(false);

			return result;
		}

		public bool MemberHasCommercialCapability(MemberCapabilities memberCapabilities)
        {
            try
            {
				if (memberCapabilities == null) return false;

				var commercialSubscription = memberCapabilities
					.GetType()
					.GetProperties()
					.Where(x => x.Name == "Commercial")
					.FirstOrDefault();

				if (commercialSubscription == null) return false;

				if (commercialSubscription.GetValue(memberCapabilities).GetType() != typeof(bool)) return false;

				bool isCommercial = (bool)commercialSubscription.GetValue(memberCapabilities);

				if (!isCommercial) return false;

				return true;
			}
            catch (Exception)
            {

				return false;
            }

		}

		/// <summary>
		/// Returns the object containing array of the user objects
		/// </summary>
		/// <param name="parameters">Empty GenericParameters object</param>
		/// <param name="errorString">Error message text</param>
		/// <returns>The object of the type GetUsersResponse</returns>
		public async Task<Tuple<GetUsersResponse, ErrorResponse>> GetUsers(GenericParameters parameters)
		{
			var result = await Get<GetUsersResponse>(
								parameters,
								R4MEInfrastructureSettings.GetUsersHost,
								false).ConfigureAwait(false);

			return result;
		}

		#endregion

		#region Vehicles

		public async Task<Tuple<DataTypes.V5.Vehicle[], ErrorResponse>> GetVehicles(VehicleParameters vehParams)
		{
			var apiKey = _r4mApi4Service.Client.DefaultRequestHeaders.GetValues("X-Api-Key").FirstOrDefault();

			var result = await Get<DataTypes.V5.Vehicle[]>(
					vehParams,
					R4MEInfrastructureSettings.Vehicle_V4,
					false,
					apiKey
				).ConfigureAwait(false);

			return result;
		}

		public async Task<Tuple<DataTypes.V5.Vehicle, ErrorResponse>> GetVehicle(VehicleParameters vehParams)
		{
			var apiKey = _r4mApi4Service.Client.DefaultRequestHeaders.GetValues("X-Api-Key").FirstOrDefault();

			var result = await Get<DataTypes.V5.Vehicle>(
					vehParams,
					R4MEInfrastructureSettings.Vehicle_V4+$"/{vehParams.VehicleId}",
					false,
					apiKey
				).ConfigureAwait(false);

			return result;
		}

		/// <summary>
		/// Creates a vehicle
		/// </summary>
		/// <param name="vehicle">The VehicleV4Parameters type object as the request payload </param>
		/// <param name="errorString"> out: Error as string </param>
		/// <returns>A created vehicle </returns>
		public async Task<Tuple<VehicleV4CreateResponse, ErrorResponse>> CreateVehicle(VehicleV4Parameters vehicle)
		{
			var result = await Post<VehicleV4CreateResponse>(vehicle,
												R4MEInfrastructureSettings.Vehicle_V4_API,
												null,
												false).ConfigureAwait(false);

			return result;
			//return GetJsonObjectFromAPI<VehicleV4CreateResponse>(vehicle,
			//				R4MEInfrastructureSettings.Vehicle_V4_API,
			//				HttpMethodType.Post,
			//				out errorString);
		}

		/// <summary>
		/// Updates a vehicle
		/// </summary>
		/// <param name="vehParams">The VehicleV4Parameters type object as the request payload</param>
		/// <param name="vehicleId">Vehicle ID</param>
		/// <param name="errorString"> out: Error as string </param>
		/// <returns>The updated vehicle</returns>
		public async Task<Tuple<VehicleV4Response, ErrorResponse>> UpdateVehicle(VehicleV4Parameters vehParams)
		{
			var apiKey = _r4mApi4Service.Client.DefaultRequestHeaders.GetValues("X-Api-Key").FirstOrDefault();

			var result = await Put<VehicleV4Response>(
						vehParams,
						R4MEInfrastructureSettings.Vehicle_V4 + @"/" + vehParams.VehicleId,
						null,
						false,
						apiKey).ConfigureAwait(false);

			return result;
		}

		/// <summary>
		/// Removes a vehicle from a user's account
		/// </summary>
		/// <param name="vehParams"> The VehicleParameters type object as the query parameters containing parameter VehicleId </param>
		/// <param name="errorString"> out: Error as string </param>
		/// <returns>The removed vehicle</returns>
		public async Task<Tuple<VehicleV4Response, ErrorResponse>> DeleteVehicle(VehicleV4Parameters vehParams)
		{
			var apiKey = _r4mApi4Service.Client.DefaultRequestHeaders.GetValues("X-Api-Key").FirstOrDefault();

			var result = await Delete<VehicleV4Response>(vehParams,
												R4MEInfrastructureSettings.Vehicle_V4 + "/" + vehParams.VehicleId,
												null,
												false,
												apiKey).ConfigureAwait(false);

			return result;

			//return GetJsonObjectFromAPI<VehicleV4Response>(vehParams,
			//				R4MEInfrastructureSettings.Vehicle_V4 + "/" + vehParams.VehicleId,
			//				HttpMethodType.Delete,
			//				out errorString);
		}

		#endregion

		#region Address Notes

		/// <summary>
		/// Returns an array of the custom note types
		/// </summary>
		/// <param name="errorString">Error message text</param>
		/// <returns>An array of the custom note types</returns>
		public async Task<Tuple<CustomNoteType[], ErrorResponse>> GetAllCustomNoteTypes()
		{
			var response = await Get<CustomNoteType[]>(
				new GenericParameters(),
				R4MEInfrastructureSettings.CustomNoteType,
				false).ConfigureAwait(false);

			return response;
		}

		/// <summary>
		/// Retrieve a custom note type by custom note type.
		/// </summary>
		/// <param name="CustomNoteType">Custom note type</param>
		/// <returns>CustomNoteType type object</returns>
		public async Task<Tuple<CustomNoteType, ErrorResponse>> GetCustomNoteType(string CustomNoteType)
        {
			var customNoteTypes = await GetAllCustomNoteTypes();

			var customNoteType = (customNoteTypes?.Item1?.Length ?? 0) > 0
				? customNoteTypes?.Item1.ToList().Where(x => x.NoteCustomType == CustomNoteType).FirstOrDefault()
				: null;

			return new Tuple<CustomNoteType, ErrorResponse>(
					customNoteType,
					customNoteType!=null 
						? null 
						: _r4mApi4Service.ErrorStringsToErrorResponse(
							new string[] { $"Cannot find the custom note type: {CustomNoteType}" }
						  )
				);
		}

		/// <summary>
		/// The method offers ability to send a complex note at once,
		/// with text content, uploading file, custom notes.
		/// </summary>
		/// <param name="noteParameters">The note parameters of the type NoteParameters
		/// Note: contains form data elemets too</param>
		/// <param name="errorString">Error string</param>
		/// <returns>Created address note</returns>
		public async Task<Tuple<AddAddressNoteResponse, ErrorResponse>> AddAddressNote(NoteParameters noteParameters)
		{
			FileStream attachmentFileStream = null;
			StreamContent attachmentStreamContent = null;

			var multipartFormDataContent = new MultipartFormDataContent();

			if (noteParameters.StrFileName != null)
			{
				attachmentFileStream = System.IO.File.OpenRead(noteParameters.StrFileName);
				attachmentStreamContent = new StreamContent(attachmentFileStream);
				multipartFormDataContent.Add(attachmentStreamContent, "strFilename", Path.GetFileName(noteParameters.StrFileName));
			}

			multipartFormDataContent.Add(new StringContent(noteParameters.ActivityType), "strUpdateType");
			multipartFormDataContent.Add(new StringContent(noteParameters.StrNoteContents), "strNoteContents");

			if (noteParameters.CustomNoteTypes != null && noteParameters.CustomNoteTypes.Count > 0)
			{
				foreach (KeyValuePair<string, string> customNote in noteParameters.CustomNoteTypes)
				{
					multipartFormDataContent.Add(new StringContent(customNote.Value), customNote.Key);
				}
			}

			HttpContent httpContent = multipartFormDataContent;

			var response = await Post<AddAddressNoteResponse>(noteParameters,
												R4MEInfrastructureSettings.AddRouteNotesHost,
												httpContent,
												false).ConfigureAwait(false);

			if (attachmentStreamContent != null) attachmentStreamContent.Dispose();
			if (attachmentFileStream != null) attachmentFileStream.Dispose();

			return response;
		}

		/// <summary>
		/// Adds a file as an address note to the route destination.
		/// </summary>
		/// <param name="noteParameters">The NoteParameters type object</param>
		/// <param name="noteContents">The note content text</param>
		/// <param name="attachmentFilePath">An attached file path</param>
		/// <param name="errorString">Error message text</param>
		/// <returns>An AddressNote type object</returns>
		public async Task<Tuple<AddAddressNoteResponse, ErrorResponse>> AddAddressNote(NoteParameters noteParameters, 
																		string noteContents, 
																		string attachmentFilePath)
		{
			var strUpdateType = "unclassified";

			if (noteParameters.ActivityType != null && noteParameters.ActivityType.Length > 0)
			{
				strUpdateType = noteParameters.ActivityType;
			}

			HttpContent httpContent;
			FileStream attachmentFileStream = null;
			StreamContent attachmentStreamContent = null;

			if (attachmentFilePath != null)
			{
				attachmentFileStream = System.IO.File.OpenRead(attachmentFilePath);
				attachmentStreamContent = new StreamContent(attachmentFileStream);

				httpContent = new MultipartFormDataContent
				{
					{ attachmentStreamContent, "strFilename", Path.GetFileName(attachmentFilePath) },
					{ new StringContent(strUpdateType), "strUpdateType" },
					{ new StringContent(noteContents), "strNoteContents" }
				};
			}
			else
			{
				var keyValues = new List<KeyValuePair<string, string>>()
				{
					new KeyValuePair<string, string>("strUpdateType", strUpdateType),
					new KeyValuePair<string, string>("strNoteContents", noteContents)
				};

				httpContent = new FormUrlEncodedContent(keyValues);
			}

			var response = await Post<AddAddressNoteResponse>(noteParameters,
												R4MEInfrastructureSettings.AddRouteNotesHost,
												httpContent,
												false).ConfigureAwait(false);

			if (attachmentStreamContent != null) attachmentStreamContent.Dispose();
			if (attachmentFileStream != null) attachmentFileStream.Dispose();

			if ((response?.Item1 ?? null) != null && 
				response.Item1.Note == null && 
				response.Item1.Status == false &&
				response.Item2 == null)
            {
				response = new Tuple<AddAddressNoteResponse, ErrorResponse>(
					response.Item1, 
					_r4mApi4Service.ErrorStringsToErrorResponse(new string[] { "Note not added" })
					);
			}

			return response;
		}

		public async Task<Tuple<AddAddressNoteResponse, ErrorResponse>> AddAddressNote(
																			NoteParameters noteParameters, 
																			string noteContents)
		{
			var addressNoteResult = await this.AddAddressNote(noteParameters, noteContents, null);

			return addressNoteResult;
		}

		/// <summary>
		/// Returns an array of the address notes
		/// </summary>
		/// <param name="noteParameters">An object of the type NoteParameters containing the parameters:
		/// <para>RouteId: a route ID</para>
		/// <para>AddressId: a route destination ID</para>
		/// </param>
		/// <param name="errorString">Error message text</param>
		/// <returns>An array of the AddressNote type objects </returns>
		public async Task<Tuple<AddressNote[], ErrorResponse>> GetAddressNotes(NoteParameters noteParameters)
		{
			var addressParameters = new AddressParameters()
			{
				RouteId = noteParameters.RouteId,
				RouteDestinationId = noteParameters.AddressId,
				Notes = true
			};

			var addressResult = await this.GetAddress(addressParameters);

			return new Tuple<AddressNote[], ErrorResponse>(
				addressResult?.Item1?.Notes ?? null,
				addressResult.Item2);
		}

		/// <summary>
		/// Adds custom note type to a route destination.
		/// </summary>
		/// <param name="customType">A custom note type</param>
		/// <param name="values">Array of the string type notes</param>
		/// <param name="errorString">Error message text</param>
		/// <returns>If succefful, returns non-negative affected number, otherwise: -1</returns>
		public async Task<Tuple<AddCustomNoteTypeResponse, ErrorResponse>> AddCustomNoteType(string customType, string[] values)
		{
			var request = new AddCustomNoteTypeRequest()
			{
				Type = customType,
				Values = values
			};

			var result = await Post<AddCustomNoteTypeResponse>(request,
												R4MEInfrastructureSettings.CustomNoteType,
												null,
												false).ConfigureAwait(false);

			return result;
		}

		/// <summary>
		/// Removes a custom note type from a user's account by custom note ID.
		/// </summary>
		/// <param name="customNoteId">The custom note type ID</param>
		/// <param name="errorString">Error message text</param>
		/// <returns>if succefful, returns non-negative affected number, otherwise: -1</returns>
		public async Task<Tuple<AddCustomNoteTypeResponse, ErrorResponse>> RemoveCustomNoteType(int customNoteId)
		{
			var request = new RemoveCustomNoteTypeRequest() { Id = customNoteId };

			var result = await Delete<AddCustomNoteTypeResponse>(request,
												R4MEInfrastructureSettings.CustomNoteType,
												null,
												false).ConfigureAwait(false);

			return result;
		}

		/// <summary>
		/// Adds a custom note to aroute
		/// </summary>
		/// <param name="noteParameters">A NoteParameters type object</param>
		/// <param name="customNotes">The Dictionary<string, string> type object</param>
		/// <param name="errorString">Error message text</param>
		/// <returns>The AddAddressNoteResponse type object</returns>
		public async Task<Tuple<AddAddressNoteResponse, ErrorResponse>> AddCustomNoteToRoute(NoteParameters noteParameters, 
																			 Dictionary<string, string> customNotes)
		{
			var keyValues = new List<KeyValuePair<string, string>>();

			customNotes.ForEach(kv1 => { keyValues.Add(new KeyValuePair<string, string>(kv1.Key, kv1.Value)); });

			using (HttpContent httpContent = new FormUrlEncodedContent(keyValues))
			{
				var result = await Post<AddAddressNoteResponse>(noteParameters,
												R4MEInfrastructureSettings.AddRouteNotesHost,
												httpContent,
												false).ConfigureAwait(false);

				return result;
			};
		}

		#endregion

		#region Address Book Contacts

		/// <summary>
		/// Returns address book contacts
		/// </summary>
		/// <param name="addressBookParameters">>An AddressParameters type object as the input parameters containg the parameters: Offset, Limit</param>
		/// <param name="total">out: Number of the returned contacts</param>
		/// <param name="errorString">out: Error as string</param>
		/// <returns>The array of the address book contacts</returns>
		public async Task<Tuple<GetAddressBookContactsResponse, ErrorResponse>> GetAddressBookContacts(AddressBookParameters addressBookParameters)
		{
			var response = await Get<GetAddressBookContactsResponse>(
				addressBookParameters,
				R4MEInfrastructureSettings.AddressBook,
				false).ConfigureAwait(false);

			return response;
		}

		/// <summary>
		/// Returns an address book contact
		/// </summary>
		/// <param name="addressBookParameters">An AddressParameters type object as the input parameter 
		/// containing the parameter AddressId (comma-delimited list of the address IDs)
		/// </param>
		/// <param name="total">out: Number of the returned contacts</param>
		/// <param name="errorString">out: Error as string</param>
		/// <returns>The array of the address book contacts</returns>
		public async Task<Tuple<GetAddressBookContactsResponse, ErrorResponse>> GetAddressBookLocation(AddressBookParameters addressBookParameters)
		{
			//parseWithNewtonJson = true;

			if (addressBookParameters.AddressId != null && !addressBookParameters.AddressId.Contains(","))
			{
				addressBookParameters.AddressId += "," + addressBookParameters.AddressId;
			}

			var response = await Get<GetAddressBookContactsResponse>(
				addressBookParameters,
				R4MEInfrastructureSettings.AddressBook,
				false).ConfigureAwait(false);

			return response;
		}

		/// <summary>
		/// Searches for the address book locations 
		/// </summary>
		/// <param name="addressBookParameters">An AddressParameters type object as the input parameter</param>
		/// <param name="total">out: Number of the returned contacts</param>
		/// <param name="errorString">out: Error as string</param>
		/// <returns>List of the selected fields values</returns>
		public async Task<Tuple<SearchAddressBookLocationResponse, ErrorResponse>> SearchAddressBookLocation(AddressBookParameters addressBookParameters)
		{
			if (addressBookParameters.Fields == null)
			{
				return new Tuple<SearchAddressBookLocationResponse, ErrorResponse>(
						null,
						_r4mApi4Service.ErrorStringsToErrorResponse(new string[] { "Fields property should be specified." })
					);
			}

			var request = new SearchAddressBookLocationRequest();

			var contactsFromObjects = new List<AddressBookContact>();

			if (addressBookParameters.AddressId != null) request.AddressId = addressBookParameters.AddressId;
			if (addressBookParameters.Query != null) request.Query = addressBookParameters.Query;
			request.Fields = addressBookParameters.Fields;
			if (addressBookParameters.Offset != null) request.Offset = addressBookParameters.Offset >= 0 ? (int)addressBookParameters.Offset : 0;
			if (addressBookParameters.Limit != null) request.Limit = addressBookParameters.Limit >= 0 ? (int)addressBookParameters.Limit : 0;

			//parseWithNewtonJson = true;

			var response = await Get<SearchAddressBookLocationResponse>(
				request,
				R4MEInfrastructureSettings.AddressBook,
				false).ConfigureAwait(false);

			string errorString;

			if ((response?.Item1 ?? null) != null && (response?.Item1?.Total ?? 0) > 0)
			{
				var orderedPropertyNames = R4MeUtils.OrderPropertiesByPosition<AddressBookContact>(
												response.Item1.Fields.ToList(), 
												out errorString);

				foreach (object[] contactObjects in response.Item1.Results)
				{
					var contactFromObject = new AddressBookContact();
					foreach (var propertyName in orderedPropertyNames)
					{
						var value = contactObjects[orderedPropertyNames.IndexOf(propertyName)];

						var valueType = value != null ? value.GetType().Name : "";

						PropertyInfo propInfo = typeof(AddressBookContact).GetProperty(propertyName);

						switch (propertyName)
						{
							case "address_custom_data":
								var customData = R4MeUtils.ToObject<Dictionary<string, string>>(value, out string errorString1);
								if (errorString1 == "")
									propInfo.SetValue(contactFromObject, customData);
								else
									propInfo.SetValue(contactFromObject,
										new Dictionary<string, string>() { { "<WRONG DATA>", "<WRONG DATA>" } });
								break;
							case "schedule":
								var schedules = R4MeUtils.ToObject<Schedule[]>(value, out string errorString2);
								if (errorString2 == "")
									propInfo.SetValue(contactFromObject, schedules);
								else
									propInfo.SetValue(contactFromObject, null);
								break;
							case "schedule_blacklist":
								var scheduleBlackList = R4MeUtils.ToObject<string[]>(value, out string errorString3);
								if (errorString3 == "")
									propInfo.SetValue(contactFromObject, scheduleBlackList);
								else
									propInfo.SetValue(contactFromObject, new string[] { "<WRONG DATA>" });
								break;
							default:
								var convertedValue = valueType != ""
									? R4MeUtils.ConvertObjectToPropertyType(value, propInfo)
									: value;
								propInfo.SetValue(contactFromObject, convertedValue);
								break;
						}
					}

					contactsFromObjects.Add(contactFromObject);
				}
			}

			return response;
		}

		/// <summary>
		/// Adds an address book contact to a user's account.
		/// </summary>
		/// <param name="contact">The AddressBookContact type object as input parameters</param>
		/// <param name="errorString">out: Error as string</param>
		/// <returns>The AddressBookContact type object</returns>
		public async Task<Tuple<AddressBookContact, ErrorResponse>> AddAddressBookContact(AddressBookContact contact)
		{
			//parseWithNewtonJson = true;

			contact.PrepareForSerialization();

			var result = await Post<AddressBookContact>(contact,
												R4MEInfrastructureSettings.AddressBook,
												null,
												false).ConfigureAwait(false);

			return result;
		}

		/// <summary>
		/// Updates an address book contact.
		/// </summary>
		/// <param name="contact">The AddressBookContact type object as input parameters</param>
		/// <param name="errorString">out: Error as string</param>
		/// <returns>The AddressBookContact type object</returns>
		public async Task<Tuple<AddressBookContact, ErrorResponse>> UpdateAddressBookContact(AddressBookContact contact)
		{
			//contact.PrepareForSerialization();

			var result = await Put<AddressBookContact>(contact,
						R4MEInfrastructureSettings.AddressBook,
						null,
						false).ConfigureAwait(false);

			return result;
		}

		/// <summary>
		/// Updates an address book contact.
		/// Used in case fo sending specified, limited number of the Contact parameters.
		/// </summary>
		/// <param name="contact">Address Book Contact</param>
		/// <param name="updatableProperties">List of the properties which should be updated - 
		/// despite are they null or not</param>
		/// <param name="errorString">Error strings</param>
		/// <returns>Address book contact</returns>
		public async Task<Tuple<AddressBookContact, ErrorResponse>> UpdateAddressBookContact(
																		AddressBookContact contact, 
																		List<string> updatableProperties)
		{
			//parseWithNewtonJson = true;

			var myDynamicClass = new Route4MeDynamicClass();
			myDynamicClass.CopyPropertiesFromClass(contact, updatableProperties, out string errorString0);

			var jsonString = fastJSON.JSON.ToJSON(myDynamicClass.DynamicProperties);

			var genParams = new GenericParameters();

			var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

			var result = await Put<AddressBookContact>(genParams,
						R4MEInfrastructureSettings.AddressBook,
						content,
						false).ConfigureAwait(false);

			return result;
		}

		/// <summary>
		/// Updates a contact by comparing initial and modified contact objects and
		/// by updating only modified proeprties of a contact.
		/// </summary>
		/// <param name="contact">A address book contact object as input (modified or created virtual contact)</param>
		/// <param name="initialContact">An initial address book contact</param>
		/// <param name="errorString">Error string</param>
		/// <returns>Updated address book contact</returns>
		public async Task<Tuple<AddressBookContact, ErrorResponse>> UpdateAddressBookContact(
																		AddressBookContact contact, 
																		AddressBookContact initialContact)
		{
			//parseWithNewtonJson = true;

			if (initialContact == null || initialContact == contact)
			{
				return new Tuple<AddressBookContact, ErrorResponse>(
						null,
						_r4mApi4Service.ErrorStringsToErrorResponse(
							new string[] { "The initial and modified contacts should not be null" }
							)
					);
			}

			var updatableContactProperties = R4MeUtils
			.GetPropertiesWithDifferentValues(contact, initialContact, out string errorString);

			updatableContactProperties.Add("AddressId");

			if (updatableContactProperties != null && updatableContactProperties.Count > 0)
			{
				var dynamicContactProperties = new Route4MeDynamicClass();

				dynamicContactProperties.CopyPropertiesFromClass(contact, updatableContactProperties, out string errorString0);

				var contactParamsJsonString = R4MeUtils.SerializeObjectToJson(dynamicContactProperties.DynamicProperties, true);

				var genParams = new GenericParameters();

				var content = new StringContent(contactParamsJsonString, System.Text.Encoding.UTF8, "application/json");

				var result = await Put<AddressBookContact>(genParams,
						R4MEInfrastructureSettings.AddressBook,
						content,
						false).ConfigureAwait(false);

				return result;
			}

			return new Tuple<AddressBookContact, ErrorResponse>(
						null,
						_r4mApi4Service.ErrorStringsToErrorResponse(
							new string[] { errorString!=null ? errorString : "The updatable properties not specified" }
							)
					);
		}

		/// <summary>
		/// Remove the address book contacts.
		/// </summary>
		/// <param name="addressIds">The array of the address IDs</param>
		/// <param name="errorString">out: Error as string</param>
		/// <returns>If true the contacts were removed successfully</returns>
		public async Task<Tuple<StatusResponse, ErrorResponse>> RemoveAddressBookContacts(string[] addressIds)
		{
			var request = new RemoveAddressBookContactsRequest()
			{
				AddressIds = addressIds
			};

			var result = await Delete<StatusResponse>(request,
												R4MEInfrastructureSettings.AddressBook,
												null,
												false).ConfigureAwait(false);

			return result;
		}


		#endregion

		#region Address Book Group

		/// <summary>
		/// Get the address book groups
		/// </summary>
		/// <param name="addressBookGroupParameters">Query parameters</param>
		/// <param name="errorString">Error string</param>
		/// <returns>An array of the address book contacts</returns>
		public async Task<Tuple<AddressBookGroup[], ErrorResponse>> GetAddressBookGroups(AddressBookGroupParameters addressBookGroupParameters, out string errorString)
		{
			var response = GetJsonObjectFromAPI<AddressBookGroup[]>(addressBookGroupParameters,
												R4MEInfrastructureSettings.AddressBookGroup,
												HttpMethodType.Get,
												out errorString);

			return response;
		}

		#endregion

		[HttpGet]
		public async Task<Tuple<T, ErrorResponse>> Get<T>(GenericParameters optimizationParameters,
									   string url,
									   bool isString,
									   string urlApiKey = null) where T : class
		{
			string parametersURI = optimizationParameters.Serialize(urlApiKey != null ? urlApiKey : String.Empty);
			parametersURI = parametersURI != null ? parametersURI.Replace("?&", "?") : "";

			url = $"{url}{parametersURI}";

			return await _r4mApi4Service.Get<T>(url, false).ConfigureAwait(false);
		}

        [HttpPost]
        public async Task<Tuple<T, ErrorResponse>> Post<T>(GenericParameters optimizationParameters,
									   string url,
									   HttpContent httpContent,
									   bool isString) where T : class
		{
			string parametersURI = optimizationParameters.Serialize(String.Empty);
			parametersURI = parametersURI != null && parametersURI!="?" ? parametersURI.Replace("?&", "?") : "";

			url = $"{url}{parametersURI}";

			HttpContent content = null;

			if (httpContent != null)
			{
				content = httpContent;
			}
			else
			{

				string jsonString = (this.mandatoryFields?.Length ?? 0) > 0
					? R4MeUtils.SerializeObjectToJson(optimizationParameters, this.mandatoryFields)
					: R4MeUtils.SerializeObjectToJson(optimizationParameters);
				content = new StringContent(jsonString);
				content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			}

			return await _r4mApi4Service.Post<T>(
				optimizationParameters,
				url,
				content,
				isString).ConfigureAwait(false);

		}

		[HttpPut]
		public async Task<Tuple<T, ErrorResponse>> Put<T>(GenericParameters optimizationParameters,
									  string url,
									  HttpContent httpContent,
									  bool isString, 
									  string urlApiKey = null) where T : class
        {
			string parametersURI = optimizationParameters.Serialize(urlApiKey!=null ? urlApiKey : String.Empty);
			parametersURI = parametersURI != null && parametersURI != "?" ? parametersURI.Replace("?&", "?") : "";

			url = $"{url}{parametersURI}";

			HttpContent content = null;

			if (httpContent != null)
			{
				content = httpContent;
			}
			else
			{

				string jsonString = (this.mandatoryFields?.Length ?? 0) > 0
					? R4MeUtils.SerializeObjectToJson(optimizationParameters, this.mandatoryFields)
					: R4MeUtils.SerializeObjectToJson(optimizationParameters);
				content = new StringContent(jsonString);
				content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			}

			return await _r4mApi4Service.Put<T>(
				optimizationParameters,
				url,
				content,
				isString).ConfigureAwait(false);
		}

		[HttpPut]
		public async Task<Tuple<T, ErrorResponse>> Delete<T>(GenericParameters optimizationParameters,
									  string url,
									  HttpContent httpContent,
									  bool isString,
									  string urlApiKey = null) where T : class
        {
			string parametersURI = optimizationParameters.Serialize(urlApiKey != null ? urlApiKey : String.Empty);
			parametersURI = parametersURI != null && parametersURI != "?" ? parametersURI.Replace("?&", "?") : "";

			url = $"{url}{parametersURI}";

			HttpContent content = null;

			if (httpContent != null)
			{
				content = httpContent;
			}
			else
			{

				string jsonString = (this.mandatoryFields?.Length ?? 0) > 0
					? R4MeUtils.SerializeObjectToJson(optimizationParameters, this.mandatoryFields)
					: R4MeUtils.SerializeObjectToJson(optimizationParameters);
				content = new StringContent(jsonString);
				content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			}

			return await _r4mApi4Service.Delete<T>(
				optimizationParameters,
				url,
				content,
				isString).ConfigureAwait(false);
		}

	}
}
