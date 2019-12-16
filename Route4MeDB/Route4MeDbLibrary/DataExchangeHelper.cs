using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Route4MeDB.ApplicationCore.Entities.RouteAggregate;
using Route4MeDB.ApplicationCore.Entities.RouteAddressAggregate;
using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;
using Route4MeDB.ApplicationCore.Entities.OrderAggregate;
using AddressBookContactEntity = Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate.AddressBookContact;
using ScheduleEntity = Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate.Schedule;
using OrderEntity = Route4MeDB.ApplicationCore.Entities.OrderAggregate.Order;
using OrderCustomFieldEntity = Route4MeDB.ApplicationCore.Entities.OrderAggregate.OrderCustomField;
using RouteEntity = Route4MeDB.ApplicationCore.Entities.RouteAggregate.Route;
using RouteParametersEntity = Route4MeDB.ApplicationCore.Entities.RouteAggregate.RouteParameters;
using AddressEntity = Route4MeDB.ApplicationCore.Entities.RouteAddressAggregate.Address;
using AddressNoteEntity = Route4MeDB.ApplicationCore.Entities.RouteAddressAggregate.AddressNote;
using AddressManifestEntity = Route4MeDB.ApplicationCore.Entities.RouteAddressAggregate.AddressManifest;
using GeocodingEntity = Route4MeDB.ApplicationCore.Entities.GeocodingAggregate.Geocoding;
using PathToNextEntity = Route4MeDB.ApplicationCore.Entities.RouteAddressAggregate.PathToNext;
using PathEntity = Route4MeDB.ApplicationCore.Entities.RouteAggregate.Path;
using TrackingHistoryEntity = Route4MeDB.ApplicationCore.Entities.RouteAggregate.TrackingHistory;
using DirectionEntity = Route4MeDB.ApplicationCore.Entities.RouteAggregate.Direction;
using LocationEntity = Route4MeDB.ApplicationCore.Entities.RouteAggregate.DirectionLocation;
using StepEntity = Route4MeDB.ApplicationCore.Entities.RouteAggregate.DirectionStep;
using RouteVehicleEntity = Route4MeDB.ApplicationCore.Entities.RouteAggregate.RouteVehicle;
using Route4MeDB.ApplicationCore;
using Route4MeDB.Route4MeDbLibrary;
using EnumR4M = Route4MeDB.ApplicationCore.Enum;
using Route4MeDB.ApplicationCore.Services;
using System.Linq;
using System.IO;
using Newtonsoft.Json.Linq;
using Route4Me = Route4MeSDK.DataTypes;
using RouteTable = Route4MeSDK.DataTypes.DataObjectRoute;
using RouteVehicleTable = Route4MeSDK.DataTypes.VehicleV4Response;
using AddressTable = Route4MeSDK.DataTypes.Address;
using AddressNoteTable = Route4MeSDK.DataTypes.AddressNote;
using TrackingHistoryTable = Route4MeSDK.DataTypes.TrackingHistory;
using DirectionTable = Route4MeSDK.DataTypes.Direction;
using AddressBookContactTable = Route4MeSDK.DataTypes.AddressBookContact;
using OrderTable = Route4MeSDK.DataTypes.Order;
using OrderCustomFieldTable = Route4MeSDK.DataTypes.OrderCustomField;
using System.Reflection;

namespace Route4MeDB.Route4MeDbLibrary
{
    /// <summary>
    /// The class-helper for exchanging data between the Route4Me system and databases.
    /// </summary>
    public class DataExchangeHelper
    {
        public OptimizationProblem _optimization;
        //public readonly OptimizationTestData optimizationTestData;
        string testDataFile;
        JsonSerializerSettings settings;

        public DataExchangeHelper()
        {
            //optimizationTestData = new OptimizationTestData();
            settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            settings.NullValueHandling = NullValueHandling.Ignore;
        }

        #region // Converting from SDK to Entity

        /// <summary>
        /// Converts JSON content of a Route4Me SDK object (e.g. response from query of optimization/route/address etc) 
        /// to a Route4Me entity.
        /// </summary>
        /// <typeparam name="T">Route4MeDb entity type:
        /// available types: 
        /// OptimizationProblem, Route, Address, AddressNote, Geocoding, TrackingHistory, Direction</typeparam>
        /// <param name="jsonContent">JSON content of the Route4Me object</param>
        /// <param name="errorMessage">Error message in case of failure</param>
        /// <returns>Route4Me entity</returns>
        public T ConvertSdkJsonContentToEntity<T>(string jsonContent, out string errorMessage) where T : class
        {
            T result = default(T);
            errorMessage = string.Empty;

            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            settings.NullValueHandling = NullValueHandling.Ignore;

            try
            {
                if (typeof(T) == typeof(OptimizationProblem))
                {
                    var importedSdkOptimization = JsonConvert.DeserializeObject<Route4Me.DataObject>(jsonContent);
                    result = ConvertSdkOptimizationToEntity((Route4Me.DataObject)importedSdkOptimization) as T;
                }
                else if (typeof(T) == typeof(RouteEntity))
                {
                    var importedSdkRoute = JsonConvert.DeserializeObject<Route4Me.DataObjectRoute>(jsonContent);
                    result = ConvertSdkRouteToEntity((Route4Me.DataObjectRoute)importedSdkRoute) as T;
                }
                else if (typeof(T) == typeof(AddressEntity))
                {
                    var importedSdkAddress = JsonConvert.DeserializeObject<Route4Me.Address>(jsonContent);
                    result = ConvertSdkAddressToEntity((Route4Me.Address)importedSdkAddress) as T;
                }
                else if (typeof(T) == typeof(AddressNoteEntity))
                {
                    var importedSdkAddressNote = JsonConvert.DeserializeObject<Route4Me.AddressNote>(jsonContent);
                    result = ConvertSdkAddressNoteToEntity((Route4Me.AddressNote)importedSdkAddressNote) as T;
                }
                else if (typeof(T) == typeof(GeocodingEntity))
                {
                    var importedSdkGeocoding = JsonConvert.DeserializeObject<Route4Me.Geocoding>(jsonContent);
                    result = ConvertSdkGeocodingToEntity((Route4Me.Geocoding)importedSdkGeocoding) as T;
                }
                else if (typeof(T) == typeof(TrackingHistoryEntity))
                {
                    var importedSdkTrackingHistory = JsonConvert.DeserializeObject<Route4Me.TrackingHistory>(jsonContent);
                    result = ConvertSdkTrackingHistoryToEntity((Route4Me.TrackingHistory)importedSdkTrackingHistory) as T;
                }
                else if (typeof(T) == typeof(DirectionEntity))
                {
                    var importedSdkDirection = JsonConvert.DeserializeObject<Route4Me.Direction>(jsonContent);
                    result = ConvertSdkDirectionToEntity((Route4Me.Direction)importedSdkDirection) as T;
                }
                else if (typeof(T) == typeof(AddressBookContactEntity))
                {
                    var importedSdkContact = JsonConvert.DeserializeObject<Route4Me.AddressBookContact>(jsonContent);
                    result = ConvertSdkAddressBookContactToEntity((Route4Me.AddressBookContact)importedSdkContact) as T;
                }
                else if (typeof(T) == typeof(OrderEntity))
                {
                    var importedSdkOrder = JsonConvert.DeserializeObject<Route4Me.Order>(jsonContent);
                    result = ConvertSdkOrderToEntity((Route4Me.Order)importedSdkOrder) as T;
                }

                return result;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return result;
            }
        }

        /// <summary>
        /// Converts Route4Me SDK optimization object to the Route4MeDb optimization entity.
        /// </summary>
        /// <param name="optimizationObject">Route4Me SDK optimization object</param>
        /// <returns>Route4MeDb optimization entity</returns>
        public OptimizationProblem ConvertSdkOptimizationToEntity(Route4Me.DataObject optimizationObject)
        {
            var optimization = new OptimizationProblem();

            var optimizationEntityProperties = typeof(OptimizationProblem).GetProperties().Select(x => x.Name);

            foreach (PropertyInfo prop in typeof(Route4Me.DataObject).GetProperties())
            {
                if (optimizationEntityProperties.Contains(prop.Name))
                {
                    if (prop.GetValue(optimizationObject) == null) continue;

                    switch (prop.Name)
                    {
                        case "Parameters":
                            optimization.ParametersObj = ConvertSdkRouteParametersToEntity(optimizationObject.Parameters);
                            break;
                        case "Links":
                            optimization.Links = JsonConvert.SerializeObject(optimizationObject.Links, settings);
                            break;
                        case "UserErrors":
                            optimization.UserErrors = JsonConvert.SerializeObject(optimizationObject.UserErrors, settings);
                            break;
                        case "Addresses":
                            optimization.Addresses = new List<AddressEntity>();
                            foreach (var address in optimizationObject.Addresses)
                            {
                                optimization.Addresses.Add(ConvertSdkAddressToEntity(address));
                            }
                            break;
                        case "Routes":
                            optimization.Routes = new List<RouteEntity>();
                            foreach (var route in optimizationObject.Routes)
                            {
                                optimization.Routes.Add(ConvertSdkRouteToEntity(route));
                            }
                            break;
                        default:
                            var pValue = prop.GetValue(optimizationObject);

                            PropertyInfo propInfo = typeof(OptimizationProblem).GetProperties()
                                .Where(x => x.Name == prop.Name).FirstOrDefault();

                            if ((pValue?.GetType() ?? null) != (propInfo?.PropertyType ?? null))
                            {

                                var convertedValue = Route4MeDB.Route4MeDbLibrary.Utils.ConvertObjectToPropertyType(pValue, propInfo);
                                propInfo.SetValue(optimization, convertedValue);
                            }
                            else
                            {
                                propInfo.SetValue(optimization, pValue);
                            }
                            break;
                    }
                }
            }

            return optimization;
        }

