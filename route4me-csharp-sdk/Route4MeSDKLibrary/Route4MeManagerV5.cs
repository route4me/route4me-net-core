using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.DataTypes.V5.TelematicsPlatform;
using Route4MeSDK.QueryTypes;
using Route4MeSDK.QueryTypes.V5;
using Route4MeSDKLibrary;
using Route4MeSDKLibrary.DataTypes.Internal.Requests;
using Route4MeSDKLibrary.DataTypes.Internal.Response;
using Route4MeSDKLibrary.DataTypes.V5;
using Route4MeSDKLibrary.DataTypes.V5.AddressBookContact;
using Route4MeSDKLibrary.DataTypes.V5.Internal.Requests;
using Route4MeSDKLibrary.DataTypes.V5.Orders;
using Route4MeSDKLibrary.DataTypes.V5.Routes;
using Route4MeSDKLibrary.DataTypes.V5.RouteStatus;
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
        public async Task<Tuple<StatusResponse, ResultResponse, string>> RemoveAddressBookContactsAsync(long[] contactIDs)
        {
            var request = new AddressBookContactsRequest
            {
                AddressIds = contactIDs
            };

            var response = await GetJsonObjectFromAPIAsync<StatusResponse>(request,
                R4MEInfrastructureSettingsV5.ContactsDeleteMultiple,
                HttpMethodType.Delete,
                null,
                false,
                false).ConfigureAwait(false);

            return response;
        }

        /// <summary>
        ///     Remove the address book contacts by areas
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="resultResponse">out: Result response</param>
        /// <returns>If true the contacts were removed successfully</returns>
        public bool RemoveAddressBookContactsByAreas(AddressBookContactsFilter filter, out ResultResponse resultResponse)
        {
            GetJsonObjectFromAPI<StatusResponse>(filter,
                R4MEInfrastructureSettingsV5.ContactsDeleteByAreas,
                HttpMethodType.Delete,
                null,
                false,
                true,
                out resultResponse,
                null,
                true);

            return resultResponse == null;
        }

        /// <summary>
        ///     Remove the address book contacts.
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <returns>If true the contacts were removed successfully</returns>
        public async Task<Tuple<bool, ResultResponse>> RemoveAddressBookContactsByAreasAsync(AddressBookContactsFilter filter)
        {

            var response = await GetJsonObjectFromAPIAsync<StatusResponse>(filter,
                R4MEInfrastructureSettingsV5.ContactsDeleteByAreas,
                HttpMethodType.Delete, null, true, false, null, true).ConfigureAwait(false);

            return new Tuple<bool, ResultResponse>(response.Item1 == null, response.Item2);
        }

        /// <summary>
        ///     Get all Address custom fields.
        /// </summary>
        /// <param name="resultResponse">out: Result response</param>
        /// <returns>All Address custom fields</returns>
        public CustomFieldsResponse GetCustomFields(out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<CustomFieldsResponse>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.ContactsGetCustomFields,
                HttpMethodType.Get,
                out resultResponse);
        }

        /// <summary>
        ///     Get all Address custom fields.
        /// </summary>
        /// <returns>If true the contacts were removed successfully</returns>
        public async Task<Tuple<CustomFieldsResponse, ResultResponse>> GetCustomFieldsAsync()
        {

            var res = await GetJsonObjectFromAPIAsync<CustomFieldsResponse>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.ContactsGetCustomFields,
                HttpMethodType.Get).ConfigureAwait(false);

            return new Tuple<CustomFieldsResponse, ResultResponse>(res.Item1, res.Item2);
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
        public Task<Tuple<AddressBookContactsResponse, ResultResponse, string>> GetAddressBookContactsAsync(AddressBookParameters addressBookParameters)
        {
            return GetJsonObjectFromAPIAsync<AddressBookContactsResponse>(addressBookParameters,
                R4MEInfrastructureSettingsV5.ContactsGetAll,
                HttpMethodType.Get,
                null,
                true,
                false);
        }

        /// <summary>
        ///     Returns address book contacts
        /// </summary>
        /// <param name="addressBookContactsBodyRequest">Request body with filters, limits and so on</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An AddressBookContactsResponse type object</returns>
        public AddressBookContactsResponse GetAddressBookContacts(AddressBookContactsBodyRequest addressBookContactsBodyRequest,
            out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<AddressBookContactsResponse>(addressBookContactsBodyRequest,
                R4MEInfrastructureSettingsV5.ContactsGetAll,
                HttpMethodType.Post,
                out resultResponse);

            return response;
        }

        /// <summary>
        ///     Returns address book contacts
        /// </summary>
        /// <param name="addressBookContactsBodyRequest">Request body with filters, limits and so on</param>
        /// <returns>An AddressBookContactsResponse type object</returns>
        public Task<Tuple<AddressBookContactsResponse, ResultResponse>> GetAddressBookContacts(AddressBookContactsBodyRequest addressBookContactsBodyRequest)
        {
            return GetJsonObjectFromAPIAsync<AddressBookContactsResponse>(addressBookContactsBodyRequest,
                R4MEInfrastructureSettingsV5.ContactsGetAll,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Returns address book contacts
        /// </summary>
        /// <param name="addressBookParametersPaginated">Input parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An AddressBookContactsResponse type object</returns>
        public AddressBookContactsResponse GetAddressBookContactsPaginated(AddressBookParametersPaginated addressBookParametersPaginated,
            out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<AddressBookContactsResponse>(addressBookParametersPaginated,
                R4MEInfrastructureSettingsV5.ContactsGetAllPaginated,
                HttpMethodType.Get,
                out resultResponse);

            return response;
        }

        /// <summary>
        ///     Returns address book contacts
        /// </summary>
        /// <param name="addressBookParametersPaginated">Input parameters</param>
        /// <returns>An AddressBookContactsResponse type object</returns>
        public Task<Tuple<AddressBookContactsResponse, ResultResponse>> GetAddressBookContactsPaginatedAsync(AddressBookParametersPaginated addressBookParametersPaginated)
        {
            return GetJsonObjectFromAPIAsync<AddressBookContactsResponse>(addressBookParametersPaginated,
                R4MEInfrastructureSettingsV5.ContactsGetAllPaginated,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Returns address book contacts
        /// </summary>
        /// <param name="addressBookContactsBodyPaginatedRequest">Input parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An AddressBookContactsResponse type object</returns>
        public AddressBookContactsResponse GetAddressBookContactsPaginated(AddressBookContactsBodyPaginatedRequest addressBookContactsBodyPaginatedRequest,
            out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<AddressBookContactsResponse>(addressBookContactsBodyPaginatedRequest,
                R4MEInfrastructureSettingsV5.ContactsGetAllPaginated,
                HttpMethodType.Post,
                out resultResponse);

            return response;
        }

        /// <summary>
        ///     Returns address book contacts
        /// </summary>
        /// <param name="addressBookContactsBodyPaginatedRequest">Input parameters</param>
        /// <returns>An AddressBookContactsResponse type object</returns>
        public Task<Tuple<AddressBookContactsResponse, ResultResponse>> GetAddressBookContactsPaginatedAsync(AddressBookContactsBodyPaginatedRequest addressBookContactsBodyPaginatedRequest)
        {
            return GetJsonObjectFromAPIAsync<AddressBookContactsResponse>(addressBookContactsBodyPaginatedRequest,
                R4MEInfrastructureSettingsV5.ContactsGetAllPaginated,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Get the Address clusters filtered by the corresponding query text, and with the option to filter the result by the 'routed' and 'unrouted' state.
        /// </summary>
        /// <param name="addressBookParametersClustering">Input parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An AddressBookContactsResponse type object</returns>
        public AddressBookContactsClusteringResponse GetAddressBookContactsClustering(AddressBookParametersClustering addressBookParametersClustering,
            out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<AddressBookContactsClusteringResponse>(addressBookParametersClustering,
                R4MEInfrastructureSettingsV5.ContactsGetClusters,
                HttpMethodType.Get,
                out resultResponse);

            return response;
        }

        /// <summary>
        ///     Returns address book contacts
        /// </summary>
        /// <param name="addressBookParametersClustering">Input parameters</param>
        /// <returns>An AddressBookContactsResponse type object</returns>
        public Task<Tuple<AddressBookContactsClusteringResponse, ResultResponse>> GetAddressBookContactsClusteringAsync(AddressBookParametersClustering addressBookParametersClustering)
        {
            return GetJsonObjectFromAPIAsync<AddressBookContactsClusteringResponse>(addressBookParametersClustering,
                R4MEInfrastructureSettingsV5.ContactsGetClusters,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Get the Address clusters filtered by the corresponding query text, and with the option to filter the result by the 'routed' and 'unrouted' state.
        /// </summary>
        /// <param name="addressBookParametersClusteringBodyRequest">Request body</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An AddressBookContactsResponse type object</returns>
        public AddressBookContactsClusteringResponse GetAddressBookContactsClustering(AddressBookParametersClusteringBodyRequest addressBookParametersClusteringBodyRequest,
            out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<AddressBookContactsClusteringResponse>(addressBookParametersClusteringBodyRequest,
                R4MEInfrastructureSettingsV5.ContactsGetClusters,
                HttpMethodType.Post,
                out resultResponse);

            return response;
        }

        /// <summary>
        ///     Returns address book contacts
        /// </summary>
        /// <param name="addressBookParametersClusteringBodyRequest">Request body</param>
        /// <returns>An AddressBookContactsResponse type object</returns>
        public Task<Tuple<AddressBookContactsClusteringResponse, ResultResponse>> GetAddressBookContactsClusteringAsync(AddressBookParametersClusteringBodyRequest addressBookParametersClusteringBodyRequest)
        {
            return GetJsonObjectFromAPIAsync<AddressBookContactsClusteringResponse>(addressBookParametersClusteringBodyRequest,
                R4MEInfrastructureSettingsV5.ContactsGetClusters,
                HttpMethodType.Post);
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
        ///     Get address book contacts by sending an array of address IDs.
        /// </summary>
        /// <param name="contactIDs">An array of address IDs</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An object containing an array of the address book contacts and result resposne</returns>
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
        ///     Get address book contacts asynchronously by sending an array of address IDs
        /// </summary>
        /// <param name="contactIDs">An array of address IDs</param>
        /// <returns>An object containing an array of the address book contacts and result resposne</returns>
        public Task<Tuple<AddressBookContactsResponse, ResultResponse>> GetAddressBookContactByIdsAsync(long[] contactIDs)
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
        ///     Get depots from address book contacts asynchromously.
        /// </summary>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An object containing an array of the depot-eligible contacts and result resposne</returns>
        public Task<Tuple<AddressBookContact[], ResultResponse, string>> GetDepotsFromAddressBookAsync()
        {
            return GetJsonObjectFromAPIAsync<AddressBookContact[]>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.AddressBookDepots,
                HttpMethodType.Get,
                null,
                false,
                true);
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
        public async Task<Tuple<AddressBookContact, ResultResponse>> AddAddressBookContactAsync(AddressBookContact contactParams)
        {
            contactParams.PrepareForSerialization();

            var result = await GetJsonObjectFromAPIAsync<AddressBookContact>(contactParams,
                R4MEInfrastructureSettingsV5.ContactsAddNew,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<AddressBookContact, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        ///     Update address book contact.
        /// </summary>
        /// <param name="id">ID of the book contract</param>
        /// <param name="contactParams">The contact parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Created address book contact</returns>
        public AddressBookContact UpdateAddressBookContact(long id, AddressBookContact contactParams,
            out ResultResponse resultResponse)
        {
            contactParams.PrepareForSerialization();

            var url = R4MEInfrastructureSettingsV5.ContactsUpdateById.Replace("{address_id}", id.ToString());

            return GetJsonObjectFromAPI<AddressBookContact>(contactParams,
                url,
                HttpMethodType.Put,
                false,
                true,
                out resultResponse);
        }

        /// <summary>
        ///     Update address book contact.
        /// </summary>
        /// <param name="id">ID of the book contract</param>
        /// <param name="contactParams">The contact parameters</param>
        /// <returns>Created address book contact</returns>
        public async Task<Tuple<AddressBookContact, ResultResponse>> UpdateAddressBookContactAsync(long id, AddressBookContact contactParams)
        {
            contactParams.PrepareForSerialization();

            var url = R4MEInfrastructureSettingsV5.ContactsUpdateById.Replace("{address_id}", id.ToString());

            var result = await GetJsonObjectFromAPIAsync<AddressBookContact>(contactParams,
                url,
                HttpMethodType.Put,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<AddressBookContact, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        ///      Batch update for address book contacts.
        /// </summary>
        /// <param name="contactsParams">The contacts parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Created address book contact</returns>
        public AddressBookContact[] BatchUpdateAddressBookContact(AddressBookContactMultiple contactsParams, out ResultResponse resultResponse)
        {
            contactsParams.PrepareForSerialization();

            return GetJsonObjectFromAPI<AddressBookContact[]>(contactsParams,
                R4MEInfrastructureSettingsV5.ContactsUpdateMultiple,
                HttpMethodType.Put,
                false,
                true,
                out resultResponse);
        }

        /// <summary>
        ///     Batch update for address book contacts.
        /// </summary>
        /// <param name="contactsParams">The contacts parameters</param>
        /// <returns>Created address book contact</returns>
        public async Task<Tuple<AddressBookContact[], ResultResponse>> BatchUpdateAddressBookContactAsync(AddressBookContactMultiple contactsParams)
        {
            var result = await GetJsonObjectFromAPIAsync<AddressBookContact[]>(contactsParams,
                R4MEInfrastructureSettingsV5.ContactsUpdateMultiple,
                HttpMethodType.Put,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<AddressBookContact[], ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        ///      Batch update for address book contacts.
        /// </summary>
        /// <param name="request">The contacts parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Created address book contact</returns>
        public bool UpdateAddressBookContactByAreas(UpdateAddressBookContactByAreasRequest request, out ResultResponse resultResponse)
        {
            var response =  GetJsonObjectFromAPI<StatusResponse>(request,
                R4MEInfrastructureSettingsV5.ContactsUpdateByAreas,
                HttpMethodType.Put,
                false,
                true,
                out resultResponse);

            return resultResponse == null;
        }

        /// <summary>
        ///     Batch update for address book contacts.
        /// </summary>
        /// <param name="request">The contacts parameters</param>
        /// <returns>Created address book contact</returns>
        public async Task<Tuple<bool, ResultResponse>> UpdateAddressBookContactByAreas(UpdateAddressBookContactByAreasRequest request)
        {
            var response = await GetJsonObjectFromAPIAsync<StatusResponse>(request,
                R4MEInfrastructureSettingsV5.ContactsUpdateByAreas,
                HttpMethodType.Put,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<bool, ResultResponse>(response.Item1 == null, response.Item2);
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
        public async Task<Tuple<StatusResponse, ResultResponse>> BatchCreateAdressBookContactsAsync(BatchCreatingAddressBookContactsRequest contactParams,
            string[] mandatoryNullableFields)
        {
            contactParams.PrepareForSerialization();

            var result = await GetJsonObjectFromAPIAsync<StatusResponse>(contactParams,
                R4MEInfrastructureSettingsV5.ContactsAddMultiple,
                HttpMethodType.Post,
                null,
                false,
                false,
                mandatoryNullableFields).ConfigureAwait(false);

            return new Tuple<StatusResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        ///     Get the job status of an asynchronous address book contact process. 
        /// </summary>
        /// <param name="jobId">Job ID</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Status response (TO DO: expected result with created multiple contacts)</returns>
        public StatusResponse GetContactsJobStatus(string jobId, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<StatusResponse>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.ContactsGetAsyncJobStatus + "/"+jobId,
                HttpMethodType.Get,
                out resultResponse);
        }

        /// <summary>
        ///     Get the job status of an asynchronous address book contact process asynchronously.
        /// </summary>
        /// <param name="jobId">Job ID</param>
        /// <returns>A Tuple type object containing a job status or/and failure response</returns>
        public Task<Tuple<StatusResponse, ResultResponse>> GetContactsJobStatusAsync(string jobId)
        {
            return GetJsonObjectFromAPIAsync<StatusResponse>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.ContactsGetAsyncJobStatus + "/" + jobId,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Get the job result of an asynchronous address book contact process. 
        /// </summary>
        /// <param name="jobId">Job ID</param>
        /// <param name="resultResponse">Failing response</param>
        public StatusResponse GetContactsJobResult(string jobId, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<StatusResponse>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.ContactsGetAsyncJobResult + "/" + jobId,
                HttpMethodType.Get,
                out resultResponse);
        }

        /// <summary>
        ///     Get the job result of an asynchronous address book contact process asynchronously.
        /// </summary>
        /// <param name="jobId">Job ID</param>
        /// <returns>A Tuple type object containing a job status or/and failure response</returns>
        public Task<Tuple<StatusResponse, ResultResponse>> GetContactsJobResultAsync(string jobId)
        {
            return GetJsonObjectFromAPIAsync<StatusResponse>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.ContactsGetAsyncJobResult + "/" + jobId,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Asynchronously exports the addresses to the account's team file store.
        /// </summary>
        /// <param name="exportParams">Export parameters</param>
        /// <returns>A tuple type object containing:
        /// - a StatusResponse type object in case of success, null - if failure,
        /// - a ResultResponse type object in case of failure, null - if success</returns>
        public Task<Tuple<StatusResponse, ResultResponse>> ExportAddressesAsync(AddressExportParameters exportParams)
        {
            return GetJsonObjectFromAPIAsync<StatusResponse>(exportParams,
                R4MEInfrastructureSettingsV5.ContactsExport,
                HttpMethodType.Post);

        }

        /// <summary>
        ///     Exports the addresses to the account's team file store.
        /// </summary>
        /// <param name="exportParams">Export parameters</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Status of an export operation</returns>
        public StatusResponse ExportAddresses(AddressExportParameters exportParams, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<StatusResponse>(exportParams,
                R4MEInfrastructureSettingsV5.ContactsExport,
                HttpMethodType.Post,
                null,
                false,
                false,
                out resultResponse);
        }

        /// <summary>
        ///     Export the Address Book Contacts located in the selected areas by sending the corresponding body payload.
        /// </summary>
        /// <param name="exportParams">Export parameters</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Status of an export operation</returns>
        public StatusResponse ExportAddressesByAreas(AddressExportByAreasParameters exportParams, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<StatusResponse>(exportParams,
                R4MEInfrastructureSettingsV5.ContactsExportByAreas,
                HttpMethodType.Post,
                null,
                false,
                true,
                out resultResponse,
                null,
                true);
        }

        /// <summary>
        ///     Export the Address Book Contacts located in the selected areas by sending the corresponding body payload.
        /// </summary>
        /// <param name="exportParams">Export parameters</param>
        /// <returns>A tuple type object containing:
        /// - a StatusResponse type object in case of success, null - if failure,
        /// - a ResultResponse type object in case of failure, null - if success</returns>
        public async Task<Tuple<StatusResponse, ResultResponse>> ExportAddressesByAreasAsync(AddressExportByAreasParameters exportParams)
        {
            var result = await GetJsonObjectFromAPIAsync<StatusResponse>(exportParams,
                R4MEInfrastructureSettingsV5.ContactsExportByAreas,
                HttpMethodType.Post,
                null,
                true,
                false,
                null,
                true).ConfigureAwait(false);

            return new Tuple<StatusResponse, ResultResponse>(result.Item1, result.Item2);

        }

        /// <summary>
        ///     Export the Address Book Contacts located in provided area IDs by sending the corresponding body payload.
        /// </summary>
        /// <param name="exportParams">Export parameters</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Status of an export operation</returns>
        public StatusResponse ExportAddressesByAreaIds(AddressExportByAreaIdsParameters exportParams, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<StatusResponse>(exportParams,
                R4MEInfrastructureSettingsV5.ContactsExportByAreaIds,
                HttpMethodType.Post,
                out resultResponse);
        }

        /// <summary>
        ///     Export the Address Book Contacts located in provided area IDs by sending the corresponding body payload.
        /// </summary>
        /// <param name="exportParams">Export parameters</param>
        /// <returns>A tuple type object containing:
        /// - a StatusResponse type object in case of success, null - if failure,
        /// - a ResultResponse type object in case of failure, null - if success</returns>
        public async Task<Tuple<StatusResponse, ResultResponse>> ExportAddressesByAreaIdsAsync(AddressExportByAreaIdsParameters exportParams)
        {
            var result = await GetJsonObjectFromAPIAsync<StatusResponse>(exportParams,
                R4MEInfrastructureSettingsV5.ContactsExportByAreaIds,
                HttpMethodType.Post).ConfigureAwait(false);

            return new Tuple<StatusResponse, ResultResponse>(result.Item1, result.Item2);

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
        /// <param name="conflicts">Conflict resolving rule</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns></returns>
        public ResultResponse BulkCreateTeamMembers(TeamRequest[] membersParams, Conflicts conflicts, out ResultResponse resultResponse)
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
                Users = membersParams,
                Conflicts = conflicts.Description()
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
        /// <param name="conflicts">Conflict resolving rule</param>
        /// <returns></returns>
        public Task<Tuple<ResultResponse, ResultResponse>> BulkCreateTeamMembersAsync(TeamRequest[] membersParams, Conflicts conflicts)
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
                Users = membersParams,
                Conflicts = conflicts.Description()
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
            parameters.PrepareForSerialization();

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
        public async Task<Tuple<DriverReviewsResponse, ResultResponse>> GetDriverReviewListAsync(DriverReviewParameters parameters)
        {
            var result = await GetJsonObjectFromAPIAsync<DriverReviewsResponse>(parameters,
                R4MEInfrastructureSettingsV5.DriverReview,
                HttpMethodType.Get,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<DriverReviewsResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        ///     Get driver review by ID
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Driver review</returns>
        public DriverReviewResponse GetDriverReviewById(DriverReviewParameters parameters,
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

            var result = GetJsonObjectFromAPI<DriverReviewResponse>(parameters,
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
        public async Task<Tuple<DriverReviewResponse, ResultResponse>> GetDriverReviewByIdAsync(DriverReviewParameters parameters)
        {
            if (parameters?.RatingId == null)
            {
                return new Tuple<DriverReviewResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The RatingId parameter is not specified" } }
                    }
                });
            }

            var result = await GetJsonObjectFromAPIAsync<DriverReviewResponse>(parameters,
                R4MEInfrastructureSettingsV5.DriverReview + "/" + parameters.RatingId,
                HttpMethodType.Get,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<DriverReviewResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        ///     Upload driver review to the server
        /// </summary>
        /// <param name="driverReview">Request payload</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Driver review</returns>
        public DriverReviewResponse CreateDriverReview(DriverReview driverReview, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<DriverReviewResponse>(
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
        public Task<Tuple<DriverReviewResponse, ResultResponse>> CreateDriverReviewAsync(DriverReview driverReview)
        {
            return GetJsonObjectFromAPIAsync<DriverReviewResponse>(
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
        public DriverReviewResponse UpdateDriverReview(DriverReview driverReview,
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

            return GetJsonObjectFromAPI<DriverReviewResponse>(
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
        public async Task<Tuple<DriverReviewResponse, ResultResponse>> UpdateDriverReviewAsync(DriverReview driverReview, HttpMethodType method)
        {
            if (method != HttpMethodType.Patch && method != HttpMethodType.Put)
            {
                return new Tuple<DriverReviewResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The parameter method has an incorect value."}}
                    }
                });
            }

            if (driverReview.RatingId == null)
            {
                return new Tuple<DriverReviewResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The parameters doesn't contain parameter RatingId." } }
                    }
                });
            }

            var result = await GetJsonObjectFromAPIAsync<DriverReviewResponse>(
                driverReview,
                R4MEInfrastructureSettingsV5.DriverReview + "/" + driverReview.RatingId,
                method, null, false, false).ConfigureAwait(false);

            return new Tuple<DriverReviewResponse, ResultResponse>(result.Item1, result.Item2);
        }

        #endregion

        #region Routes

        /// <summary>
        /// Retrieves a list of the routes.
        /// </summary>
        /// <param name="routeParameters">Query parameters</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>An array of the routes</returns>
        public DataObjectRoute[] GetRoutes(RouteParametersQuery routeParameters, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute[]>(routeParameters,
                R4MEInfrastructureSettingsV5.Routes,
                HttpMethodType.Get,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Retrieves a list of the routes asynchronously.
        /// </summary>
        /// <param name="routeParameters">Query parameters <see cref="RouteParametersQuery"/></param>
        /// <returns>A Tuple type object containing a route list or/and failure response</returns>
        public async Task<Tuple<DataObjectRoute[], ResultResponse>> GetRoutesAsync(RouteParametersQuery routeParameters)
        {
            var result = await GetJsonObjectFromAPIAsync<DataObjectRoute[]>(routeParameters,
                R4MEInfrastructureSettingsV5.Routes,
                HttpMethodType.Get,
                null, true, false).ConfigureAwait(false);

            return new Tuple<DataObjectRoute[], ResultResponse>(result.Item1, result.Item2);
        }


        public DataObjectRoute GetRouteById(RouteParametersQuery routeParameters, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<DataObjectRoute>(routeParameters,
                R4MEInfrastructureSettingsV5.Routes,
                HttpMethodType.Get,
                false,
                true,
                out resultResponse);
        }


        public async Task<Tuple<DataObjectRoute, ResultResponse>> GetRouteByIdAsync(RouteParametersQuery routeParameters)
        {
            var result = await GetJsonObjectFromAPIAsync<DataObjectRoute>(routeParameters,
                R4MEInfrastructureSettingsV5.Routes,
                HttpMethodType.Get, null, true, false).ConfigureAwait(false);
            return new Tuple<DataObjectRoute, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Retrieves a paginated list of the routes.
        /// </summary>
        /// <param name="routeParameters">Query parameters <see cref="RouteParametersQuery"/></param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>An array of the routes</returns>
        public RoutesResponse GetAllRoutesWithPagination(RouteParametersQuery routeParameters, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<RoutesResponse>(routeParameters,
                R4MEInfrastructureSettingsV5.RoutesPaginate,
                HttpMethodType.Get, null, false, true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Retrieves a paginated list of the routes asynchronously.
        /// </summary>
        /// <param name="routeParameters">Query parameters <see cref="RouteParametersQuery"/></param>
        /// <returns>A Tuple type object containing a route list or/and failure response</returns>
        public async Task<Tuple<RoutesResponse, ResultResponse>> GetAllRoutesWithPaginationAsync(RouteParametersQuery routeParameters)
        {
            var result = await GetJsonObjectFromAPIAsync<RoutesResponse>(routeParameters,
                R4MEInfrastructureSettingsV5.RoutesPaginate,
                HttpMethodType.Get, null, true, false).ConfigureAwait(false);

            return new Tuple<RoutesResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Retrieves a paginated list of the routes without elastic search.
        /// </summary>
        /// <param name="routeParameters">Query parameters <see cref="RouteParametersQuery"/></param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>An array of the routes</returns>
        public RoutesResponse GetPaginatedRouteListWithoutElasticSearch(RouteParametersQuery routeParameters,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<RoutesResponse>(routeParameters,
                R4MEInfrastructureSettingsV5.RoutesFallbackPaginate,
                HttpMethodType.Get, null, false, true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves a paginated list of the routes without elastic search.
        /// </summary>
        /// <param name="routeParameters">Query parameters <see cref="RouteParametersQuery"/></param>
        /// <returns>A Tuple type object containing a route list or/and failure response</returns>
        public async Task<Tuple<RoutesResponse, ResultResponse>> GetPaginatedRouteListWithoutElasticSearchAsync(RouteParametersQuery routeParameters)
        {
            var result = await GetJsonObjectFromAPIAsync<RoutesResponse>(routeParameters,
                R4MEInfrastructureSettingsV5.RoutesFallbackPaginate,
                HttpMethodType.Get, null, true, false).ConfigureAwait(false);

            return new Tuple<RoutesResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Get a route list by filtering.
        /// </summary>
        /// <param name="routeFilterParameters">Query parameters <see cref="RouteParametersQuery"/></param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>An object <see cref="RouteFilterResponse"/></returns>
        public RouteFilterResponse GetRoutesByFilter(RouteFilterParameters routeFilterParameters,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<RouteFilterResponse>(
                routeFilterParameters,
                R4MEInfrastructureSettingsV5.Routes51,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Get a route list by filtering asynchronously.
        /// </summary>
        /// <param name="routeFilterParameters">Route filtering parameters</param>
        /// <returns>A RouteFilterResponse type object<see cref="RouteFilterResponse"/></returns>
        public Task<Tuple<RouteFilterResponse, ResultResponse>> GetRoutesByFilterAsync(RouteFilterParameters routeFilterParameters)
        {
            var result = GetJsonObjectFromAPIAsync<RouteFilterResponse>(
                routeFilterParameters,
                R4MEInfrastructureSettingsV5.Routes51,
                HttpMethodType.Post);

            return result;
        }


        public RoutesResponse GetRouteDataTableWithElasticSearch(
            RouteFilterParameters routeFilterParameters,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<RoutesResponse>(
                routeFilterParameters,
                R4MEInfrastructureSettingsV5.RoutesFallbackDatatable,
                HttpMethodType.Post, null, false, true,
                out resultResponse);

            return result;
        }

        public async Task<Tuple<RoutesResponse, ResultResponse>> GetRouteDataTableWithElasticSearchAsync(RouteFilterParameters routeFilterParameters)
        {
            var result = await GetJsonObjectFromAPIAsync<RoutesResponse>(
                routeFilterParameters,
                R4MEInfrastructureSettingsV5.RoutesFallbackDatatable,
                HttpMethodType.Post).ConfigureAwait(false);

            return new Tuple<RoutesResponse, ResultResponse>(result.Item1, result.Item2);
        }

        public DataObjectRoute[] GetRouteDatatableWithElasticSearch(
            RouteFilterParameters routeFilterParameters,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute[]>(
                routeFilterParameters,
                R4MEInfrastructureSettingsV5.RoutesDatatable,
                HttpMethodType.Post,
                null, false, true,
                out resultResponse);

            return result;
        }

        public async Task<Tuple<DataObjectRoute[], ResultResponse>> GetRouteDatatableWithElasticSearchAsync(
            RouteFilterParameters routeFilterParameters)
        {
            var result = await GetJsonObjectFromAPIAsync<DataObjectRoute[]>(
                routeFilterParameters,
                R4MEInfrastructureSettingsV5.RoutesDatatable,
                HttpMethodType.Post, null, true, false).ConfigureAwait(false);

            return new Tuple<DataObjectRoute[], ResultResponse>(result.Item1, result.Item2);
        }

        public DataObjectRoute[] GetRouteListWithoutElasticSearch(RouteParametersQuery routeParameters,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute[]>(routeParameters,
                R4MEInfrastructureSettingsV5.RoutesFallback,
                HttpMethodType.Get, null, false, true,
                out resultResponse);

            return result;
        }

        public async Task<Tuple<DataObjectRoute[], ResultResponse>> GetRouteListWithoutElasticSearchAsync(RouteParametersQuery routeParameters)
        {
            var result = await GetJsonObjectFromAPIAsync<DataObjectRoute[]>(routeParameters,
                R4MEInfrastructureSettingsV5.RoutesFallback,
                HttpMethodType.Get).ConfigureAwait(false);

            return new Tuple<DataObjectRoute[], ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Makes a copy of the existing route.
        /// </summary>
        /// <param name="routeIDs">An array of route IDs.</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>A RouteDuplicateResponse type object <see cref="RouteDuplicateResponse"/></returns>
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

        /// <summary>
        /// Makes a copy of the existing route asynchronously.
        /// </summary>
        /// <param name="routeIDs">An array of route IDs.</param>
        /// <returns>A Tuple type object containing an array of the duplicated route IDs
        /// or/and failure response</returns>
        public async Task<Tuple<RouteDuplicateResponse, ResultResponse>> DuplicateRouteAsync(string[] routeIDs)
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

            var result = await GetJsonObjectFromAPIAsync<RouteDuplicateResponse>(
                genParams,
                R4MEInfrastructureSettingsV5.RoutesDuplicate,
                HttpMethodType.Post,
                content,
                false,
                false).ConfigureAwait(false);

            return new Tuple<RouteDuplicateResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Removes specified routes from the account.
        /// </summary>
        /// <param name="routeIds">An array of route IDs.</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>A DeleteRoutes type object <see cref="RoutesDeleteResponse"/></returns>
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

        /// <summary>
        /// Removes specified routes from the account asynchronously.
        /// </summary>
        /// <param name="routeIds">An array of route IDs.</param>
        /// <returns>A Tuple type object containing deleted route ID(s) 
        /// or/and failure response</returns>
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
        /// Updates a route by sending route query parameters containg Route Parameters and Addresses.
        /// </summary>
        /// <param name="routeQuery">Route query parameters</param>
        /// <param name="resultResponse"></param>
        /// <returns>Updated route</returns>
        public DataObjectRoute UpdateRoute(RouteParametersQuery routeQuery, out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<DataObjectRoute>(
                routeQuery,
                R4MEInfrastructureSettingsV5.Routes,
                HttpMethodType.Put,
                false,
                true,
                out resultResponse);

            return response;
        }

        /// <summary>
        ///     Updates asynchronously a route by sending route query parameters  
        /// containg Route Parameters and Addresses.
        /// </summary>
        /// <param name="routeQuery">Route query parameters</param>
        /// <returns>A Tuple type object containing updated route or/and failure response</returns>
        public Task<Tuple<DataObjectRoute, ResultResponse, string>> UpdateRouteAsync(RouteParametersQuery routeQuery)
        {
            var response = GetJsonObjectFromAPIAsync<DataObjectRoute>(
                routeQuery,
                R4MEInfrastructureSettingsV5.Routes,
                HttpMethodType.Put,
                null,
                true,
                false);

            return response;
        }

        /// <summary>
        /// Inserts the route breaks.
        /// </summary>
        /// <param name="breaks">Route breaks <see cref="RouteBreaks"/></param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>The status of the inserting process </returns>
        public StatusResponse InsertRouteBreaks(RouteBreaks breaks, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<StatusResponse>(
                breaks,
                R4MEInfrastructureSettingsV5.RouteBreaks,
                HttpMethodType.Post,
                out resultResponse);
        }

        /// <summary>
        /// Inserts the route breaks asyncronously.
        /// </summary>
        /// <param name="breaks">Route breaks <see cref="RouteBreaks"/></param>
        /// <returns>A Tuple type object containing the status of the inserting process
        /// or/and failure response</returns>
        public Task<Tuple<StatusResponse, ResultResponse, string>> InsertRouteBreaksAsync(RouteBreaks breaks)
        {
            return GetJsonObjectFromAPIAsync<StatusResponse>(
                breaks,
                R4MEInfrastructureSettingsV5.RouteBreaks,
                HttpMethodType.Post,
                null,
                false,
                false);
        }

        /// <summary>
        /// Returns found routes for the specified scheduled date and location.
        /// </summary>
        /// <param name="dynamicInsertRequest">Request body parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An array of the found routes with predicted time and distance parameters</returns>
        public DynamicInsertMatchedRoute[] DynamicInsertRouteAddresses(
                                                DynamicInsertRequest dynamicInsertRequest, 
                                                out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<DynamicInsertMatchedRoute[]>(
                dynamicInsertRequest,
                R4MEInfrastructureSettingsV5.RouteAddressDynamicInsert,
                HttpMethodType.Post,
                out resultResponse);
        }

        /// <summary>
        /// Returns found routes for the specified scheduled date and location asynchronously.
        /// </summary>
        /// <param name="dynamicInsertRequest">Request body parameters</param>
        /// <returns>A Tuple type object containing the matched routes 
        /// or/and failure response</returns>
        public Task<Tuple<DynamicInsertMatchedRoute[], ResultResponse, string>> DynamicInsertRouteAddressesAsync(
                                                DynamicInsertRequest dynamicInsertRequest)
        {
            return GetJsonObjectFromAPIAsync<DynamicInsertMatchedRoute[]>(
                dynamicInsertRequest,
                R4MEInfrastructureSettingsV5.RouteAddressDynamicInsert,
                HttpMethodType.Post,
                null,
                false,
                false);
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
                true,
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
        public async Task<Tuple<DataObject, ResultResponse>> RunOptimizationAsync(OptimizationParameters optimizationParameters)
        {
            var result = await GetJsonObjectFromAPIAsync<DataObject>(optimizationParameters,
                R4MEInfrastructureSettings.ApiHost,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<DataObject, ResultResponse>(result.Item1, result.Item2);
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
        ///     Get the paginated list of Vehicles.
        /// </summary>
        /// <param name="vehicleParams">Query params</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An array of the vehicles</returns>
        public Vehicle[] GetVehicles(GetVehicleParameters vehicleParams, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<Vehicle[]>(
                vehicleParams,
                R4MEInfrastructureSettingsV5.Vehicles,
                HttpMethodType.Get,
                false,
                true,
                out resultResponse);
        }

        /// <summary>
        ///     Get the paginated list of Vehicles.
        /// </summary>
        /// <param name="vehicleParams">Query params</param>
        /// <returns>An array of the vehicles</returns>
        public async Task<Tuple<Vehicle[], ResultResponse>> GetVehiclesAsync(GetVehicleParameters vehicleParams)
        {
            var result = await GetJsonObjectFromAPIAsync<Vehicle[]>(
                vehicleParams,
                R4MEInfrastructureSettingsV5.Vehicles,
                HttpMethodType.Get, null, true, false).ConfigureAwait(false);

            return new Tuple<Vehicle[], ResultResponse>(result.Item1, result.Item2);
        }

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
        public VehiclesResponse GetPaginatedVehiclesList(VehicleParameters vehicleParams, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<VehiclesResponse>(
                vehicleParams,
                R4MEInfrastructureSettingsV5.VehiclePaginated,
                HttpMethodType.Get,
                false,
                true,
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
                R4MEInfrastructureSettingsV5.VehiclePaginated,
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
                R4MEInfrastructureSettingsV5.Vehicles + "/" + vehicleId,
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
                R4MEInfrastructureSettingsV5.Vehicles + "/" + vehicleId,
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
        ///     Get vehicle by license plate.
        /// </summary>
        /// <param name="vehicleParams">Vehicle parameter containing vehicle license plate</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Vehicle</returns>
        public VehicleResponse GetVehicleByLicensePlate(string licensePlate,
            out ResultResponse resultResponse)
        {
            if ((licensePlate?.Length ?? 0) < 2)
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

            //var vehicleParams = new VehicleParameters()
            //{
            //    VehicleLicensePlate = licensePlate
            //};

            var gparams = new GenericParameters();
            gparams.ParametersCollection.Add("vehicle_license_plate", licensePlate);

            var result = GetJsonObjectFromAPI<VehicleResponse>(
                gparams,
                R4MEInfrastructureSettingsV5.VehicleLicense,
                HttpMethodType.Get,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get vehicle by license plate asynchronously.
        /// </summary>
        /// <param name="vehicleParams">Vehicle parameter containing vehicle license plate</param>
        /// <returns>Vehicle</returns>
        public Task<Tuple<VehicleResponse, ResultResponse, string>> GetVehicleByLicensePlateAsync(string licensePlate)
        {
            if ((licensePlate?.Length ?? 0) < 2)
            {
                return Task.FromResult(new Tuple<VehicleResponse, ResultResponse, string>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle license plate is not specified"}}
                    }
                },
                null));
            }

            var vehicleParams = new VehicleParameters()
            {
                VehicleLicensePlate = licensePlate
            };

            return GetJsonObjectFromAPIAsync<VehicleResponse>(
                vehicleParams,
                R4MEInfrastructureSettingsV5.VehicleLicense,
                HttpMethodType.Get,
                null,
                true,
                false);
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

            var updateBodyJsonString = R4MeUtils.SerializeObjectToJson(vehicleParams, true);

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
        public Task<Tuple<Vehicle, ResultResponse, string>> UpdateVehicleAsync(Vehicle vehicleParams)
        {
            if (vehicleParams == null || (vehicleParams.VehicleId?.Length ?? 0) != 32)
            {
                return Task.FromResult(new Tuple<Vehicle, ResultResponse, string>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle ID is not specified"}}
                    }
                },
                null));
            }

            var updateBodyJsonString = R4MeUtils.SerializeObjectToJson(vehicleParams, true);

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
        /// Get the vehicles by their state.
        /// </summary>
        /// <param name="vehicleState">Vehicle state. <see cref="VehicleStates" /> </param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An array of the vehicles</returns>
        public Vehicle[] GetVehiclesByState(VehicleStates vehicleState, out ResultResponse resultResponse)
        {
            var vehicleParams = new VehicleParameters()
            {
                Show = vehicleState.Description()
            };

            var result = GetJsonObjectFromAPI<Vehicle[]>(
                vehicleParams,
                R4MEInfrastructureSettingsV5.Vehicles,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Get asynchronously the vehicles by their state.
        /// </summary>
        /// <param name="vehicleState">Vehicle state <see cref="VehicleStates"/></param>
        /// <returns>>A Tuple type object containing a vehicle list or/and failure response</returns>
        public Task<Tuple<Vehicle[], ResultResponse, string>> GetVehiclesByStateAsync(VehicleStates vehicleState)
        {
            var vehicleParams = new GenericParameters();
            vehicleParams.ParametersCollection.Add("show", vehicleState.Description());


            var result = GetJsonObjectFromAPIAsync<Vehicle[]>(
                vehicleParams,
                R4MEInfrastructureSettingsV5.Vehicles,
                HttpMethodType.Get,
                null,
                false,
                false);

            return result;
        }


        #region Bulk Vehicle Operations

        /// <summary>
        /// Activates deactivated vehicles
        /// </summary>
        /// <param name="vehicleIDs">An array of the vehicle IDs</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Activated vehicles</returns>
        public VehiclesResults ActivateVehicles(string[] vehicleIDs, out ResultResponse resultResponse)
        {
            var parameters = new GenericParameters();
            foreach (var vehicleId in vehicleIDs)
            {
                parameters.ParametersCollection.Add("vehicle_ids[]", vehicleId);
            }

            var result = GetJsonObjectFromAPI<VehiclesResults>(
                parameters,
                R4MEInfrastructureSettingsV5.VehicleBulkActivate,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Activates deactivated vehicles asynchronously.
        /// </summary>
        /// <param name="vehicleIDs">An array of the vehicle IDs</param>
        /// <returns>A Tuple type object containing a vehicle list or/and failure response</returns>
        public Task<Tuple<VehiclesResults, ResultResponse, string>> ActivateVehiclesAsync(string[] vehicleIDs)
        {
            var parameters = new GenericParameters();

            foreach (var vehicleId in vehicleIDs)
            {
                parameters.ParametersCollection.Add("vehicle_ids[]", vehicleId);
            }

            return GetJsonObjectFromAPIAsync<VehiclesResults>(
                parameters,
                R4MEInfrastructureSettingsV5.VehicleBulkActivate,
                HttpMethodType.Post,
                null,
                false,
                false);
        }

        /// <summary>
        ///     Deactivates active vehicles
        /// </summary>
        /// <param name="vehicleIDs">An array of the vehicle IDs</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Deactivated vehicles</returns>
        public Vehicle[] DeactivateVehicles(string[] vehicleIDs, out ResultResponse resultResponse)
        {
            var parameters = new GenericParameters();
            foreach (var vehicleId in vehicleIDs)
            {
                parameters.ParametersCollection.Add("vehicle_ids[]", vehicleId);
            }

            var result = GetJsonObjectFromAPI<Vehicle[]>(
                parameters,
                R4MEInfrastructureSettingsV5.VehicleBulkDeactivate,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Deactivates active vehicles asynchronously.
        /// </summary>
        /// <param name="vehicleIDs">An array of the vehicle IDs</param>
        /// <returns>A Tuple type object containing a vehicle list or/and failure response</returns>
        public Task<Tuple<Vehicle[], ResultResponse, string>> DeactivateVehiclesAsync(string[] vehicleIDs)
        {
            var parameters = new GenericParameters();
            foreach (var vehicleId in vehicleIDs)
            {
                parameters.ParametersCollection.Add("vehicle_ids[]", vehicleId);
            }

            return GetJsonObjectFromAPIAsync<Vehicle[]>(
                parameters,
                R4MEInfrastructureSettingsV5.VehicleBulkDeactivate,
                HttpMethodType.Post,
                null,
                false,
                false);
        }

        /// <summary>
        ///     Updates multiple vehicles at once
        /// </summary>
        /// <param name="vehicleArray">An array of the vehicles</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Status response</returns>
        public StatusResponse UpdateVehicles(Vehicles vehicleArray, out ResultResponse resultResponse)
        {

            var result = GetJsonObjectFromAPI<StatusResponse>(
                vehicleArray,
                R4MEInfrastructureSettingsV5.VehicleBulkUpdate,
                HttpMethodType.Post,
                null,
                false,
                false,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Updates multiple vehicles at once asynchronously.
        /// </summary>
        /// <param name="vehicleArray">An object containing an array of the vehicles</param>
        /// <returns>Returns a result as a tuple object:
        /// - object: if success, this items is not null
        /// - ResultResponse: if failed, this items is not null
        /// - string: if success, this items is a job ID</returns>
        public Task<Tuple<object, ResultResponse, string>> UpdateVehiclesAsync(Vehicles vehicleArray)
        {

            var result = GetJsonObjectAndJobFromAPIAsync<object>(
                vehicleArray,
                R4MEInfrastructureSettingsV5.VehicleBulkUpdate,
                HttpMethodType.Post);

            return result;
        }

        /// <summary>
        /// Removes multiple vehicles at once.
        /// </summary>
        /// <param name="vehicleIDs">An array of the vehicle IDs</param>
        /// <returns>Status of the update operation</returns>
        public StatusResponse DeleteVehicles(string[] vehicleIDs, out ResultResponse resultResponse)
        {
            var parameters = new GenericParameters();
            foreach (var vehicleId in vehicleIDs)
            {
                parameters.ParametersCollection.Add("vehicle_ids[]", vehicleId);
            }

            return GetJsonObjectFromAPI<StatusResponse>(
                parameters,
                R4MEInfrastructureSettingsV5.VehicleBulkDelete,
                HttpMethodType.Post,
                null,
                false,
                false,
                out resultResponse);
        }

        /// <summary>
        /// Removes multiple vehicles at once asynchronously.
        /// </summary>
        /// <param name="vehicleIDs">An array of the vehicle IDs</param>
        /// <returns>Returns a result as a tuple object:
        /// - Status response: if success, this items is not null
        /// - ResultResponse: if failed, this items is not null
        /// - string: if success, this items is a job ID</returns>
        public Task<Tuple<StatusResponse, ResultResponse, string>> DeleteVehiclesAsync(string[] vehicleIDs)
        {
            var parameters = new GenericParameters();
            foreach (var vehicleId in vehicleIDs)
            {
                parameters.ParametersCollection.Add("vehicle_ids[]", vehicleId);
            }

            var result = GetJsonObjectAndJobFromAPIAsync<StatusResponse>(
                parameters,
                R4MEInfrastructureSettingsV5.VehicleBulkDelete,
                HttpMethodType.Post);

            return result;
        }

        /// <summary>
        /// Restore multiple vehicles at once.
        /// </summary>
        /// <param name="vehicleIDs">An array of the vehicle IDs</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>StatusResponse: if success, this items is not null
        /// ResultResponse: if failed, this items is not null
        /// </returns>
        public StatusResponse RestoreVehicles(string[] vehicleIDs, out ResultResponse resultResponse)
        {
            var parameters = new GenericParameters();
            foreach (var vehicleId in vehicleIDs)
            {
                parameters.ParametersCollection.Add("vehicle_ids[]", vehicleId);
            }

            return GetJsonObjectFromAPI<StatusResponse>(
                parameters,
                R4MEInfrastructureSettingsV5.VehicleBulkRestore,
                HttpMethodType.Post,
                null,
                false,
                false,
                out resultResponse);
        }

        /// <summary>
        /// Restore multiple vehicles at once asynchronously.
        /// </summary>
        /// <param name="vehicleIDs">An array of the vehicle IDs</param>
        /// <returns>Returns a result as a tuple object:
        /// - StatusResponse: if success, this items is not null
        /// - ResultResponse: if failed, this items is not null
        /// - string: if success, this items is a job ID</returns>
        public Task<Tuple<StatusResponse, ResultResponse, string>> RestoreVehiclesAsync(string[] vehicleIDs)
        {
            var parameters = new GenericParameters();
            foreach (var vehicleId in vehicleIDs)
            {
                parameters.ParametersCollection.Add("vehicle_ids[]", vehicleId);
            }

            var result = GetJsonObjectAndJobFromAPIAsync<StatusResponse>(
                parameters,
                R4MEInfrastructureSettingsV5.VehicleBulkRestore,
                HttpMethodType.Post);

            return result;
        }

        /// <summary>
        /// Returns vehicle job result.
        /// </summary>
        /// <param name="JobId">Job ID</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>The object containing status of the JOB</returns>
        public StatusResponse GetVehicleJobResult(string JobId, out ResultResponse resultResponse)
        {
            var emptyParams = new GenericParameters();

            var result = GetJsonObjectFromAPI<StatusResponse>(
                emptyParams,
                R4MEInfrastructureSettingsV5.VehicleJobResult+"/"+ JobId,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves a vehicle job result.
        /// </summary>
        /// <param name="JobId">Job ID</param>
        /// <returns>Returns a result as a tuple object:
        /// - StatusResponse: if success, this items is not null
        /// - ResultResponse: if failed, this items is not null
        /// - string: if success, this items is a job ID</returns>
        public Task<Tuple<StatusResponse, ResultResponse, string>> GetVehicleJobResultAsync(string JobId)
        {
            var emptyParams = new GenericParameters();

            var result = GetJsonObjectFromAPIAsync<StatusResponse>(
                emptyParams,
                R4MEInfrastructureSettingsV5.VehicleJobResult + "/" + JobId,
                HttpMethodType.Get,
                null,
                false,
                false);

            return result;
        }

        /// <summary>
        /// Returns vehicle job status.
        /// </summary>
        /// <param name="jobId">Job ID</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>The object containing status of the JOB</returns>
        public StatusResponse GetVehicleJobStatus(string jobId, out ResultResponse resultResponse)
        {
            var emptyParams = new GenericParameters();

            var result = GetJsonObjectFromAPI<StatusResponse>(
                emptyParams,
                R4MEInfrastructureSettingsV5.VehicleJobStatus + "/" + jobId,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves a vehicle job status.
        /// </summary>
        /// <param name="jobId">Job ID</param>
        /// <returns>Returns a job status </returns>
        public async Task<Tuple<StatusResponse, ResultResponse>> GetVehicleJobStatusAsync(string jobId)
        {
            var emptyParams = new GenericParameters();

            var result = await GetJsonObjectFromAPIAsync<StatusResponse>(
                emptyParams,
                R4MEInfrastructureSettingsV5.VehicleJobStatus + "/" + jobId,
                HttpMethodType.Get,
                null,
                false,
                false);

            return new Tuple<StatusResponse, ResultResponse>(result.Item1, result.Item2);
        }

        #endregion

        #region Vehicle Tracking

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
        ///  Get latest vehicle locations by specified vehicle IDs.
        /// </summary>
        /// <param name="vehicleIDs">An array of the vehicle IDs</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>A VehicleLocationResponse type object <see cref="VehicleLocationResponse"/></returns>
        public VehicleLocationResponse GetVehicleLocations(string[] vehicleIDs,
            out ResultResponse resultResponse)
        {
            var parameters = new GenericParameters();
            foreach (var vehicleId in vehicleIDs)
            {
                parameters.ParametersCollection.Add("ids[]", vehicleId);
            }

            var result = GetJsonObjectFromAPI<VehicleLocationResponse>(
                parameters,
                R4MEInfrastructureSettingsV5.VehicleLocation,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Asynchronously get latest vehicle locations by specified vehicle IDs.
        /// </summary>
        /// <param name="vehicleIDs">An array of the vehicle IDs</param>
        /// <returns>Returns a result as a tuple object:
        /// - VehicleLocationResponse: if success, this items is not null
        /// - ResultResponse: if failed, this items is not null
        /// - string: if success, this items is a job ID</returns>
        public Task<Tuple<VehicleLocationResponse, ResultResponse, string>> GetVehicleLocationsAsync(string[] vehicleIDs)
        {
            var parameters = new GenericParameters();
            foreach (var vehicleId in vehicleIDs)
            {
                parameters.ParametersCollection.Add("ids[]", vehicleId);
            }

            return GetJsonObjectFromAPIAsync<VehicleLocationResponse>(
                parameters,
                R4MEInfrastructureSettingsV5.VehicleLocation,
                HttpMethodType.Get,
                null,
                false,
                false);
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
        ///     Get the Vehicle track by specifying vehicle ID.
        /// </summary>
        /// <param name="vehicleId">Vehicle ID</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Vehicle track object</returns>
        public VehicleTrackResponse GetVehicleTrack(VehicleParameters vehicleParameters, out ResultResponse resultResponse)
        {
            var newParams = new VehicleParameters()
            {
                Start = vehicleParameters.Start,
                End = vehicleParameters.End
            };
             
            var result = GetJsonObjectFromAPI<VehicleTrackResponse>(
                newParams,
                R4MEInfrastructureSettingsV5.Vehicles + "/" + vehicleParameters.VehicleId + "/" + "track",
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get the Vehicle track by specifying vehicle ID.
        /// </summary>
        /// <param name="vehicleId">Vehicle ID</param>
        /// <returns>Vehicle track object</returns>
        public Task<Tuple<VehicleTrackResponse, ResultResponse>> GetVehicleTrackAsync(VehicleParameters vehicleParameters)
        {
            var newParams = new VehicleParameters()
            {
                Start = vehicleParameters.Start,
                End = vehicleParameters.End
            };

            return GetJsonObjectFromAPIAsync<VehicleTrackResponse>(
                newParams,
                R4MEInfrastructureSettingsV5.Vehicles + "/" + vehicleParameters.VehicleId + "/" + "track",
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Search vehicles by sending request body.
        /// </summary>
        /// <param name="searchParams">Search parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An array of the found vehicles</returns>
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
        ///     asynchronously search vehicles by sending request body.
        /// </summary>
        /// <param name="searchParams">Search parameters</param>
        /// <returns>Returns a result as a tuple object:
        /// - Vehicle[]: if success, this item is not null
        /// - ResultResponse: if failed, this item is not null
        /// - string: if success, this item is a job ID</returns>
        public Task<Tuple<Vehicle[], ResultResponse, string>> SearchVehiclesAsync(VehicleSearchParameters searchParams)
        {
            return GetJsonObjectFromAPIAsync<Vehicle[]>(
                searchParams,
                R4MEInfrastructureSettingsV5.VehicleSearch,
                HttpMethodType.Post,
                null,
                false,
                false);
        }

        /// <summary>
        /// Sync the Vehicle by sending the corresponding body payload.
        /// </summary>
        /// <param name="syncParams">Query parameters <see cref="VehicleTelematicsSync"/></param>
        /// <param name="resultResponse"></param>
        /// <returns>Synchronized vehicle</returns>
        public Vehicle SyncPendingTelematicsData(VehicleTelematicsSync syncParams, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<Vehicle>(
                syncParams,
                R4MEInfrastructureSettingsV5.VehicleSyncTelematics,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }


        public Task<Tuple<Vehicle, ResultResponse, string>> SyncPendingTelematicsDataAsync(VehicleTelematicsSync syncParams)
        {
            var result = GetJsonObjectFromAPIAsync<Vehicle>(
                syncParams,
                R4MEInfrastructureSettingsV5.VehicleSyncTelematics,
                HttpMethodType.Post,
                null,
                false,
                false);

            return result;
        }

        #endregion


        #region Vehicle Profiles

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
        public Task<Tuple<VehicleProfile, ResultResponse, string>> CreateVehicleProfileAsync(VehicleProfile vehicleProfileParams)
        {
            return GetJsonObjectFromAPIAsync<VehicleProfile>(
                vehicleProfileParams,
                R4MEInfrastructureSettingsV5.VehicleProfiles,
                HttpMethodType.Post,
                null,
                true,
                false);
        }

        /// <summary>
        ///     Remove a vehicle profile from database.
        ///     TO DO: adjust response structure.
        /// </summary>
        /// <param name="vehicleProfileParams">Vehicle profile parameter containing a vehicle profile ID </param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Removed vehicle profile</returns>
        public VehicleProfile DeleteVehicleProfile(VehicleProfileParameters vehicleProfileParams,
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

            var result = GetJsonObjectFromAPI<VehicleProfile>(new GenericParameters(),
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
        public Task<Tuple<VehicleProfile, ResultResponse>> GetVehicleProfileByIdAsync(VehicleProfileParameters vehicleProfileParams)
        {
            return GetJsonObjectFromAPIAsync<VehicleProfile>(
                new VehicleParameters(),
                R4MEInfrastructureSettingsV5.VehicleProfiles + "/" + vehicleProfileParams.VehicleProfileId,
                HttpMethodType.Get);
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

            var updateBodyJsonString = R4MeUtils.SerializeObjectToJson(profileParams, true);

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
        public Task<Tuple<VehicleProfile, ResultResponse, string>> UpdateVehicleProfileAsync(VehicleProfile profileParams)
        {
            if (profileParams == null || (profileParams.VehicleProfileId ?? 0) < 1)
            {
                return Task.FromResult(new Tuple<VehicleProfile, ResultResponse, string>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle profile ID is not specified"}}
                    }
                },
                null));
            }

            var updateBodyJsonString = R4MeUtils.SerializeObjectToJson(profileParams, true);

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


        #region Vehicle Capacity Profile

        /// <summary>
        ///     Create a vehicle capacity profile.
        /// </summary>
        /// <param name="vehicleCapacityProfile">Vehicle capacity profile body parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Created vehicle capacity profile</returns>
        public VehicleCapacityProfileResponse CreateVehicleCapacityProfile(VehicleCapacityProfile vehicleCapacityProfile,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<VehicleCapacityProfileResponse>(
                vehicleCapacityProfile,
                R4MEInfrastructureSettingsV5.VehicleCapacityProfiles,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Create a vehicle capacity profile asynchronously.
        /// </summary>
        /// <param name="vehicleCapacityProfile">Vehicle capacity profile body parameters</param>
        /// <returns>A tuple type object with a created vehicle capacity profile
        /// or/and failure response</returns>
        public Task<Tuple<VehicleCapacityProfileResponse, ResultResponse>> CreateVehicleCapacityProfileAsync(VehicleCapacityProfile vehicleCapacityProfile)
        {
            return GetJsonObjectFromAPIAsync<VehicleCapacityProfileResponse>(
                vehicleCapacityProfile,
                R4MEInfrastructureSettingsV5.VehicleCapacityProfiles,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Remove a vehicle capacity profile from database.
        /// </summary>
        /// <param name="vehicleCapacityProfileParams">Vehicle capacity profile parameter containing a vehicle capacity profile ID </param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Removed vehicle capacity profile</returns>
        public VehicleCapacityProfileResponse DeleteVehicleCapacityProfile(VehicleCapacityProfileParameters vehicleCapacityProfileParams,
            out ResultResponse resultResponse)
        {
            if (vehicleCapacityProfileParams == null || 
                vehicleCapacityProfileParams.VehicleCapacityProfileId == null || 
                vehicleCapacityProfileParams.VehicleCapacityProfileId == 0)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle capacity profile ID is not specified"}}
                    }
                };

                return null;
            }

            var result = GetJsonObjectFromAPI<VehicleCapacityProfileResponse>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.VehicleCapacityProfiles + "/" + vehicleCapacityProfileParams.VehicleCapacityProfileId,
                HttpMethodType.Delete,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Remove a vehicle capacity profile from database asynchronously.
        /// </summary>
        /// <param name="vehicleCapacityProfileParams">Vehicle capacity profile parameter containing a vehicle capacity profile ID</param>
        /// <returns>A tuple type object with a deleted vehicle capacity profile
        /// or/and failure response</returns>
        public Task<Tuple<VehicleCapacityProfileResponse, ResultResponse>> DeleteVehicleCapacityProfileAsync(VehicleCapacityProfileParameters vehicleCapacityProfileParams)
        {
            if (vehicleCapacityProfileParams == null ||
                vehicleCapacityProfileParams.VehicleCapacityProfileId == null ||
                vehicleCapacityProfileParams.VehicleCapacityProfileId == 0)
            {
                return Task.FromResult(new Tuple<VehicleCapacityProfileResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"he vehicle capacity profile ID is not specified"}}
                    }
                }));
            }

            return GetJsonObjectFromAPIAsync<VehicleCapacityProfileResponse>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.VehicleCapacityProfiles + "/" + vehicleCapacityProfileParams.VehicleCapacityProfileId,
                HttpMethodType.Delete);
        }

        /// <summary>
        ///     Get paginated list of the vehicle capacity profiles.
        /// </summary>
        /// <param name="profileParams">Vehicle capacity profile request parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>The data including list of the vehicle profiles.</returns>
        public VehicleCapacityProfilesResponse GetVehicleCapacityProfiles(VehicleCapacityProfileParameters capacityProfileParams,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<VehicleCapacityProfilesResponse>(
                capacityProfileParams,
                R4MEInfrastructureSettingsV5.VehicleCapacityProfiles,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Get paginated list of the vehicle capacity profiles asynchronously.
        /// </summary>
        /// <param name="capacityProfileParams">Vehicle capacity profile request parameters</param>
        /// <returns>A tuple type object with the retrieved vehicle capacity profiles
        /// or/and failure response</returns>
        public Task<Tuple<VehicleCapacityProfilesResponse, ResultResponse>> GetVehicleCapacityProfilesAsync(VehicleCapacityProfileParameters capacityProfileParams)
        {
            return GetJsonObjectFromAPIAsync<VehicleCapacityProfilesResponse>(
                capacityProfileParams,
                R4MEInfrastructureSettingsV5.VehicleCapacityProfiles,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Get vehicle capacity profile by ID.
        /// </summary>
        /// <param name="vehicleCapacityProfileParams">Vehicle capacity profile parameter containing a vehicle capacity profile ID </param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Vehicle capacity profile</returns>
        public VehicleCapacityProfileResponse GetVehicleCapacityProfileById(VehicleCapacityProfileParameters vehicleCapacityProfileParams,
            out ResultResponse resultResponse)
        {
            if (vehicleCapacityProfileParams == null || 
                vehicleCapacityProfileParams.VehicleCapacityProfileId == null || 
                vehicleCapacityProfileParams.VehicleCapacityProfileId == 0)
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

            var result = GetJsonObjectFromAPI<VehicleCapacityProfileResponse>(
                new VehicleCapacityProfileParameters(),
                R4MEInfrastructureSettingsV5.VehicleCapacityProfiles + "/" + vehicleCapacityProfileParams.VehicleCapacityProfileId,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get vehicle capacity profile by ID asynchronously.
        /// </summary>
        /// <param name="vehicleCapacityProfileParams">Vehicle capacity profile parameter 
        /// containing a vehicle capacity profile ID</param>
        /// <returns>A tuple type object with a retrieved vehicle capacity profile
        /// or/and failure response</returns>
        public Task<Tuple<VehicleCapacityProfileResponse, ResultResponse>> GetVehicleCapacityProfileByIdAsync(VehicleCapacityProfileParameters vehicleCapacityProfileParams)
        {
            var result = GetJsonObjectFromAPIAsync<VehicleCapacityProfileResponse>(
                new VehicleCapacityProfileParameters(),
                R4MEInfrastructureSettingsV5.VehicleCapacityProfiles + "/" + vehicleCapacityProfileParams.VehicleCapacityProfileId,
                HttpMethodType.Get);

            return result;
        }

        /// <summary>
        ///     Update a vehicle capacity profile.
        /// </summary>
        /// <param name="profileParams">Vehicle capacity profile object as body payload</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Updated vehicle capacity profile</returns>
        public VehicleCapacityProfileResponse UpdateVehicleCapacityProfile(VehicleCapacityProfile capacityProfileParams, out ResultResponse resultResponse)
        {
            if (capacityProfileParams == null || (capacityProfileParams.VehicleCapacityProfileId ?? 0) < 1)
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

            var capProfId = capacityProfileParams.VehicleCapacityProfileId;

            capacityProfileParams.VehicleCapacityProfileId = null;

            var updateBodyJsonString = R4MeUtils.SerializeObjectToJson(capacityProfileParams, true);

            var content = new StringContent(updateBodyJsonString, Encoding.UTF8, "application/json");

            var genParams = new RouteParametersQuery();

            var result = GetJsonObjectFromAPI<VehicleCapacityProfileResponse>(
                genParams,
                R4MEInfrastructureSettingsV5.VehicleCapacityProfiles + "/" + capProfId,
                HttpMethodType.Patch,
                content,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Update a vehicle capacity profile asynchronously.
        /// </summary>
        /// <param name="capacityProfileParams">Vehicle capacity profile object as body payload</param>
        /// <returns>A tuple type object with updated vehicle capacity profile
        /// or/and failure response</returns>
        public Task<Tuple<VehicleCapacityProfileResponse, ResultResponse, string>> UpdateVehicleCapacityProfileAsync(VehicleCapacityProfile capacityProfileParams)
        {
            if (capacityProfileParams == null || (capacityProfileParams.VehicleCapacityProfileId ?? 0) < 1)
            {
                var resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle capacity profile ID is not specified"}}
                    }
                };

                return Task.FromResult(new Tuple<VehicleCapacityProfileResponse, ResultResponse, string>(
                    null, 
                    resultResponse,
                    null));
            }

            var capProfId = capacityProfileParams.VehicleCapacityProfileId;

            capacityProfileParams.VehicleCapacityProfileId = null;

            var updateBodyJsonString = R4MeUtils.SerializeObjectToJson(capacityProfileParams, true);

            var content = new StringContent(updateBodyJsonString, Encoding.UTF8, "application/json");

            var genParams = new RouteParametersQuery();

            return GetJsonObjectFromAPIAsync<VehicleCapacityProfileResponse>(
                genParams,
                R4MEInfrastructureSettingsV5.VehicleCapacityProfiles + "/" + capProfId,
                HttpMethodType.Patch,
                content,
                false,
                false);
        }

        #endregion

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
            return GetJsonObjectFromAPI<ArchiveOrdersResponse>(parameters,
                R4MEInfrastructureSettingsV5.OrdersArchive,
                HttpMethodType.Post,
                null,
                false,
                true,
                out resultResponse);
        }

        /// <summary>
        ///  Display the Archive Orders.
        /// </summary>
        /// <param name="parameters">Request payload</param>
        /// <returns>Archived Orders</returns>
        public Task<Tuple<ArchiveOrdersResponse, ResultResponse, string>> ArchiveOrdersAsync(ArchiveOrdersParameters parameters)
        {
            return GetJsonObjectFromAPIAsync<ArchiveOrdersResponse>(parameters,
                R4MEInfrastructureSettingsV5.OrdersArchive,
                HttpMethodType.Post,
                null,
                true,
                false);
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
                HttpMethodType.Get, 
                false, 
                true, 
                out resultResponse);
        }

        /// <summary>
        ///  Display the order history.
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns>Order history</returns>
        public async Task<Tuple<OrderHistoryResponse, ResultResponse>> GetOrderHistoryAsync(OrderHistoryParameters parameters)
        {
            var result = await GetJsonObjectFromAPIAsync<OrderHistoryResponse>(parameters,
                R4MEInfrastructureSettingsV5.OrdersHistory,
                HttpMethodType.Get, null, true, false).ConfigureAwait(false);

            return new Tuple<OrderHistoryResponse, ResultResponse>(result.Item1, result.Item2);
        }

        #endregion

        #region Route Status

        /// <summary>
        /// Get the status by specifying the path parameter ID.
        /// </summary>
        /// <param name="routeId">ID of the route</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns></returns>
        public RouteStatusResponse GetRouteStatus(string routeId, out ResultResponse resultResponse)
        {
            var url = R4MEInfrastructureSettingsV5.RouteStatus.Replace("{routeId}", routeId);

            return GetJsonObjectFromAPI<RouteStatusResponse>(new GenericParameters(),
                url,
                HttpMethodType.Get,
                out resultResponse);
        }

        /// <summary>
        /// Get the status by specifying the path parameter ID.
        /// </summary>
        /// <param name="routeId">ID of the route</param>
        /// <returns></returns>
        public Task<Tuple<RouteStatusResponse, ResultResponse>> GetRouteStatusAsync(string routeId)
        {
            var url = R4MEInfrastructureSettingsV5.RouteStatus.Replace("{routeId}", routeId);

            return GetJsonObjectFromAPIAsync<RouteStatusResponse>(new GenericParameters(),
                url,
                HttpMethodType.Get);
        }

        /// <summary>
        /// Update the status by specifying the path parameter ID.Route statuses change only forward - planned > started/paused > completed
        /// </summary>
        /// <param name="routeId">ID of the route</param>
        /// <param name="request">Request body</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns></returns>
        public RouteStatusResponse UpdateRouteStatus(string routeId, RouteStatusRequest request, out ResultResponse resultResponse)
        {
            var url = R4MEInfrastructureSettingsV5.RouteStatus.Replace("{routeId}", routeId);

            return GetJsonObjectFromAPI<RouteStatusResponse>(request,
                url,
                HttpMethodType.Post,
                out resultResponse);
        }

        /// <summary>
        /// Update the status by specifying the path parameter ID.Route statuses change only forward - planned > started/paused > completed
        /// </summary>
        /// <param name="routeId">ID of the route</param>
        /// <param name="request">Request body</param>
        /// <returns></returns>
        public Task<Tuple<RouteStatusResponse, ResultResponse>> UpdateRouteStatusAsync(string routeId, RouteStatusRequest request)
        {
            var url = R4MEInfrastructureSettingsV5.RouteStatus.Replace("{routeId}", routeId);

            return GetJsonObjectFromAPIAsync<RouteStatusResponse>(request,
                url,
                HttpMethodType.Post);
        }

        /// <summary>
        /// Get route status history by specifying the path parameter ID.
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns></returns>
        public RouteStatusHistoryResponse GetRouteStatusHistory(RouteStatusHistoryParameters parameters, out ResultResponse resultResponse)
        {
            var url = R4MEInfrastructureSettingsV5.RouteStatusHistory.Replace("{routeId}", parameters.RouteId);

            return GetJsonObjectFromAPI<RouteStatusHistoryResponse>(parameters,
                url,
                HttpMethodType.Get,
                out resultResponse);
        }

        /// <summary>
        /// Get route status history by specifying the path parameter ID.
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <returns></returns>
        public Task <Tuple<RouteStatusHistoryResponse, ResultResponse>> GetRouteStatusHistoryAsync(RouteStatusHistoryParameters parameters)
        {
            var url = R4MEInfrastructureSettingsV5.RouteStatusHistory.Replace("{routeId}", parameters.RouteId);

            return GetJsonObjectFromAPIAsync<RouteStatusHistoryResponse>(parameters,
                url,
                HttpMethodType.Get);
        }

        /// <summary>
        /// Roll back route status by specifying the path parameter ID. Sometimes a status rollback is possible.
        /// </summary>
        /// <param name="routeId">Route ID</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns></returns>
        public RouteStatusResponse RollbackRouteStatus(string routeId, out ResultResponse resultResponse)
        {
            var url = R4MEInfrastructureSettingsV5.RollbackRouteStatus.Replace("{routeId}", routeId);

            return GetJsonObjectFromAPI<RouteStatusResponse>(new GenericParameters(),
                url,
                HttpMethodType.Get,
                out resultResponse);
        }

        /// <summary>
        /// Roll back route status by specifying the path parameter ID. Sometimes a status rollback is possible.
        /// </summary>
        /// <param name="routeId">Route ID</param>
        /// <returns></returns>
        public Task<Tuple<RouteStatusResponse, ResultResponse>> RollbackRouteStatusAsync(string routeId)
        {
            var url = R4MEInfrastructureSettingsV5.RollbackRouteStatus.Replace("{routeId}", routeId);

            return GetJsonObjectFromAPIAsync<RouteStatusResponse>(new GenericParameters(),
                url,
                HttpMethodType.Get);
        }

        /// <summary>
        /// Store a new Status in the database with planned status.
        /// </summary>
        /// <param name="parameters">Route IDs</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns></returns>
        public UpdateRouteStatusAsPlannedResponse UpdateRouteStatusAsPlanned(UpdateRouteStatusAsPlannedParameters parameters, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<UpdateRouteStatusAsPlannedResponse>(parameters,
                R4MEInfrastructureSettingsV5.PlannedRouteStatus,
                HttpMethodType.Post,
                out resultResponse);
        }

        /// <summary>
        /// Store a new Status in the database with planned status.
        /// </summary>
        /// <param name="parameters">Route IDs</param>
        /// <returns></returns>
        public Task<Tuple<UpdateRouteStatusAsPlannedResponse, ResultResponse>> UpdateRouteStatusAsPlanned(UpdateRouteStatusAsPlannedParameters parameters)
        {
            return GetJsonObjectFromAPIAsync<UpdateRouteStatusAsPlannedResponse>(parameters,
                R4MEInfrastructureSettingsV5.PlannedRouteStatus,
                HttpMethodType.Post);
        }

        /// <summary>
        /// Store a new Status in the database with planned status.
        /// </summary>
        /// <param name="parameters">parameters</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns></returns>
        public StatusResponse SetRouteStopStatus(SetRouteStopStatusParameters parameters, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<StatusResponse>(parameters,
                R4MEInfrastructureSettingsV5.RouteStopStatus,
                HttpMethodType.Post,
                out resultResponse);
        }

        /// <summary>
        /// Store a new Status in the database with planned status.
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns></returns>
        public Task<Tuple<StatusResponse, ResultResponse>> SetRouteStopStatusAsync(SetRouteStopStatusParameters parameters)
        {
            return GetJsonObjectFromAPIAsync<StatusResponse>(parameters,
                R4MEInfrastructureSettingsV5.RouteStopStatus,
                HttpMethodType.Post);
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

        private Task<Tuple<T, ResultResponse, string>> GetJsonObjectAndJobFromAPIAsync<T>(GenericParameters optimizationParameters,
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

        private async Task<Tuple<T, ResultResponse>> GetJsonObjectFromAPIAsync<T>(GenericParameters optimizationParameters,
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

        private async Task<Tuple<T, ResultResponse, string>> GetJsonObjectFromAPIAsync<T>(
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
                                    var jsonString = ((mandatoryFields?.Length ?? 0) > 0) || serializeBodyWithNewtonJson
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
                                        result.GetType().GetProperty("StatusCode")
                                            .SetValue(result, (int)response.StatusCode);

                                        result.GetType().GetProperty("IsSuccessStatusCode")
                                            .SetValue(result, response.IsSuccessStatusCode);
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

        private string ExtractJobId(HttpResponseMessage response)
        {

            var jobLocation = response?.Headers?.Location;

            string jobId = (jobLocation?.OriginalString?.Length ?? 0) > 32
                    ? jobLocation.OriginalString.Split('/').Last()
                    : null;

            return (jobId?.Length ?? 0) == 32 ? jobId : null;
        }


        private T GetJsonObjectFromAPI<T>(GenericParameters optimizationParameters,
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
                                    var jsonString = ((mandatoryFields?.Length ?? 0) > 0) || serializeBodyWithNewtonJson
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
                                    //content.Headers.ContentType = new MediaTypeHeaderValue("application/json-patch+json");
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
                                    var streamTask = ((StreamContent)response.Result.Content).ReadAsStreamAsync();
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
                                    
                                    if (typeof(T) == typeof(StatusResponse))
                                    {
                                        result.GetType().GetProperty("StatusCode")
                                            .SetValue(result, (int)response.Result.StatusCode);

                                        result.GetType().GetProperty("IsSuccessStatusCode")
                                            .SetValue(result, response.Result.IsSuccessStatusCode);
                                    }
                                    
                                }
                                else
                                {
                                    Task<Stream> streamTask = null;
                                    Task<string> errorMessageContent = null;

                                    if (response.Result.Content.GetType() == typeof(StreamContent))
                                        streamTask = ((StreamContent)response.Result.Content).ReadAsStreamAsync();
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

        #endregion
    }
}