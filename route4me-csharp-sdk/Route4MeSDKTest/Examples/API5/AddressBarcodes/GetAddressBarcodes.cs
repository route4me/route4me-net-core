using Route4MeSDK.DataTypes.V5;
//using Route4MeSDK.QueryTypes;
using Route4MeSDK.QueryTypes.V5;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of retrieving the address barcodes.
        /// </summary>
        public void GetAddressBarcodes()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);


            SaveAddressBarcode(false);

            var barCodeAddress = SD10Stops_route.Addresses[1];

            var getAddressBarcodesParameters = new GetAddressBarcodesParameters
            {
                RouteId = SD10Stops_route.RouteId,
                RouteDestinationId = (long)barCodeAddress.RouteDestinationId,
                Limit = 10
            };

            var readResult1 =
                route4Me.GetAddressBarcodes(getAddressBarcodesParameters, out ResultResponse resultResponse);

            Console.WriteLine(
                (readResult1?.Status ?? "false") == "true" ? "The barcode retrieved" : "Cannot retrieve the barcode"
            );

            RemoveTestOptimizations();
        }
    }
}