using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Search locations by IDs asynchronously.
        /// </summary>
        public async void SearchLocationsByIDsAsync()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            CreateTestContacts();

            var contactIDs = new long[] { (long)contact1.AddressId, (long)contact2.AddressId };

            // Run the query
            var result = await route4Me.GetAddressBookContactByIdsAsync(
                contactIDs);

            PrintExampleContact(
                result.Item1.Results,
                (uint)result.Item1.Total,
                result.Item2?.Status.ToString() ?? null);

            RemoveTestContacts();
        }
    }
}