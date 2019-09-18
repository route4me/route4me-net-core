using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Route4MeDB.ApplicationCore.Entities.RouteAggregate;
using Route4MeDB.ApplicationCore.Entities.RouteAddressAggregate;
using RouteEntity = Route4MeDB.ApplicationCore.Entities.RouteAggregate.Route;
using AddressEntity = Route4MeDB.ApplicationCore.Entities.RouteAddressAggregate.Address;
using AddressNoteEntity = Route4MeDB.ApplicationCore.Entities.RouteAddressAggregate.AddressNote;
using Route4MeDB.ApplicationCore;
using Route4MeDbLibrary;
using EnumR4M = Route4MeDB.ApplicationCore.Enum;
using Route4MeDB.ApplicationCore.Services;
using System.Linq;
using System.IO;
using Newtonsoft.Json.Linq;
using Route4MeSDK.DataTypes;
using RouteTable = Route4MeSDK.DataTypes.DataObjectRoute;
using AddressTable = Route4MeSDK.DataTypes.Address;
using AddressNoteTable = Route4MeSDK.DataTypes.AddressNote;
using System.Reflection;

namespace Route4MeDB.UnitTests.Builders
{
    public class RouteBuilder
    {
        public Route _route;
        //public readonly OptimizationTestData optimizationTestData;
        string testDataFile;
        JsonSerializerSettings settings;
        OptimizationBuilder optimizationBuilder = new OptimizationBuilder();

        public RouteBuilder()
        {
            //optimizationTestData = new OptimizationTestData();
            settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            settings.NullValueHandling = NullValueHandling.Ignore;
        }

        public Route RouteWithAllParameters()
        {
            testDataFile = @"TestData/route_with_all_parameters.json";

            StreamReader reader = new StreamReader(testDataFile);
            String jsonContent = reader.ReadToEnd();
            //jsonContent = "{" + jsonContent + "}";
            reader.Close();

            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            settings.NullValueHandling = NullValueHandling.Ignore;

            var importedRoute = JsonConvert.DeserializeObject<DataObjectRoute>(jsonContent);

            _route = optimizationBuilder.getRouteEntity(importedRoute);

            return _route;
        }

        public Route RouteWith10Stops()
        {
            testDataFile = @"TestData/route_plain_with_10_stops.json";

            StreamReader reader = new StreamReader(testDataFile);
            var jsonContent = reader.ReadToEnd();
            reader.Close();

            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            settings.NullValueHandling = NullValueHandling.Ignore;

            var importedRoute = JsonConvert.DeserializeObject<DataObjectRoute>(jsonContent);

            _route = optimizationBuilder.getRouteEntity(importedRoute);

            return _route;
        }

        public Route RouteWith97Stops()
        {
            testDataFile = @"TestData/route_with_97_stops.json";

            StreamReader reader = new StreamReader(testDataFile);
            var jsonContent = reader.ReadToEnd();
            reader.Close();

            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            settings.NullValueHandling = NullValueHandling.Ignore;

            var importedRoute = JsonConvert.DeserializeObject<DataObjectRoute>(jsonContent);

            _route = optimizationBuilder.getRouteEntity(importedRoute);

            return _route;
        }
    }
}
