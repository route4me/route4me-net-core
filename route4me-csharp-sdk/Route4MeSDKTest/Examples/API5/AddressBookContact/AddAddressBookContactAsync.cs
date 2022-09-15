using Route4MeSDK.DataTypes.V5;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Add address book contact asynchronously.
        /// </summary>
        public async void AddAddressBookContactAsync()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var contact = new AddressBookContact()
            {
                FirstName = "Test FirstName " + (new Random()).Next().ToString(),
                Address1 = "Test Address1 " + (new Random()).Next().ToString(),
                CachedLat = 38.024654,
                CachedLng = -77.338814,
                AddressCustomData = new Dictionary<string, string>()
                {
                    { "Service type", "publishing" },
                    { "Facilities", "storage" },
                    { "Parking", "temporarry" }
                }
            };

            // Run the query
            var result = await route4Me.AddAddressBookContactAsync(contact);

            PrintExampleContact(
                result.Item1,
                1,
                result.Item2?.Status.ToString() ?? null);

            RemoveTestContacts();
        }
    }
}