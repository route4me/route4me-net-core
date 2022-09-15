using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of saving an address barcode asynchronously.
        /// </summary>
        public async void SaveAddressBacodeAsync()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            #region Prepare Testing Data

            RunOptimizationSingleDriverRoute10Stops();
            OptimizationsToRemove = new List<string>()
            {
                SD10Stops_optimization_problem_id
            };

            #endregion

            var barCodeAddress = SD10Stops_route.Addresses[1];

            var barCodeParams = new SaveAddressBarcodesParameters()
            {
                RouteId = SD10Stops_route.RouteId,
                RouteDestinationId = (long)barCodeAddress.RouteDestinationId,
                Barcodes = new BarcodeDataRequest[]
                {
                    new BarcodeDataRequest()
                    {
                        Barcode = "some barcode",
                        Latitude = barCodeAddress.Latitude,
                        Longitude = barCodeAddress.Longitude,
                        TimestampDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.Now),
                        TimestampUtc = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow),
                        ScannedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        ScanType = "QR-code"
                    }
                }
            };

            var result = await route4Me.SaveAddressBarcodesAsync(barCodeParams);

            Console.WriteLine(
                result.Item1.status ? "The barcode saved" : "Cannot save the barcode"
            );

            RemoveTestOptimizations();
        }
    }
}