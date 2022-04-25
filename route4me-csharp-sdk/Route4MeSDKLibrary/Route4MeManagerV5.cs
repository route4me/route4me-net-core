using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using fastJSON;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.DataTypes.V5.TelematicsPlatform;
using Route4MeSDK.QueryTypes;
using Route4MeSDK.QueryTypes.V5;
using Route4MeSDKLibrary;
using Route4MeSDKLibrary.DataTypes.V5.Orders;
using Route4MeSDKLibrary.QueryTypes.V5.Orders;
using AddressBookParameters = Route4MeSDK.QueryTypes.V5.AddressBookParameters;
using OptimizationParameters = Route4MeSDK.QueryTypes.V5.OptimizationParameters;
using RouteParametersQuery = Route4MeSDK.QueryTypes.V5.RouteParametersQuery;
using VehicleParameters = Route4MeSDK.QueryTypes.V5.VehicleParameters;

namespace Route4MeSDK
{
    /// <summary>
    ///     This class encapsulates the Route4Me REST API 5
    ///     1. Create an instance of Route4MeManager with the api_key
    ///     1. Shortcut methods: Use shortcuts methods (for example Route4MeManager.GetOptimization()) to access the most
    ///     popular functionality.
    ///     See examples Route4MeExamples.GetOptimization(), Route4MeExamples.SingleDriverRoundTrip()
    ///     2. Generic methods: Use generic methods (for example Route4MeManager.GetJsonObjectFromAPI() or
    ///     Route4MeManager.GetStringResponseFromAPI())
    ///     to access any availaible functionality.
    ///     See examples Route4MeExamples.GenericExample(), Route4MeExamples.SingleDriverRoundTripGeneric()
    /// </summary>
    public sealed class Route4MeManagerV5
    {
        #region Constructors

        public Route4MeManagerV5(string apiKey)
        {
            _mApiKey = apiKey;
        }

        #endregion

        #region Fields

        private readonly string _mApiKey;

        #endregion

        #region Address Book Contacts

        /// <summary>
        ///     Remove the address book contacts.
        /// </summary>
        /// <param name="contactIDs">The array of the contract IDs</param>
        /// <param name="resultResponse">out: Result response</param>
        /// <returns>If true the contacts were removed successfully</returns>
        public bool RemoveAddressBookContacts(long[] contactIDs, out ResultResponse resultResponse)
        {
            var request = new AddressBookContactsRequest
            {
                AddressIds = contactIDs
            };

            GetJsonObjectFromAPI<StatusResponse>(request,
                R4MEInfrastructureSettingsV5.ContactsDeleteMultiple,
                HttpMethodType.Delete,
                out resultResponse);

            return resultResponse == null;
        }

        /// <summary>
        ///     Remove the address book contacts.
        /// </summary>
        /// <param name="contactIDs">The array of the contract IDs</param>
        /// <returns>If true the contacts were removed successfully</returns>
        public async Task<Tuple<bool, ResultResponse>> RemoveAddressBookContactsAsync(long[] contactIDs)
        {
            var request = new AddressBookContactsRequest
            {
                AddressIds = contactIDs
            };

            var response = await GetJsonObjectFromAPIAsync<StatusResponse>(request,
                R4MEInfrastructureSettingsV5.ContactsDeleteMultiple,
                HttpMethodType.Delete).ConfigureAwait(false);

            return new Tuple<bool, ResultResponse>(response.Item1 == null, response.Item2);
        }

        /// <summary>
        ///     Returns address book contacts
        /// </summary>
        /// <param name="addressBookParameters">
        ///     An AddressParameters type object as the input parameters containg the parameters:
        ///     Offset, Limit
        /// </param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An AddressBookContactsResponse type object</returns>
        public AddressBookContactsResponse GetAddressBookContacts(AddressBookParameters addressBookParameters,
            out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<AddressBookContactsResponse>(addressBookParameters,
                R4MEInfrastructureSettingsV5.ContactsGetAll,
                HttpMethodType.Get,
                out resultResponse);

            return response;
        }

