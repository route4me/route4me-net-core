using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;
using Route4MeSDKLibrary.DataTypes.V5;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example demonstrates the process of asynchronously creating many address book contacts at once.
        /// </summary>
        public async void BatchCreateAdressBookContactsAsync()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            // Prepare a list of the contacts.
            var lsContacts = new List<AddressBookContact>
            {
                new AddressBookContact
                {
                    FirstName = "Test FirstName " + new Random().Next(),
                    Address1 = "Test Address1 " + new Random().Next(),
                    CachedLat = 38.024754,
                    CachedLng = -77.338914,
                    AddressStopType = AddressStopType.PickUp.Description()
                },
                new AddressBookContact
                {
                    FirstName = "Test FirstName " + new Random().Next(),
                    Address1 = "Test Address1 " + new Random().Next(),
                    CachedLat = 38.024554,
                    CachedLng = -77.338714,
                    AddressStopType = AddressStopType.PickUp.Description()
                }
            };

            // The function requires a list of the mandatory fields.
            var mandatoryFields = new[]
            {
                R4MeUtils.GetPropertyName(() => lsContacts[0].FirstName),
                R4MeUtils.GetPropertyName(() => lsContacts[0].Address1),
                R4MeUtils.GetPropertyName(() => lsContacts[0].CachedLat),
                R4MeUtils.GetPropertyName(() => lsContacts[0].CachedLng),
                R4MeUtils.GetPropertyName(() => lsContacts[0].AddressStopType)
            };

            // Put the contact parameters in the batch creation request.
            var contactParams = new BatchCreatingAddressBookContactsRequest
            {
                Data = lsContacts.ToArray()
            };

            // Run the the batch creation request
            var response =
                await route4Me.BatchCreateAddressBookContactsAsync(contactParams, mandatoryFields);

            Console.WriteLine(
                response?.Item1?.Status ?? false
                    ? "The batch creating process of the contacts finished successfully"
                    : "The batch creating process of the contacts failed"
            );

            #region Remove Testing Contacts

            ContactsToRemove = new List<string>();

            var searchParamss = new AddressBookParameters() { Query = lsContacts[0].Address1 };

            var result1 = route4Me.GetAddressBookContacts(searchParamss, out ResultResponse resultResponse);

            if ((result1?.Results[0]?.AddressId ?? 0) > 0)
                ContactsToRemove.Add(result1.Results[0].AddressId.ToString());

            searchParamss = new AddressBookParameters() { Query = lsContacts[1].Address1 };

            var result2 = route4Me.GetAddressBookContacts(searchParamss, out resultResponse);

            if ((result2?.Results[0]?.AddressId ?? 0) > 0)
                ContactsToRemove.Add(result2.Results[0].AddressId.ToString());

            RemoveTestContacts();

            #endregion
        }
    }
}