        /// <summary>
        /// Converts Route4Me SDK route object to the Route4MeDb route entity.
        /// </summary>
        /// <param name="routeObject">Route4Me SDK route object</param>
        /// <returns>Route4MeDb route entity</returns>
        public RouteEntity ConvertSdkRouteToEntity(RouteTable routeObject)
        {
            var route = new RouteEntity();

            var routeEntityProperties = typeof(RouteEntity).GetProperties().Select(x => x.Name);

            var routeTableProperties = typeof(RouteTable).GetProperties();

            foreach (PropertyInfo prop in routeTableProperties)
            {
                //Console.WriteLine(prop.Name + " -> " + prop.PropertyType.ToString());
                if (routeEntityProperties.Contains(prop.Name) || prop.Name == "TrackingHistory" || prop.Name == "Path")
                {
                    if (prop.GetValue(routeObject) == null) continue;

                    switch (prop.Name)
                    {
                        case "Parameters":
                            route.ParametersObj = ConvertSdkRouteParametersToEntity(routeObject.Parameters);
                            break;
                        case "Addresses":
                            if (routeObject.Addresses == null) continue;

                            foreach (var address in routeObject.Addresses)
                            {
                                if (route.Addresses == null) route.Addresses = new List<AddressEntity>();
                                route.Addresses.Add(ConvertSdkAddressToEntity(address));
                            }
                            break;
                        case "Directions":
                            if (routeObject.Directions == null) continue;

                            foreach (var direction in routeObject.Directions)
                            {
                                var directionEntity = ConvertSdkDirectionToEntity(direction);
                                directionEntity.RouteId = routeObject.RouteId;

                                if (directionEntity != null)
                                {
                                    if (route.Directions == null) route.Directions = new List<DirectionEntity>();
                                    route.Directions.Add(directionEntity);
                                }
                            }
                            break;
                        case "Notes":
                            if (routeObject.Notes == null) continue;

                            foreach (var note in routeObject.Notes)
                            {
                                var noteEntity = ConvertSdkAddressNoteToEntity(note);
                                if (noteEntity != null)
                                {
                                    if (route.Notes == null) route.Notes = new List<AddressNoteEntity>();
                                    route.Notes.Add(noteEntity);
                                }
                            }
                            break;
                        case "Path":
                            if (routeObject.Path == null) continue;

                            foreach (var path in routeObject.Path)
                            {
                                if (path == null) continue;

                                var pathEntity = new PathEntity()
                                {
                                    RouteId = route.RouteId,
                                    Lat = path.Lat,
                                    Lng = path.Lng
                                };
                                if (pathEntity != null)
                                {
                                    if (route.Pathes == null) route.Pathes = new List<PathEntity>();
                                    route.Pathes.Add(pathEntity);
                                }
                            }
                            break;
                        case "TrackingHistory":
                            if (routeObject.TrackingHistory == null) continue;

                            foreach (var trackingHistory in routeObject.TrackingHistory)
                            {
                                if (trackingHistory == null) continue;

                                var trackingHistoryEntity = ConvertSdkTrackingHistoryToEntity(trackingHistory);

                                if (trackingHistoryEntity != null)
                                {
                                    if (route.TrackingHistories == null) route.TrackingHistories = new List<TrackingHistoryEntity>();
                                    route.TrackingHistories.Add(trackingHistoryEntity);
                                }
                            }
                            break;
                        case "Links":
                            route.Links = JsonConvert.SerializeObject(routeObject.Links, settings);
                            break;
                        case "MemberConfigStorage":
                            route.MemberConfigStorage = JsonConvert.SerializeObject(routeObject.MemberConfigStorage, settings);
                            break;
                        case "Vehicle":
                            route.Vehicle = JsonConvert.SerializeObject(routeObject.Vehilce, settings);
                            break;
                        case "GeofencePolygonType":
                            EnumR4M.TerritoryType geofencePolygonType;
                            if (System.Enum.TryParse(routeObject.GeofencePolygonType, out geofencePolygonType))
                                route.GeofencePolygonType = geofencePolygonType;
                            break;
                        default:
                            var pValue = prop.GetValue(routeObject);

                            PropertyInfo propInfo = typeof(RouteEntity).GetProperties()
                                .Where(x => x.Name == prop.Name).FirstOrDefault();

                            if ((pValue?.GetType() ?? null) != (propInfo?.PropertyType ?? null))
                            {

                                var convertedValue = Route4MeDB.Route4MeDbLibrary.Utils.ConvertObjectToPropertyType(pValue, propInfo);
                                propInfo.SetValue(route, convertedValue);
                            }
                            else
                            {
                                propInfo.SetValue(route, pValue);
                            }

                            break;
                    }
                }
            }

            return route;
        }

        /// <summary>
        /// Converts Route4Me SDK address object to the Route4MeDb address entity.
        /// </summary>
        /// <param name="addressObject">Route4Me SDK address object</param>
        /// <returns>Route4MeDb address entity</returns>
        public AddressEntity ConvertSdkAddressToEntity(AddressTable addressObject)
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
                            if (addressObject.Notes == null) continue;

                            foreach (var note in addressObject.Notes)
                            {
                                var noteEntity = ConvertSdkAddressNoteToEntity(note);
                                if (noteEntity != null)
                                {
                                    if (address.Notes == null) address.Notes = new List<AddressNoteEntity>();
                                    address.Notes.Add(noteEntity);
                                }
                            }
                            break;
                        case "Geocodings":
                            if (addressObject.Geocodings == null) continue;

                            foreach (var geocoding in addressObject.Geocodings)
                            {
                                if (geocoding != null)
                                {
                                    var geocodingEntity = ConvertSdkGeocodingToEntity(geocoding);
                                    if (geocodingEntity != null)
                                    {
                                        if (address.Geocodings == null) address.Geocodings = new List<GeocodingEntity>();
                                        address.Geocodings.Add(geocodingEntity);
                                    }
                                }
                            }

                            break;
                        case "PathToNext":
                            if (addressObject.PathToNext == null) continue;

                            foreach (var path in addressObject.PathToNext)
                            {
                                if (path == null) continue;

                                var pathEntity = new PathToNext()
                                {
                                    RouteId = address.RouteId,
                                    RouteDestinationId = address.RouteDestinationId,
                                    SequenceNo = address.SequenceNo,
                                    Lat = path.Latitude,
                                    Lng = path.Longitude
                                };
                                if (pathEntity != null)
                                {
                                    if (address.PathToNext == null) address.PathToNext = new List<PathToNextEntity>();
                                    address.PathToNext.Add(pathEntity);
                                }
                            }
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
                            var pValue = prop.GetValue(addressObject);

                            PropertyInfo propInfo = typeof(AddressEntity).GetProperties()
                                .Where(x => x.Name == prop.Name).FirstOrDefault();

                            if ((pValue?.GetType() ?? null) != (propInfo?.PropertyType ?? null))
                            {
                                var convertedValue = Utils.ConvertObjectToPropertyType(pValue, propInfo);
                                propInfo.SetValue(address, convertedValue);
                            }
                            else
                            {
                                propInfo.SetValue(address, pValue);
                            }

