using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get address book contacts asynchronously.
        /// </summary>
        [System.Obsolete]
        public async void GetAddressBookContactsAsync()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var addressBookParameters = new AddressBookParameters()
            {
                Limit = 10,
                Offset = 0
            };

            // Run the query
            var result = await route4Me.GetAddressBookContactsAsync(
                addressBookParameters);

            PrintExampleContact(
                result.Item1.Results,
                (uint)result.Item1.Total,
                result.Item2?.Status.ToString() ?? null);

            RemoveTestContacts();
        }
    }
}