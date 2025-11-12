using System;
using System.Collections.Generic;

using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Bundling Using Multiple Fields
        /// </summary>
        public void BundlingUsingMultipleFields()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var routeParameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.CVRP_TW_SD,
                RouteName = $"Single Depot, Multiple Driver, Bundling Multiple Fields {DateTime.Now}",
                TravelMode = TravelMode.Driving.Description(),
                Bundling = new AddressBundling()
                {
                    Mode = AddressBundlingMode.MultipleFields,
                    ModeParams = new string[]
                    {
                        "custom_fields.BUNDLING_KEY",
                        "custom_fields.LOCATION_ID"
                    },
                    MergeMode = AddressBundlingMergeMode.KeepAsSeparateDestinations,
                    ServiceTimeRules = new ServiceTimeRulesClass()
                    {
                        FirstItemMode = AddressBundlingFirstItemMode.CustomTime,
                        FirstItemModeParams = new int[] { 900 },
                        AdditionalItemsMode = AddressBundlingAdditionalItemsMode.CustomTime,
                        AdditionalItemsModeParams = new int[] { 200 }
                    }
                }
            };

            var addresses = new List<Address>()
            {
                new Address()
                {
                    AddressString = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                    IsDepot = true,
                    Latitude = 38.141598,
                    Longitude = -85.793846,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "1407 MCCOY, Louisville, KY, 40215",
                    Latitude = 38.202496,
                    Longitude = -85.786514,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "730 CECIL AVENUE, Louisville, KY, 40211",
                    Latitude = 38.248684,
                    Longitude = -85.821121,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "650 SOUTH 29TH ST UNIT 315, Louisville, KY, 40211",
                    Latitude = 38.251923,
                    Longitude = -85.800034,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "4629 HILLSIDE DRIVE, Louisville, KY, 40216",
                    Latitude = 38.176067,
                    Longitude = -85.824638,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "4738 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.179806,
                    Longitude = -85.775558,
                    Time = 300,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "LOCATION_ID", "FLOOR001" },
                        { "BUNDLING_KEY", "ZONE001" }
                    }
                },
                new Address()
                {
                    AddressString = "4738 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.179806,
                    Longitude = -85.775558,
                    Time = 300,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "LOCATION_ID", "FLOOR001" },
                        { "BUNDLING_KEY", "ZONE001" }
                    }
                },
                new Address()
                {
                    AddressString = "4738 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.179806,
                    Longitude = -85.775558,
                    Time = 300,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "LOCATION_ID", "FLOOR003" },
                        { "BUNDLING_KEY", "ZONE001" }
                    }
                },
                new Address()
                {
                    AddressString = "4738 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.179806,
                    Longitude = -85.775558,
                    Time = 300,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "LOCATION_ID", "FLOOR003" },
                        { "BUNDLING_KEY", "ZONE001" }
                    }
                },
                new Address()
                {
                    AddressString = "4738 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.179806,
                    Longitude = -85.775558,
                    Time = 300,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "LOCATION_ID", "FLOOR003" },
                        { "BUNDLING_KEY", "ZONE001" }
                    }
                },
                new Address()
                {
                    AddressString = "4738 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.179806,
                    Longitude = -85.775558,
                    Time = 300,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "LOCATION_ID", "FLOOR003" },
                        { "BUNDLING_KEY", "ZONE001" }
                    }
                },
                new Address()
                {
                    AddressString = "318 SO. 39TH STREET, Louisville, KY, 40212",
                    Latitude = 38.259335,
                    Longitude = -85.815094,
                    Time = 300,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "LOCATION_ID", "FLOOR001" },
                        { "BUNDLING_KEY", "ZONE002" }
                    }
                },
                new Address()
                {
                    AddressString = "318 SO. 39TH STREET, Louisville, KY, 40212",
                    Latitude = 38.259335,
                    Longitude = -85.815094,
                    Time = 300,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "LOCATION_ID", "FLOOR001" },
                        { "BUNDLING_KEY", "ZONE002" }
                    }
                },
                new Address()
                {
                    AddressString = "318 SO. 39TH STREET, Louisville, KY, 40212",
                    Latitude = 38.259335,
                    Longitude = -85.815094,
                    Time = 300,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "LOCATION_ID", "FLOOR004" },
                        { "BUNDLING_KEY", "ZONE002" }
                    }
                },
                new Address()
                {
                    AddressString = "318 SO. 39TH STREET, Louisville, KY, 40212",
                    Latitude = 38.259335,
                    Longitude = -85.815094,
                    Time = 300,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "LOCATION_ID", "FLOOR004" },
                        { "BUNDLING_KEY", "ZONE002" }
                    }
                },
                new Address()
                {
                    AddressString = "1324 BLUEGRASS AVE, Louisville, KY, 40215",
                    Latitude = 38.179253,
                    Longitude = -85.785118,
                    Time = 300,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "LOCATION_ID", "FLOOR001" },
                        { "BUNDLING_KEY", "ZONE003" }
                    }
                },
                new Address()
                {
                    AddressString = "1324 BLUEGRASS AVE, Louisville, KY, 40215",
                    Latitude = 38.179253,
                    Longitude = -85.785118,
                    Time = 300,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "LOCATION_ID", "FLOOR001" },
                        { "BUNDLING_KEY", "ZONE003" }
                    }
                },
                new Address()
                {
                    AddressString = "1324 BLUEGRASS AVE, Louisville, KY, 40215",
                    Latitude = 38.179253,
                    Longitude = -85.785118,
                    Time = 300,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "LOCATION_ID", "FLOOR002" },
                        { "BUNDLING_KEY", "ZONE003" }
                    }
                },
                new Address()
                {
                    AddressString = "1324 BLUEGRASS AVE, Louisville, KY, 40215",
                    Latitude = 38.179253,
                    Longitude = -85.785118,
                    Time = 300,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "LOCATION_ID", "FLOOR002" },
                        { "BUNDLING_KEY", "ZONE003" }
                    }
                },
                new Address()
                {
                    AddressString = "1324 BLUEGRASS AVE, Louisville, KY, 40215",
                    Latitude = 38.179253,
                    Longitude = -85.785118,
                    Time = 300,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "LOCATION_ID", "FLOOR003" },
                        { "BUNDLING_KEY", "ZONE003" }
                    }
                },
                new Address()
                {
                    AddressString = "1324 BLUEGRASS AVE, Louisville, KY, 40215",
                    Latitude = 38.179253,
                    Longitude = -85.785118,
                    Time = 300,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "LOCATION_ID", "FLOOR003" },
                        { "BUNDLING_KEY", "ZONE003" }
                    }
                },
                new Address()
                {
                    AddressString = "4805 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.162472,
                    Longitude = -85.792854,
                    Time = 300,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "LOCATION_ID", "FLOOR001" },
                        { "BUNDLING_KEY", "ZONE004" }
                    }
                },
                new Address()
                {
                    AddressString = "4805 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.162472,
                    Longitude = -85.792854,
                    Time = 300,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "LOCATION_ID", "FLOOR001" },
                        { "BUNDLING_KEY", "ZONE004" }
                    }
                },
                new Address()
                {
                    AddressString = "4805 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.162472,
                    Longitude = -85.792854,
                    Time = 300,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "LOCATION_ID", "FLOOR002" },
                        { "BUNDLING_KEY", "ZONE004" }
                    }
                },
                new Address()
                {
                    AddressString = "4805 BELLEVUE AVE, Louisville, KY, 40215",
                    Alias = "Bundled Zone",
                    Latitude = 38.162472,
                    Longitude = -85.792854,
                    Time = 300,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "LOCATION_ID", "FLOOR002" },
                        { "BUNDLING_KEY", "ZONE004" }
                    }
                }
            };


            var optimizationParameters = new OptimizationParameters()
            {
                Parameters = routeParameters,
                Addresses = addresses.ToArray()
            };

            var dataObject = route4Me.RunOptimization(optimizationParameters, out string errorString);

            OptimizationsToRemove = new List<string>()
            {
                dataObject?.OptimizationProblemId ?? null
            };

            // Output the result
            PrintExampleOptimizationResult(dataObject, errorString);

            RemoveTestOptimizations();
        }
    }
}