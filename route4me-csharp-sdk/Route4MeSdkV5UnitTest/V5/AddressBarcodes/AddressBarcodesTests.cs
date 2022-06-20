using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSdkV5UnitTest.V5.AddressBarcodes
{
    [TestFixture]
    public class AddressBarcodesTests
    {
        private static TestDataRepository tdr;
        private static List<string> lsOptimizationIDs;

        Address barCodeAddress;

        [SetUp]
        public void Setup()
        {
            lsOptimizationIDs = new List<string>();

            tdr = new TestDataRepository();

            var result = tdr.RunOptimizationSingleDriverRoute10Stops();
            Assert.True(result, "Single Driver 10 Stops generation failed.");

            barCodeAddress = tdr.SD10Stops_route.Addresses[1];

            lsOptimizationIDs.Add(tdr.SD10Stops_optimization_problem_id);

            SaveAddressBacodeTest();
        }

        [TearDown]
        public void TearDown()
        {
            var optimizationResult = tdr.RemoveOptimization(lsOptimizationIDs.ToArray());

            Assert.True(optimizationResult, "Removing of the testing optimization problem failed.");
        }

        [Test]
        public void SaveAddressBacodeTest()
        {
            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var barCodeParams = new SaveAddressBarcodesParameters()
            {
                RouteId = tdr.SD10Stops_route_id,
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

            var result = route4me.SaveAddressBarcodes(barCodeParams, out ResultResponse resultResponse);


            Assert.True(result?.status ?? false);
        }

        [Test]
        public void GetAddressBarcodesTest()
        {
            // The test requires special subscription.
            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var getAddressBarcodesParameters = new GetAddressBarcodesParameters
            {
                RouteId = tdr.SD10Stops_route_id,
                RouteDestinationId = (long)barCodeAddress.RouteDestinationId,
                Limit = 10
            };

            var readResult1 = route4me.GetAddressBarcodes(getAddressBarcodesParameters, out var resultResponse);

            Assert.IsNotEmpty(readResult1.Data);
        }

        [Test]
        public async Task GetAddressBarcodesAsyncTest()
        {
            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var getAddressBarcodesParameters = new GetAddressBarcodesParameters
            {
                RouteId = tdr.SD10Stops_route_id,
                RouteDestinationId = (long)barCodeAddress.RouteDestinationId,
                Limit = 10
            };

            var readResult1 = await route4me.GetAddressBarcodesAsync(getAddressBarcodesParameters);

            Assert.IsNotEmpty(readResult1.Item1.Data);
        }

    }
}