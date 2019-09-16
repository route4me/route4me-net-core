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
    public class OptimizationBuilder
    {
        public OptimizationProblem _optimization;
        public readonly OptimizationTestData optimizationTestData;
        string testDataFile;
        JsonSerializerSettings settings;

        public class OptimizationTestData
        {

        }

        public OptimizationBuilder()
        {
            optimizationTestData = new OptimizationTestData();
            settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            settings.NullValueHandling = NullValueHandling.Ignore;
        }

        public OptimizationProblem OptimizationWith24Stops()
        {
            testDataFile = @"TestData/mdmd_optimization_24stops_RESPONSE.json";

            StreamReader reader = new StreamReader(testDataFile);
            String jsonContent = reader.ReadToEnd();
            //jsonContent = "{" + jsonContent + "}";
            reader.Close();

            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            settings.NullValueHandling = NullValueHandling.Ignore;

            var importedOptimization = JsonConvert.DeserializeObject<DataObject>(jsonContent);

            _optimization = getOptimizationEntity(importedOptimization);

            return _optimization;
        }

        public OptimizationProblem OptimizationWithSingleDriverRoundtrip()
        {
            testDataFile = @"TestData/sdrt_optimization_RESPONSE.json";

            StreamReader reader = new StreamReader(testDataFile);
            String jsonContent = reader.ReadToEnd();
            reader.Close();

            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            settings.NullValueHandling = NullValueHandling.Ignore;

            var importedOptimization = JsonConvert.DeserializeObject<DataObject>(jsonContent);

            _optimization = getOptimizationEntity(importedOptimization);

            return _optimization;
        }

        public OptimizationProblem OptimizationWith10Stops()
        {
            testDataFile = @"TestData/sd_optimization_10stops_RESPONSE.json";

            StreamReader reader = new StreamReader(testDataFile);
            String jsonContent = reader.ReadToEnd();
            reader.Close();

            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            settings.NullValueHandling = NullValueHandling.Ignore;
            //string jsonContent = File.ReadAllText(testDataFile);

            var importedOptimization = JsonConvert.DeserializeObject<DataObject>(jsonContent);

            _optimization = getOptimizationEntity(importedOptimization);

            return _optimization;
        }

        public OptimizationProblem OptimizationWithCustomFields()
        {
            testDataFile = @"TestData/optimization_with_custom_fields.json";

            StreamReader reader = new StreamReader(testDataFile);
            String jsonContent = reader.ReadToEnd();
            reader.Close();

            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            settings.NullValueHandling = NullValueHandling.Ignore;
            //string jsonContent = File.ReadAllText(testDataFile);

            var importedOptimization = JsonConvert.DeserializeObject<DataObjectRoute>(jsonContent);

            _optimization = getOptimizationEntity(importedOptimization);

            return _optimization;
        }

        private OptimizationProblem getOptimizationEntity(DataObject optimizationObject)
        {
            var optimization = new OptimizationProblem();

            var optimizationEntityProperties = typeof(OptimizationProblem).GetProperties().Select(x => x.Name);

            foreach (PropertyInfo prop in typeof(DataObject).GetProperties())
            {
                if (optimizationEntityProperties.Contains(prop.Name))
                {
                    if (prop.GetValue(optimizationObject)==null) continue;

                    switch (prop.Name)
                    {
                        case "Parameters":
                            optimization.Parameters = JsonConvert.SerializeObject(optimizationObject.Parameters, settings);
                            break;
                        case "Links":
                            optimization.Links = JsonConvert.SerializeObject(optimizationObject.Links, settings);
                            break;
                        case "UserErrors":
                            optimization.UserErrors = JsonConvert.SerializeObject(optimizationObject.UserErrors, settings);
                            break;
                        //case "TrackingHistory":
                        //    optimization.TrackingHistory = JsonConvert.SerializeObject(optimizationObject.TrackingHistory, settings);
                        //    break;
                        //case "Path":
                        //    optimization.Path = JsonConvert.SerializeObject(optimizationObject.Path, settings);
                        //    break;
                        case "Addresses":
                            optimization.Addresses = new List<AddressEntity>();
                            foreach (var address in optimizationObject.Addresses)
                            {
                                optimization.Addresses.Add(getAddressEntity(address));
                            }
                            break;
                        case "Routes":
                            optimization.Routes = new List<RouteEntity>();
                            foreach (var route in optimizationObject.Routes)
                            {
                                optimization.Routes.Add(getRouteEntity(route));
                            }
                            break;
                        default:
                            typeof(OptimizationProblem).GetProperties().Where(x => x.Name == prop.Name).FirstOrDefault()
                            .SetValue(optimization, prop.GetValue(optimizationObject));
                            break;
                    }
                }
            }

            return optimization;
        }

        private RouteEntity getRouteEntity(RouteTable routeObject)
        {
            var route = new RouteEntity();

            var routeEntityProperties = typeof(RouteEntity).GetProperties().Select(x => x.Name);

            var routeTableProperties = typeof(RouteTable).GetProperties();

            foreach (PropertyInfo prop in routeTableProperties)
            {
                if (routeEntityProperties.Contains(prop.Name))
                {
                    if (prop.GetValue(routeObject) == null) continue;

                    switch (prop.Name)
                    {
                        case "Parameters":
                            route.Parameters = JsonConvert.SerializeObject(routeObject.Parameters, settings);
                            break;
                        case "Addresses":
                            route.Addresses = new List<AddressEntity>();
                            foreach (var address in routeObject.Addresses)
                            {
                                route.Addresses.Add(getAddressEntity(address));
                            }
                            break;
                        case "Notes":
                            var notes = prop.GetValue(routeObject) as AddressNoteTable[];
                            foreach (var note in notes)
                            {
                                var noteEntity = getAddressNoteEntity(note);
                                if (noteEntity != null)
                                {
                                    if (route.Notes == null) route.Notes = new List<AddressNoteEntity>();
                                    route.Notes.Add(noteEntity);
                                }
                            }
                            break;
                        //case "Path":
                        //    route.Path = JsonConvert.SerializeObject(routeObject.Path);
                        //    break;
                        //case "TrackingHistory":
                        //    route.TrackingHistory = JsonConvert.SerializeObject(routeObject.TrackingHistory, settings);
                        //    break;
                        case "Links":
                            route.Links = JsonConvert.SerializeObject(routeObject.Links, settings);
                            break;
                        case "MemberConfigStorage":
                            route.MemberConfigStorage = JsonConvert.SerializeObject(routeObject.MemberConfigStorage, settings);
                            break;
                        case "Vehicle":
                            route.Vehicle = JsonConvert.SerializeObject(routeObject.Vehicle, settings);
                            break;
                        case "GeofencePolygonType":
                            EnumR4M.TerritoryType geofencePolygonType;
                            if (System.Enum.TryParse(routeObject.GeofencePolygonType, out geofencePolygonType))
                                route.GeofencePolygonType = geofencePolygonType;
                            break;
                        default:
                            typeof(RouteEntity).GetProperties().Where(x => x.Name == prop.Name).FirstOrDefault()
                            .SetValue(route, prop.GetValue(routeObject));
                            break;
                    }
                }
            }

            return route;
        }

        private AddressEntity getAddressEntity(AddressTable addressObject)
        {
            var address = new AddressEntity();

            var addressEntityProperties = typeof(AddressEntity).GetProperties().Select(x => x.Name);

            foreach (PropertyInfo prop in typeof(AddressTable).GetProperties())
            {
                if (addressEntityProperties.Contains(prop.Name))
                {
                    if (prop.GetValue(addressObject) == null) continue;

                    switch (prop.Name)
                    {
                        case "Notes":
                            var notes = prop.GetValue(addressObject) as AddressNoteTable[];
                            foreach (var note in notes)
                            {
                                var noteEntity = getAddressNoteEntity(note);
                                if (noteEntity != null)
                                {
                                    if (address.Notes==null) address.Notes = new List<AddressNoteEntity>();
                                    address.Notes.Add(noteEntity);
                                }
                            }
                            break;
                        case "Geocodings":

                            break;
                        case "Manifest":
                            address.Manifest = JsonConvert.SerializeObject(addressObject.Manifest, settings);
                            break;
                        case "CustomFields":
                            address.CustomFields = JsonConvert.SerializeObject(addressObject.CustomFields, settings);
                            break;
                        case "CustomFieldsConfig":
                            address.CustomFieldsConfig = JsonConvert.SerializeObject(addressObject.CustomFieldsConfig, settings);
                            break;
                        case "MemberId":
                            address.MemberId = Convert.ToInt32(addressObject.MemberId);
                            break;
                        case "AddressStopType":
                            EnumR4M.AddressStopType addressStopType;
                            if (System.Enum.TryParse(addressObject.AddressStopType, out addressStopType))
                                address.AddressStopType = addressStopType;
                            break;
                        default:
                            typeof(AddressEntity).GetProperties().Where(x => x.Name == prop.Name).FirstOrDefault()
                            .SetValue(address, prop.GetValue(addressObject));
                            break;
                    }
                }
            }

            return address;
        }

        private AddressNoteEntity  getAddressNoteEntity(AddressNoteTable addressNoteObject)
        {
            var addressNote = new AddressNoteEntity();

            var addressNoteEntityProperties = typeof(AddressNoteEntity).GetProperties().Select(x => x.Name);

            foreach (PropertyInfo prop in typeof(AddressNoteTable).GetProperties())
            {
                if (addressNoteEntityProperties.Contains(prop.Name))
                {
                    if (prop.GetValue(addressNoteObject) == null) continue;

                    switch (prop.Name)
                    {
                        case "CustomTypes":
                            addressNote.CustomTypes = JsonConvert.SerializeObject(addressNoteObject.CustomTypes, settings);
                            break;
                        case "ActivityType":
                            EnumR4M.StatusUpdateType statusUpdateType;
                            if (System.Enum.TryParse(addressNoteObject.ActivityType, out statusUpdateType))
                                addressNote.StatusUpdateType = statusUpdateType;
                            break;
                        case "UploadType":
                            EnumR4M.UploadType uploadType;
                            if (System.Enum.TryParse(addressNoteObject.UploadType, out uploadType))
                                addressNote.UploadType = uploadType;
                            break;
                        case "DeviceType":
                            EnumR4M.DeviceType devicedType;
                            if (System.Enum.TryParse(addressNoteObject.DeviceType, out devicedType))
                                addressNote.DeviceType = devicedType;
                            break;
                        default:
                            typeof(AddressNoteEntity).GetProperties().Where(x => x.Name == prop.Name).FirstOrDefault()
                            .SetValue(addressNote, prop.GetValue(addressNoteObject));
                            break;
                    }
                }
            }

            return addressNote;
        }
    }
}
