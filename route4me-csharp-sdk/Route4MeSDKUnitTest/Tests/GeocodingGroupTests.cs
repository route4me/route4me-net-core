using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.FastProcessing;
using Route4MeSDK.QueryTypes;
using Route4MeSDKUnitTest.Types;

namespace Route4MeSDKUnitTest.Tests
{
    [TestFixture]
    public class GeocodingGroupTests
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;

        [Test]
        public void GeocodingForwardTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var geoParams = new GeocodingParameters
            {
                Addresses = "Los Angeles International Airport, CA||3495 Purdue St, Cuyahoga Falls, OH 44221",
                ExportFormat = "json"
            };

            //Run the query
            var result = route4Me.Geocoding(geoParams, out var errorString);

            Assert.IsNotNull(result, "GeocodingForwardTest failed. " + errorString);
        }

        [Test]
        public void BatchGeocodingForwardTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var geoParams = new GeocodingParameters
            {
                Addresses = "Los Angeles International Airport, CA\n3495 Purdue St, Cuyahoga Falls, OH 44221",
                ExportFormat = "json"
            };

            //Run the query
            var result = route4Me.BatchGeocoding(geoParams, out var errorString);

            Assert.IsNotNull(result, "GeocodingForwardTest failed. " + errorString);
        }

        [Test]
        public async Task BatchGeocodingForwardAsyncTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var geoParams = new GeocodingParameters
            {
                Addresses = "Los Angeles International Airport, CA\n3495 Purdue St, Cuyahoga Falls, OH 44221",
                ExportFormat = "json"
            };

            //Run the query
            var result = await route4Me.BatchGeocodingAsync(geoParams).ConfigureAwait(false);

            Assert.IsNotNull(result.Item1, "GeocodingForwardTest failed. " + result.Item2);
        }


        [Test]
        public void UploadAndGeocodeLargeJsonFile()
        {
            var fastProcessing = new FastBulkGeocoding(c_ApiKey);
            var lsGeocodedAddressTotal = new List<AddressGeocoded>();
            var lsAddresses = new List<string>();

            var addressesInFile = 13;

            fastProcessing.GeocodingIsFinished += (sender, e) =>
            {
                Assert.IsNotNull(lsAddresses, "Geocoding process failed");

                Assert.AreEqual(
                    addressesInFile,
                    lsAddresses.Count,
                    "Not all the addresses were geocoded");

                Console.WriteLine("Large addresses file geocoding is finished");
            };

            fastProcessing.AddressesChunkGeocoded += (sender, e) =>
            {
                if (e.lsAddressesChunkGeocoded != null)
                    foreach (var addr1 in e.lsAddressesChunkGeocoded)
                        lsAddresses.Add(addr1.GeocodedAddress.AddressString);

                Console.WriteLine("Total Geocoded Addresses -> " + lsAddresses.Count);
            };

            var stPath = AppDomain.CurrentDomain.BaseDirectory;
            fastProcessing.UploadAndGeocodeLargeJsonFile(
                stPath + @"\Data\JSON\batch_socket_upload_error_addresses_data_5.json");
        }

        private void FastProcessing_AddressesChunkGeocoded(object sender,
            FastBulkGeocoding.AddressesChunkGeocodedArgs e)
        {
            if (e.lsAddressesChunkGeocoded != null)
                Console.WriteLine("Geocoded addresses " + e.lsAddressesChunkGeocoded.Count);
        }

        [Test]
        public void RapidStreetDataAllTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var geoParams = new GeocodingParameters();

            // Run the query
            var result = route4Me.RapidStreetData(geoParams, out var errorString);

            Assert.IsNotNull(result, "RapidStreetDataAllTest failed. " + errorString);
        }

        [Test]
        public void RapidStreetDataLimitedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var geoParams = new GeocodingParameters
            {
                Offset = 10,
                Limit = 10
            };

            // Run the query
            var result = route4Me.RapidStreetData(geoParams, out var errorString);

            Assert.IsNotNull(result, "RapidStreetDataLimitedTest failed. " + errorString);
        }

        [Test]
        public void RapidStreetDataSingleTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var geoParams = new GeocodingParameters
            {
                Pk = 4
            };

            // Run the query
            var result = route4Me.RapidStreetData(geoParams, out var errorString);

            Assert.IsNotNull(result, "RapidStreetDataSingleTest failed. " + errorString);
        }

        [Test]
        public void RapidStreetServiceAllTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var geoParams = new GeocodingParameters
            {
                Zipcode = "00601",
                Housenumber = "17"
            };

            // Run the query
            var result = route4Me.RapidStreetService(geoParams, out var errorString);

            Assert.IsNotNull(result, "RapidStreetServiceAllTest failed. " + errorString);
        }

        [Test]
        public void RapidStreetServiceLimitedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var geoParams = new GeocodingParameters
            {
                Zipcode = "00601",
                Housenumber = "17",
                Offset = 1,
                Limit = 10
            };

            // Run the query
            var result = route4Me.RapidStreetService(geoParams, out var errorString);

            Assert.IsNotNull(
                result,
                "RapidStreetServiceLimitedTest failed. " + errorString);
        }

        [Test]
        public void RapidStreetZipcodeAllTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var geoParams = new GeocodingParameters
            {
                Zipcode = "00601"
            };

            // Run the query
            var result = route4Me.RapidStreetZipcode(geoParams, out var errorString);

            Assert.IsNotNull(
                result,
                "RapidStreetZipcodeAllTest failed. " + errorString);
        }

        [Test]
        public void RapidStreetZipcodeLimitedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var geoParams = new GeocodingParameters
            {
                Zipcode = "00601",
                Offset = 1,
                Limit = 10
            };

            // Run the query
            var result = route4Me.RapidStreetZipcode(geoParams, out var errorString);

            Assert.IsNotNull(
                result,
                "RapidStreetZipcodeLimitedTest failed. " + errorString);
        }

        [Test]
        public void ReverseGeocodingTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var geoParams = new GeocodingParameters
            {
                Addresses = "41.00367151,-81.59846105"
            };

            geoParams.ExportFormat = "json";

            // Run the query
            var result = route4Me.Geocoding(geoParams, out var errorString);

            Assert.IsNotNull(result, "ReverseGeocodingTest failed. " + errorString);
        }
    }
}