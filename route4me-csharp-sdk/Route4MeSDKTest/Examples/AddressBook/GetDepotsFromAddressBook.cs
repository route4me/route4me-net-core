//using Route4MeSDK.DataTypes;
using Route4MeSDK.DataTypes.V5;
//using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get the address book contacts
        /// </summary>
        public void GetDepotsFromAddressBook()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManagerV5(ActualApiKey);


            // Run the query
            AddressBookContact[] contacts = route4Me.GetDepotsFromAddressBook(
                out ResultResponse resultResponse);

            PrintExampleContact(contacts, resultResponse);
        }
    }
}