                            break;
                    }
                }
            }

            return address;
        }

        /// <summary>
        /// Converts Route4Me SDK address note object to the Route4MeDb address note entity.
        /// </summary>
        /// <param name="addressObject">Route4Me SDK address note object</param>
        /// <returns>Route4MeDb address note entity</returns>
        public AddressNoteEntity ConvertSdkAddressNoteToEntity(AddressNoteTable addressNoteObject)
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
                            var pValue = prop.GetValue(addressNoteObject);

                            PropertyInfo propInfo = typeof(AddressNoteEntity).GetProperties()
                                .Where(x => x.Name == prop.Name).FirstOrDefault();

                            if ((pValue?.GetType() ?? null) != (propInfo?.PropertyType ?? null))
                            {
                                var convertedValue = Utils.ConvertObjectToPropertyType(pValue, propInfo);
                                propInfo.SetValue(addressNote, convertedValue);
                            }
                            else
                            {
                                propInfo.SetValue(addressNote, pValue);
                            }

                            break;
                    }
                }
            }

            return addressNote;
        }

        public RouteParametersEntity ConvertSdkRouteParametersToEntity(Route4Me.RouteParameters routeParametersObject)
        {
            var routeParametersEntity = new RouteParametersEntity();

            var routeParametersEntityProperties = typeof(RouteParametersEntity).GetProperties().Select(x => x.Name);

            foreach (PropertyInfo prop in typeof(Route4Me.RouteParameters).GetProperties())
            {
                if (prop.GetValue(routeParametersObject) == null) continue;

                switch (prop.Name)
                {
                    case "AlgorithmType":
                        EnumR4M.AlgorithmType algorithmType;
                        if (System.Enum.TryParse(routeParametersObject.AlgorithmType.ToString(), out algorithmType))
                            routeParametersEntity.AlgorithmType = algorithmType;
                        break;
                    case "AvoidanceZones":
                        List<string> avZones = new List<string>();

                        foreach (var avZone in routeParametersObject.AvoidanceZones)
                        {
                            avZones.Add(avZone);
                        }

                        routeParametersEntity.AvoidanceZonesArray = avZones.ToArray();
                        break;
                    case "Metric":
                        EnumR4M.Metric metric;
                        if (System.Enum.TryParse(routeParametersObject.Metric.ToString(), out metric))
                            routeParametersEntity.Metric = metric;
                        break;
                    case "OverrideAddresses":
                        if (routeParametersObject.OverrideAddresses.Time != null)
                        {
                            routeParametersEntity.overrideAddressesObject = new OverrideAddresses()
                            {
                                Time = routeParametersObject.OverrideAddresses.Time
                            };
                        }
                        break;
                    default:
                        var pValue = prop.GetValue(routeParametersObject);

                        PropertyInfo propInfo = typeof(RouteParametersEntity).GetProperties()
                            .Where(x => x.Name == prop.Name).FirstOrDefault();

                        if ((pValue?.GetType() ?? null) != (propInfo?.PropertyType ?? null))
                        {
                            var convertedValue = Utils.ConvertObjectToPropertyType(pValue, propInfo);
                            propInfo.SetValue(routeParametersEntity, convertedValue);
                        }
                        else
                        {
                            propInfo.SetValue(routeParametersEntity, pValue);
                        }

                        break;
                }
            }

            return routeParametersEntity;
        }

        /// <summary>
        /// Converts Route4Me SDK Geocoding object to the Route4MeDb Geocoding entity.
        /// </summary>
        /// <param name="addressObject">Route4Me SDK address object</param>
        /// <returns>Route4MeDb Geocoding entity</returns>
        public GeocodingEntity ConvertSdkGeocodingToEntity(Route4Me.Geocoding geocodingObject)
        {
            var geocoding = new GeocodingEntity();

            var geocodingEntityProperties = typeof(GeocodingEntity).GetProperties().Select(x => x.Name);

            foreach (PropertyInfo prop in typeof(Route4Me.Geocoding).GetProperties())
            {
                if (prop.GetValue(geocodingObject) == null) continue;

                switch (prop.Name)
                {
                    case "CurbsideCoordinates":
                        geocoding.CurbsideCoordinates = JsonConvert.SerializeObject(geocodingObject.CurbsideCoordinates, settings);
                        break;
                    case "Bbox":
                        geocoding.Bbox = JsonConvert.SerializeObject(geocodingObject.Bbox, settings);
                        break;
                    default:
                        typeof(GeocodingEntity).GetProperties().Where(x => x.Name == prop.Name).FirstOrDefault()
                        .SetValue(geocoding, prop.GetValue(geocodingObject));
                        break;
                }
            }

            return geocoding;
        }

        /// <summary>
        /// Converts Route4Me SDK tracking history object to the Route4MeDb tracking history entity.
        /// </summary>
        /// <param name="addressObject">Route4Me SDK tracking history object</param>
        /// <returns>Route4MeDb tracking history entity</returns>
        public TrackingHistoryEntity ConvertSdkTrackingHistoryToEntity(TrackingHistoryTable trackingHistoryObject)
        {
            var trackingHistory = new TrackingHistoryEntity();

            var trackingHistoryProperties = typeof(TrackingHistoryEntity).GetProperties().Select(x => x.Name);

            foreach (PropertyInfo prop in typeof(TrackingHistoryTable).GetProperties())
            {
                if (prop.GetValue(trackingHistoryObject) == null) continue;

                switch (prop.Name)
                {
                    default:
                        typeof(TrackingHistoryEntity).GetProperties().Where(x => x.Name == prop.Name).FirstOrDefault()
                        .SetValue(trackingHistory, prop.GetValue(trackingHistoryObject));
                        break;
                }
            }

            return trackingHistory;
        }

        /// <summary>
        /// Converts Route4Me SDK direction object to the Route4MeDb direction entity.
        /// </summary>
        /// <param name="addressObject">Route4Me SDK direction object</param>
        /// <returns>Route4MeDb direction entity</returns>
        public DirectionEntity ConvertSdkDirectionToEntity(DirectionTable directionObject)
        {
            var direction = new DirectionEntity();

            var directionProperties = typeof(DirectionEntity).GetProperties().Select(x => x.Name);

            foreach (PropertyInfo prop in typeof(DirectionTable).GetProperties())
            {
                if (prop.GetValue(directionObject) == null) continue;

                switch (prop.Name)
                {
                    case "Location":
                        direction.Location = JsonConvert.SerializeObject(directionObject.Location, settings);
                        break;
                    case "Steps":
                        direction.Steps = JsonConvert.SerializeObject(directionObject.Steps, settings);
                        break;
                    default:
                        typeof(DirectionEntity).GetProperties().Where(x => x.Name == prop.Name).FirstOrDefault()
                        .SetValue(direction, prop.GetValue(directionObject));
                        break;
                }
            }

            return direction;
        }

        /// <summary>
        /// Converts Route4Me SDK AddressBookContact object to the Route4MeDb AddressBookContact entity.
        /// </summary>
        /// <param name="contactObject">Route4Me SDK AddressBookContact object</param>
        /// <returns>Route4MeDb AddressBookContact entity</returns>
        public AddressBookContactEntity ConvertSdkAddressBookContactToEntity(AddressBookContactTable contactObject)
        {
            var contact = new AddressBookContactEntity();

            var contactEntityProperties = typeof(AddressBookContactEntity).GetProperties().Select(x => x.Name);

            foreach (PropertyInfo prop in typeof(AddressBookContactTable).GetProperties())
            {
                if (contactEntityProperties.Contains(prop.Name))
                {
                    if (prop.GetValue(contactObject) == null) continue;

                    switch (prop.Name)
                    {
                        case "AddressCustomData":
                            contact.AddressCustomData = JsonConvert.SerializeObject(contactObject.AddressCustomData, settings);
                            break;
                        case "ScheduleBlacklist":
                            if (contactObject.ScheduleBlacklist == null) continue;

                            List<string> lsBlackDays = new List<string>();

                            foreach (var blackDay in contactObject.ScheduleBlacklist)
                            {
                                lsBlackDays.Add(blackDay);
                            }

                            if (lsBlackDays.Count > 0) contact.ScheduleBlackListArray = lsBlackDays.ToArray();
                            break;
                        case "AddressStopType":
                            EnumR4M.AddressStopType addressStopType;
                            if (System.Enum.TryParse(contactObject.AddressStopType, out addressStopType))
                                contact.AddressStopType = addressStopType.Description();
                            break;
                        case "Schedule":
                            if (contactObject.Schedule == null) continue;

                            var lsSchedules = new List<Schedule>();

                            foreach (var schedule in contactObject.Schedule)
                            {
                                if (schedule != null)
                                {
                                    var scheduleEntity = ConvertSdkScheduleToEntity(schedule);

                                    lsSchedules.Add(scheduleEntity);
                                }
                            }

                            if (lsSchedules.Count > 0) contact.SchedulesArray = lsSchedules.ToArray();
                            break;
                        default:
                            var pValue = prop.GetValue(contactObject);

                            PropertyInfo propInfo = typeof(AddressBookContactEntity).GetProperties()
                                .Where(x => x.Name == prop.Name).FirstOrDefault();

                            if ((pValue?.GetType() ?? null) != (propInfo?.PropertyType ?? null))
                            {
                                var convertedValue = Utils.ConvertObjectToPropertyType(pValue, propInfo);
                                propInfo.SetValue(contact, convertedValue);
                            }
                            else
                            {
                                propInfo.SetValue(contact, pValue);
                            }

                            break;
                    }
                }
            }

            return contact;
        }

        /// <summary>
        /// Converts Route4Me SDK Schedule object to the Route4MeDb Schedule entity.
        /// </summary>
        /// <param name="scheduleObject">Route4Me SDK Schedule object</param>
        /// <returns>Route4MeDb Schedule entity</returns>
        public ScheduleEntity ConvertSdkScheduleToEntity(Route4MeSDK.DataTypes.Schedule scheduleObject)
        {
            if (scheduleObject != null) return null;

            ScheduleEntity scheduleEntity = new ScheduleEntity();

            if (scheduleObject.Enabled != null) scheduleEntity.Enabled = scheduleObject.Enabled;
            if (scheduleObject.Mode != null) scheduleEntity.Mode = scheduleObject.Mode;
            if (scheduleObject.From != null) scheduleEntity.From = scheduleObject.From;

            if (scheduleObject.Daily != null)
            {
                scheduleEntity.Daily = new ScheduleDaily();
                if (scheduleObject.Daily.Every != null) scheduleEntity.Daily.Every = scheduleObject.Daily.Every;
            }

            if (scheduleObject.Weekly != null)
            {
                scheduleEntity.Weekly = new ScheduleWeekly();

                if (scheduleObject.Weekly.Every != null) scheduleObject.Weekly.Every = scheduleObject.Weekly.Every;
                if (scheduleObject.Weekly.Weekdays != null) scheduleObject.Weekly.Weekdays = scheduleObject.Weekly.Weekdays;
            }

            if (scheduleObject.Monthly != null)
            {
                scheduleEntity.Monthly = new ScheduleMonthly();

                if (scheduleObject.Monthly.Every != null) scheduleEntity.Monthly.Every = scheduleObject.Monthly.Every;
                if (scheduleObject.Monthly.Mode != null) scheduleEntity.Monthly.Mode = scheduleObject.Monthly.Mode;
                if (scheduleObject.Monthly.Dates != null) scheduleEntity.Monthly.Dates = scheduleObject.Monthly.Dates;
                if (scheduleObject.Monthly.Nth != null)
                {
                    scheduleEntity.Monthly.Nth = new ScheduleMonthlyNth();

                    if (scheduleObject.Monthly.Nth.N != null) scheduleEntity.Monthly.Nth.N = scheduleObject.Monthly.Nth.N;
                    if (scheduleObject.Monthly.Nth.What != null) scheduleEntity.Monthly.Nth.What = scheduleObject.Monthly.Nth.What;
                }
            }

            return scheduleEntity;
        }

        /// <summary>
        /// Converts Route4Me SDK Order object to the Route4MeDb Order entity.
        /// </summary>
        /// <param name="orderObject">Route4Me SDK Order object</param>
        /// <returns>Route4MeDb Order entity</returns>
        public OrderEntity ConvertSdkOrderToEntity(OrderTable orderObject)
        {
            var order = new OrderEntity();

            var orderEntityProperties = typeof(OrderEntity).GetProperties().Select(x => x.Name);

            var orderTableProperties = typeof(OrderTable).GetProperties();

            foreach (PropertyInfo prop in orderTableProperties)
            {
                if (orderEntityProperties.Contains(prop.Name))
                {
                    if (prop.GetValue(orderObject) == null) continue;

                    switch (prop.Name)
                    {
                        case "CustomUserFields":
                            if (orderObject.CustomUserFields == null) continue;

                            List<OrderCustomFieldEntity> lsCustomFileds = new List<OrderCustomFieldEntity>();

                            foreach (var customUserField in orderObject.CustomUserFields)
                            {
                                var customFieldEntity = ConvertSdkOrderCustomFieldToEntity(customUserField);
                                if (customFieldEntity != null) lsCustomFileds.Add(customFieldEntity);
                            }

                            if (lsCustomFileds.Count > 0) order.CustomFieldsObj = lsCustomFileds.ToArray();
                            break;
                        case "ExtFieldCustomData":
                            if (orderObject.ExtFieldCustomData == null) continue;

                            order.EXT_FIELD_custom_datas = new Dictionary<string, string>();

                            foreach (var kv1 in (Dictionary<string, string>)orderObject.ExtFieldCustomData)
                            {
                                order.EXT_FIELD_custom_datas[kv1.Key] = kv1.Value;
                            }

                            break;
                        default:
                            var pValue = prop.GetValue(orderObject);

                            PropertyInfo propInfo = typeof(OrderEntity).GetProperties()
                                .Where(x => x.Name == prop.Name).FirstOrDefault();

                            if ((pValue?.GetType() ?? null) != (propInfo?.PropertyType ?? null))
                            {
                                var convertedValue = Utils.ConvertObjectToPropertyType(pValue, propInfo);
                                propInfo.SetValue(order, convertedValue);
                            }
                            else
                            {
                                propInfo.SetValue(order, pValue);
                            }

                            break;
                    }
                }
            }

            return order;
        }

        /// <summary>
        /// Converts Route4Me SDK OrderCustomField object to the Route4MeDb OrderCustomField entity.
        /// </summary>
        /// <param name="orderCustomFieldObject">Route4Me SDK OrderCustomField object</param>
        /// <returns>Route4MeDb OrderCustomField entity</returns>
        public OrderCustomFieldEntity ConvertSdkOrderCustomFieldToEntity(OrderCustomFieldTable orderCustomFieldObject)
        {
            var orderCustomField = new OrderCustomFieldEntity();

            var orderCustomFieldEntityProperties = typeof(OrderCustomFieldEntity).GetProperties().Select(x => x.Name);

            foreach (PropertyInfo prop in typeof(OrderCustomFieldTable).GetProperties())
            {
                if (orderCustomFieldEntityProperties.Contains(prop.Name))
                {
                    if (prop.GetValue(orderCustomFieldObject) == null) continue;

                    switch (prop.Name)
                    {
                        case "OrderCustomFieldTypeInfo":
                            if (orderCustomFieldObject.OrderCustomFieldTypeInfo == null) continue;

                            orderCustomField.dicOrderCustomFieldTypeInfo = new Dictionary<string, object>();

                            foreach (var kv1 in (Dictionary<string, object>)orderCustomFieldObject.OrderCustomFieldTypeInfo)
                            {
                                orderCustomField.dicOrderCustomFieldTypeInfo[kv1.Key] = kv1.Value;
                            }
                            break;
                        default:
                            var pValue = prop.GetValue(orderCustomFieldObject);

                            PropertyInfo propInfo = typeof(OrderCustomFieldEntity).GetProperties()
                                .Where(x => x.Name == prop.Name).FirstOrDefault();

                            if ((pValue?.GetType() ?? null) != (propInfo?.PropertyType ?? null))
                            {
                                var convertedValue = Utils.ConvertObjectToPropertyType(pValue, propInfo);
                                propInfo.SetValue(orderCustomField, convertedValue);
                            }
                            else
                            {
                                propInfo.SetValue(orderCustomField, pValue);
                            }

                            break;
                    }
                }
            }

            return orderCustomField;
        }

        #endregion

        #region // Converting from Entity to SDK

        /// <summary>
        /// Converts Route4MeDb entity (e.g. OptimizationProblem, Route AddressBookContact etc)) 
        /// to a Route4Me SDK object.
        /// </summary>
        /// <typeparam name="T">Route4Me SDK object type</typeparam>
        /// <param name="entity">Route4MeDb entity</param>
        /// <param name="errorMessage">Error message in case of failure</param>
        /// <returns>Route4Me SDK object</returns>
        public T ConvertEntityToSDK<T>(object entity, out string errorMessage) where T : class
        {
            T result = default(T);
            errorMessage = "";

            try
            {
                if (typeof(T) == typeof(Route4Me.DataObject))
                {
                    result = ConvertOptimizationEntityToSdk(entity as OptimizationProblem) as T;
                }
                else if (typeof(T) == typeof(Route4Me.DataObjectRoute))
                {
                    result = ConvertRouteEntityToSdk(entity as RouteEntity) as T;
                }
                else if (typeof(T) == typeof(Route4Me.Address))
                {
                    result = ConvertAddressEntityToSdk(entity as AddressEntity) as T;
                }
                else if (typeof(T) == typeof(Route4Me.AddressNote))
                {
                    result = ConvertAddressNoteEntityToSdk(entity as AddressNoteEntity) as T;
                }
                else if (typeof(T) == typeof(Route4Me.Geocoding))
                {
                    result = ConvertGeocodingEntityToSdk(entity as GeocodingEntity) as T;
                }
                else if (typeof(T) == typeof(Route4Me.TrackingHistory))
                {
                    result = ConvertTrackingHistoryEntityToSdk(entity as TrackingHistoryEntity) as T;
                }
                else if (typeof(T) == typeof(Route4Me.Direction))
                {
                    result = ConvertDirectionEntityToSdk(entity as DirectionEntity) as T;
                }
                else if (typeof(T) == typeof(Route4Me.AddressBookContact))
                {
                    result = ConvertAddressBookContactEntityToSdk(entity as AddressBookContactEntity) as T;
                }
                else if (typeof(T) == typeof(Route4Me.Order))
                {
                    result = ConvertOrderEntityToSdk(entity as OrderEntity) as T;
                }

                return result;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return result;
            }
        }

        /// <summary>
        /// Converts Route4MeDb optimization entity to the Route4Me SDK optimization object.
        /// </summary>
        /// <param name="routeObject">Route4MeDb optimization entity</param>
        /// <returns>Route4Me SDK optimization object</returns>
        public Route4Me.DataObject ConvertOptimizationEntityToSdk(OptimizationProblem optimizationEntityObject)
        {
            var optimization = new Route4Me.DataObject();

            var optimizationSdkProperties = typeof(Route4Me.DataObject).GetProperties().Select(x => x.Name);

            foreach (PropertyInfo prop in typeof(OptimizationProblem).GetProperties())
            {
                if (optimizationSdkProperties.Contains(prop.Name))
                {
                    if (prop.GetValue(optimizationEntityObject) == null) continue;

                    switch (prop.Name)
                    {
                        case "Parameters":
                            optimization.Parameters = ConvertRouteParametersEntityToSdk(optimizationEntityObject.ParametersObj);
                            break;
                        case "Links":
                            if (optimizationEntityObject.LinksObj != null)
                            {
                                optimization.Links = new Route4Me.Links();

                                foreach (var k1 in optimizationEntityObject.LinksObj.Keys)
                                {
                                    if (optimizationEntityObject.LinksObj[k1] == null) continue;

                                    switch (k1.ToLower())
                                    {
                                        case "route":
                                            optimization.Links.Route = optimizationEntityObject.LinksObj[k1];
                                            break;
                                        case "view":
                                            optimization.Links.View = optimizationEntityObject.LinksObj[k1];
                                            break;
                                        case "optimization_problem_id":
                                            optimization.Links.OptimizationProblemId = optimizationEntityObject.LinksObj[k1];
                                            break;
                                    }
                                }
                            }
                            break;
                        case "UserErrors":
                            optimization.UserErrors = optimizationEntityObject.UserErrorsArray;
                            break;
                        case "Addresses":
                            var addresses = new List<AddressTable>();

                            foreach (var address in optimizationEntityObject.Addresses)
                            {
                                addresses.Add(ConvertAddressEntityToSdk(address));
                            }

                            optimization.Addresses = addresses.ToArray();
                            break;
                        case "Routes":
                            var routes = new List<RouteTable>();

                            foreach (var route in optimizationEntityObject.Routes)
                            {
                                routes.Add(ConvertRouteEntityToSdk(route));
                            }

                            optimization.Routes = routes.ToArray();
                            break;
                        default:
                            var pValue = prop.GetValue(optimizationEntityObject);

                            PropertyInfo propInfo = typeof(Route4Me.DataObject).GetProperties()
                                .Where(x => x.Name == prop.Name).FirstOrDefault();

                            if ((pValue?.GetType() ?? null) != (propInfo?.PropertyType ?? null))
                            {

                                var convertedValue = Route4MeDB.Route4MeDbLibrary.Utils.ConvertObjectToPropertyType(pValue, propInfo);
                                propInfo.SetValue(optimization, convertedValue);
                            }
                            else
                            {
                                propInfo.SetValue(optimization, pValue);
                            }

                            break;
                    }
                }
            }

            return optimization;
        }

        /// <summary>
        /// Converts Route4MeDb route entity to the Route4Me SDK route object.
        /// </summary>
        /// <param name="routeObject">Route4MeDb route entity</param>
        /// <returns>Route4Me SDK route object</returns>
        public RouteTable ConvertRouteEntityToSdk(RouteEntity routeEntityObject)
        {
            var route = new RouteTable();

            var routeSdkProperties = typeof(RouteTable).GetProperties().Select(x => x.Name);

            var routeEntityProperties = typeof(RouteEntity).GetProperties();

            foreach (PropertyInfo prop in routeEntityProperties)
            {
                if (routeSdkProperties.Contains(prop.Name) || prop.Name == "TrackingHistory" || prop.Name == "Path")
                {
                    if (prop.GetValue(routeEntityObject) == null) continue;

                    switch (prop.Name)
                    {
                        case "Parameters":
                            route.Parameters = ConvertRouteParametersEntityToSdk(routeEntityObject.ParametersObj);
                            break;
                        case "Addresses":
                            var addresses = new List<AddressTable>();

                            foreach (var address in routeEntityObject.Addresses)
                            {
                                addresses.Add(ConvertAddressEntityToSdk(address));
                            }

                            route.Addresses = addresses.ToArray();
                            break;
                        case "Directions":
                            var lsDirections = new List<DirectionTable>();

                            foreach (var direction in routeEntityObject.Directions)
                            {
                                var directionEntity = ConvertDirectionEntityToSdk(direction);

                                lsDirections.Add(directionEntity);
                            }

                            route.Directions = lsDirections.ToArray();
                            break;
                        case "Notes":
                            var lsAddressNotes = new List<AddressNoteTable>();

                            foreach (var note in routeEntityObject.Notes)
                            {
                                lsAddressNotes.Add(ConvertAddressNoteEntityToSdk(note));
                            }

                            route.Notes = lsAddressNotes.ToArray();
                            break;
                        case "Path":
                            var lsPathPoints = new List<Route4Me.DirectionPathPoint>();

                            foreach (var path in routeEntityObject.Pathes)
                            {
                                lsPathPoints.Add(new Route4Me.DirectionPathPoint()
                                {
                                    Lat = path.Lat,
                                    Lng = path.Lng
                                });
                            }

                            route.Path = lsPathPoints.ToArray();
                            break;
                        case "TrackingHistory":
                            var lsTrackingHistories = new List<TrackingHistoryTable>();

                            foreach (var trackingHistory in routeEntityObject.TrackingHistories)
                            {
                                lsTrackingHistories.Add(ConvertTrackingHistoryEntityToSdk(trackingHistory));
                            }

                            route.TrackingHistory = lsTrackingHistories.ToArray();
                            break;
                        case "Links":
                            route.Links = new Route4Me.Links();
                            if (routeEntityObject.LinksObj.Route != null) route.Links.Route = routeEntityObject.LinksObj.Route;
                            if (routeEntityObject.LinksObj.View != null) route.Links.View = routeEntityObject.LinksObj.View;
                            if (routeEntityObject.LinksObj.OptimizationProblemId != null) route.Links.OptimizationProblemId = routeEntityObject.LinksObj.OptimizationProblemId;
                            break;
                        case "MemberConfigStorage":
                            route.MemberConfigStorage = new Dictionary<string, string>();

                            foreach (var k1 in routeEntityObject.MemberConfigStorageDic.Keys)
                            {
                                route.MemberConfigStorage[k1] = routeEntityObject.MemberConfigStorageDic[k1];
                            }
                            break;
                        case "Vehicle":
                            route.Vehilce = ConvertRouteVehicleEntityToSdk(routeEntityObject.VehicleObj);
                            break;
                        case "GeofencePolygonType":
                            route.GeofencePolygonType = Route4MeSDK.R4MeUtils.Description(routeEntityObject.GeofencePolygonType);
                            break;
                        default:
                            var pValue = prop.GetValue(routeEntityObject);

                            PropertyInfo propInfo = typeof(RouteTable).GetProperties()
                                .Where(x => x.Name == prop.Name).FirstOrDefault();

                            if ((pValue?.GetType() ?? null) != (propInfo?.PropertyType ?? null))
                            {

                                var convertedValue = Route4MeDbLibrary.Utils.ConvertObjectToPropertyType(pValue, propInfo);
                                propInfo.SetValue(route, convertedValue);
                            }
                            else
                            {
                                propInfo.SetValue(route, pValue);
                            }

                            break;
                    }
                }
            }

            return route;
        }

        /// <summary>
        /// Converts Route4MeDb address entity to the Route4Me SDK address object.
        /// </summary>
        /// <param name="addressObject">Route4MeDb address entity</param>
        /// <returns>Route4Me SDK address object</returns>
        public AddressTable ConvertAddressEntityToSdk(AddressEntity addressEntityObject)
        {
            var address = new AddressTable();

            var addressSdkProperties = typeof(AddressTable).GetProperties().Select(x => x.Name);

            foreach (PropertyInfo prop in typeof(AddressEntity).GetProperties())
            {
                if (addressSdkProperties.Contains(prop.Name))
                {
                    if (prop.GetValue(addressEntityObject) == null) continue;

                    switch (prop.Name)
                    {
                        case "Notes":
                            var lsNotes = new List<AddressNoteTable>();

                            foreach (var note in addressEntityObject.Notes)
                            {
                                lsNotes.Add(ConvertAddressNoteEntityToSdk(note));
                            }

                            address.Notes = lsNotes.ToArray();
                            break;
                        case "Geocodings":
                            var lsGeocodings = new List<Route4Me.Geocoding>();

                            foreach (var geocoding in addressEntityObject.Geocodings)
                            {
                                lsGeocodings.Add(ConvertGeocodingEntityToSdk(geocoding));
                            }

                            address.Geocodings = lsGeocodings.ToArray();
                            break;
                        case "PathToNext":
                            var lsPathToTnext = new List<Route4Me.GeoPoint>();

                            foreach (var path in addressEntityObject.PathToNext)
                            {
                                if (path == null) continue;

                                lsPathToTnext.Add(new Route4Me.GeoPoint()
                                {
                                    Latitude = path.Lat,
                                    Longitude = path.Lng
                                });
                            }

                            address.PathToNext = lsPathToTnext.ToArray();
                            break;
                        case "Manifest":
                            address.Manifest = ConvertAddressManifestEntityToSdk(addressEntityObject.ManifestObj);
                            break;
                        case "CustomFields":
                            address.CustomFields = new Dictionary<string, string>();

                            foreach (var k1 in addressEntityObject.dicCustomFields.Keys)
                            {
                                if (address.CustomFields.ContainsKey(k1))
                                {
                                    address.CustomFields[k1] = addressEntityObject.dicCustomFields[k1];
                                }
                            }
                            break;
                        case "CustomFieldsConfig":
                            address.CustomFieldsConfig = addressEntityObject.CustomFieldsConfigArray;
                            break;
                        case "MemberId":
                            address.MemberId = Convert.ToInt32(addressEntityObject.MemberId);
                            break;
                        case "AddressStopType":
                            address.AddressStopType = Route4MeSDK.R4MeUtils.Description(addressEntityObject.AddressStopType);
                            break;
                        default:
                            var pValue = prop.GetValue(addressEntityObject);

                            PropertyInfo propInfo = typeof(AddressTable).GetProperties()
                                .Where(x => x.Name == prop.Name).FirstOrDefault();

                            if ((pValue?.GetType() ?? null) != (propInfo?.PropertyType ?? null))
                            {
                                var convertedValue = Utils.ConvertObjectToPropertyType(pValue, propInfo);
                                propInfo.SetValue(address, convertedValue);
                            }
                            else
                            {
                                propInfo.SetValue(address, pValue);
                            }

                            break;
                    }
                }
            }

            return address;
        }

        /// <summary>
        /// Converts Route4MeDb address manifest entity to the Route4Me SDK address manifest object.
        /// </summary>
        /// <param name="addressObject">Route4MeDb address manifest entity</param>
        /// <returns>Route4Me SDK address manifest object</returns>
        public Route4Me.AddressManifest ConvertAddressManifestEntityToSdk(AddressManifestEntity addressManifestEntityObject)
        {
            var addressManifest = new Route4Me.AddressManifest();

            var addressManifestSdkProperties = typeof(Route4Me.AddressManifest).GetProperties().Select(x => x.Name);

            foreach (PropertyInfo prop in typeof(AddressManifestEntity).GetProperties())
            {
                if (addressManifestSdkProperties.Contains(prop.Name))
                {
                    if (prop.GetValue(addressManifestEntityObject) == null) continue;

                    switch (prop.Name)
                    {
                        default:
                            var pValue = prop.GetValue(addressManifestEntityObject);

                            PropertyInfo propInfo = typeof(Route4Me.AddressManifest).GetProperties()
                                .Where(x => x.Name == prop.Name).FirstOrDefault();

                            if ((pValue?.GetType() ?? null) != (propInfo?.PropertyType ?? null))
                            {
                                var convertedValue = Route4MeDbLibrary.Utils.ConvertObjectToPropertyType(pValue, propInfo);
                                propInfo.SetValue(addressManifestEntityObject, convertedValue);
                            }
                            else
                            {
                                propInfo.SetValue(addressManifestEntityObject, pValue);
                            }

                            break;
                    }
                }
            }

            return addressManifest;
        }

        /// <summary>
        /// Converts Route4MeDb Geocoding entity to the Route4Me SDK Geocoding object.
        /// </summary>
        /// <param name="addressObject">Route4MeDb Geocoding entity</param>
        /// <returns>Route4Me SDK Geocoding object</returns>
        public Route4Me.Geocoding ConvertGeocodingEntityToSdk(GeocodingEntity geocodingEntityObject)
        {
            var geocoding = new Route4Me.Geocoding();

            var geocodingSdkProperties = typeof(Route4Me.Geocoding).GetProperties().Select(x => x.Name);

            foreach (PropertyInfo prop in typeof(GeocodingEntity).GetProperties())
            {
                if (prop.GetValue(geocodingEntityObject) == null) continue;

                switch (prop.Name)
                {
                    case "CurbsideCoordinates":
                        if (geocodingEntityObject.CurbsideCoordinatesObj!=null)
                        {
                            geocoding.CurbsideCoordinates = new Route4Me.GeoPoint()
                            {
                                Latitude = geocodingEntityObject.CurbsideCoordinatesObj.Latitude,
                                Longitude = geocodingEntityObject.CurbsideCoordinatesObj.Longitude
                            };
                        }
                        break;
                    case "Bbox":
                        geocoding.Bbox = geocodingEntityObject.BboxArray;
                        break;
                    default:
                        var pValue = prop.GetValue(geocodingEntityObject);

                        PropertyInfo propInfo = typeof(Route4Me.Geocoding).GetProperties()
                            .Where(x => x.Name == prop.Name).FirstOrDefault();

                        if ((pValue?.GetType() ?? null) != (propInfo?.PropertyType ?? null))
                        {
                            var convertedValue = Utils.ConvertObjectToPropertyType(pValue, propInfo);
                            propInfo.SetValue(geocodingEntityObject, convertedValue);
                        }
                        else
                        {
                            propInfo.SetValue(geocodingEntityObject, pValue);
                        }

                        break;
                }
            }

            return geocoding;
        }

        /// <summary>
        /// Converts Route4MeDb route parameters entity to the Route4Me SDK route parameters object.
        /// </summary>
        /// <param name="addressObject">Route4MeDb route parameters entity</param>
        /// <returns>Route4Me SDK route parameters object</returns>
        public Route4Me.RouteParameters ConvertRouteParametersEntityToSdk(RouteParametersEntity routeParametersEntityObject)
        {
            var routeParameters = new Route4Me.RouteParameters();

            var routeParametersSdkProperties = typeof(Route4Me.RouteParameters).GetProperties().Select(x => x.Name);

            foreach (PropertyInfo prop in typeof(RouteParametersEntity).GetProperties())
            {
                if (routeParametersSdkProperties.Contains(prop.Name))
                {
                    if (prop.GetValue(routeParametersEntityObject) == null) continue;

                    switch (prop.Name)
                    {
                        case "AlgorithmType":
                            Route4Me.AlgorithmType algorithmType;
                            if (System.Enum.TryParse(routeParametersEntityObject.AlgorithmType.ToString(), out algorithmType))
                                routeParameters.AlgorithmType = algorithmType;
                            break;
                        case "AvoidanceZones":
                            routeParameters.AvoidanceZones = routeParametersEntityObject.AvoidanceZonesArray;
                            break;
                        case "Metric":
                            Route4Me.Metric metric;
                            if (System.Enum.TryParse(routeParametersEntityObject.Metric.ToString(), out metric))
                                routeParameters.Metric = metric;
                            break;
                        case "OverrideAddresses":
                            if (routeParametersEntityObject.overrideAddressesObject.Time != null)
                            {
                                routeParameters.OverrideAddresses = new Route4Me.OverrideAddresses()
                                {
                                    Time = routeParametersEntityObject.overrideAddressesObject.Time
                                };
                            }
                            break;
                        default:
                            var pValue = prop.GetValue(routeParametersEntityObject);

                            PropertyInfo propInfo = typeof(Route4Me.RouteParameters).GetProperties()
                                .Where(x => x.Name == prop.Name).FirstOrDefault();

                            if ((pValue?.GetType() ?? null) != (propInfo?.PropertyType ?? null))
                            {
                                var convertedValue = Utils.ConvertObjectToPropertyType(pValue, propInfo);
                                propInfo.SetValue(routeParameters, convertedValue);
                            }
                            else
                            {
                                propInfo.SetValue(routeParameters, pValue);
                            }

                            break;
                    }
                }
            }

            return routeParameters;
        }

        /// <summary>
        /// Converts Route4MeDb tracking history entity to the Route4Me SDK tracking history object.
        /// </summary>
        /// <param name="addressObject">Route4MeDb tracking history entity</param>
        /// <returns> Route4Me SDK tracking history object</returns>
        public TrackingHistoryTable ConvertTrackingHistoryEntityToSdk(TrackingHistoryEntity trackingHistoryEntityObject)
        {
            var trackingHistory = new TrackingHistoryTable();

            var trackingHistorySdkProperties = typeof(TrackingHistoryTable).GetProperties().Select(x => x.Name);

            foreach (PropertyInfo prop in typeof(TrackingHistoryEntity).GetProperties())
            {
                if (trackingHistorySdkProperties.Contains(prop.Name))
                {
                    if (prop.GetValue(trackingHistoryEntityObject) == null) continue;

                    switch (prop.Name)
                    {
                        default:
                            var pValue = prop.GetValue(trackingHistoryEntityObject);

                            PropertyInfo propInfo = typeof(TrackingHistoryTable).GetProperties()
                                .Where(x => x.Name == prop.Name).FirstOrDefault();

                            if ((pValue?.GetType() ?? null) != (propInfo?.PropertyType ?? null))
                            {
                                var convertedValue = Route4MeDbLibrary.Utils.ConvertObjectToPropertyType(pValue, propInfo);
                                propInfo.SetValue(trackingHistory, convertedValue);
                            }
                            else
                            {
                                propInfo.SetValue(trackingHistory, pValue);
                            }

                            break;
                    }
                }
            }

            return trackingHistory;
        }

        /// <summary>
        /// Converts Route4MeDb direction entity to the Route4Me SDK direction object.
        /// </summary>
        /// <param name="routeObject">Route4MeDb direction entity</param>
        /// <returns>Route4Me SDK direction object</returns>
        public DirectionTable ConvertDirectionEntityToSdk(DirectionEntity directionEntityObject)
        {
            var direction = new DirectionTable();

            var directionSdkProperties = typeof(DirectionTable).GetProperties().Select(x => x.Name);

            foreach (PropertyInfo prop in typeof(DirectionEntity).GetProperties())
            {
                if (directionSdkProperties.Contains(prop.Name))
                {
                    if (prop.GetValue(directionEntityObject) == null) continue;

                    switch (prop.Name)
                    {
                        case "LocationObj":
                            direction.Location = ConvertLocationEntityToSdk(directionEntityObject.LocationObj);
                            break;
                        case "StepsArray":
                            List<Route4Me.DirectionStep> lsSteps = new List<Route4Me.DirectionStep>();

                            foreach (var step in directionEntityObject.StepsArray)
                            {
                                lsSteps.Add(ConvertStepEntityToSdk(step));
                            }

                            direction.Steps = lsSteps.ToArray();
                            break;
                        default:
                            var pValue = prop.GetValue(directionEntityObject);

                            PropertyInfo propInfo = typeof(DirectionTable).GetProperties()
                                .Where(x => x.Name == prop.Name).FirstOrDefault();

                            if ((pValue?.GetType() ?? null) != (propInfo?.PropertyType ?? null))
                            {

                                var convertedValue = Route4MeDB.Route4MeDbLibrary.Utils.ConvertObjectToPropertyType(pValue, propInfo);
                                propInfo.SetValue(direction, convertedValue);
                            }
                            else
                            {
                                propInfo.SetValue(direction, pValue);
                            }

                            break;
                    }
                }
            }

            return direction;
        }

        /// <summary>
        /// Converts Route4MeDb location entity to the Route4Me SDK location object.
        /// </summary>
        /// <param name="routeObject">Route4MeDb location entity</param>
        /// <returns>Route4Me SDK location object</returns>
        public Route4Me.DirectionLocation ConvertLocationEntityToSdk(LocationEntity locationEntityObject)
        {
            var location = new Route4Me.DirectionLocation();

            var locationSdkProperties = typeof(Route4Me.DirectionLocation).GetProperties().Select(x => x.Name);

            foreach (PropertyInfo prop in typeof(LocationEntity).GetProperties())
            {
                if (locationSdkProperties.Contains(prop.Name))
                {
                    if (prop.GetValue(locationEntityObject) == null) continue;

                    switch (prop.Name)
                    {
                        default:
                            var pValue = prop.GetValue(locationEntityObject);

                            PropertyInfo propInfo = typeof(Route4Me.DirectionLocation).GetProperties()
                                .Where(x => x.Name == prop.Name).FirstOrDefault();

                            if ((pValue?.GetType() ?? null) != (propInfo?.PropertyType ?? null))
                            {

                                var convertedValue = Route4MeDB.Route4MeDbLibrary.Utils.ConvertObjectToPropertyType(pValue, propInfo);
                                propInfo.SetValue(location, convertedValue);
                            }
                            else
                            {
                                propInfo.SetValue(location, pValue);
                            }

                            break;
                    }
                }
            }

            return location;
        }

        /// <summary>
        /// Converts Route4MeDb step entity to the Route4Me SDK step object.
        /// </summary>
        /// <param name="routeObject">Route4MeDb step entity</param>
        /// <returns>Route4Me SDK step object</returns>
        public Route4Me.DirectionStep ConvertStepEntityToSdk(StepEntity stepEntityObject)
        {
            var step = new Route4Me.DirectionStep();

            var stepSdkProperties = typeof(Route4Me.DirectionStep).GetProperties().Select(x => x.Name);

            foreach (PropertyInfo prop in typeof(StepEntity).GetProperties())
            {
                if (stepSdkProperties.Contains(prop.Name))
                {
                    if (prop.GetValue(stepEntityObject) == null) continue;

                    switch (prop.Name)
                    {
                        default:
                            var pValue = prop.GetValue(stepEntityObject);

                            PropertyInfo propInfo = typeof(Route4Me.DirectionStep).GetProperties()
                                .Where(x => x.Name == prop.Name).FirstOrDefault();

                            if ((pValue?.GetType() ?? null) != (propInfo?.PropertyType ?? null))
                            {

                                var convertedValue = Route4MeDB.Route4MeDbLibrary.Utils.ConvertObjectToPropertyType(pValue, propInfo);
                                propInfo.SetValue(step, convertedValue);
                            }
                            else
                            {
                                propInfo.SetValue(step, pValue);
                            }

                            break;
                    }
                }
            }

            return step;
        }

        /// <summary>
        /// Converts Route4Me SDK address note object to the Route4MeDb address note entity.
        /// </summary>
        /// <param name="addressObject">Route4Me SDK address note object</param>
        /// <returns>Route4MeDb address note entity</returns>
        public AddressNoteTable ConvertAddressNoteEntityToSdk(AddressNoteEntity addressNoteEntityObject)
        {
            var addressNote = new AddressNoteTable();

            var addressNoteSdkProperties = typeof(AddressNoteTable).GetProperties().Select(x => x.Name);

            foreach (PropertyInfo prop in typeof(AddressNoteEntity).GetProperties())
            {
                if (addressNoteSdkProperties.Contains(prop.Name))
                {
                    if (prop.GetValue(addressNoteEntityObject) == null) continue;

                    switch (prop.Name)
                    {
                        case "CustomTypes":
                            var lsCustomNotes = new List<Route4Me.AddressCustomNote>();

                            foreach (var customType in addressNoteEntityObject.CustomTypesObj)
                            {
                                lsCustomNotes.Add(ConvertCustomNoteTypeEntityToSdk(customType));
                            }

                            addressNote.CustomTypes = lsCustomNotes.ToArray();
                            break;
                        case "ActivityType":
                            addressNote.ActivityType = Route4MeSDK.R4MeUtils.Description(addressNoteEntityObject.StatusUpdateType);
                            break;
                        case "UploadType":
                            addressNote.UploadType = Route4MeSDK.R4MeUtils.Description(addressNoteEntityObject.UploadType);
                            break;
                        case "DeviceType":
                            addressNote.DeviceType = Route4MeSDK.R4MeUtils.Description(addressNoteEntityObject.DeviceType);
                            break;
                        default:
                            var pValue = prop.GetValue(addressNoteEntityObject);

                            PropertyInfo propInfo = typeof(AddressNoteTable).GetProperties()
                                .Where(x => x.Name == prop.Name).FirstOrDefault();

                            if ((pValue?.GetType() ?? null) != (propInfo?.PropertyType ?? null))
                            {
                                var convertedValue = Utils.ConvertObjectToPropertyType(pValue, propInfo);
                                propInfo.SetValue(addressNote, convertedValue);
                            }
                            else
                            {
                                propInfo.SetValue(addressNote, pValue);
                            }

                            break;
                    }
                }
            }

            return addressNote;
        }

        /// <summary>
        /// Converts Route4MeDb route vehicle entity to the Route4Me SDK vehicle object.
        /// </summary>
        /// <param name="addressObject">Route4MeDb route vehicle entity</param>
        /// <returns>Route4Me SDK vehicle object</returns>
        public RouteVehicleTable ConvertRouteVehicleEntityToSdk(RouteVehicleEntity vehicleEntityObject)
        {
            var vehicle = new RouteVehicleTable();

            var vehicleSdkProperties = typeof(RouteVehicleTable).GetProperties().Select(x => x.Name);

            foreach (PropertyInfo prop in typeof(RouteVehicleEntity).GetProperties())
            {
                if (vehicleSdkProperties.Contains(prop.Name))
                {
                    if (prop.GetValue(vehicleEntityObject) == null) continue;

                    switch (prop.Name)
                    {
                        default:
                            var pValue = prop.GetValue(vehicleEntityObject);

                            PropertyInfo propInfo = typeof(RouteVehicleTable).GetProperties()
                                .Where(x => x.Name == prop.Name).FirstOrDefault();

                            if ((pValue?.GetType() ?? null) != (propInfo?.PropertyType ?? null))
                            {

                                var convertedValue = Route4MeDbLibrary.Utils.ConvertObjectToPropertyType(pValue, propInfo);
                                propInfo.SetValue(vehicle, convertedValue);
                            }
                            else
                            {
                                propInfo.SetValue(vehicle, pValue);
                            }

                            break;
                    }
                }
            }

            return vehicle;
        }

        /// <summary>
        /// Converts Route4MeDb custom address note entity to the Route4Me SDK custom address note object.
        /// </summary>
        /// <param name="addressObject">Route4MeDb custom address note entity</param>
        /// <returns>Route4Me SDK custom address note object</returns>
        public Route4Me.AddressCustomNote ConvertCustomNoteTypeEntityToSdk(AddressCustomNote customNoteEntityObject)
        {
            var customNote = new Route4Me.AddressCustomNote();

            var customNoteSdkProperties = typeof(Route4Me.AddressCustomNote).GetProperties().Select(x => x.Name);

            foreach (PropertyInfo prop in typeof(AddressCustomNote).GetProperties())
            {
                if (customNoteSdkProperties.Contains(prop.Name))
                {
                    if (prop.GetValue(customNoteEntityObject) == null) continue;

                    switch (prop.Name)
                    {
                        default:
                            var pValue = prop.GetValue(customNoteEntityObject);

                            PropertyInfo propInfo = typeof(Route4Me.AddressCustomNote).GetProperties()
                                .Where(x => x.Name == prop.Name).FirstOrDefault();

                            if ((pValue?.GetType() ?? null) != (propInfo?.PropertyType ?? null))
                            {

                                var convertedValue = Route4MeDB.Route4MeDbLibrary.Utils.ConvertObjectToPropertyType(pValue, propInfo);
                                propInfo.SetValue(customNote, convertedValue);
                            }
                            else
                            {
                                propInfo.SetValue(customNote, pValue);
                            }

                            break;
                    }
                }
             }

            return customNote;
        }

        /// <summary>
        /// Converts Route4MeDb AddressBookContact entity to the Route4Me SDK AddressBookContact object.
        /// </summary>
        /// <param name="contactObject">Route4MeDb AddressBookContact entity</param>
        /// <returns>Route4Me SDK AddressBookContact object</returns>
        public AddressBookContactTable ConvertAddressBookContactEntityToSdk(AddressBookContactEntity contactEntityObject)
        {
            var contact = new AddressBookContactTable();

            var contactSdkProperties = typeof(AddressBookContactTable).GetProperties().Select(x => x.Name);

            foreach (PropertyInfo prop in typeof(AddressBookContactEntity).GetProperties())
            {
                if (contactSdkProperties.Contains(prop.Name))
                {
                    if (prop.GetValue(contactEntityObject) == null) continue;

                    switch (prop.Name)
                    {
                        case "AddressCustomData":
                            contact.AddressCustomData = new Dictionary<string, string>();

                            foreach (var k1 in contactEntityObject.AddressCustomDataDic.Keys)
                            {
                                contact.AddressCustomData[k1] = contactEntityObject.AddressCustomDataDic[k1];
                            }
                            break;
                        case "ScheduleBlacklist":
                            contact.ScheduleBlacklist = contactEntityObject.ScheduleBlackListArray;
                            break;
                        case "AddressStopType":
                            contact.AddressStopType = contactEntityObject.AddressStopType;
                            break;
                        case "Schedule":
                            if (contactEntityObject.Schedules == null) continue;

                            var lsSchedules = new List<Route4MeSDK.DataTypes.Schedule>();

                            foreach (var schedule in contactEntityObject.SchedulesArray)
                            {
                                if (schedule != null)
                                {
                                    lsSchedules.Add(ConvertScheduleEntityToSdk(schedule));
                                }
                            }

                            if (lsSchedules.Count > 0) contact.Schedule = lsSchedules.ToArray();
                            break;
                        default:
                            var pValue = prop.GetValue(contactEntityObject);

                            PropertyInfo propInfo = typeof(AddressBookContactTable).GetProperties()
                                .Where(x => x.Name == prop.Name).FirstOrDefault();

                            if ((pValue?.GetType() ?? null) != (propInfo?.PropertyType ?? null))
                            {
                                var convertedValue = Utils.ConvertObjectToPropertyType(pValue, propInfo);
                                propInfo.SetValue(contact, convertedValue);
                            }
                            else
                            {
                                propInfo.SetValue(contact, pValue);
                            }

                            break;
                    }
                }
            }

            return contact;
        }

        /// <summary>
        /// Converts Route4MeDb Schedule entity to the Route4Me SDK Schedule object.
        /// </summary>
        /// <param name="scheduleObject">Route4MeDb Schedule entity</param>
        /// <returns>Route4Me SDK Schedule object</returns>
        public Route4MeSDK.DataTypes.Schedule ConvertScheduleEntityToSdk(ScheduleEntity scheduleEntityObject)
        {
            if (scheduleEntityObject != null) return null;

            var scheduleSdk = new Route4MeSDK.DataTypes.Schedule();

            if (scheduleEntityObject.Enabled != null) scheduleSdk.Enabled = scheduleEntityObject.Enabled;
            if (scheduleEntityObject.Mode != null) scheduleSdk.Mode = scheduleEntityObject.Mode;
            if (scheduleEntityObject.From != null) scheduleSdk.From = scheduleEntityObject.From;

            if (scheduleEntityObject.Daily != null)
            {
                scheduleSdk.Daily = new Route4Me.ScheduleDaily();
                if (scheduleEntityObject.Daily.Every != null) scheduleSdk.Daily.Every = scheduleEntityObject.Daily.Every;
            }

            if (scheduleEntityObject.Weekly != null)
            {
                scheduleSdk.Weekly = new Route4Me.ScheduleWeekly();

                if (scheduleEntityObject.Weekly.Every != null) scheduleSdk.Weekly.Every = scheduleEntityObject.Weekly.Every;
                if (scheduleEntityObject.Weekly.Weekdays != null) scheduleSdk.Weekly.Weekdays = scheduleEntityObject.Weekly.Weekdays;
            }

            if (scheduleEntityObject.Monthly != null)
            {
                scheduleSdk.Monthly = new Route4Me.ScheduleMonthly();

                if (scheduleEntityObject.Monthly.Every != null) scheduleSdk.Monthly.Every = scheduleEntityObject.Monthly.Every;
                if (scheduleEntityObject.Monthly.Mode != null) scheduleSdk.Monthly.Mode = scheduleEntityObject.Monthly.Mode;
                if (scheduleEntityObject.Monthly.Dates != null) scheduleSdk.Monthly.Dates = scheduleEntityObject.Monthly.Dates;
                if (scheduleEntityObject.Monthly.Nth != null)
                {
                    scheduleSdk.Monthly.Nth = new Route4Me.ScheduleMonthlyNth();

                    if (scheduleEntityObject.Monthly.Nth.N != null) scheduleSdk.Monthly.Nth.N = scheduleEntityObject.Monthly.Nth.N;
                    if (scheduleEntityObject.Monthly.Nth.What != null) scheduleSdk.Monthly.Nth.What = scheduleEntityObject.Monthly.Nth.What;
                }
            }

            return scheduleSdk;
        }

        /// <summary>
        /// Converts Route4MeDb Order entity to the Route4Me SDK Order object.
        /// </summary>
        /// <param name="orderObject">Route4MeDb Order entity</param>
        /// <returns>Route4Me SDK Order object</returns>
        public OrderTable ConvertOrderEntityToSdk(OrderEntity orderEntityObject)
        {
            var order = new OrderTable();

            var orderSdkProperties = typeof(OrderTable).GetProperties().Select(x => x.Name);

            var orderEntityProperties = typeof(OrderEntity).GetProperties();

            foreach (PropertyInfo prop in orderEntityProperties)
            {
                if (orderSdkProperties.Contains(prop.Name))
                {
                    if (prop.GetValue(orderEntityObject) == null) continue;

                    switch (prop.Name)
                    {
                        case "CustomUserFields":
                            var lsCustomFileds = new List<OrderCustomFieldTable>();

                            foreach (var customUserField in orderEntityObject.CustomFieldsObj)
                            {
                                lsCustomFileds.Add(ConvertOrderCustomFieldEntityToSdk(customUserField));
                            }

                            if (lsCustomFileds.Count > 0) order.CustomUserFields = lsCustomFileds.ToArray();
                            break;
                        case "ExtFieldCustomData":
                            var fieldCustomData = new Dictionary<string, string>();

                            foreach (var k1 in orderEntityObject.EXT_FIELD_custom_datas.Keys)
                            {
                                fieldCustomData[k1] = orderEntityObject.EXT_FIELD_custom_datas[k1];
                            }

                            order.ExtFieldCustomData = fieldCustomData;
                            break;
                        default:
                            var pValue = prop.GetValue(orderEntityObject);

                            PropertyInfo propInfo = typeof(OrderTable).GetProperties()
                                .Where(x => x.Name == prop.Name).FirstOrDefault();

                            if ((pValue?.GetType() ?? null) != (propInfo?.PropertyType ?? null))
                            {
                                var convertedValue = Utils.ConvertObjectToPropertyType(pValue, propInfo);
                                propInfo.SetValue(order, convertedValue);
                            }
                            else
                            {
                                propInfo.SetValue(order, pValue);
                            }

                            break;
                    }
                }
            }

            return order;
        }

        /// <summary>
        /// Converts Route4MeDb OrderCustomField entity to the Route4Me SDK OrderCustomField object.
        /// </summary>
        /// <param name="orderCustomFieldObject">oute4MeDb OrderCustomField entity</param>
        /// <returns>Route4Me SDK OrderCustomField object</returns>
        public OrderCustomFieldTable ConvertOrderCustomFieldEntityToSdk(OrderCustomFieldEntity orderCustomFieldEntityObject)
        {
            var orderCustomField = new OrderCustomFieldTable();

            var orderCustomFieldSdkProperties = typeof(OrderCustomFieldTable).GetProperties().Select(x => x.Name);

            foreach (PropertyInfo prop in typeof(OrderCustomFieldEntity).GetProperties())
            {
                if (orderCustomFieldSdkProperties.Contains(prop.Name))
                {
                    if (prop.GetValue(orderCustomFieldEntityObject) == null) continue;

                    switch (prop.Name)
                    {
                        case "OrderCustomFieldTypeInfo":
                            orderCustomField.OrderCustomFieldTypeInfo = new Dictionary<string, object>();

                            foreach (var k1 in orderCustomFieldEntityObject.dicOrderCustomFieldTypeInfo.Keys)
                            {
                                orderCustomField.OrderCustomFieldTypeInfo[k1] = orderCustomFieldEntityObject.dicOrderCustomFieldTypeInfo[k1];
                            }
                            break;
                        default:
                            var pValue = prop.GetValue(orderCustomFieldEntityObject);

                            PropertyInfo propInfo = typeof(OrderCustomFieldTable).GetProperties()
                                .Where(x => x.Name == prop.Name).FirstOrDefault();

                            if ((pValue?.GetType() ?? null) != (propInfo?.PropertyType ?? null))
                            {
                                var convertedValue = Utils.ConvertObjectToPropertyType(pValue, propInfo);
                                propInfo.SetValue(orderCustomField, convertedValue);
                            }
                            else
                            {
                                propInfo.SetValue(orderCustomField, pValue);
                            }

                            break;
                    }
                }
            }

            return orderCustomField;
        }

        #endregion
    }
}