        /// <summary>
        ///     Returns address book contacts
        /// </summary>
        /// <param name="addressBookParameters">
        ///     An AddressParameters type object as the input parameters containg the parameters:
        ///     Offset, Limit
        /// </param>
        /// <returns>An AddressBookContactsResponse type object</returns>
        public Task<Tuple<AddressBookContactsResponse, ResultResponse>> GetAddressBookContactsAsync(AddressBookParameters addressBookParameters)
        {
            return GetJsonObjectFromAPIAsync<AddressBookContactsResponse>(addressBookParameters,
                R4MEInfrastructureSettingsV5.ContactsGetAll,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Get an address book contact by ID
        /// </summary>
        /// <param name="contactId">contact ID</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An AddressBookContact type object</returns>
        public AddressBookContact GetAddressBookContactById(long contactId, out ResultResponse resultResponse)
        {
            var gparams = new GenericParameters();
            gparams.ParametersCollection.Add("address_id", contactId.ToString());

            var response = GetJsonObjectFromAPI<AddressBookContact>(gparams,
                R4MEInfrastructureSettingsV5.ContactsFind,
                HttpMethodType.Get,
                out resultResponse);

            return response;
        }

        /// <summary>
        ///     Get an address book contact by ID
        /// </summary>
        /// <param name="contactId">contact ID</param>
        /// <returns>An AddressBookContact type object</returns>
        public Task<Tuple<AddressBookContact, ResultResponse>> GetAddressBookContactByIdAsync(long contactId)
        {
            var gparams = new GenericParameters();
            gparams.ParametersCollection.Add("address_id", contactId.ToString());

            return GetJsonObjectFromAPIAsync<AddressBookContact>(gparams,
                R4MEInfrastructureSettingsV5.ContactsFind,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     The request parameter for the address book contacts removing process.
        /// </summary>
        [DataContract]
        public sealed class AddressBookContactsRequest : GenericParameters
        {
            /// The array of the address IDs
            [DataMember(Name = "address_ids", EmitDefaultValue = false)]
            public long[] AddressIds { get; set; }
        }

        /// <summary>
        ///     Get address book contacts by sending an array of address IDs.
        /// </summary>
        /// <param name="contactIDs">An array of address IDs</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An AddressBookContactsResponse type object</returns>
        public AddressBookContactsResponse GetAddressBookContactsByIds(long[] contactIDs,
            out ResultResponse resultResponse)
        {
            var request = new AddressBookContactsRequest
            {
                AddressIds = contactIDs
            };

            var response = GetJsonObjectFromAPI<AddressBookContactsResponse>(request,
                R4MEInfrastructureSettingsV5.ContactsFind,
                HttpMethodType.Post,
                out resultResponse);

            return response;
        }

        /// <summary>
        ///     Get depots from address book contacts.
        /// </summary>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An array of depot-eligible contacts</returns>
        public AddressBookContact[] GetDepotsFromAddressBook(out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<AddressBookContact[]>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.AddressBookDepots,
                HttpMethodType.Get,
                false,
                true,
                out resultResponse);

            return response;
        }

        /// <summary>
        ///     Get address book contacts by sending an array of address IDs.
        /// </summary>
        /// <param name="contactIDs">An array of address IDs</param>
        /// <returns>An AddressBookContactsResponse type object</returns>
        public Task<Tuple<AddressBookContactsResponse, ResultResponse>> GetAddressBookContactsByIdsAsync(long[] contactIDs)
        {
            var request = new AddressBookContactsRequest
            {
                AddressIds = contactIDs
            };

            return GetJsonObjectFromAPIAsync<AddressBookContactsResponse>(request,
                R4MEInfrastructureSettingsV5.ContactsFind,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Add an address book contact to database.
        /// </summary>
        /// <param name="contactParams">The contact parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Created address book contact</returns>
        public AddressBookContact AddAddressBookContact(AddressBookContact contactParams,
            out ResultResponse resultResponse)
        {
            contactParams.PrepareForSerialization();

            return GetJsonObjectFromAPI<AddressBookContact>(contactParams,
                R4MEInfrastructureSettingsV5.ContactsAddNew,
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);
        }

        /// <summary>
        ///     Add an address book contact to database.
        /// </summary>
        /// <param name="contactParams">The contact parameters</param>
        /// <returns>Created address book contact</returns>
        public Task<Tuple<AddressBookContact, ResultResponse>> AddAddressBookContactAsync(AddressBookContact contactParams)
        {
            contactParams.PrepareForSerialization();

            return GetJsonObjectFromAPIAsync<AddressBookContact>(contactParams,
                R4MEInfrastructureSettingsV5.ContactsAddNew,
                HttpMethodType.Post,
                null,
                true,
                false);
        }

        /// <summary>
        ///     The request parameter for the multiple address book contacts creating process.
        /// </summary>
        [DataContract]
        public sealed class BatchCreatingAddressBookContactsRequest : GenericParameters
        {
            /// The array of the address IDs
            [DataMember(Name = "data", EmitDefaultValue = false)]
            public AddressBookContact[] Data { get; set; }
        }

        /// <summary>
        ///     Add multiple address book contacts to database.
        /// </summary>
        /// <param name="contactParams">The data with multiple contacts parameters</param>
        /// <param name="mandatoryNullableFields">mandatory nullable fields</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Status response (TO DO: expected result with created multiple contacts)</returns>
        public StatusResponse BatchCreateAdressBookContacts(BatchCreatingAddressBookContactsRequest contactParams,
            string[] mandatoryNullableFields,
            out ResultResponse resultResponse)
        {
            contactParams.PrepareForSerialization();

            return GetJsonObjectFromAPI<StatusResponse>(contactParams,
                R4MEInfrastructureSettingsV5.ContactsAddMultiple,
                HttpMethodType.Post,
                out resultResponse,
                mandatoryNullableFields);
        }

        /// <summary>
        ///     Add multiple address book contacts to database.
        /// </summary>
        /// <param name="contactParams">The data with multiple contacts parameters</param>
        /// <param name="mandatoryNullableFields">mandatory nullable fields</param>
        /// <returns>Status response (TO DO: expected result with created multiple contacts)</returns>
        public Task<Tuple<StatusResponse, ResultResponse>> BatchCreateAdressBookContactsAsync(BatchCreatingAddressBookContactsRequest contactParams,
            string[] mandatoryNullableFields)
        {
            contactParams.PrepareForSerialization();

            return GetJsonObjectFromAPIAsync<StatusResponse>(contactParams,
                R4MEInfrastructureSettingsV5.ContactsAddMultiple,
                HttpMethodType.Post,
                null,
                false,
                false,
                mandatoryNullableFields);
        }

        #endregion

        #region Account Profile

        /// <summary>
        ///     Get account profile
        /// </summary>
        /// <param name="failResponse">Error response</param>
        /// <returns>Account profile</returns>
        public AccountProfile GetAccountProfile(out ResultResponse failResponse)
        {
            var parameters = new GenericParameters();

            var result = GetJsonObjectFromAPI<AccountProfile>(parameters,
                R4MEInfrastructureSettingsV5.AccountProfile,
                HttpMethodType.Get,
                out failResponse);

            return result;
        }

        /// <summary>
        ///     Get account profile
        /// </summary>
        /// <returns>Account profile</returns>
        public Task<Tuple<AccountProfile, ResultResponse>> GetAccountProfileAsync()
        {
            var parameters = new GenericParameters();

            return GetJsonObjectFromAPIAsync<AccountProfile>(parameters,
                R4MEInfrastructureSettingsV5.AccountProfile,
                HttpMethodType.Get);
        }

        public string GetAccountPreferedUnit(out ResultResponse failResponse)
        {
            var accountProfile = GetAccountProfile(out failResponse);

            var ownerId = accountProfile.RootMemberId;

            var r4Me = new Route4MeManager(_mApiKey);

            var memPars = new MemberParametersV4 {MemberId = ownerId};

            var user = r4Me.GetUserById(memPars, out _);

            var prefUnit = user.PreferredUnits;

            return prefUnit;
        }

        public async Task<Tuple<string, ResultResponse>> GetAccountPreferedUnitAsync()
        {
            var accountProfile = await GetAccountProfileAsync().ConfigureAwait(false);

            var ownerId = accountProfile.Item1.RootMemberId;

            var r4Me = new Route4MeManager(_mApiKey);

            var memPars = new MemberParametersV4 { MemberId = ownerId };

            var user = await r4Me.GetUserByIdAsync(memPars).ConfigureAwait(false);

            var prefUnit = user.Item1.PreferredUnits;

            return new Tuple<string, ResultResponse>(prefUnit, accountProfile.Item2);
        }

        #endregion

        #region Barcodes

        /// <summary>
        ///     Returns address barcodes
        /// </summary>
        /// <param name="getAddressBarcodesParameters">Request parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An <see cref="GetAddressBarcodesResponse" /> type object</returns>
        public GetAddressBarcodesResponse GetAddressBarcodes(GetAddressBarcodesParameters getAddressBarcodesParameters,
            out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<GetAddressBarcodesResponse>(getAddressBarcodesParameters,
                R4MEInfrastructureSettingsV5.AddressBarcodes,
                HttpMethodType.Get,
                out resultResponse);

            return response;
        }

        /// <summary>
        ///     Returns address barcodes
        /// </summary>
        /// <param name="getAddressBarcodesParameters">Request parameters</param>
        /// <returns>An <see cref="GetAddressBarcodesResponse" /> type object and <see cref="ResultResponse"/> type object</returns>
        public Task<Tuple<GetAddressBarcodesResponse, ResultResponse>> GetAddressBarcodesAsync(GetAddressBarcodesParameters getAddressBarcodesParameters)
        {
            var response = GetJsonObjectFromAPIAsync<GetAddressBarcodesResponse>(getAddressBarcodesParameters,
                R4MEInfrastructureSettingsV5.AddressBarcodes,
                HttpMethodType.Get);

            return response;
        }

        /// <summary>
        ///     Saves address bar codes
        /// </summary>
        /// <param name="saveAddressBarcodesParameters">The contact parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Created address book contact</returns>
        public StatusResponse SaveAddressBarcodes(SaveAddressBarcodesParameters saveAddressBarcodesParameters,
            out ResultResponse resultResponse)
        {
            saveAddressBarcodesParameters.PrepareForSerialization();

            return GetJsonObjectFromAPI<StatusResponse>(saveAddressBarcodesParameters,
                R4MEInfrastructureSettingsV5.AddressBarcodes,
                HttpMethodType.Post,
                out resultResponse);
        }

        /// <summary>
        ///     Saves address bar codes
        /// </summary>
        /// <param name="saveAddressBarcodesParameters">The contact parameters</param>
        /// <returns>Created address book contact</returns>
        public Task<Tuple<StatusResponse, ResultResponse>> SaveAddressBarcodesAsync(SaveAddressBarcodesParameters saveAddressBarcodesParameters)
        {
            saveAddressBarcodesParameters.PrepareForSerialization();

            return GetJsonObjectFromAPIAsync<StatusResponse>(saveAddressBarcodesParameters,
                R4MEInfrastructureSettingsV5.AddressBarcodes,
                HttpMethodType.Post);
        }

        #endregion

        #region Team Management

        /// <summary>
        ///     The request parameters for retrieving team members.
        /// </summary>
        [DataContract]
        public sealed class MemberQueryParameters : GenericParameters
        {
            /// <value>Team user ID</value>
            [HttpQueryMember(Name = "user_id", EmitDefaultValue = false)]
            public string UserId { get; set; }
        }

        /// <summary>
        ///     The request class to bulk create the team members.
        /// </summary>
        [DataContract]
        private sealed class BulkMembersRequest : GenericParameters
        {
            // Array of the team member requests
            [DataMember(Name = "users")] public TeamRequest[] Users { get; set; }
        }

        /// <summary>
        ///     Retrieve all existing sub-users associated with the Member’s account.
        /// </summary>
        /// <param name="failResponse">Failing response</param>
        /// <returns>An array of the TeamResponseV5 type objects</returns>
        public TeamResponse[] GetTeamMembers(out ResultResponse failResponse)
        {
            var parameters = new GenericParameters();

            var result = GetJsonObjectFromAPI<TeamResponse[]>(parameters,
                R4MEInfrastructureSettingsV5.TeamUsers,
                HttpMethodType.Get,
                out failResponse);

            return result;
        }

        /// <summary>
        ///     Retrieve all existing sub-users associated with the Member’s account.
        /// </summary>
        /// <returns>An array of the TeamResponseV5 type objects</returns>
        public Task<Tuple<TeamResponse[], ResultResponse>> GetTeamMembersAsync()
        {
            var parameters = new GenericParameters();

            return GetJsonObjectFromAPIAsync<TeamResponse[]>(parameters,
                R4MEInfrastructureSettingsV5.TeamUsers,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Retrieve a team member by the parameter UserId
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Retrieved team member</returns>
        public TeamResponse GetTeamMemberById(MemberQueryParameters parameters,
            out ResultResponse resultResponse)
        {
            if (parameters?.UserId == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The UserId parameter is not specified"}}
                    }
                };

                return null;
            }

            var result = GetJsonObjectFromAPI<TeamResponse>(parameters,
                R4MEInfrastructureSettingsV5.TeamUsers + "/" + parameters.UserId,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Retrieve a team member by the parameter UserId
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <returns>Retrieved team member</returns>
        public Task<Tuple<TeamResponse, ResultResponse>> GetTeamMemberByIdAsync(MemberQueryParameters parameters)
        {
            if (parameters?.UserId == null)
            {
                return Task.FromResult(new Tuple<TeamResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The UserId parameter is not specified"}}
                    }
                }));
            }

            return GetJsonObjectFromAPIAsync<TeamResponse>(parameters,
                R4MEInfrastructureSettingsV5.TeamUsers + "/" + parameters.UserId,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Creates new team member (sub-user) in the user's account
        /// </summary>
        /// <param name="memberParams">An object of the type MemberParametersV4</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Created team member</returns>
        public TeamResponse CreateTeamMember(TeamRequest memberParams,
            out ResultResponse resultResponse)
        {
            if (!memberParams.ValidateMemberCreateRequest(out var error0))
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {error0}}
                    }
                };

                return null;
            }

            return GetJsonObjectFromAPI<TeamResponse>(
                memberParams,
                R4MEInfrastructureSettingsV5.TeamUsers,
                HttpMethodType.Post,
                out resultResponse);
        }

        /// <summary>
        ///     Creates new team member (sub-user) in the user's account
        /// </summary>
        /// <param name="memberParams">An object of the type MemberParametersV4</param>
        /// <returns>Created team member</returns>
        public Task<Tuple<TeamResponse, ResultResponse>> CreateTeamMemberAsync(TeamRequest memberParams)
        {
            if (!memberParams.ValidateMemberCreateRequest(out var error0))
            {
                return Task.FromResult(new Tuple<TeamResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {error0}}
                    }
                }));
            }

            return GetJsonObjectFromAPIAsync<TeamResponse>(
                memberParams,
                R4MEInfrastructureSettingsV5.TeamUsers,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Bulk create the team members
        ///     TO DO: there is no response from the function.
        /// </summary>
        /// <param name="membersParams"></param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns></returns>
        public ResultResponse BulkCreateTeamMembers(TeamRequest[] membersParams, out ResultResponse resultResponse)
        {
            resultResponse = default;

            if (membersParams == null || (membersParams.Length == 0))
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The array of the user parameters is empty"}}
                    }
                };

