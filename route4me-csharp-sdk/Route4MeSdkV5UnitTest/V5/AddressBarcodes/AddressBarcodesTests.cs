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
        [Test]
        public void AddressBarcodesGetSaveTest()
        {
            var route4Me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var getAddressBarcodesParameters = new GetAddressBarcodesParameters
            {
                RouteId = "893E6C33F0494572DEB2FAE34B2D3E0B",
                RouteDestinationId = 705601646
            };
            var readResult1 = route4Me.GetAddressBarcodes(getAddressBarcodesParameters, out _);
            Assert.That(readResult1.Data, Is.Not.Empty);

            var saveAddressBarcodesResponse = route4Me.SaveAddressBarcodes(new SaveAddressBarcodesParameters
            {
                RouteId = "893E6C33F0494572DEB2FAE34B2D3E0B",
                RouteDestinationId = 705601646,
                Barcodes = new[]
                {
                    new BarcodeDataRequest
                    {
                        Barcode = "TEST2",
                        Latitude = 40.610804,
                        Longitude = -73.920172,
                        TimestampDate = 1634169600,
                        TimestampUtc = 1634198666,
                        ScanType = "picked_up",
                        ScannedAt = "2021-10-15 19:18:11"
                    }
                }
            }, out _);

            Assert.True(saveAddressBarcodesResponse.status);

            var readResult2 = route4Me.GetAddressBarcodes(getAddressBarcodesParameters, out _);

            Assert.True(readResult2.Data.Length - readResult1.Data.Length == 1);
        }

        [Test]
        public async Task AddressBarcodesGetSaveAsyncTest()
        {
            var route4Me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var getAddressBarcodesParameters = new GetAddressBarcodesParameters
            {
                RouteId = "893E6C33F0494572DEB2FAE34B2D3E0B",
                RouteDestinationId = 705601646
            };
            var readResult1 = await route4Me.GetAddressBarcodesAsync(getAddressBarcodesParameters);
            Assert.That(readResult1.Item1.Data, Is.Not.Empty);

            var saveAddressBarcodesResponse = await route4Me.SaveAddressBarcodesAsync(new SaveAddressBarcodesParameters
            {
                RouteId = "893E6C33F0494572DEB2FAE34B2D3E0B",
                RouteDestinationId = 705601646,
                Barcodes = new[]
                {
                    new BarcodeDataRequest
                    {
                        Barcode = "TEST2",
                        Latitude = 40.610804,
                        Longitude = -73.920172,
                        TimestampDate = 1634169600,
                        TimestampUtc = 1634198666,
                        ScanType = "picked_up",
                        ScannedAt = "2021-10-15 19:18:11"
                    }
                }
            });

            Assert.True(saveAddressBarcodesResponse.Item1.status);

            var readResult2 = await route4Me.GetAddressBarcodesAsync(getAddressBarcodesParameters);

            Assert.True(readResult2.Item1.Data.Length - readResult1.Item1.Data.Length == 1);
        }
    }
}