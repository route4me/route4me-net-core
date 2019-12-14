using System;
using Newtonsoft.Json;
using Route4MeDB.Route4MeDbLibrary;
using Route4MeDB.ApplicationCore.Entities.RouteAggregate;
using RouteEntity = Route4MeDB.ApplicationCore.Entities.RouteAggregate.Route;
using System.IO;
using Route4MeSDK.DataTypes;

namespace Route4MeDB.UnitTests.Builders
{
    public class RouteBuilder
    {
        public Route _route;
        string testDataFile;
        JsonSerializerSettings settings;

        DataExchangeHelper dataExchange = new DataExchangeHelper();

        public RouteBuilder()
        {
            settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            settings.NullValueHandling = NullValueHandling.Ignore;
        }

        public Route RouteWithAllParameters()
        {
            testDataFile = @"TestData/route_with_all_parameters.json";

            using (StreamReader reader = new StreamReader(testDataFile))
            {
                var jsonContent = reader.ReadToEnd();
                reader.Close();

                _route = dataExchange.ConvertSdkJsonContentToEntity<RouteEntity>(jsonContent, out string errorString);

                if (_route != null) Console.WriteLine(errorString);
            }

            return _route;
        }

        public Route RouteWith10Stops()
        {
            testDataFile = @"TestData/route_plain_with_10_stops.json";

            using (StreamReader reader = new StreamReader(testDataFile))
            {
                var jsonContent = reader.ReadToEnd();
                reader.Close();

                _route = dataExchange.ConvertSdkJsonContentToEntity<RouteEntity>(jsonContent, out string errorString);

                if (_route != null) Console.WriteLine(errorString);
            }

            return _route;
        }

        public Route RouteWith97Stops()
        {
            testDataFile = @"TestData/route_with_97_stops.json";

            using (StreamReader reader = new StreamReader(testDataFile))
            {
                var jsonContent = reader.ReadToEnd();
                reader.Close();

                settings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    NullValueHandling = NullValueHandling.Ignore
                };

                var importedRoute = JsonConvert.DeserializeObject<DataObjectRoute>(jsonContent);

                _route = dataExchange.ConvertSdkJsonContentToEntity<RouteEntity>(jsonContent, out string errorString);

                if (_route != null) Console.WriteLine(errorString);
            }

            return _route;
        }
    }
}
