using Route4MeSDK.QueryTypes.V5;
using System;
using System.Threading.Tasks;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the asynchronous process of retrieving the address barcodes.
        /// </summary>
        public async Task GetAddressBarcodesAsync()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);


            SaveAddressBacode(false);

            var barCodeAddress = SD10Stops_route.Addresses[1];

            var getAddressBarcodesParameters = new GetAddressBarcodesParameters
            {
                RouteId = SD10Stops_route.RouteId,
                RouteDestinationId = (long)barCodeAddress.RouteDestinationId,
                Limit = 10
            };

            var readResult1 = await route4Me.GetAddressBarcodesAsync(getAddressBarcodesParameters);

            Console.WriteLine(
                (readResult1.Item1?.Status ?? "false") == "true"
                    ? "The barcode retrieved asynchronously"
                    : "Cannot retrieve the barcode asynchronously"
            );

            RemoveTestOptimizations();
        }
    }
}