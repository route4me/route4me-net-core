using System;
using System.Linq;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        // Export a list of the contacts to the file.
        public async void ExportAddressesAsync()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            #region Retrieve the contacts to export to the account's team store

            var addressBookParameters = new AddressBookParameters()
            {
                Limit = 10,
                Offset = 0
            };

            var addresses = route4Me.GetAddressBookContacts(addressBookParameters, out ResultResponse resultResponse);

            if ((addresses?.Results?.Length ?? 0)<1)
            {
                Console.WriteLine("Cannot retrieve the contacts from the account. The example failed.");
            }

            #endregion

            // Prepare the parameters to export the addresses. 
            var exportParams = new AddressExportParameters()
            {
                AddressIds = addresses.Results.Select(x=>(long)x.AddressId).ToArray(),
                Filename = "AB contacts export "+DateTime.Now.ToString("yyMMddHHmmss")
            };

            // Send a request to the server
            var exportResult = await route4Me.ExportAddressesAsync(exportParams);

            Console.WriteLine(
                exportResult?.Item1?.IsSuccessStatusCode ?? false 
                    ? "The ExportAddresses task finsished successfully"
                    : "The ExportAddresses task failed"
                );
        }
    }
}
