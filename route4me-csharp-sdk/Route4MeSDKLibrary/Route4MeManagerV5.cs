using System;
using System.Threading.Tasks;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.DataTypes.V5.TelematicsPlatform;
using Route4MeSDK.QueryTypes.V5;
using Route4MeSDKLibrary.DataTypes.V5;
using Route4MeSDKLibrary.DataTypes.V5.AddressBookContact;
using Route4MeSDKLibrary.DataTypes.V5.Internal.Requests;
using Route4MeSDKLibrary.DataTypes.V5.Orders;
using Route4MeSDKLibrary.DataTypes.V5.Routes;
using Route4MeSDKLibrary.DataTypes.V5.RouteStatus;
using Route4MeSDKLibrary.Managers;
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
            _addressBookContactsManager = new AddressBookContactsManagerV5(apiKey);
            _accountProfileManager = new AccountProfileManagerV5(apiKey);
            _addressBarcodeManager = new AddressBarcodeManagerV5(apiKey);
            _teamManagementManager = new TeamManagementManagerV5(apiKey);
            _driveReviewManager = new DriveReviewManagerV5(apiKey);
            _routeManager = new RouteManagerV5(apiKey);
            _optimizationManager = new OptimizationManagerV5(apiKey);
            _vehicleManager = new VehicleManagerV5(apiKey);
            _telematicsManager = new TelematicsManagerV5(apiKey);
            _orderManager = new OrderManagerV5(apiKey);
            _routeStatusManager = new RouteStatusManagerV5(apiKey);
            _facilityManager = new FacilityManagerV5(apiKey);
        }

        #endregion

        #region Fields

        private readonly AddressBookContactsManagerV5 _addressBookContactsManager;
        private readonly AccountProfileManagerV5 _accountProfileManager;
        private readonly AddressBarcodeManagerV5 _addressBarcodeManager;
        private readonly TeamManagementManagerV5 _teamManagementManager;
        private readonly DriveReviewManagerV5 _driveReviewManager;
        private readonly RouteManagerV5 _routeManager;
        private readonly OptimizationManagerV5 _optimizationManager;
        private readonly VehicleManagerV5 _vehicleManager;
        private readonly TelematicsManagerV5 _telematicsManager;
        private readonly OrderManagerV5 _orderManager;
        private readonly RouteStatusManagerV5 _routeStatusManager;
        private readonly FacilityManagerV5 _facilityManager;

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
            return _addressBookContactsManager.RemoveAddressBookContacts(contactIDs, out resultResponse);
        }

        /// <summary>
        ///     Remove the address book contacts.
        /// </summary>
        /// <param name="contactIDs">The array of the contract IDs</param>
        /// <returns>If true the contacts were removed successfully</returns>
        public Task<Tuple<StatusResponse, ResultResponse, string>> RemoveAddressBookContactsAsync(long[] contactIDs)
        {
            return _addressBookContactsManager.RemoveAddressBookContactsAsync(contactIDs);
        }

        /// <summary>
        ///     Remove the address book contacts by areas
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="resultResponse">out: Result response</param>
        /// <returns>If true the contacts were removed successfully</returns>
        public bool RemoveAddressBookContactsByAreas(AddressBookContactsFilter filter, out ResultResponse resultResponse)
        {
            return _addressBookContactsManager.RemoveAddressBookContactsByAreas(filter, out resultResponse);
        }

        /// <summary>
        ///     Remove the address book contacts.
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <returns>If true the contacts were removed successfully</returns>
        public Task<Tuple<bool, ResultResponse>> RemoveAddressBookContactsByAreasAsync(AddressBookContactsFilter filter)
        {
            return _addressBookContactsManager.RemoveAddressBookContactsByAreasAsync(filter);
        }

        /// <summary>
        ///     Get all Address custom fields.
        /// </summary>
        /// <param name="resultResponse">out: Result response</param>
        /// <returns>All Address custom fields</returns>
        public CustomFieldsResponse GetCustomFields(out ResultResponse resultResponse)
        {
            return _addressBookContactsManager.GetCustomFields(out resultResponse);
        }

        /// <summary>
        ///     Get all Address custom fields.
        /// </summary>
        /// <returns>If true the contacts were removed successfully</returns>
        public Task<Tuple<CustomFieldsResponse, ResultResponse>> GetCustomFieldsAsync()
        {
            return _addressBookContactsManager.GetCustomFieldsAsync();
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
            return _addressBookContactsManager.GetAddressBookContacts(addressBookParameters, out resultResponse);
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
            return _addressBookContactsManager.GetAddressBookContactsAsync(addressBookParameters);
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
            return _addressBookContactsManager.GetAddressBookContacts(addressBookContactsBodyRequest, out resultResponse);
        }

        /// <summary>
        ///     Returns address book contacts
        /// </summary>
        /// <param name="addressBookContactsBodyRequest">Request body with filters, limits and so on</param>
        /// <returns>An AddressBookContactsResponse type object</returns>
        public Task<Tuple<AddressBookContactsResponse, ResultResponse>> GetAddressBookContacts(AddressBookContactsBodyRequest addressBookContactsBodyRequest)
        {
            return _addressBookContactsManager.GetAddressBookContacts(addressBookContactsBodyRequest);
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
            return _addressBookContactsManager.GetAddressBookContactsPaginated(addressBookParametersPaginated, out resultResponse);
        }

        /// <summary>
        ///     Returns address book contacts
        /// </summary>
        /// <param name="addressBookParametersPaginated">Input parameters</param>
        /// <returns>An AddressBookContactsResponse type object</returns>
        public Task<Tuple<AddressBookContactsResponse, ResultResponse>> GetAddressBookContactsPaginatedAsync(AddressBookParametersPaginated addressBookParametersPaginated)
        {
            return _addressBookContactsManager.GetAddressBookContactsPaginatedAsync(addressBookParametersPaginated);
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
            return _addressBookContactsManager.GetAddressBookContactsPaginated(addressBookContactsBodyPaginatedRequest, out resultResponse);
        }

        /// <summary>
        ///     Returns address book contacts
        /// </summary>
        /// <param name="addressBookContactsBodyPaginatedRequest">Input parameters</param>
        /// <returns>An AddressBookContactsResponse type object</returns>
        public Task<Tuple<AddressBookContactsResponse, ResultResponse>> GetAddressBookContactsPaginatedAsync(AddressBookContactsBodyPaginatedRequest addressBookContactsBodyPaginatedRequest)
        {
            return _addressBookContactsManager.GetAddressBookContactsPaginatedAsync(addressBookContactsBodyPaginatedRequest);
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
            return _addressBookContactsManager.GetAddressBookContactsClustering(addressBookParametersClustering, out resultResponse);
        }

        /// <summary>
        ///     Returns address book contacts
        /// </summary>
        /// <param name="addressBookParametersClustering">Input parameters</param>
        /// <returns>An AddressBookContactsResponse type object</returns>
        public Task<Tuple<AddressBookContactsClusteringResponse, ResultResponse>> GetAddressBookContactsClusteringAsync(AddressBookParametersClustering addressBookParametersClustering)
        {
            return _addressBookContactsManager.GetAddressBookContactsClusteringAsync(addressBookParametersClustering);
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
            return _addressBookContactsManager.GetAddressBookContactsClustering(addressBookParametersClusteringBodyRequest, out resultResponse);
        }

        /// <summary>
        ///     Returns address book contacts
        /// </summary>
        /// <param name="addressBookParametersClusteringBodyRequest">Request body</param>
        /// <returns>An AddressBookContactsResponse type object</returns>
        public Task<Tuple<AddressBookContactsClusteringResponse, ResultResponse>> GetAddressBookContactsClusteringAsync(AddressBookParametersClusteringBodyRequest addressBookParametersClusteringBodyRequest)
        {
            return _addressBookContactsManager.GetAddressBookContactsClusteringAsync(addressBookParametersClusteringBodyRequest);
        }

        /// <summary>
        ///     Get an address book contact by ID
        /// </summary>
        /// <param name="contactId">contact ID</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An AddressBookContact type object</returns>
        public AddressBookContact GetAddressBookContactById(long contactId, out ResultResponse resultResponse)
        {
            return _addressBookContactsManager.GetAddressBookContactById(contactId, out resultResponse);
        }

        /// <summary>
        ///     Get an address book contact by ID
        /// </summary>
        /// <param name="contactId">contact ID</param>
        /// <returns>An AddressBookContact type object</returns>
        public Task<Tuple<AddressBookContact, ResultResponse>> GetAddressBookContactByIdAsync(long contactId)
        {
            return _addressBookContactsManager.GetAddressBookContactByIdAsync(contactId);
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
            return _addressBookContactsManager.GetAddressBookContactsByIds(contactIDs, out resultResponse);
        }

        /// <summary>
        ///     Get address book contacts asynchronously by sending an array of address IDs
        /// </summary>
        /// <param name="contactIDs">An array of address IDs</param>
        /// <returns>An object containing an array of the address book contacts and result resposne</returns>
        public Task<Tuple<AddressBookContactsResponse, ResultResponse>> GetAddressBookContactByIdsAsync(long[] contactIDs)
        {
            return _addressBookContactsManager.GetAddressBookContactByIdsAsync(contactIDs);
        }

        /// <summary>
        ///     Get depots from address book contacts.
        /// </summary>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An array of depot-eligible contacts</returns>
        public AddressBookContact[] GetDepotsFromAddressBook(out ResultResponse resultResponse)
        {
            return _addressBookContactsManager.GetDepotsFromAddressBook(out resultResponse);
        }

        /// <summary>
        ///     Get depots from address book contacts async.
        /// </summary>
        /// <returns>An object containing an array of the depot-eligible contacts and result response</returns>
        public Task<Tuple<AddressBookContact[], ResultResponse, string>> GetDepotsFromAddressBookAsync()
        {
            return _addressBookContactsManager.GetDepotsFromAddressBookAsync();
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
            return _addressBookContactsManager.AddAddressBookContact(contactParams, out resultResponse);
        }

        /// <summary>
        ///     Add an address book contact to database.
        /// </summary>
        /// <param name="contactParams">The contact parameters</param>
        /// <returns>Created address book contact</returns>
        public Task<Tuple<AddressBookContact, ResultResponse>> AddAddressBookContactAsync(AddressBookContact contactParams)
        {
            return _addressBookContactsManager.AddAddressBookContactAsync(contactParams);
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
            return _addressBookContactsManager.UpdateAddressBookContact(id, contactParams, out resultResponse);
        }

        /// <summary>
        ///     Update address book contact.
        /// </summary>
        /// <param name="id">ID of the book contract</param>
        /// <param name="contactParams">The contact parameters</param>
        /// <returns>Created address book contact</returns>
        public Task<Tuple<AddressBookContact, ResultResponse>> UpdateAddressBookContactAsync(long id, AddressBookContact contactParams)
        {
            return _addressBookContactsManager.UpdateAddressBookContactAsync(id, contactParams);
        }

        /// <summary>
        ///      Batch update for address book contacts.
        /// </summary>
        /// <param name="contactsParams">The contacts parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Created address book contact</returns>
        public AddressBookContact[] BatchUpdateAddressBookContact(AddressBookContactMultiple contactsParams, out ResultResponse resultResponse)
        {
            return _addressBookContactsManager.BatchUpdateAddressBookContact(contactsParams, out resultResponse);
        }

        /// <summary>
        ///     Batch update for address book contacts.
        /// </summary>
        /// <param name="contactsParams">The contacts parameters</param>
        /// <returns>Created address book contact</returns>
        public Task<Tuple<AddressBookContact[], ResultResponse>> BatchUpdateAddressBookContactAsync(AddressBookContactMultiple contactsParams)
        {
            return _addressBookContactsManager.BatchUpdateAddressBookContactAsync(contactsParams);
        }

        /// <summary>
        ///      Batch update for address book contacts.
        /// </summary>
        /// <param name="request">The contacts parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Created address book contact</returns>
        public bool UpdateAddressBookContactByAreas(UpdateAddressBookContactByAreasRequest request, out ResultResponse resultResponse)
        {
            return _addressBookContactsManager.UpdateAddressBookContactByAreas(request, out resultResponse);
        }

        /// <summary>
        ///     Batch update for address book contacts.
        /// </summary>
        /// <param name="request">The contacts parameters</param>
        /// <returns>Created address book contact</returns>
        public Task<Tuple<bool, ResultResponse>> UpdateAddressBookContactByAreas(UpdateAddressBookContactByAreasRequest request)
        {
            return _addressBookContactsManager.UpdateAddressBookContactByAreas(request);
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
            return _addressBookContactsManager.BatchCreateAddressBookContacts(contactParams, mandatoryNullableFields, out resultResponse);
        }

        /// <summary>
        ///     Add multiple address book contacts to database.
        /// </summary>
        /// <param name="contactParams">The data with multiple contacts parameters</param>
        /// <param name="mandatoryNullableFields">mandatory nullable fields</param>
        /// <returns>Status response (TO DO: expected result with created multiple contacts)</returns>
        public Task<Tuple<StatusResponse, ResultResponse>> BatchCreateAddressBookContactsAsync(BatchCreatingAddressBookContactsRequest contactParams,
            string[] mandatoryNullableFields)
        {
            return _addressBookContactsManager.BatchCreateAddressBookContactsAsync(contactParams, mandatoryNullableFields);
        }

        /// <summary>
        ///     Get the job status of an asynchronous address book contact process. 
        /// </summary>
        /// <param name="jobId">Job ID</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Status response (TO DO: expected result with created multiple contacts)</returns>
        public StatusResponse GetContactsJobStatus(string jobId, out ResultResponse resultResponse)
        {
            return _addressBookContactsManager.GetContactsJobStatus(jobId, out resultResponse);
        }

        /// <summary>
        ///     Get the job status of an asynchronous address book contact process asynchronously.
        /// </summary>
        /// <param name="jobId">Job ID</param>
        /// <returns>A Tuple type object containing a job status or/and failure response</returns>
        public Task<Tuple<StatusResponse, ResultResponse>> GetContactsJobStatusAsync(string jobId)
        {
            return _addressBookContactsManager.GetContactsJobStatusAsync(jobId);
        }

        /// <summary>
        ///     Get the job result of an asynchronous address book contact process. 
        /// </summary>
        /// <param name="jobId">Job ID</param>
        /// <param name="resultResponse">Failing response</param>
        public StatusResponse GetContactsJobResult(string jobId, out ResultResponse resultResponse)
        {
            return _addressBookContactsManager.GetContactsJobResult(jobId, out resultResponse);
        }

        /// <summary>
        ///     Get the job result of an asynchronous address book contact process asynchronously.
        /// </summary>
        /// <param name="jobId">Job ID</param>
        /// <returns>A Tuple type object containing a job status or/and failure response</returns>
        public Task<Tuple<StatusResponse, ResultResponse>> GetContactsJobResultAsync(string jobId)
        {
            return _addressBookContactsManager.GetContactsJobResultAsync(jobId);
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
            return _addressBookContactsManager.ExportAddressesAsync(exportParams);
        }

        /// <summary>
        ///     Exports the addresses to the account's team file store.
        /// </summary>
        /// <param name="exportParams">Export parameters</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Status of an export operation</returns>
        public StatusResponse ExportAddresses(AddressExportParameters exportParams, out ResultResponse resultResponse)
        {
            return _addressBookContactsManager.ExportAddresses(exportParams, out resultResponse);
        }

        /// <summary>
        ///     Export the Address Book Contacts located in the selected areas by sending the corresponding body payload.
        /// </summary>
        /// <param name="exportParams">Export parameters</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Status of an export operation</returns>
        public StatusResponse ExportAddressesByAreas(AddressExportByAreasParameters exportParams, out ResultResponse resultResponse)
        {
            return _addressBookContactsManager.ExportAddressesByAreas(exportParams, out resultResponse);
        }

        /// <summary>
        ///     Export the Address Book Contacts located in the selected areas by sending the corresponding body payload.
        /// </summary>
        /// <param name="exportParams">Export parameters</param>
        /// <returns>A tuple type object containing:
        /// - a StatusResponse type object in case of success, null - if failure,
        /// - a ResultResponse type object in case of failure, null - if success</returns>
        public Task<Tuple<StatusResponse, ResultResponse>> ExportAddressesByAreasAsync(AddressExportByAreasParameters exportParams)
        {
            return _addressBookContactsManager.ExportAddressesByAreasAsync(exportParams);
        }

        /// <summary>
        ///     Export the Address Book Contacts located in provided area IDs by sending the corresponding body payload.
        /// </summary>
        /// <param name="exportParams">Export parameters</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Status of an export operation</returns>
        public StatusResponse ExportAddressesByAreaIds(AddressExportByAreaIdsParameters exportParams, out ResultResponse resultResponse)
        {
            return _addressBookContactsManager.ExportAddressesByAreaIds(exportParams, out resultResponse);
        }

        /// <summary>
        ///     Export the Address Book Contacts located in provided area IDs by sending the corresponding body payload.
        /// </summary>
        /// <param name="exportParams">Export parameters</param>
        /// <returns>A tuple type object containing:
        /// - a StatusResponse type object in case of success, null - if failure,
        /// - a ResultResponse type object in case of failure, null - if success</returns>
        public Task<Tuple<StatusResponse, ResultResponse>> ExportAddressesByAreaIdsAsync(AddressExportByAreaIdsParameters exportParams)
        {
            return _addressBookContactsManager.ExportAddressesByAreaIdsAsync(exportParams);
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
            return _accountProfileManager.GetAccountProfile(out failResponse);
        }

        /// <summary>
        ///     Get account profile
        /// </summary>
        /// <returns>Account profile</returns>
        public Task<Tuple<AccountProfile, ResultResponse>> GetAccountProfileAsync()
        {
            return _accountProfileManager.GetAccountProfileAsync();
        }

        public string GetAccountPreferredUnit(out ResultResponse failResponse)
        {
            return _accountProfileManager.GetAccountPreferredUnit(out failResponse);
        }

        public Task<Tuple<string, ResultResponse>> GetAccountPreferredUnitAsync()
        {
            return _accountProfileManager.GetAccountPreferredUnitAsync();
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
            return _addressBarcodeManager.GetAddressBarcodes(getAddressBarcodesParameters, out resultResponse);
        }

        /// <summary>
        ///     Returns address barcodes
        /// </summary>
        /// <param name="getAddressBarcodesParameters">Request parameters</param>
        /// <returns>An <see cref="GetAddressBarcodesResponse" /> type object and <see cref="ResultResponse"/> type object</returns>
        public Task<Tuple<GetAddressBarcodesResponse, ResultResponse>> GetAddressBarcodesAsync(GetAddressBarcodesParameters getAddressBarcodesParameters)
        {
            return _addressBarcodeManager.GetAddressBarcodesAsync(getAddressBarcodesParameters);
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
            return _addressBarcodeManager.SaveAddressBarcodes(saveAddressBarcodesParameters, out resultResponse);
        }

        /// <summary>
        ///     Saves address bar codes
        /// </summary>
        /// <param name="saveAddressBarcodesParameters">The contact parameters</param>
        /// <returns>Created address book contact</returns>
        public Task<Tuple<StatusResponse, ResultResponse>> SaveAddressBarcodesAsync(SaveAddressBarcodesParameters saveAddressBarcodesParameters)
        {
            return _addressBarcodeManager.SaveAddressBarcodesAsync(saveAddressBarcodesParameters);
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
            return _teamManagementManager.GetTeamMembers(out failResponse);
        }

        /// <summary>
        ///     Retrieve all existing sub-users associated with the Member’s account.
        /// </summary>
        /// <returns>An array of the TeamResponseV5 type objects</returns>
        public Task<Tuple<TeamResponse[], ResultResponse>> GetTeamMembersAsync()
        {
            return _teamManagementManager.GetTeamMembersAsync();
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
            return _teamManagementManager.GetTeamMemberById(parameters, out resultResponse);
        }

        /// <summary>
        ///     Retrieve a team member by the parameter UserId
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <returns>Retrieved team member</returns>
        public Task<Tuple<TeamResponse, ResultResponse>> GetTeamMemberByIdAsync(MemberQueryParameters parameters)
        {
            return _teamManagementManager.GetTeamMemberByIdAsync(parameters);
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
            return _teamManagementManager.CreateTeamMember(memberParams, out resultResponse);
        }

        /// <summary>
        ///     Creates new team member (sub-user) in the user's account
        /// </summary>
        /// <param name="memberParams">An object of the type MemberParametersV4</param>
        /// <returns>Created team member</returns>
        public Task<Tuple<TeamResponse, ResultResponse>> CreateTeamMemberAsync(TeamRequest memberParams)
        {
            return _teamManagementManager.CreateTeamMemberAsync(memberParams);
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
            return _teamManagementManager.BulkCreateTeamMembers(membersParams, conflicts, out resultResponse);
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
            return _teamManagementManager.BulkCreateTeamMembersAsync(membersParams, conflicts);
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
            return _teamManagementManager.RemoveTeamMember(parameters, out resultResponse);
        }

        /// <summary>
        ///     Removes a team member (sub-user) from the user's account.
        /// </summary>
        /// <param name="parameters">An object of the type MemberParametersV4 containg the parameter UserId</param>
        /// <returns>Removed team member</returns>
        public Task<Tuple<TeamResponse, ResultResponse>>  RemoveTeamMemberAsync(MemberQueryParameters parameters)
        {
            return _teamManagementManager.RemoveTeamMemberAsync(parameters);
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
            return _teamManagementManager.UpdateTeamMember(queryParameters, requestPayload, out resultResponse);
        }

        /// <summary>
        ///     Update a team member
        /// </summary>
        /// <param name="queryParameters">Member query parameters</param>
        /// <param name="requestPayload">Member request parameters</param>
        /// <returns>Updated team member</returns>
        public Task<Tuple<TeamResponse, ResultResponse>> UpdateTeamMemberAsync(MemberQueryParameters queryParameters, TeamRequest requestPayload)
        {
            return _teamManagementManager.UpdateTeamMemberAsync(queryParameters, requestPayload);
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
            return _teamManagementManager.AddSkillsToDriver(queryParameters, skills, out resultResponse);
        }

        /// <summary>
        ///     Add an array of skills to the driver.
        /// </summary>
        /// <param name="queryParameters">Query parameters</param>
        /// <param name="skills">An array of the driver skills</param>
        /// <returns>Updated team member</returns>
        public Task<Tuple<TeamResponse, ResultResponse>> AddSkillsToDriverAsync(MemberQueryParameters queryParameters, string[] skills)
        {
            return _teamManagementManager.AddSkillsToDriverAsync(queryParameters, skills);
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
            return _driveReviewManager.GetDriverReviewList(parameters, out resultResponse);
        }

        /// <summary>
        ///     Get list of the drive reviews.
        /// </summary>
        /// <param name="parameters">Query parmeters</param>
        /// <returns>List of the driver reviews</returns>
        public Task<Tuple<DriverReviewsResponse, ResultResponse>> GetDriverReviewListAsync(DriverReviewParameters parameters)
        {
            return _driveReviewManager.GetDriverReviewListAsync(parameters);
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
            return _driveReviewManager.GetDriverReviewById(parameters, out resultResponse);
        }

        /// <summary>
        ///     Get driver review by ID
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <returns>Driver review</returns>
        public Task<Tuple<DriverReviewResponse, ResultResponse>> GetDriverReviewByIdAsync(DriverReviewParameters parameters)
        {
            return _driveReviewManager.GetDriverReviewByIdAsync(parameters);
        }

        /// <summary>
        ///     Upload driver review to the server
        /// </summary>
        /// <param name="driverReview">Request payload</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Driver review</returns>
        public DriverReviewResponse CreateDriverReview(DriverReview driverReview, out ResultResponse resultResponse)
        {
            return _driveReviewManager.CreateDriverReview(driverReview, out resultResponse);
        }

        /// <summary>
        ///     Upload driver review to the server
        /// </summary>
        /// <param name="driverReview">Request payload</param>
        /// <returns>Driver review</returns>
        public Task<Tuple<DriverReviewResponse, ResultResponse>> CreateDriverReviewAsync(DriverReview driverReview)
        {
            return _driveReviewManager.CreateDriverReviewAsync(driverReview);
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
            return _driveReviewManager.UpdateDriverReview(driverReview, method, out resultResponse);
        }

        /// <summary>
        ///     Update a driver review.
        /// </summary>
        /// <param name="driverReview">Request payload</param>
        /// <param name="method">Http method</param>
        /// <returns>Driver review</returns>
        public Task<Tuple<DriverReviewResponse, ResultResponse>> UpdateDriverReviewAsync(DriverReview driverReview, HttpMethodType method)
        {
            return _driveReviewManager.UpdateDriverReviewAsync(driverReview, method);
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
            return _routeManager.GetRoutes(routeParameters, out resultResponse);
        }

        /// <summary>
        /// Retrieves a list of the routes asynchronously.
        /// </summary>
        /// <param name="routeParameters">Query parameters <see cref="RouteParametersQuery"/></param>
        /// <returns>A Tuple type object containing a route list or/and failure response</returns>
        public Task<Tuple<DataObjectRoute[], ResultResponse>> GetRoutesAsync(RouteParametersQuery routeParameters)
        {
            return _routeManager.GetRoutesAsync(routeParameters);
        }

        /// <summary>
        /// Retrieves a paginated list of the routes.
        /// </summary>
        /// <param name="routeParameters">Query parameters <see cref="RouteParametersQuery"/></param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>An array of the routes</returns>
        public RoutesResponse GetAllRoutesWithPagination(RouteParametersQuery routeParameters, out ResultResponse resultResponse)
        {
            return _routeManager.GetAllRoutesWithPagination(routeParameters, out resultResponse);
        }

        /// <summary>
        /// Retrieves a paginated list of the routes asynchronously.
        /// </summary>
        /// <param name="routeParameters">Query parameters <see cref="RouteParametersQuery"/></param>
        /// <returns>A Tuple type object containing a route list or/and failure response</returns>
        public Task<Tuple<RoutesResponse, ResultResponse>> GetAllRoutesWithPaginationAsync(RouteParametersQuery routeParameters)
        {
            return _routeManager.GetAllRoutesWithPaginationAsync(routeParameters);
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
            return _routeManager.GetPaginatedRouteListWithoutElasticSearch(routeParameters, out resultResponse);
        }

        /// <summary>
        /// Asynchronously retrieves a paginated list of the routes without elastic search.
        /// </summary>
        /// <param name="routeParameters">Query parameters <see cref="RouteParametersQuery"/></param>
        /// <returns>A Tuple type object containing a route list or/and failure response</returns>
        public Task<Tuple<RoutesResponse, ResultResponse>> GetPaginatedRouteListWithoutElasticSearchAsync(RouteParametersQuery routeParameters)
        {
            return _routeManager.GetPaginatedRouteListWithoutElasticSearchAsync(routeParameters);
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
            return _routeManager.GetRoutesByFilter(routeFilterParameters, out resultResponse);
        }

        /// <summary>
        /// Get a route list by filtering asynchronously.
        /// </summary>
        /// <param name="routeFilterParameters">Route filtering parameters</param>
        /// <returns>A RouteFilterResponse type object<see cref="RouteFilterResponse"/></returns>
        public Task<Tuple<RouteFilterResponse, ResultResponse>> GetRoutesByFilterAsync(RouteFilterParameters routeFilterParameters)
        {
            return _routeManager.GetRoutesByFilterAsync(routeFilterParameters);
        }

        public RoutesResponse GetRouteDataTableWithElasticSearch(
            RouteFilterParameters routeFilterParameters,
            out ResultResponse resultResponse)
        {
            return _routeManager.GetRouteDataTableWithElasticSearch(routeFilterParameters, out resultResponse);
        }

        public Task<Tuple<RoutesResponse, ResultResponse>> GetRouteDataTableWithElasticSearchAsync(RouteFilterParameters routeFilterParameters)
        {
            return _routeManager.GetRouteDataTableWithElasticSearchAsync(routeFilterParameters);
        }

        public DataObjectRoute[] GetRouteDatatableWithElasticSearch(
            RouteFilterParameters routeFilterParameters,
            out ResultResponse resultResponse)
        {
            return _routeManager.GetRouteDatatableWithElasticSearch(routeFilterParameters, out resultResponse);
        }

        public Task<Tuple<DataObjectRoute[], ResultResponse>> GetRouteDatatableWithElasticSearchAsync(
            RouteFilterParameters routeFilterParameters)
        {
            return _routeManager.GetRouteDatatableWithElasticSearchAsync(routeFilterParameters);
        }

        public DataObjectRoute[] GetRouteListWithoutElasticSearch(RouteParametersQuery routeParameters,
            out ResultResponse resultResponse)
        {
            return _routeManager.GetRouteListWithoutElasticSearch(routeParameters, out resultResponse);
        }

        public Task<Tuple<DataObjectRoute[], ResultResponse>> GetRouteListWithoutElasticSearchAsync(RouteParametersQuery routeParameters)
        {
            return _routeManager.GetRouteListWithoutElasticSearchAsync(routeParameters);
        }

        /// <summary>
        /// Makes a copy of the existing route.
        /// </summary>
        /// <param name="routeIDs">An array of route IDs.</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>A RouteDuplicateResponse type object <see cref="RouteDuplicateResponse"/></returns>
        public RouteDuplicateResponse DuplicateRoute(string[] routeIDs, out ResultResponse resultResponse)
        {
            return _routeManager.DuplicateRoute(routeIDs, out resultResponse);
        }

        /// <summary>
        /// Makes a copy of the existing route asynchronously.
        /// </summary>
        /// <param name="routeIDs">An array of route IDs.</param>
        /// <returns>A Tuple type object containing an array of the duplicated route IDs
        /// or/and failure response</returns>
        public Task<Tuple<RouteDuplicateResponse, ResultResponse>> DuplicateRouteAsync(string[] routeIDs)
        {
            return _routeManager.DuplicateRouteAsync(routeIDs);
        }

        /// <summary>
        /// Removes specified routes from the account.
        /// </summary>
        /// <param name="routeIds">An array of route IDs.</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>A DeleteRoutes type object <see cref="RoutesDeleteResponse"/></returns>
        public RoutesDeleteResponse DeleteRoutes(string[] routeIds, out ResultResponse resultResponse)
        {
            return _routeManager.DeleteRoutes(routeIds, out resultResponse);
        }

        /// <summary>
        /// Removes specified routes from the account asynchronously.
        /// </summary>
        /// <param name="routeIds">An array of route IDs.</param>
        /// <returns>A Tuple type object containing deleted route ID(s) 
        /// or/and failure response</returns>
        public Task<Tuple<RoutesDeleteResponse, ResultResponse>> DeleteRoutesAsync(string[] routeIds)
        {
            return _routeManager.DeleteRoutesAsync(routeIds);
        }

        public RouteDataTableConfigResponse GetRouteDataTableConfig(out ResultResponse resultResponse)
        {
            return _routeManager.GetRouteDataTableConfig(out resultResponse);
        }

        public Task<Tuple<RouteDataTableConfigResponse, ResultResponse>> GetRouteDataTableConfigAsync()
        {
            return _routeManager.GetRouteDataTableConfigAsync();
        }

        public RouteDataTableConfigResponse GetRouteDataTableFallbackConfig(out ResultResponse resultResponse)
        {
            return _routeManager.GetRouteDataTableFallbackConfig(out resultResponse);
        }

        public Task<Tuple<RouteDataTableConfigResponse, ResultResponse>> GetRouteDataTableFallbackConfigAsync()
        {
            return _routeManager.GetRouteDataTableFallbackConfigAsync();
        }

        /// <summary>
        /// Updates a route by sending route query parameters containg Route Parameters and Addresses.
        /// </summary>
        /// <param name="routeQuery">Route query parameters</param>
        /// <param name="resultResponse"></param>
        /// <returns>Updated route</returns>
        public DataObjectRoute UpdateRoute(RouteParametersQuery routeQuery, out ResultResponse resultResponse)
        {
            return _routeManager.UpdateRoute(routeQuery, out resultResponse);
        }

        /// <summary>
        ///     Updates asynchronously a route by sending route query parameters  
        /// containg Route Parameters and Addresses.
        /// </summary>
        /// <param name="routeQuery">Route query parameters</param>
        /// <returns>A Tuple type object containing updated route or/and failure response</returns>
        public Task<Tuple<DataObjectRoute, ResultResponse, string>> UpdateRouteAsync(RouteParametersQuery routeQuery)
        {
            return _routeManager.UpdateRouteAsync(routeQuery);
        }

        /// <summary>
        /// Inserts the route breaks.
        /// </summary>
        /// <param name="breaks">Route breaks <see cref="RouteBreaks"/></param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>The status of the inserting process </returns>
        public StatusResponse InsertRouteBreaks(RouteBreaks breaks, out ResultResponse resultResponse)
        {
            return _routeManager.InsertRouteBreaks(breaks, out resultResponse);
        }

        /// <summary>
        /// Inserts the route breaks asyncronously.
        /// </summary>
        /// <param name="breaks">Route breaks <see cref="RouteBreaks"/></param>
        /// <returns>A Tuple type object containing the status of the inserting process
        /// or/and failure response</returns>
        public Task<Tuple<StatusResponse, ResultResponse, string>> InsertRouteBreaksAsync(RouteBreaks breaks)
        {
            return _routeManager.InsertRouteBreaksAsync(breaks);
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
            return _routeManager.DynamicInsertRouteAddresses(dynamicInsertRequest, out resultResponse);
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
            return _routeManager.DynamicInsertRouteAddressesAsync(dynamicInsertRequest);
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
            return _optimizationManager.RunOptimization(optimizationParameters, out resultResponse);
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
            return _optimizationManager.RunOptimizationAsync(optimizationParameters);
        }

        /// <summary>
        ///     Remove an existing optimization belonging to an user.
        /// </summary>
        /// <param name="optimizationProblemIDs"> Optimization Problem IDs </param>
        /// <param name="resultResponse"> Returned error string in case of the processs failing </param>
        /// <returns> Result status true/false </returns>
        public bool RemoveOptimization(string[] optimizationProblemIDs, out ResultResponse resultResponse)
        {
            return _optimizationManager.RemoveOptimization(optimizationProblemIDs, out resultResponse);
        }

        /// <summary>
        ///     Remove an existing optimization belonging to an user.
        /// </summary>
        /// <param name="optimizationProblemIDs"> Optimization Problem IDs </param>
        /// <returns> Result status true/false </returns>
        public Task<Tuple<bool, ResultResponse>> RemoveOptimizationAsync(string[] optimizationProblemIDs)
        {
            return _optimizationManager.RemoveOptimizationAsync(optimizationProblemIDs);
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
            return _vehicleManager.GetVehicles(vehicleParams, out resultResponse);
        }

        /// <summary>
        ///     Get the paginated list of Vehicles.
        /// </summary>
        /// <param name="vehicleParams">Query params</param>
        /// <returns>An array of the vehicles</returns>
        public Task<Tuple<Vehicle[], ResultResponse>> GetVehiclesAsync(GetVehicleParameters vehicleParams)
        {
            return _vehicleManager.GetVehiclesAsync(vehicleParams);
        }

        /// <summary>
        ///     Creates a vehicle
        /// </summary>
        /// <param name="vehicle">The VehicleV4Parameters type object as the request payload </param>
        /// <param name="resultResponse"> Failing response </param>
        /// <returns>The created vehicle </returns>
        public Vehicle CreateVehicle(Vehicle vehicle, out ResultResponse resultResponse)
        {
            return _vehicleManager.CreateVehicle(vehicle, out resultResponse);
        }

        /// <summary>
        ///     Creates a vehicle
        /// </summary>
        /// <param name="vehicle">The VehicleV4Parameters type object as the request payload </param>
        /// <returns>The created vehicle </returns>
        public Task<Tuple<Vehicle, ResultResponse>> CreateVehicleAsync(Vehicle vehicle)
        {
            return _vehicleManager.CreateVehicleAsync(vehicle);
        }

        /// <summary>
        ///     Returns the VehiclesPaginated type object containing an array of the vehicles
        /// </summary>
        /// <param name="vehicleParams">The VehicleParameters type object as the query parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An array of the vehicles</returns>
        public VehiclesResponse GetPaginatedVehiclesList(VehicleParameters vehicleParams, out ResultResponse resultResponse)
        {
            return _vehicleManager.GetPaginatedVehiclesList(vehicleParams, out resultResponse);
        }

        /// <summary>
        ///     Returns the VehiclesPaginated type object containing an array of the vehicles
        /// </summary>
        /// <param name="vehicleParams">The VehicleParameters type object as the query parameters</param>
        /// <returns>An array of the vehicles</returns>
        public Task<Tuple<Vehicle[], ResultResponse>> GetPaginatedVehiclesListAsync(VehicleParameters vehicleParams)
        {
            return _vehicleManager.GetPaginatedVehiclesListAsync(vehicleParams);
        }

        /// <summary>
        ///     Removes a vehicle from a user's account
        /// </summary>
        /// <param name="vehicleId"> The Vehicle ID </param>
        /// <param name="resultResponse"> Failing response </param>
        /// <returns>The removed vehicle</returns>
        public Vehicle DeleteVehicle(string vehicleId, out ResultResponse resultResponse)
        {
            return _vehicleManager.DeleteVehicle(vehicleId, out resultResponse);
        }

        /// <summary>
        ///     Removes a vehicle from a user's account
        /// </summary>
        /// <param name="vehicleId"> The Vehicle ID </param>
        /// <returns>The removed vehicle</returns>
        public Task<Tuple<Vehicle, ResultResponse>> DeleteVehicleAsync(string vehicleId)
        {
            return _vehicleManager.DeleteVehicleAsync(vehicleId);
        }

        /// <summary>
        ///     Creates temporary vehicle in the database.
        /// </summary>
        /// <param name="vehParams">Request parameters for creating a temporary vehicle</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>A result with an order ID</returns>
        public VehicleTemporary CreateTemporaryVehicle(VehicleTemporary vehParams, out ResultResponse resultResponse)
        {
            return _vehicleManager.CreateTemporaryVehicle(vehParams, out resultResponse);
        }

        /// <summary>
        ///     Creates temporary vehicle in the database.
        /// </summary>
        /// <param name="vehParams">Request parameters for creating a temporary vehicle</param>
        /// <returns>A result with an order ID</returns>
        public Task<Tuple<VehicleTemporary, ResultResponse>> CreateTemporaryVehicleAsync(VehicleTemporary vehParams)
        {
            return _vehicleManager.CreateTemporaryVehicleAsync(vehParams);
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
            return _vehicleManager.ExecuteVehicleOrder(vehOrderParams, out resultResponse);
        }

        /// <summary>
        ///     Execute a vehicle order
        /// </summary>
        /// <param name="vehOrderParams">Vehicle order parameters</param>
        /// <returns>Created vehicle order</returns>
        public Task<Tuple<VehicleOrderResponse, ResultResponse>> ExecuteVehicleOrderAsync(VehicleOrderParameters vehOrderParams)
        {
            return _vehicleManager.ExecuteVehicleOrderAsync(vehOrderParams);
        }

        /// <summary>
        ///     Get the Vehicle by specifying vehicle ID.
        /// </summary>
        /// <param name="vehicleId">Vehicle ID</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Vehicle object</returns>
        public Vehicle GetVehicleById(string vehicleId, out ResultResponse resultResponse)
        {
            return _vehicleManager.GetVehicleById(vehicleId, out resultResponse);
        }

        /// <summary>
        ///     Get the Vehicle by specifying vehicle ID.
        /// </summary>
        /// <param name="vehicleId">Vehicle ID</param>
        /// <returns>Vehicle object</returns>
        public Task<Tuple<Vehicle, ResultResponse>> GetVehicleByIdAsync(string vehicleId)
        {
            return _vehicleManager.GetVehicleByIdAsync(vehicleId);
        }

        /// <summary>
        ///     Get vehicle by license plate.
        /// </summary>
        /// <param name="licensePlate">Vehicle parameter containing vehicle license plate</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Vehicle</returns>
        public VehicleResponse GetVehicleByLicensePlate(string licensePlate,
            out ResultResponse resultResponse)
        {
            return _vehicleManager.GetVehicleByLicensePlate(licensePlate, out resultResponse);
        }

        /// <summary>
        ///     Get vehicle by license plate asynchronously.
        /// </summary>
        /// <param name="licensePlate">Vehicle parameter containing vehicle license plate</param>
        /// <returns>Vehicle</returns>
        public Task<Tuple<VehicleResponse, ResultResponse, string>> GetVehicleByLicensePlateAsync(string licensePlate)
        {
            return _vehicleManager.GetVehicleByLicensePlateAsync(licensePlate);
        }

        /// <summary>
        ///     Update a vehicle
        /// </summary>
        /// <param name="vehicleParams">Vehicle body parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Updated vehicle </returns>
        public Vehicle UpdateVehicle(Vehicle vehicleParams, out ResultResponse resultResponse)
        {
            return _vehicleManager.UpdateVehicle(vehicleParams, out resultResponse);
        }

        /// <summary>
        ///     Update a vehicle
        /// </summary>
        /// <param name="vehicleParams">Vehicle body parameters</param>
        /// <returns>Updated vehicle </returns>
        public Task<Tuple<Vehicle, ResultResponse, string>> UpdateVehicleAsync(Vehicle vehicleParams)
        {
            return _vehicleManager.UpdateVehicleAsync(vehicleParams);
        }

        /// <summary>
        /// Get the vehicles by their state.
        /// </summary>
        /// <param name="vehicleState">Vehicle state. <see cref="VehicleStates" /> </param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An array of the vehicles</returns>
        public Vehicle[] GetVehiclesByState(VehicleStates vehicleState, out ResultResponse resultResponse)
        {
            return _vehicleManager.GetVehiclesByState(vehicleState, out resultResponse);
        }

        /// <summary>
        /// Get asynchronously the vehicles by their state.
        /// </summary>
        /// <param name="vehicleState">Vehicle state <see cref="VehicleStates"/></param>
        /// <returns>>A Tuple type object containing a vehicle list or/and failure response</returns>
        public Task<Tuple<Vehicle[], ResultResponse, string>> GetVehiclesByStateAsync(VehicleStates vehicleState)
        {
            return _vehicleManager.GetVehiclesByStateAsync(vehicleState);
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
            return _vehicleManager.ActivateVehicles(vehicleIDs, out resultResponse);
        }

        /// <summary>
        /// Activates deactivated vehicles asynchronously.
        /// </summary>
        /// <param name="vehicleIDs">An array of the vehicle IDs</param>
        /// <returns>A Tuple type object containing a vehicle list or/and failure response</returns>
        public Task<Tuple<VehiclesResults, ResultResponse, string>> ActivateVehiclesAsync(string[] vehicleIDs)
        {
            return _vehicleManager.ActivateVehiclesAsync(vehicleIDs);
        }

        /// <summary>
        ///     Deactivates active vehicles
        /// </summary>
        /// <param name="vehicleIDs">An array of the vehicle IDs</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Deactivated vehicles</returns>
        public Vehicle[] DeactivateVehicles(string[] vehicleIDs, out ResultResponse resultResponse)
        {
            return _vehicleManager.DeactivateVehicles(vehicleIDs, out resultResponse);
        }

        /// <summary>
        ///     Deactivates active vehicles asynchronously.
        /// </summary>
        /// <param name="vehicleIDs">An array of the vehicle IDs</param>
        /// <returns>A Tuple type object containing a vehicle list or/and failure response</returns>
        public Task<Tuple<Vehicle[], ResultResponse, string>> DeactivateVehiclesAsync(string[] vehicleIDs)
        {
            return _vehicleManager.DeactivateVehiclesAsync(vehicleIDs);
        }

        /// <summary>
        ///     Updates multiple vehicles at once
        /// </summary>
        /// <param name="vehicleArray">An array of the vehicles</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Status response</returns>
        public StatusResponse UpdateVehicles(Vehicles vehicleArray, out ResultResponse resultResponse)
        {
            return _vehicleManager.UpdateVehicles(vehicleArray, out resultResponse);
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
            return _vehicleManager.UpdateVehiclesAsync(vehicleArray);
        }

        /// <summary>
        /// Removes multiple vehicles at once.
        /// </summary>
        /// <param name="vehicleIDs">An array of the vehicle IDs</param>
        /// <returns>Status of the update operation</returns>
        public StatusResponse DeleteVehicles(string[] vehicleIDs, out ResultResponse resultResponse)
        {
            return _vehicleManager.DeleteVehicles(vehicleIDs, out resultResponse);
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
            return _vehicleManager.DeleteVehiclesAsync(vehicleIDs);
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
            return _vehicleManager.RestoreVehicles(vehicleIDs, out resultResponse);
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
            return _vehicleManager.RestoreVehiclesAsync(vehicleIDs);
        }

        /// <summary>
        /// Returns vehicle job result.
        /// </summary>
        /// <param name="jobId">Job ID</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>The object containing status of the JOB</returns>
        public StatusResponse GetVehicleJobResult(string jobId, out ResultResponse resultResponse)
        {
            return _vehicleManager.GetVehicleJobResult(jobId, out resultResponse);
        }

        /// <summary>
        /// Asynchronously retrieves a vehicle job result.
        /// </summary>
        /// <param name="jobId">Job ID</param>
        /// <returns>Returns a result as a tuple object:
        /// - StatusResponse: if success, this items is not null
        /// - ResultResponse: if failed, this items is not null
        /// - string: if success, this items is a job ID</returns>
        public Task<Tuple<StatusResponse, ResultResponse, string>> GetVehicleJobResultAsync(string jobId)
        {
            return _vehicleManager.GetVehicleJobResultAsync(jobId);
        }

        /// <summary>
        /// Returns vehicle job status.
        /// </summary>
        /// <param name="jobId">Job ID</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>The object containing status of the JOB</returns>
        public StatusResponse GetVehicleJobStatus(string jobId, out ResultResponse resultResponse)
        {
            return _vehicleManager.GetVehicleJobStatus(jobId, out resultResponse);
        }

        /// <summary>
        /// Asynchronously retrieves a vehicle job status.
        /// </summary>
        /// <param name="jobId">Job ID</param>
        /// <returns>Returns a job status </returns>
        public Task<Tuple<StatusResponse, ResultResponse>> GetVehicleJobStatusAsync(string jobId)
        {
            return _vehicleManager.GetVehicleJobStatusAsync(jobId);
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
            return _vehicleManager.GetVehicleLocations(vehParams, out resultResponse);
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
            return _vehicleManager.GetVehicleLocations(vehicleIDs, out resultResponse);
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
            return _vehicleManager.GetVehicleLocationsAsync(vehicleIDs);
        }

        /// <summary>
        ///     Get latest vehicle locations by specified vehicle IDs.
        /// </summary>
        /// <param name="vehParams">Vehicle query parameters containing vehicle IDs.</param>
        /// <returns>Data with vehicles</returns>
        public Task<Tuple<VehicleLocationResponse, ResultResponse>> GetVehicleLocationsAsync(VehicleParameters vehParams)
        {
            return _vehicleManager.GetVehicleLocationsAsync(vehParams);
        }

        /// <summary>
        ///     Get the Vehicle track by specifying vehicle ID.
        /// </summary>
        /// <param name="vehicleParameters">Vehicle params</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Vehicle track object</returns>
        public VehicleTrackResponse GetVehicleTrack(VehicleParameters vehicleParameters, out ResultResponse resultResponse)
        {
            return _vehicleManager.GetVehicleTrack(vehicleParameters, out resultResponse);
        }

        /// <summary>
        ///     Get the Vehicle track by specifying vehicle ID.
        /// </summary>
        /// <param name="vehicleParameters">Vehicle param</param>
        /// <returns>Vehicle track object</returns>
        public Task<Tuple<VehicleTrackResponse, ResultResponse>> GetVehicleTrackAsync(VehicleParameters vehicleParameters)
        {
            return _vehicleManager.GetVehicleTrackAsync(vehicleParameters);
        }

        /// <summary>
        ///     Search vehicles by sending request body.
        /// </summary>
        /// <param name="searchParams">Search parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An array of the found vehicles</returns>
        public Vehicle[] SearchVehicles(VehicleSearchParameters searchParams, out ResultResponse resultResponse)
        {
            return _vehicleManager.SearchVehicles(searchParams, out resultResponse);
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
            return _vehicleManager.SearchVehiclesAsync(searchParams);
        }

        /// <summary>
        /// Sync the Vehicle by sending the corresponding body payload.
        /// </summary>
        /// <param name="syncParams">Query parameters <see cref="VehicleTelematicsSync"/></param>
        /// <param name="resultResponse"></param>
        /// <returns>Synchronized vehicle</returns>
        public Vehicle SyncPendingTelematicsData(VehicleTelematicsSync syncParams, out ResultResponse resultResponse)
        {
            return _vehicleManager.SyncPendingTelematicsData(syncParams, out resultResponse);
        }

        public Task<Tuple<Vehicle, ResultResponse, string>> SyncPendingTelematicsDataAsync(VehicleTelematicsSync syncParams)
        {
            return _vehicleManager.SyncPendingTelematicsDataAsync(syncParams);
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
            return _vehicleManager.GetVehicleProfiles(profileParams, out resultResponse);
        }

        /// <summary>
        ///     Get paginated list of the vehicle profiles.
        /// </summary>
        /// <param name="profileParams">Vehicle profile request parameters</param>
        /// <returns>The data including list of the vehicle profiles.</returns>
        public Task<Tuple<VehicleProfilesResponse, ResultResponse>> GetVehicleProfilesAsync(VehicleProfileParameters profileParams)
        {
            return _vehicleManager.GetVehicleProfilesAsync(profileParams);
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
            return _vehicleManager.CreateVehicleProfile(vehicleProfileParams, out resultResponse);
        }

        /// <summary>
        ///     Create a vehicle profile.
        /// </summary>
        /// <param name="vehicleProfileParams">Vehicle profile body parameters</param>
        /// <returns>Created vehicle profile</returns>
        public Task<Tuple<VehicleProfile, ResultResponse, string>> CreateVehicleProfileAsync(VehicleProfile vehicleProfileParams)
        {
            return _vehicleManager.CreateVehicleProfileAsync(vehicleProfileParams);
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
            return _vehicleManager.DeleteVehicleProfile(vehicleProfileParams, out resultResponse);
        }

        /// <summary>
        ///     Remove a vehicle profile from database.
        ///     TO DO: adjust response structure.
        /// </summary>
        /// <param name="vehicleProfileParams">Vehicle profile parameter containing a vehicle profile ID </param>
        /// <returns>Removed vehicle profile</returns>
        public Task<Tuple<object, ResultResponse>> DeleteVehicleProfileAsync(VehicleProfileParameters vehicleProfileParams)
        {
            return _vehicleManager.DeleteVehicleProfileAsync(vehicleProfileParams);
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
            return _vehicleManager.GetVehicleProfileById(vehicleProfileParams, out resultResponse);
        }

        /// <summary>
        ///     Get vehicle profile by ID.
        /// </summary>
        /// <param name="vehicleProfileParams">Vehicle profile parameter containing a vehicle profile ID </param>
        /// <returns>Vehicle profile</returns>
        public Task<Tuple<VehicleProfile, ResultResponse>> GetVehicleProfileByIdAsync(VehicleProfileParameters vehicleProfileParams)
        {
            return _vehicleManager.GetVehicleProfileByIdAsync(vehicleProfileParams);
        }

        /// <summary>
        ///     Update a vehicle profile.
        /// </summary>
        /// <param name="profileParams">Vehicle profile object as body payload</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Updated vehicle profile</returns>
        public VehicleProfile UpdateVehicleProfile(VehicleProfile profileParams, out ResultResponse resultResponse)
        {
            return _vehicleManager.UpdateVehicleProfile(profileParams, out resultResponse);
        }

        /// <summary>
        ///     Update a vehicle profile.
        /// </summary>
        /// <param name="profileParams">Vehicle profile object as body payload</param>
        /// <returns>Updated vehicle profile</returns>
        public Task<Tuple<VehicleProfile, ResultResponse, string>> UpdateVehicleProfileAsync(VehicleProfile profileParams)
        {
            return _vehicleManager.UpdateVehicleProfileAsync(profileParams);
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
            return _vehicleManager.CreateVehicleCapacityProfile(vehicleCapacityProfile, out resultResponse);
        }

        /// <summary>
        /// Create a vehicle capacity profile asynchronously.
        /// </summary>
        /// <param name="vehicleCapacityProfile">Vehicle capacity profile body parameters</param>
        /// <returns>A tuple type object with a created vehicle capacity profile
        /// or/and failure response</returns>
        public Task<Tuple<VehicleCapacityProfileResponse, ResultResponse>> CreateVehicleCapacityProfileAsync(VehicleCapacityProfile vehicleCapacityProfile)
        {
            return _vehicleManager.CreateVehicleCapacityProfileAsync(vehicleCapacityProfile);
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
            return _vehicleManager.DeleteVehicleCapacityProfile(vehicleCapacityProfileParams, out resultResponse);
        }

        /// <summary>
        /// Remove a vehicle capacity profile from database asynchronously.
        /// </summary>
        /// <param name="vehicleCapacityProfileParams">Vehicle capacity profile parameter containing a vehicle capacity profile ID</param>
        /// <returns>A tuple type object with a deleted vehicle capacity profile
        /// or/and failure response</returns>
        public Task<Tuple<VehicleCapacityProfileResponse, ResultResponse>> DeleteVehicleCapacityProfileAsync(VehicleCapacityProfileParameters vehicleCapacityProfileParams)
        {
            return _vehicleManager.DeleteVehicleCapacityProfileAsync(vehicleCapacityProfileParams);
        }

        /// <summary>
        ///     Get paginated list of the vehicle capacity profiles.
        /// </summary>
        /// <param name="capacityProfileParams">Vehicle capacity profile request parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>The data including list of the vehicle profiles.</returns>
        public VehicleCapacityProfilesResponse GetVehicleCapacityProfiles(VehicleCapacityProfileParameters capacityProfileParams,
            out ResultResponse resultResponse)
        {
            return _vehicleManager.GetVehicleCapacityProfiles(capacityProfileParams, out resultResponse);
        }

        /// <summary>
        /// Get paginated list of the vehicle capacity profiles asynchronously.
        /// </summary>
        /// <param name="capacityProfileParams">Vehicle capacity profile request parameters</param>
        /// <returns>A tuple type object with the retrieved vehicle capacity profiles
        /// or/and failure response</returns>
        public Task<Tuple<VehicleCapacityProfilesResponse, ResultResponse>> GetVehicleCapacityProfilesAsync(VehicleCapacityProfileParameters capacityProfileParams)
        {
            return _vehicleManager.GetVehicleCapacityProfilesAsync(capacityProfileParams);
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
            return _vehicleManager.GetVehicleCapacityProfileById(vehicleCapacityProfileParams, out resultResponse);
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
            return _vehicleManager.GetVehicleCapacityProfileByIdAsync(vehicleCapacityProfileParams);
        }

        /// <summary>
        ///     Update a vehicle capacity profile.
        /// </summary>
        /// <param name="capacityProfileParams">Vehicle capacity profile object as body payload</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Updated vehicle capacity profile</returns>
        public VehicleCapacityProfileResponse UpdateVehicleCapacityProfile(VehicleCapacityProfile capacityProfileParams, out ResultResponse resultResponse)
        {
            return _vehicleManager.UpdateVehicleCapacityProfile(capacityProfileParams, out resultResponse);
        }

        /// <summary>
        /// Update a vehicle capacity profile asynchronously.
        /// </summary>
        /// <param name="capacityProfileParams">Vehicle capacity profile object as body payload</param>
        /// <returns>A tuple type object with updated vehicle capacity profile
        /// or/and failure response</returns>
        public Task<Tuple<VehicleCapacityProfileResponse, ResultResponse, string>> UpdateVehicleCapacityProfileAsync(VehicleCapacityProfile capacityProfileParams)
        {
            return _vehicleManager.UpdateVehicleCapacityProfileAsync(capacityProfileParams);
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
            return _telematicsManager.GetTelematicsConnections(out resultResponse);
        }

        /// <summary>
        ///     Get all registered telematics connections.
        /// </summary>
        /// <returns>An array of the Connection type objects</returns>
        public Task<Tuple<Connection[], ResultResponse>> GetTelematicsConnectionsAsync()
        {
            return _telematicsManager.GetTelematicsConnectionsAsync();
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
            return _telematicsManager.GetTelematicsConnectionByToken(connectionParams, out resultResponse);
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
            return _telematicsManager.GetTelematicsConnectionByTokenAsync(connectionParams);
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
            return _telematicsManager.RegisterTelematicsConnection(connectionParams, out resultResponse);
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
            return _telematicsManager.RegisterTelematicsConnectionAsync(connectionParams);
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
            return _telematicsManager.DeleteTelematicsConnection(connectionParams, out resultResponse);
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
            return _telematicsManager.DeleteTelematicsConnectionAsync(connectionParams);
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
            return _telematicsManager.UpdateTelematicsConnection(connectionParams, out resultResponse);
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
            return _telematicsManager.UpdateTelematicsConnectionAsync(connectionParams);
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
            return _orderManager.ArchiveOrders(parameters, out resultResponse);
        }

        /// <summary>
        ///  Display the Archive Orders.
        /// </summary>
        /// <param name="parameters">Request payload</param>
        /// <returns>Archived Orders</returns>
        public Task<Tuple<ArchiveOrdersResponse, ResultResponse, string>> ArchiveOrdersAsync(ArchiveOrdersParameters parameters)
        {
            return _orderManager.ArchiveOrdersAsync(parameters);
        }

        /// <summary>
        ///  Display the order history.
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Order history</returns>
        public OrderHistoryResponse GetOrderHistory(OrderHistoryParameters parameters, out ResultResponse resultResponse)
        {
            return _orderManager.GetOrderHistory(parameters, out resultResponse);
        }

        /// <summary>
        ///  Display the order history.
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns>Order history</returns>
        public Task<Tuple<OrderHistoryResponse, ResultResponse>> GetOrderHistoryAsync(OrderHistoryParameters parameters)
        {
            return _orderManager.GetOrderHistoryAsync(parameters);
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
            return _routeStatusManager.GetRouteStatus(routeId, out resultResponse);
        }

        /// <summary>
        /// Get the status by specifying the path parameter ID.
        /// </summary>
        /// <param name="routeId">ID of the route</param>
        /// <returns></returns>
        public Task<Tuple<RouteStatusResponse, ResultResponse>> GetRouteStatusAsync(string routeId)
        {
            return _routeStatusManager.GetRouteStatusAsync(routeId);
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
            return _routeStatusManager.UpdateRouteStatus(routeId, request, out resultResponse);
        }

        /// <summary>
        /// Update the status by specifying the path parameter ID.Route statuses change only forward - planned > started/paused > completed
        /// </summary>
        /// <param name="routeId">ID of the route</param>
        /// <param name="request">Request body</param>
        /// <returns></returns>
        public Task<Tuple<RouteStatusResponse, ResultResponse>> UpdateRouteStatusAsync(string routeId, RouteStatusRequest request)
        {
            return _routeStatusManager.UpdateRouteStatusAsync(routeId, request);
        }

        /// <summary>
        /// Get route status history by specifying the path parameter ID.
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns></returns>
        public RouteStatusHistoryResponse GetRouteStatusHistory(RouteStatusHistoryParameters parameters, out ResultResponse resultResponse)
        {
            return _routeStatusManager.GetRouteStatusHistory(parameters, out resultResponse);
        }

        /// <summary>
        /// Get route status history by specifying the path parameter ID.
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <returns></returns>
        public Task <Tuple<RouteStatusHistoryResponse, ResultResponse>> GetRouteStatusHistoryAsync(RouteStatusHistoryParameters parameters)
        {
            return _routeStatusManager.GetRouteStatusHistoryAsync(parameters);
        }

        /// <summary>
        /// Roll back route status by specifying the path parameter ID. Sometimes a status rollback is possible.
        /// </summary>
        /// <param name="routeId">Route ID</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns></returns>
        public RouteStatusResponse RollbackRouteStatus(string routeId, out ResultResponse resultResponse)
        {
            return _routeStatusManager.RollbackRouteStatus(routeId, out resultResponse);
        }

        /// <summary>
        /// Roll back route status by specifying the path parameter ID. Sometimes a status rollback is possible.
        /// </summary>
        /// <param name="routeId">Route ID</param>
        /// <returns></returns>
        public Task<Tuple<RouteStatusResponse, ResultResponse>> RollbackRouteStatusAsync(string routeId)
        {
            return _routeStatusManager.RollbackRouteStatusAsync(routeId);
        }

        /// <summary>
        /// Store a new Status in the database with planned status.
        /// </summary>
        /// <param name="parameters">Route IDs</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns></returns>
        public UpdateRouteStatusAsPlannedResponse UpdateRouteStatusAsPlanned(UpdateRouteStatusAsPlannedParameters parameters, out ResultResponse resultResponse)
        {
            return _routeStatusManager.UpdateRouteStatusAsPlanned(parameters, out resultResponse);
        }

        /// <summary>
        /// Store a new Status in the database with planned status.
        /// </summary>
        /// <param name="parameters">Route IDs</param>
        /// <returns></returns>
        public Task<Tuple<UpdateRouteStatusAsPlannedResponse, ResultResponse>> UpdateRouteStatusAsPlanned(UpdateRouteStatusAsPlannedParameters parameters)
        {
            return _routeStatusManager.UpdateRouteStatusAsPlanned(parameters);
        }

        /// <summary>
        /// Store a new Status in the database with planned status.
        /// </summary>
        /// <param name="parameters">parameters</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns></returns>
        public StatusResponse SetRouteStopStatus(SetRouteStopStatusParameters parameters, out ResultResponse resultResponse)
        {
            return _routeStatusManager.SetRouteStopStatus(parameters, out resultResponse);
        }

        /// <summary>
        /// Store a new Status in the database with planned status.
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns></returns>
        public Task<Tuple<StatusResponse, ResultResponse>> SetRouteStopStatusAsync(SetRouteStopStatusParameters parameters)
        {
            return _routeStatusManager.SetRouteStopStatusAsync(parameters);
        }

        #endregion

        #region Facilities

        /// <summary>
        /// Access to Facility Management API
        /// </summary>
        public FacilityManagerV5 FacilityManager => _facilityManager;

        #endregion
    }
}