                return null;
            }

            foreach (var memberParams in membersParams)
                if (!memberParams.ValidateMemberCreateRequest(out var error0))
                {
                    resultResponse = new ResultResponse
                    {
                        Status = false,
                        Messages = new Dictionary<string, string[]>
                        {
                            {"Error", new[] {error0}}
                        }
                    };

                    return null;
                }

            var newMemberParams = new BulkMembersRequest
            {
                Users = membersParams
            };

            var result = GetJsonObjectFromAPI<ResultResponse>(
                newMemberParams,
                R4MEInfrastructureSettingsV5.TeamUsersBulkCreate,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Bulk create the team members
        ///     TO DO: there is no response from the function.
        /// </summary>
        /// <param name="membersParams"></param>
        /// <returns></returns>
        public Task<Tuple<ResultResponse, ResultResponse>> BulkCreateTeamMembersAsync(TeamRequest[] membersParams)
        {
            if (membersParams == null || membersParams.Length == 0)
            {
                return Task.FromResult(new Tuple<ResultResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The array of the user parameters is empty"}}
                    }
                }));
            }

            foreach (var memberParams in membersParams)
            {
                if (!memberParams.ValidateMemberCreateRequest(out var error0))
                {
                    return Task.FromResult(new Tuple<ResultResponse, ResultResponse>(null, new ResultResponse
                    {
                        Status = false,
                        Messages = new Dictionary<string, string[]>
                        {
                            {"Error", new[] {error0}}
                        }
                    }));
                }
            }

            var newMemberParams = new BulkMembersRequest
            {
                Users = membersParams
            };

            return GetJsonObjectFromAPIAsync<ResultResponse>(
                newMemberParams,
                R4MEInfrastructureSettingsV5.TeamUsersBulkCreate,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Removes a team member (sub-user) from the user's account.
        /// </summary>
        /// <param name="parameters">An object of the type MemberParametersV4 containg the parameter UserId</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Removed team member</returns>
        public TeamResponse RemoveTeamMember(MemberQueryParameters parameters,
            out ResultResponse resultResponse)
        {
            if (parameters?.UserId == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The UserId parameter is not specified"}}
                    }
                };

                return null;
            }

            var response = GetJsonObjectFromAPI<TeamResponse>(
                parameters,
                R4MEInfrastructureSettingsV5.TeamUsers + "/" + parameters.UserId,
                HttpMethodType.Delete,
                out resultResponse);

            return response;
        }

        /// <summary>
        ///     Removes a team member (sub-user) from the user's account.
        /// </summary>
        /// <param name="parameters">An object of the type MemberParametersV4 containg the parameter UserId</param>
        /// <returns>Removed team member</returns>
        public Task<Tuple<TeamResponse, ResultResponse>>  RemoveTeamMemberAsync(MemberQueryParameters parameters)
        {
            if (parameters?.UserId == null)
            {
                return Task.FromResult(new Tuple<TeamResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The UserId parameter is not specified"}}
                    }
                }));
            }

            return GetJsonObjectFromAPIAsync<TeamResponse>(
                parameters,
                R4MEInfrastructureSettingsV5.TeamUsers + "/" + parameters.UserId,
                HttpMethodType.Delete);
        }

        /// <summary>
        ///     Update a team member
        /// </summary>
        /// <param name="queryParameters">Member query parameters</param>
        /// <param name="requestPayload">Member request parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Updated team member</returns>
        public TeamResponse UpdateTeamMember(MemberQueryParameters queryParameters,
            TeamRequest requestPayload,
            out ResultResponse resultResponse)
        {
            if (queryParameters?.UserId == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The UserId parameter is not specified"}}
                    }
                };

                return null;
            }

            if (requestPayload == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The team request object is empty"}}
                    }
                };

                return null;
            }

            var response = GetJsonObjectFromAPI<TeamResponse>(
                requestPayload,
                R4MEInfrastructureSettingsV5.TeamUsers + "/" + queryParameters.UserId,
                HttpMethodType.Patch,
                out resultResponse);

            return response;
        }

        /// <summary>
        ///     Update a team member
        /// </summary>
        /// <param name="queryParameters">Member query parameters</param>
        /// <param name="requestPayload">Member request parameters</param>
        /// <returns>Updated team member</returns>
        public Task<Tuple<TeamResponse, ResultResponse>> UpdateTeamMemberAsync(MemberQueryParameters queryParameters, TeamRequest requestPayload)
        {
            if (queryParameters?.UserId == null)
            {
                return Task.FromResult(new Tuple<TeamResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The UserId parameter is not specified"}}
                    }
                }));
            }

            if (requestPayload == null)
            {
                return Task.FromResult(new Tuple<TeamResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The team request object is empty"}}
                    }
                }));
            }

            return GetJsonObjectFromAPIAsync<TeamResponse>(
                requestPayload,
                R4MEInfrastructureSettingsV5.TeamUsers + "/" + queryParameters.UserId,
                HttpMethodType.Patch);
        }

        /// <summary>
        ///     Add an array of skills to the driver.
        /// </summary>
        /// <param name="queryParameters">Query parameters</param>
        /// <param name="skills">An array of the driver skills</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Updated team member</returns>
        public TeamResponse AddSkillsToDriver(MemberQueryParameters queryParameters,
            string[] skills,
            out ResultResponse resultResponse)
        {
            if (queryParameters?.UserId == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The UserId parameter is not specified"}}
                    }
                };

                return null;
            }

            if (skills == null || skills.Length == 0)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The driver skills array is empty."}}
                    }
                };

                return null;
            }

            #region Prepare Request From Driver Skills

            var driverSkills = new Dictionary<string, string> {{"driver_skills", string.Join(",", skills)}};

            var requestPayload = new TeamRequest
            {
                CustomData = driverSkills
            };

            #endregion

            var response = GetJsonObjectFromAPI<TeamResponse>(
                requestPayload,
                R4MEInfrastructureSettingsV5.TeamUsers + "/" + queryParameters.UserId,
                HttpMethodType.Patch,
                out resultResponse);

            return response;
        }

        /// <summary>
        ///     Add an array of skills to the driver.
        /// </summary>
        /// <param name="queryParameters">Query parameters</param>
        /// <param name="skills">An array of the driver skills</param>
        /// <returns>Updated team member</returns>
        public Task<Tuple<TeamResponse, ResultResponse>> AddSkillsToDriverAsync(MemberQueryParameters queryParameters, string[] skills)
        {
            if (queryParameters?.UserId == null)
            {
                return Task.FromResult(new Tuple<TeamResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The UserId parameter is not specified"}}
                    }
                }));
            }

            if (skills == null || skills.Length == 0)
            {
                return Task.FromResult(new Tuple<TeamResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The driver skills array is empty."}}
                    }
                }));
            }

            #region Prepare Request From Driver Skills

            var driverSkills = new Dictionary<string, string> { { "driver_skills", string.Join(",", skills) } };

            var requestPayload = new TeamRequest
            {
                CustomData = driverSkills
            };

            #endregion

            return GetJsonObjectFromAPIAsync<TeamResponse>(
                requestPayload,
                R4MEInfrastructureSettingsV5.TeamUsers + "/" + queryParameters.UserId,
                HttpMethodType.Patch);
        }

        #endregion

        #region Driver Review

        /// <summary>
        ///     Get list of the drive reviews.
        /// </summary>
        /// <param name="parameters">Query parmeters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>List of the driver reviews</returns>
        public DriverReviewsResponse GetDriverReviewList(DriverReviewParameters parameters,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DriverReviewsResponse>(parameters,
                R4MEInfrastructureSettingsV5.DriverReview,
                HttpMethodType.Get,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get list of the drive reviews.
        /// </summary>
        /// <param name="parameters">Query parmeters</param>
        /// <returns>List of the driver reviews</returns>
        public Task<Tuple<DriverReviewsResponse, ResultResponse>> GetDriverReviewListAsync(DriverReviewParameters parameters)
        {
            return GetJsonObjectFromAPIAsync<DriverReviewsResponse>(parameters,
                R4MEInfrastructureSettingsV5.DriverReview,
                HttpMethodType.Get,
                null,
                true,
                false);
        }

        /// <summary>
        ///     Get driver review by ID
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Driver review</returns>
        public DriverReview GetDriverReviewById(DriverReviewParameters parameters,
            out ResultResponse resultResponse)
        {
            if ((parameters?.RatingId) == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The RatingId parameter is not specified"}}
                    }
                };

                return null;
            }

            var result = GetJsonObjectFromAPI<DriverReview>(parameters,
                R4MEInfrastructureSettingsV5.DriverReview + "/" + parameters.RatingId,
                HttpMethodType.Get,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get driver review by ID
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <returns>Driver review</returns>
        public Task<Tuple<DriverReview, ResultResponse>> GetDriverReviewByIdAsync(DriverReviewParameters parameters)
        {
            if (parameters?.RatingId == null)
            {
                return Task.FromResult(new Tuple<DriverReview, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The RatingId parameter is not specified"}}
                    }
                }));
            }

            return GetJsonObjectFromAPIAsync<DriverReview>(parameters,
                R4MEInfrastructureSettingsV5.DriverReview + "/" + parameters.RatingId,
                HttpMethodType.Get,
                null,
                true,
                false);
        }

        /// <summary>
        ///     Upload driver review to the server
        /// </summary>
        /// <param name="driverReview">Request payload</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Driver review</returns>
        public DriverReview CreateDriverReview(DriverReview driverReview, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<DriverReview>(
                driverReview,
                R4MEInfrastructureSettingsV5.DriverReview,
                HttpMethodType.Post,
                out resultResponse);
        }

        /// <summary>
        ///     Upload driver review to the server
        /// </summary>
        /// <param name="driverReview">Request payload</param>
        /// <returns>Driver review</returns>
        public Task<Tuple<DriverReview, ResultResponse>> CreateDriverReviewAsync(DriverReview driverReview)
        {
            return GetJsonObjectFromAPIAsync<DriverReview>(
                driverReview,
                R4MEInfrastructureSettingsV5.DriverReview,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Update a driver review.
        /// </summary>
        /// <param name="driverReview">Request payload</param>
        /// <param name="method">Http method</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Driver review</returns>
        public DriverReview UpdateDriverReview(DriverReview driverReview,
            HttpMethodType method,
            out ResultResponse resultResponse)
        {
            if (method != HttpMethodType.Patch && method != HttpMethodType.Put)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The parameter method has an incorect value."}}
                    }
                };

                return null;
            }

            if (driverReview.RatingId == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The parameters doesn't contain parameter RatingId."}}
                    }
                };

                return null;
            }

            return GetJsonObjectFromAPI<DriverReview>(
                driverReview,
                R4MEInfrastructureSettingsV5.DriverReview + "/" + driverReview.RatingId,
                method,
                out resultResponse);
        }

        /// <summary>
        ///     Update a driver review.
        /// </summary>
        /// <param name="driverReview">Request payload</param>
        /// <param name="method">Http method</param>
        /// <returns>Driver review</returns>
        public Task<Tuple<DriverReview, ResultResponse>> UpdateDriverReviewAsync(DriverReview driverReview, HttpMethodType method)
        {
            if (method != HttpMethodType.Patch && method != HttpMethodType.Put)
            {
                return Task.FromResult(new Tuple<DriverReview, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The parameter method has an incorect value."}}
                    }
                }));
            }

            if (driverReview.RatingId == null)
            {
                return Task.FromResult(new Tuple<DriverReview, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The parameters doesn't contain parameter RatingId."}}
                    }
                }));
            }

            return GetJsonObjectFromAPIAsync<DriverReview>(
                driverReview,
                R4MEInfrastructureSettingsV5.DriverReview + "/" + driverReview.RatingId,
                method, null, false, false);
        }

        #endregion

        #region Routes

        public DataObjectRoute[] GetRoutes(RouteParametersQuery routeParameters, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute[]>(routeParameters,
                R4MEInfrastructureSettingsV5.Routes,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        public Task<Tuple<DataObjectRoute[], ResultResponse>> GetRoutesAsync(RouteParametersQuery routeParameters)
        {
            return GetJsonObjectFromAPIAsync<DataObjectRoute[]>(routeParameters,
                R4MEInfrastructureSettingsV5.Routes,
                HttpMethodType.Get);
        }

        public DataObjectRoute[] GetAllRoutesWithPagination(RouteParametersQuery routeParameters,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute[]>(routeParameters,
                R4MEInfrastructureSettingsV5.RoutesPaginate,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        public Task<Tuple<DataObjectRoute[], ResultResponse>> GetAllRoutesWithPagination(RouteParametersQuery routeParameters)
        {
            return GetJsonObjectFromAPIAsync<DataObjectRoute[]>(routeParameters,
                R4MEInfrastructureSettingsV5.RoutesPaginate,
                HttpMethodType.Get);
        }

        public DataObjectRoute[] GetPaginatedRouteListWithoutElasticSearch(RouteParametersQuery routeParameters,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute[]>(routeParameters,
                R4MEInfrastructureSettingsV5.RoutesFallbackPaginate,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        public Task<Tuple<DataObjectRoute[], ResultResponse>> GetPaginatedRouteListWithoutElasticSearchAsync(RouteParametersQuery routeParameters)
        {
            return GetJsonObjectFromAPIAsync<DataObjectRoute[]>(routeParameters,
                R4MEInfrastructureSettingsV5.RoutesFallbackPaginate,
                HttpMethodType.Get);
        }

        public DataObjectRoute[] GetRouteDataTableWithElasticSearch(
            RouteFilterParameters routeFilterParameters,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute[]>(
                routeFilterParameters,
                R4MEInfrastructureSettingsV5.RoutesFallbackDatatable,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        public Task<Tuple<DataObjectRoute[], ResultResponse>> GetRouteDataTableWithElasticSearchAsync(RouteFilterParameters routeFilterParameters)
        {
            return GetJsonObjectFromAPIAsync<DataObjectRoute[]>(
                routeFilterParameters,
                R4MEInfrastructureSettingsV5.RoutesFallbackDatatable,
                HttpMethodType.Post);
        }

        public DataObjectRoute[] GetRouteDatatableWithElasticSearch(
            RouteFilterParameters routeFilterParameters,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute[]>(
                routeFilterParameters,
                R4MEInfrastructureSettingsV5.RoutesDatatable,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        public Task<Tuple<DataObjectRoute[], ResultResponse>> GetRouteDatatableWithElasticSearchAsync(
            RouteFilterParameters routeFilterParameters)
        {
            return GetJsonObjectFromAPIAsync<DataObjectRoute[]>(
                routeFilterParameters,
                R4MEInfrastructureSettingsV5.RoutesDatatable,
                HttpMethodType.Post);
        }

        public DataObjectRoute[] GetRouteListWithoutElasticSearch(RouteParametersQuery routeParameters,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute[]>(routeParameters,
                R4MEInfrastructureSettingsV5.RoutesFallback,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        public Task<Tuple<DataObjectRoute[], ResultResponse>> GetRouteListWithoutElasticSearchAsync(RouteParametersQuery routeParameters)
        {
            return GetJsonObjectFromAPIAsync<DataObjectRoute[]>(routeParameters,
                R4MEInfrastructureSettingsV5.RoutesFallback,
                HttpMethodType.Get);
        }

        public RouteDuplicateResponse DuplicateRoute(string[] routeIDs, out ResultResponse resultResponse)
        {
            var duplicateParameter = new Dictionary<string, string[]>
            {
                {
                    "duplicate_routes_id", routeIDs
                }
            };

            var duplicateParameterJsonString = R4MeUtils.SerializeObjectToJson(duplicateParameter, true);

            var content = new StringContent(duplicateParameterJsonString, Encoding.UTF8, "application/json");

            var genParams = new RouteParametersQuery();

            var result = GetJsonObjectFromAPI<RouteDuplicateResponse>(
                genParams,
                R4MEInfrastructureSettingsV5.RoutesDuplicate,
                HttpMethodType.Post,
                content,
                out resultResponse);

            return result;
        }

        public Task<Tuple<RouteDuplicateResponse, ResultResponse>> DuplicateRouteAsync(string[] routeIDs)
        {
            var duplicateParameter = new Dictionary<string, string[]>
            {
                {
                    "duplicate_routes_id", routeIDs
                }
            };

            var duplicateParameterJsonString = R4MeUtils.SerializeObjectToJson(duplicateParameter, true);

            var content = new StringContent(duplicateParameterJsonString, Encoding.UTF8, "application/json");

            var genParams = new RouteParametersQuery();

            return GetJsonObjectFromAPIAsync<RouteDuplicateResponse>(
                genParams,
                R4MEInfrastructureSettingsV5.RoutesDuplicate,
                HttpMethodType.Post,
                content,
                false,
                false);
        }

        public RoutesDeleteResponse DeleteRoutes(string[] routeIds, out ResultResponse resultResponse)
        {
            var strRouteIds = "";

            foreach (var routeId in routeIds)
            {
                if (strRouteIds.Length > 0) strRouteIds += ",";
                strRouteIds += routeId;
            }

            var genericParameters = new GenericParameters();

            genericParameters.ParametersCollection.Add("route_id", strRouteIds);

            var response = GetJsonObjectFromAPI<RoutesDeleteResponse>(genericParameters,
                R4MEInfrastructureSettingsV5.Routes,
                HttpMethodType.Delete,
                out resultResponse);

            return response;
        }

        public Task<Tuple<RoutesDeleteResponse, ResultResponse>> DeleteRoutesAsync(string[] routeIds)
        {
            var strRouteIds = "";

            foreach (var routeId in routeIds)
            {
                if (strRouteIds.Length > 0) strRouteIds += ",";
                strRouteIds += routeId;
            }

            var genericParameters = new GenericParameters();

            genericParameters.ParametersCollection.Add("route_id", strRouteIds);

            return GetJsonObjectFromAPIAsync<RoutesDeleteResponse>(genericParameters,
                R4MEInfrastructureSettingsV5.Routes,
                HttpMethodType.Delete);
        }

        public RouteDataTableConfigResponse GetRouteDataTableConfig(out ResultResponse resultResponse)
        {
            var genericParameters = new GenericParameters();

            var result = GetJsonObjectFromAPI<RouteDataTableConfigResponse>(genericParameters,
                R4MEInfrastructureSettingsV5.RoutesDatatableConfig,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        public Task<Tuple<RouteDataTableConfigResponse, ResultResponse>> GetRouteDataTableConfigAsync()
        {
            var genericParameters = new GenericParameters();

            return GetJsonObjectFromAPIAsync<RouteDataTableConfigResponse>(genericParameters,
                R4MEInfrastructureSettingsV5.RoutesDatatableConfig,
                HttpMethodType.Get);
        }

        public RouteDataTableConfigResponse GetRouteDataTableFallbackConfig(out ResultResponse resultResponse)
        {
            var genericParameters = new GenericParameters();

            var result = GetJsonObjectFromAPI<RouteDataTableConfigResponse>(genericParameters,
                R4MEInfrastructureSettingsV5.RoutesDatatableConfigFallback,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        public Task<Tuple<RouteDataTableConfigResponse, ResultResponse>> GetRouteDataTableFallbackConfigAsync()
        {
            var genericParameters = new GenericParameters();

            return GetJsonObjectFromAPIAsync<RouteDataTableConfigResponse>(genericParameters,
                R4MEInfrastructureSettingsV5.RoutesDatatableConfigFallback,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     You can update a route in two ways:
        ///     1. Modify existing route and put in this function the route object as parameter.
        ///     2. Create an empty route object and assign values to the parameters:
        ///     - RouteID;
        ///     - Parameters (optional);
        ///     - Addresses (Optional).
        /// </summary>
        /// <param name="route">Route object</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Updated route</returns>
        [Obsolete("Will be finished after implementing Route Destinations API")]
        public DataObjectRoute UpdateRoute(DataObjectRoute route, out ResultResponse resultResponse)
        {
            var routeQueryParams = new RouteParametersQuery();

            routeQueryParams.RouteId = route.RouteID;

            if (route.Parameters != null) routeQueryParams.Parameters = route.Parameters;

            if (route.Addresses != null && route.Addresses.Length > 0) routeQueryParams.Addresses = route.Addresses;

            var response = GetJsonObjectFromAPI<DataObjectRoute>(
                routeQueryParams,
                R4MEInfrastructureSettingsV5.Routes,
                HttpMethodType.Put,
                out resultResponse);

            return response;
        }


        public StatusResponse InsertRouteBreaks(RouteBreaks breaks, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<StatusResponse>(
                breaks,
                R4MEInfrastructureSettingsV5.RouteBreaks,
                HttpMethodType.Post,
                out resultResponse);
        }

        #endregion

        #region Optimizations

        /// <summary>
        ///     Generates optimized routes
        /// </summary>
        /// <param name="optimizationParameters">
        ///     The input parameters for the routes optimization, which encapsulates:
        ///     the route parameters and the addresses.
        /// </param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Generated optimization problem object</returns>
        public DataObject RunOptimization(OptimizationParameters optimizationParameters,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DataObject>(optimizationParameters,
                R4MEInfrastructureSettings.ApiHost,
                HttpMethodType.Post,
                false,
                false,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Generates optimized routes
        /// </summary>
        /// <param name="optimizationParameters">
        ///     The input parameters for the routes optimization, which encapsulates:
        ///     the route parameters and the addresses.
        /// </param>
        /// <returns>Generated optimization problem object</returns>
        public Task<Tuple<DataObject, ResultResponse>> RunOptimizationAsync(OptimizationParameters optimizationParameters)
        {
            return GetJsonObjectFromAPIAsync<DataObject>(optimizationParameters,
                R4MEInfrastructureSettings.ApiHost,
                HttpMethodType.Post,
                null,
                false,
                false);
        }

        /// <summary>
        ///     The response returned by the remove optimization command
        /// </summary>
        [DataContract]
        private sealed class RemoveOptimizationResponse
        {
            /// <value>True if an optimization was removed successfuly </value>
            [DataMember(Name = "status")]
            public bool Status { get; set; }

            /// <value>The number of the removed optimizations </value>
            [DataMember(Name = "removed")]
            public int Removed { get; set; }
        }

        /// <summary>
        ///     The request parameters for an optimization removing
        /// </summary>
        [DataContract]
        private sealed class RemoveOptimizationRequest : GenericParameters
        {
            /// <value>If true will be redirected</value>
            [HttpQueryMember(Name = "redirect", EmitDefaultValue = false)]
            public int Redirect { get; set; }

            /// <value>The array of the optimization problem IDs to be removed</value>
            [DataMember(Name = "optimization_problem_ids", EmitDefaultValue = false)]
            public string[] OptimizationProblemIds { get; set; }
        }

        /// <summary>
        ///     Remove an existing optimization belonging to an user.
        /// </summary>
        /// <param name="optimizationProblemIDs"> Optimization Problem IDs </param>
        /// <param name="resultResponse"> Returned error string in case of the processs failing </param>
        /// <returns> Result status true/false </returns>
        public bool RemoveOptimization(string[] optimizationProblemIDs, out ResultResponse resultResponse)
        {
            var remParameters = new RemoveOptimizationRequest
            {
                Redirect = 0,
                OptimizationProblemIds = optimizationProblemIDs
            };

            var response = GetJsonObjectFromAPI<RemoveOptimizationResponse>(remParameters,
                R4MEInfrastructureSettings.ApiHost,
                HttpMethodType.Delete,
                out resultResponse);
            if (response != null)
            {
                if (response.Status && response.Removed > 0) return true;
                return false;
            }

            return false;
        }

        /// <summary>
        ///     Remove an existing optimization belonging to an user.
        /// </summary>
        /// <param name="optimizationProblemIDs"> Optimization Problem IDs </param>
        /// <returns> Result status true/false </returns>
        public async Task<Tuple<bool, ResultResponse>> RemoveOptimizationAsync(string[] optimizationProblemIDs)
        {
            var remParameters = new RemoveOptimizationRequest
            {
                Redirect = 0,
                OptimizationProblemIds = optimizationProblemIDs
            };

            var response = await GetJsonObjectFromAPIAsync<RemoveOptimizationResponse>(remParameters,
                R4MEInfrastructureSettings.ApiHost,
                HttpMethodType.Delete).ConfigureAwait(false);
            if (response.Item1 != null)
            {
                if (response.Item1.Status && response.Item1.Removed > 0) return new Tuple<bool, ResultResponse>(true, response.Item2);
                return new Tuple<bool, ResultResponse>(false, response.Item2);
            }

            return new Tuple<bool, ResultResponse>(false, response.Item2);
        }

        #endregion

        #region Vehicles

        /// <summary>
        ///     Creates a vehicle
        /// </summary>
        /// <param name="vehicle">The VehicleV4Parameters type object as the request payload </param>
        /// <param name="resultResponse"> Failing response </param>
        /// <returns>The created vehicle </returns>
        public Vehicle CreateVehicle(Vehicle vehicle, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<Vehicle>(
                vehicle,
                R4MEInfrastructureSettingsV5.Vehicles,
                HttpMethodType.Post,
                out resultResponse);
        }

        /// <summary>
        ///     Creates a vehicle
        /// </summary>
        /// <param name="vehicle">The VehicleV4Parameters type object as the request payload </param>
        /// <returns>The created vehicle </returns>
        public Task<Tuple<Vehicle, ResultResponse>> CreateVehicleAsync(Vehicle vehicle)
        {
            return GetJsonObjectFromAPIAsync<Vehicle>(
                vehicle,
                R4MEInfrastructureSettingsV5.Vehicles,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Returns the VehiclesPaginated type object containing an array of the vehicles
        /// </summary>
        /// <param name="vehicleParams">The VehicleParameters type object as the query parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An array of the vehicles</returns>
        public Vehicle[] GetPaginatedVehiclesList(VehicleParameters vehicleParams, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<Vehicle[]>(
                vehicleParams,
                R4MEInfrastructureSettingsV5.Vehicles,
                HttpMethodType.Get,
                out resultResponse);
        }

        /// <summary>
        ///     Returns the VehiclesPaginated type object containing an array of the vehicles
        /// </summary>
        /// <param name="vehicleParams">The VehicleParameters type object as the query parameters</param>
        /// <returns>An array of the vehicles</returns>
        public Task<Tuple<Vehicle[], ResultResponse>> GetPaginatedVehiclesListAsync(VehicleParameters vehicleParams)
        {
            return GetJsonObjectFromAPIAsync<Vehicle[]>(
                vehicleParams,
                R4MEInfrastructureSettingsV5.Vehicles,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Removes a vehicle from a user's account
        /// </summary>
        /// <param name="vehicleId"> The Vehicle ID </param>
        /// <param name="resultResponse"> Failing response </param>
        /// <returns>The removed vehicle</returns>
        public Vehicle DeleteVehicle(string vehicleId, out ResultResponse resultResponse)
        {
            if ((vehicleId?.Length ?? 0) != 32)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle ID is not specified"}}
                    }
                };

                return null;
            }

            return GetJsonObjectFromAPI<Vehicle>(new GenericParameters(),
                R4MEInfrastructureSettings.Vehicle_V4 + "/" + vehicleId,
                HttpMethodType.Delete,
                out resultResponse);
        }

        /// <summary>
        ///     Removes a vehicle from a user's account
        /// </summary>
        /// <param name="vehicleId"> The Vehicle ID </param>
        /// <returns>The removed vehicle</returns>
        public Task<Tuple<Vehicle, ResultResponse>> DeleteVehicleAsync(string vehicleId)
        {
            if ((vehicleId?.Length ?? 0) != 32)
            {
                return Task.FromResult(new Tuple<Vehicle, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle ID is not specified"}}
                    }
                }));
            }

            return GetJsonObjectFromAPIAsync<Vehicle>(new GenericParameters(),
                R4MEInfrastructureSettings.Vehicle_V4 + "/" + vehicleId,
                HttpMethodType.Delete);
        }

        /// <summary>
        ///     Creates temporary vehicle in the database.
        /// </summary>
        /// <param name="vehParams">Request parameters for creating a temporary vehicle</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>A result with an order ID</returns>
        public VehicleTemporary CreateTemporaryVehicle(VehicleTemporary vehParams, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<VehicleTemporary>(
                vehParams,
                R4MEInfrastructureSettingsV5.VehicleTemporary,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Creates temporary vehicle in the database.
        /// </summary>
        /// <param name="vehParams">Request parameters for creating a temporary vehicle</param>
        /// <returns>A result with an order ID</returns>
        public Task<Tuple<VehicleTemporary, ResultResponse>> CreateTemporaryVehicleAsync(VehicleTemporary vehParams)
        {
            return GetJsonObjectFromAPIAsync<VehicleTemporary>(
                vehParams,
                R4MEInfrastructureSettingsV5.VehicleTemporary,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Execute a vehicle order
        /// </summary>
        /// <param name="vehOrderParams">Vehicle order parameters</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Created vehicle order</returns>
        public VehicleOrderResponse ExecuteVehicleOrder(VehicleOrderParameters vehOrderParams,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<VehicleOrderResponse>(
                vehOrderParams,
                R4MEInfrastructureSettingsV5.VehicleExecuteOrder,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Execute a vehicle order
        /// </summary>
        /// <param name="vehOrderParams">Vehicle order parameters</param>
        /// <returns>Created vehicle order</returns>
        public Task<Tuple<VehicleOrderResponse, ResultResponse>> ExecuteVehicleOrderAsync(VehicleOrderParameters vehOrderParams)
        {
            return GetJsonObjectFromAPIAsync<VehicleOrderResponse>(
                vehOrderParams,
                R4MEInfrastructureSettingsV5.VehicleExecuteOrder,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Get latest vehicle locations by specified vehicle IDs.
        /// </summary>
        /// <param name="vehParams">Vehicle query parameters containing vehicle IDs.</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Data with vehicles</returns>
        public VehicleLocationResponse GetVehicleLocations(VehicleParameters vehParams,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<VehicleLocationResponse>(
                vehParams,
                R4MEInfrastructureSettingsV5.VehicleLocation,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get latest vehicle locations by specified vehicle IDs.
        /// </summary>
        /// <param name="vehParams">Vehicle query parameters containing vehicle IDs.</param>
        /// <returns>Data with vehicles</returns>
        public Task<Tuple<VehicleLocationResponse, ResultResponse>> GetVehicleLocationsAsync(VehicleParameters vehParams)
        {
            return GetJsonObjectFromAPIAsync<VehicleLocationResponse>(
                vehParams,
                R4MEInfrastructureSettingsV5.VehicleLocation,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Get the Vehicle by specifying vehicle ID.
        /// </summary>
        /// <param name="vehicleId">Vehicle ID</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Vehicle object</returns>
        public Vehicle GetVehicleById(string vehicleId, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<Vehicle>(
                new VehicleParameters(),
                R4MEInfrastructureSettingsV5.Vehicles + "/" + vehicleId,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get the Vehicle by specifying vehicle ID.
        /// </summary>
        /// <param name="vehicleId">Vehicle ID</param>
        /// <returns>Vehicle object</returns>
        public Task<Tuple<Vehicle, ResultResponse>> GetVehicleByIdAsync(string vehicleId)
        {
            return GetJsonObjectFromAPIAsync<Vehicle>(
                new VehicleParameters(),
                R4MEInfrastructureSettingsV5.Vehicles + "/" + vehicleId,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Get the Vehicle track by specifying vehicle ID.
        /// </summary>
        /// <param name="vehicleId">Vehicle ID</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Vehicle track object</returns>
        public VehicleTrackResponse GetVehicleTrack(string vehicleId, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<VehicleTrackResponse>(
                new VehicleParameters(),
                R4MEInfrastructureSettingsV5.Vehicles + "/" + vehicleId + "/" + "track",
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get the Vehicle track by specifying vehicle ID.
        /// </summary>
        /// <param name="vehicleId">Vehicle ID</param>
        /// <returns>Vehicle track object</returns>
        public Task<Tuple<VehicleTrackResponse, ResultResponse>> GetVehicleTrackAsync(string vehicleId)
        {
            return GetJsonObjectFromAPIAsync<VehicleTrackResponse>(
                new VehicleParameters(),
                R4MEInfrastructureSettingsV5.Vehicles + "/" + vehicleId + "/" + "track",
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Get paginated list of the vehicle profiles.
        /// </summary>
        /// <param name="profileParams">Vehicle profile request parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>The data including list of the vehicle profiles.</returns>
        public VehicleProfilesResponse GetVehicleProfiles(VehicleProfileParameters profileParams,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<VehicleProfilesResponse>(
                profileParams,
                R4MEInfrastructureSettingsV5.VehicleProfiles,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get paginated list of the vehicle profiles.
        /// </summary>
        /// <param name="profileParams">Vehicle profile request parameters</param>
        /// <returns>The data including list of the vehicle profiles.</returns>
        public Task<Tuple<VehicleProfilesResponse, ResultResponse>> GetVehicleProfilesAsync(VehicleProfileParameters profileParams)
        {
            return GetJsonObjectFromAPIAsync<VehicleProfilesResponse>(
                profileParams,
                R4MEInfrastructureSettingsV5.VehicleProfiles,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Create a vehicle profile.
        /// </summary>
        /// <param name="vehicleProfileParams">Vehicle profile body parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Created vehicle profile</returns>
        public VehicleProfile CreateVehicleProfile(VehicleProfile vehicleProfileParams,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<VehicleProfile>(
                vehicleProfileParams,
                R4MEInfrastructureSettingsV5.VehicleProfiles,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Create a vehicle profile.
        /// </summary>
        /// <param name="vehicleProfileParams">Vehicle profile body parameters</param>
        /// <returns>Created vehicle profile</returns>
        public Task<Tuple<VehicleProfile, ResultResponse>> CreateVehicleProfileAsync(VehicleProfile vehicleProfileParams)
        {
            return GetJsonObjectFromAPIAsync<VehicleProfile>(
                vehicleProfileParams,
                R4MEInfrastructureSettingsV5.VehicleProfiles,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Remove a vehicle profile from database.
        ///     TO DO: adjust response structure.
        /// </summary>
        /// <param name="vehicleProfileParams">Vehicle profile parameter containing a vehicle profile ID </param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Removed vehicle profile</returns>
        public object DeleteVehicleProfile(VehicleProfileParameters vehicleProfileParams,
            out ResultResponse resultResponse)
        {
            if (vehicleProfileParams == null || vehicleProfileParams.VehicleProfileId == null || vehicleProfileParams.VehicleProfileId == 0)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle ID is not specified"}}
                    }
                };

                return null;
            }

            var result = GetJsonObjectFromAPI<object>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.VehicleProfiles + "/" + vehicleProfileParams.VehicleProfileId,
                HttpMethodType.Delete,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Remove a vehicle profile from database.
        ///     TO DO: adjust response structure.
        /// </summary>
        /// <param name="vehicleProfileParams">Vehicle profile parameter containing a vehicle profile ID </param>
        /// <returns>Removed vehicle profile</returns>
        public Task<Tuple<object, ResultResponse>> DeleteVehicleProfileAsync(VehicleProfileParameters vehicleProfileParams)
        {
            if (vehicleProfileParams == null || vehicleProfileParams.VehicleProfileId == null || vehicleProfileParams.VehicleProfileId == 0)
            {
                return Task.FromResult(new Tuple<object, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle ID is not specified"}}
                    }
                }));
            }

            return GetJsonObjectFromAPIAsync<object>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.VehicleProfiles + "/" + vehicleProfileParams.VehicleProfileId,
                HttpMethodType.Delete);
        }

        /// <summary>
        ///     Get vehicle profile by ID.
        /// </summary>
        /// <param name="vehicleProfileParams">Vehicle profile parameter containing a vehicle profile ID </param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Vehicle profile</returns>
        public VehicleProfile GetVehicleProfileById(VehicleProfileParameters vehicleProfileParams,
            out ResultResponse resultResponse)
        {
            if (vehicleProfileParams == null || vehicleProfileParams.VehicleProfileId == null || vehicleProfileParams.VehicleProfileId == 0)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle ID is not specified"}}
                    }
                };

                return null;
            }

            var result = GetJsonObjectFromAPI<VehicleProfile>(
                new VehicleParameters(),
                R4MEInfrastructureSettingsV5.VehicleProfiles + "/" + vehicleProfileParams.VehicleProfileId,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get vehicle profile by ID.
        /// </summary>
        /// <param name="vehicleProfileParams">Vehicle profile parameter containing a vehicle profile ID </param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Vehicle profile</returns>
        public VehicleProfile GetVehicleProfileByIdAsync(VehicleProfileParameters vehicleProfileParams,
            out ResultResponse resultResponse)
        {
            if (vehicleProfileParams == null || vehicleProfileParams.VehicleProfileId == null || vehicleProfileParams.VehicleProfileId == 0)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle ID is not specified"}}
                    }
                };

                return null;
            }

            var result = GetJsonObjectFromAPI<VehicleProfile>(
                new VehicleParameters(),
                R4MEInfrastructureSettingsV5.VehicleProfiles + "/" + vehicleProfileParams.VehicleProfileId,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get vehicle by license plate.
        /// </summary>
        /// <param name="vehicleParams">Vehicle parameter containing vehicle license plate</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Vehicle</returns>
        public VehicleResponse GetVehicleByLicensePlate(VehicleParameters vehicleParams,
            out ResultResponse resultResponse)
        {
            if ((vehicleParams?.VehicleLicensePlate?.Length ?? 0) < 1)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle license plate is not specified"}}
                    }
                };

                return null;
            }

            var result = GetJsonObjectFromAPI<VehicleResponse>(
                vehicleParams,
                R4MEInfrastructureSettingsV5.VehicleLicense,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get vehicle by license plate.
        /// </summary>
        /// <param name="vehicleParams">Vehicle parameter containing vehicle license plate</param>
        /// <returns>Vehicle</returns>
        public Task<Tuple<VehicleResponse, ResultResponse>>  GetVehicleByLicensePlateAsync(VehicleParameters vehicleParams)
        {
            if ((vehicleParams?.VehicleLicensePlate?.Length ?? 0) < 1)
            {
                return Task.FromResult(new Tuple<VehicleResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle license plate is not specified"}}
                    }
                }));
            }

            return GetJsonObjectFromAPIAsync<VehicleResponse>(
                vehicleParams,
                R4MEInfrastructureSettingsV5.VehicleLicense,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Get vehicle by license plate.
        /// </summary>
        /// <param name="vehicleParams">Vehicle parameter containing vehicle license plate</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Vehicle</returns>
        public VehicleResponse GetVehicleByLicensePlateAsync(VehicleParameters vehicleParams,
            out ResultResponse resultResponse)
        {
            if ((vehicleParams?.VehicleLicensePlate?.Length ?? 0) < 1)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle license plate is not specified"}}
                    }
                };

                return null;
            }

            var result = GetJsonObjectFromAPI<VehicleResponse>(
                vehicleParams,
                R4MEInfrastructureSettingsV5.VehicleLicense,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Search vehicles by sending request body.
        /// </summary>
        /// <param name="searchParams">Search parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An array of the found vehicles</returns>
        [ObsoleteAttribute("This method is deprecated until resolving the response issue.")]
        public Vehicle[] SearchVehicles(VehicleSearchParameters searchParams, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<Vehicle[]>(
                searchParams,
                R4MEInfrastructureSettingsV5.VehicleSearch,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Update a vehicle
        /// </summary>
        /// <param name="vehicleParams">Vehicle body parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Updated vehicle </returns>
        public Vehicle UpdateVehicle(Vehicle vehicleParams, out ResultResponse resultResponse)
        {
            if (vehicleParams == null || (vehicleParams.VehicleId?.Length ?? 0) != 32)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle ID is not specified"}}
                    }
                };

                return null;
            }

            var updateBodyJsonString = R4MeUtils.SerializeObjectToJson(vehicleParams, false);

            var content = new StringContent(updateBodyJsonString, Encoding.UTF8, "application/json");

            var genParams = new RouteParametersQuery();

            var result = GetJsonObjectFromAPI<Vehicle>(
                genParams,
                R4MEInfrastructureSettingsV5.Vehicles + "/" + vehicleParams.VehicleId,
                HttpMethodType.Patch,
                content,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Update a vehicle
        /// </summary>
        /// <param name="vehicleParams">Vehicle body parameters</param>
        /// <returns>Updated vehicle </returns>
        public Task<Tuple<Vehicle, ResultResponse>> UpdateVehicleAsync(Vehicle vehicleParams)
        {
            if (vehicleParams == null || (vehicleParams.VehicleId?.Length ?? 0) != 32)
            {
                return Task.FromResult(new Tuple<Vehicle, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle ID is not specified"}}
                    }
                }));
            }

            var updateBodyJsonString = R4MeUtils.SerializeObjectToJson(vehicleParams, false);

            var content = new StringContent(updateBodyJsonString, Encoding.UTF8, "application/json");

            var genParams = new RouteParametersQuery();

            return GetJsonObjectFromAPIAsync<Vehicle>(
                genParams,
                R4MEInfrastructureSettingsV5.Vehicles + "/" + vehicleParams.VehicleId,
                HttpMethodType.Patch,
                content,
                false,
                false);
        }

        /// <summary>
        ///     Update a vehicle profile.
        /// </summary>
        /// <param name="profileParams">Vehicle profile object as body payload</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Updated vehicle profile</returns>
        public VehicleProfile UpdateVehicleProfile(VehicleProfile profileParams, out ResultResponse resultResponse)
        {
            if (profileParams == null || (profileParams.VehicleProfileId ?? 0) < 1)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle profile ID is not specified"}}
                    }
                };

                return null;
            }

            var updateBodyJsonString = R4MeUtils.SerializeObjectToJson(profileParams, false);

            var content = new StringContent(updateBodyJsonString, Encoding.UTF8, "application/json");

            var genParams = new RouteParametersQuery();

            var result = GetJsonObjectFromAPI<VehicleProfile>(
                genParams,
                R4MEInfrastructureSettingsV5.VehicleProfiles + "/" + profileParams.VehicleProfileId,
                HttpMethodType.Patch,
                content,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Update a vehicle profile.
        /// </summary>
        /// <param name="profileParams">Vehicle profile object as body payload</param>
        /// <returns>Updated vehicle profile</returns>
        public Task<Tuple<VehicleProfile, ResultResponse>> UpdateVehicleProfileAsync(VehicleProfile profileParams)
        {
            if (profileParams == null || (profileParams.VehicleProfileId ?? 0) < 1)
            {
                return Task.FromResult(new Tuple<VehicleProfile, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle profile ID is not specified"}}
                    }
                }));
            }

            var updateBodyJsonString = R4MeUtils.SerializeObjectToJson(profileParams, false);

            var content = new StringContent(updateBodyJsonString, Encoding.UTF8, "application/json");

            var genParams = new RouteParametersQuery();

            return GetJsonObjectFromAPIAsync<VehicleProfile>(
                genParams,
                R4MEInfrastructureSettingsV5.VehicleProfiles + "/" + profileParams.VehicleProfileId,
                HttpMethodType.Patch,
                content,
                false,
                false);
        }

        #endregion

        #region Teleamtics Platform

        #region Connection

        /// <summary>
        ///     Get all registered telematics connections.
        /// </summary>
        /// <param name="resultResponse">Error response</param>
        /// <returns>An array of the Connection type objects</returns>
        public Connection[] GetTelematicsConnections(out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<Connection[]>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.TelematicsConnection,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get all registered telematics connections.
        /// </summary>
        /// <returns>An array of the Connection type objects</returns>
        public Task<Tuple<Connection[], ResultResponse>> GetTelematicsConnectionsAsync()
        {
            return GetJsonObjectFromAPIAsync<Connection[]>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.TelematicsConnection,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Get a telematics connection by specified access token
        /// </summary>
        /// <param name="connectionParams">
        ///     The telematics query paramaters
        ///     as ConnectionPaameters type object
        /// </param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>A connection type object</returns>
        public Connection GetTelematicsConnectionByToken(ConnectionParameters connectionParams,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<Connection>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.TelematicsConnection + "/" + connectionParams.ConnectionToken,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get a telematics connection by specified access token
        /// </summary>
        /// <param name="connectionParams">
        ///     The telematics query paramaters
        ///     as ConnectionPaameters type object
        /// </param>
        /// <returns>A connection type object</returns>
        public Task<Tuple<Connection, ResultResponse>> GetTelematicsConnectionByTokenAsync(ConnectionParameters connectionParams)
        {
            return GetJsonObjectFromAPIAsync<Connection>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.TelematicsConnection + "/" + connectionParams.ConnectionToken,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Register a telematics connection.
        /// </summary>
        /// <param name="connectionParams">
        ///     The telematics query paramaters
        ///     as ConnectionPaameters type object
        /// </param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>A connection type object</returns>
        public Connection RegisterTelematicsConnection(ConnectionParameters connectionParams,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<Connection>(
                connectionParams,
                R4MEInfrastructureSettingsV5.TelematicsConnection,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Register a telematics connection.
        /// </summary>
        /// <param name="connectionParams">
        ///     The telematics query paramaters
        ///     as ConnectionPaameters type object
        /// </param>
        /// <returns>A connection type object</returns>
        public Task<Tuple<Connection, ResultResponse>> RegisterTelematicsConnectionAsync(ConnectionParameters connectionParams)
        {
            return GetJsonObjectFromAPIAsync<Connection>(
                connectionParams,
                R4MEInfrastructureSettingsV5.TelematicsConnection,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Delete telematics connection account by specified access token.
        /// </summary>
        /// <param name="connectionParams">
        ///     The telematics query paramaters
        ///     as ConnectionPaameters type object
        /// </param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Removed teleamtics connection</returns>
        public Connection DeleteTelematicsConnection(ConnectionParameters connectionParams,
            out ResultResponse resultResponse)
        {
            if (connectionParams == null || (connectionParams.ConnectionToken ?? "").Length < 1)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The connection token is not specified"}}
                    }
                };

                return null;
            }

            var result = GetJsonObjectFromAPI<Connection>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.TelematicsConnection + "/" + connectionParams.ConnectionToken,
                HttpMethodType.Delete,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Delete telematics connection account by specified access token.
        /// </summary>
        /// <param name="connectionParams">
        ///     The telematics query paramaters
        ///     as ConnectionPaameters type object
        /// </param>
        /// <returns>Removed teleamtics connection</returns>
        public Task<Tuple<Connection, ResultResponse>> DeleteTelematicsConnectionAsync(ConnectionParameters connectionParams)
        {
            if (connectionParams == null || (connectionParams.ConnectionToken ?? "").Length < 1)
            {
                return Task.FromResult(new Tuple<Connection, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The connection token is not specified"}}
                    }
                }));
            }

            return GetJsonObjectFromAPIAsync<Connection>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.TelematicsConnection + "/" + connectionParams.ConnectionToken,
                HttpMethodType.Delete);
        }

        /// <summary>
        ///     Update telemetics connection's account
        /// </summary>
        /// <param name="connectionParams">
        ///     The telematics query paramaters
        ///     as ConnectionPaameters type object
        /// </param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Updated teleamtics connection</returns>
        public Connection UpdateTelematicsConnection(ConnectionParameters connectionParams,
            out ResultResponse resultResponse)
        {
            if (connectionParams == null || (connectionParams.ConnectionToken ?? "").Length < 1)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The connection token is not specified"}}
                    }
                };

                return null;
            }

            var result = GetJsonObjectFromAPI<Connection>(
                connectionParams,
                R4MEInfrastructureSettingsV5.TelematicsConnection + "/" + connectionParams.ConnectionToken,
                HttpMethodType.Put,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Update telemetics connection's account
        /// </summary>
        /// <param name="connectionParams">
        ///     The telematics query paramaters
        ///     as ConnectionPaameters type object
        /// </param>
        /// <returns>Updated teleamtics connection</returns>
        public Task<Tuple<Connection, ResultResponse>> UpdateTelematicsConnectionAsync(ConnectionParameters connectionParams)
        {
            if (connectionParams == null || (connectionParams.ConnectionToken ?? "").Length < 1)
            {
                return Task.FromResult(new Tuple<Connection, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The connection token is not specified"}}
                    }
                }));
            }

            return GetJsonObjectFromAPIAsync<Connection>(
                connectionParams,
                R4MEInfrastructureSettingsV5.TelematicsConnection + "/" + connectionParams.ConnectionToken,
                HttpMethodType.Put);
        }

        #endregion

        #endregion

        #region Orders

        /// <summary>
        ///  Display the Archive Orders.
        /// </summary>
        /// <param name="parameters">Request payload</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Archived Orders</returns>
        public ArchiveOrdersResponse ArchiveOrders(ArchiveOrdersParameters parameters, out ResultResponse resultResponse)
        {
            var genParams = new GenericParameters();

            string jsonText = R4MeUtils.SerializeObjectToJson(parameters);

            var httpContent = new StringContent(jsonText, Encoding.UTF8, "application/json");

            var response = GetJsonObjectFromAPI<ArchiveOrdersResponse>
            (genParams, R4MEInfrastructureSettingsV5.OrdersArchive,
                HttpMethodType.Post, httpContent, false, true, out ResultResponse resultResponse2);

            resultResponse = resultResponse2;
            return response;

            //return GetJsonObjectFromAPI<ArchiveOrdersResponse>(parameters,
            //    R4MEInfrastructureSettingsV5.OrdersArchive,
            //    HttpMethodType.Post, out resultResponse);
        }

        /// <summary>
        ///  Display the Archive Orders.
        /// </summary>
        /// <param name="parameters">Request payload</param>
        /// <returns>Archived Orders</returns>
        public Task<Tuple<ArchiveOrdersResponse, ResultResponse>> ArchiveOrdersAsync(ArchiveOrdersParameters parameters)
        {
            var genParams = new GenericParameters();

            string jsonText = R4MeUtils.SerializeObjectToJson(parameters);

            var httpContent = new StringContent(jsonText, Encoding.UTF8, "application/json");

            var response = GetJsonObjectFromAPIAsync<ArchiveOrdersResponse>
            (genParams, R4MEInfrastructureSettingsV5.OrdersArchive,
                HttpMethodType.Post, httpContent, true, false);

            return response;
        }

        /// <summary>
        ///  Display the order history.
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Order history</returns>
        public OrderHistoryResponse GetOrderHistory(OrderHistoryParameters parameters, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<OrderHistoryResponse>(parameters,
                R4MEInfrastructureSettingsV5.OrdersHistory,
                HttpMethodType.Get, false, true, out resultResponse);
        }

        /// <summary>
        ///  Display the order history.
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns>Order history</returns>
        public Task<Tuple<OrderHistoryResponse, ResultResponse>> GetOrderHistoryAsync(OrderHistoryParameters parameters)
        {
            return GetJsonObjectFromAPIAsync<OrderHistoryResponse>(parameters,
                R4MEInfrastructureSettingsV5.OrdersHistory,
                HttpMethodType.Get, null, true, false);
        }

        #endregion

        #region Generic Methods

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

        private T GetJsonObjectFromAPI<T>(GenericParameters optimizationParameters,
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

        private Task<Tuple<T, ResultResponse>> GetJsonObjectFromAPIAsync<T>(GenericParameters optimizationParameters,
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

        private async Task<Tuple<T, ResultResponse>> GetJsonObjectFromAPIAsync<T>(
            GenericParameters optimizationParameters,
            string url,
            HttpMethodType httpMethod,
            HttpContent httpContent,
            bool parseWithNewtonJson,
            bool isString,
            string[] mandatoryFields = null)
            where T : class
        {

            var result = default(T);
            var resultResponse = default(ResultResponse);

            var parametersURI = optimizationParameters.Serialize(_mApiKey);
            var uri = new Uri($"{url}{parametersURI}");

            try
            {
                using (var httpClientHolder =
                    HttpClientHolderManager.AcquireHttpClientHolder(uri.GetLeftPart(UriPartial.Authority)))
                {
                    switch (httpMethod)
                    {
                        case HttpMethodType.Get:
                        {
                            var response = await httpClientHolder.HttpClient.GetStreamAsync(uri.PathAndQuery).ConfigureAwait(false);

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
                                var jsonString = (mandatoryFields?.Length ?? 0) > 0
                                    ? R4MeUtils.SerializeObjectToJson(optimizationParameters, mandatoryFields)
                                    : R4MeUtils.SerializeObjectToJson(optimizationParameters);
                                    content = new StringContent(jsonString);
                                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            }

                            HttpResponseMessage response;
                            if (isPut)
                            {
                                response = await httpClientHolder.HttpClient.PutAsync(uri.PathAndQuery, content).ConfigureAwait(false);
                            }
                            else if (isPatch)
                            {
                                content.Headers.ContentType = new MediaTypeHeaderValue("application/json-patch+json");
                                response = await httpClientHolder.HttpClient.PatchAsync(uri.PathAndQuery, content).ConfigureAwait(false);
                            }
                            else if (isDelete)
                            {
                                var request = new HttpRequestMessage
                                {
                                    Content = content,
                                    Method = HttpMethod.Delete,
                                    RequestUri = new Uri(uri.PathAndQuery, UriKind.Relative)
                                };
                                response = await httpClientHolder.HttpClient.SendAsync(request).ConfigureAwait(false);
                            }
                            else
                            {
                                response = await httpClientHolder.HttpClient.PostAsync(uri.PathAndQuery, content)
                                    .ConfigureAwait(false);
                            }

                            // Check if successful
                            if (response.Content is StreamContent)
                            {
                                var streamTask = await ((StreamContent) response.Content).ReadAsStreamAsync().ConfigureAwait(false);

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

                    resultResponse.Messages.Add("Error", new[] {e.InnerException.Message});
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

                    resultResponse.Messages.Add("InnerException Error", new[] {e.InnerException.Message});
                }

                result = default;
            }

            return new Tuple<T, ResultResponse>(result, resultResponse);
        }


        private T GetJsonObjectFromAPI<T>(GenericParameters optimizationParameters,
            string url,
            HttpMethodType httpMethod,
            HttpContent httpContent,
            bool isString,
            bool parseWithNewtonJson,
            out ResultResponse resultResponse,
            string[] mandatoryFields = null)
            where T : class
        {
            var result = default(T);
            resultResponse = default;

            var parametersUri = optimizationParameters.Serialize(_mApiKey);
            var uri = new Uri($"{url}{parametersUri}");

            try
            {
                using (var httpClientHolder =
                    HttpClientHolderManager.AcquireHttpClientHolder(uri.GetLeftPart(UriPartial.Authority)))
                {
                    switch (httpMethod)
                    {
                        case HttpMethodType.Get:
                        {
                            var response = httpClientHolder.HttpClient.GetStreamAsync(uri.PathAndQuery);
                            response.Wait();

                            if (isString)
                            {
                                result = response.Result.ReadString() as T;
                            }
                            else
                            {
                                result = parseWithNewtonJson
                                    ? response.Result.ReadObjectNew<T>()
                                    : response.Result.ReadObject<T>();
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
                                var jsonString = (mandatoryFields?.Length ?? 0) > 0
                                    ? R4MeUtils.SerializeObjectToJson(optimizationParameters, mandatoryFields)
                                    : R4MeUtils.SerializeObjectToJson(optimizationParameters);
                                content = new StringContent(jsonString);
                                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            }

                            Task<HttpResponseMessage> response;
                            if (isPut)
                            {
                                response = httpClientHolder.HttpClient.PutAsync(uri.PathAndQuery, content);
                            }
                            else if (isPatch)
                            {
                                content.Headers.ContentType = new MediaTypeHeaderValue("application/json-patch+json");
                                response = httpClientHolder.HttpClient.PatchAsync(uri.PathAndQuery, content);
                            }
                            else if (isDelete)
                            {
                                var request = new HttpRequestMessage
                                {
                                    Content = content,
                                    Method = HttpMethod.Delete,
                                    RequestUri = new Uri(uri.PathAndQuery, UriKind.Relative)
                                };
                                response = httpClientHolder.HttpClient.SendAsync(request);
                            }
                            else
                            {
                                var cts = new CancellationTokenSource();
                                cts.CancelAfter(1000 * 60 * 5); // 3 seconds

                                response = httpClientHolder.HttpClient.PostAsync(uri.PathAndQuery, content, cts.Token);
                            }

                            // Wait for response
                            response.Wait();

                            // Check if successful
                            if (response.IsCompleted &&
                                response.Result.IsSuccessStatusCode &&
                                response.Result.Content is StreamContent)
                            {
                                var streamTask = ((StreamContent) response.Result.Content).ReadAsStreamAsync();
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
                            else if (response.IsCompleted &&
                                     response.Result.IsSuccessStatusCode &&
                                     response.Result.Content
                                         .GetType().ToString().ToLower()
                                         .Contains("httpconnectionresponsecontent"))
                            {
                                var streamTask2 = response.Result.Content.ReadAsStreamAsync();
                                streamTask2.Wait();

                                if (streamTask2.IsCompleted)
                                {
                                    var content2 = response.Result.Content;

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
                            }
                            else
                            {
                                Task<Stream> streamTask = null;
                                Task<string> errorMessageContent = null;

                                if (response.Result.Content.GetType() == typeof(StreamContent))
                                    streamTask = ((StreamContent) response.Result.Content).ReadAsStreamAsync();
                                else
                                    errorMessageContent = response.Result.Content.ReadAsStringAsync();

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
                                    var responseStream = response.Result.Content.ReadAsStringAsync();
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

                    resultResponse.Messages.Add("Error", new[] {e.InnerException.Message});
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
                    resultResponse.Messages.Add("InnerException Error", new[] {e.InnerException.Message});
                }

                result = default;
            }

            return result;
        }

        #endregion
    }
}