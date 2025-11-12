using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get address book contact by ID asynchronously.
        /// </summary>
        [System.Obsolete]
        public async void GetAddressBookContactByIdAsync()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            CreateTestContactsV5();

            // Run the query
            var result = await route4Me.GetAddressBookContactByIdAsync((long)contact51.AddressId);

            PrintExampleContact(
                result.Item1,
                1,
                result.Item2?.Status.ToString() ?? null);

            RemoveTestContacts();
        }
    }
}