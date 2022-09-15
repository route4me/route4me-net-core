namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get the locations eligable for depots asynchronously.
        /// </summary>
        public async void GetDepotsFromAddressBookAsync()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            CreateTestContactsV5();

            var contactIDs = new long[] { (long)contact51.AddressId, (long)contact52.AddressId };

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