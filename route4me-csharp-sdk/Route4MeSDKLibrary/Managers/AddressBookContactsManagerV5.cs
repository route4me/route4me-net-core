using System;
using System.Threading.Tasks;
using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes;
using Route4MeSDK.QueryTypes.V5;
using Route4MeSDKLibrary.DataTypes.V5;
using Route4MeSDKLibrary.DataTypes.V5.AddressBookContact;
using AddressBookParameters = Route4MeSDK.QueryTypes.V5.AddressBookParameters;

namespace Route4MeSDKLibrary.Managers
{
    public sealed class AddressBookContactsManagerV5 : Route4MeManagerBase
    {
        public AddressBookContactsManagerV5(string apiKey) : base(apiKey)
        {
        }

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
            var response = GetJsonObjectFromAPI<StatusResponse>(request,
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
        public StatusResponse BatchCreateAddressBookContacts(BatchCreatingAddressBookContactsRequest contactParams,
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
        public async Task<Tuple<StatusResponse, ResultResponse>> BatchCreateAddressBookContactsAsync(BatchCreatingAddressBookContactsRequest contactParams,
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
                R4MEInfrastructureSettingsV5.ContactsGetAsyncJobStatus + "/" + jobId,
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
    }
}