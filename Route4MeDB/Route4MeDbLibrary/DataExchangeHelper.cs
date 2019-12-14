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
using GeocodingEntity = Route4MeDB.ApplicationCore.Entities.GeocodingAggregate.Geocoding;
using PathToNextEntity = Route4MeDB.ApplicationCore.Entities.RouteAddressAggregate.PathToNext;
using PathEntity = Route4MeDB.ApplicationCore.Entities.RouteAggregate.Path;
using TrackingHistoryEntity = Route4MeDB.ApplicationCore.Entities.RouteAggregate.TrackingHistory;
using DirectionEntity = Route4MeDB.ApplicationCore.Entities.RouteAggregate.Direction;
using Route4MeDB.ApplicationCore;
using Route4MeDB.Route4MeDbLibrary;
using EnumR4M = Route4MeDB.ApplicationCore.Enum;
using Route4MeDB.ApplicationCore.Services;
using System.Linq;
using System.IO;
using Newtonsoft.Json.Linq;
using Route4Me = Route4MeSDK.DataTypes;
using RouteTable = Route4MeSDK.DataTypes.DataObjectRoute;
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

        /// <summary>
        /// Converts JSON content of a Route4Me SDK object (e.g. response from query of optimization/route/address etc) 
        /// to a Route4Me entity.
        /// </summary>
        /// <typeparam name="T">Route4MeDb entity type:
        /// available types: 
        /// OptimizationProblem, Route, Address, AddressNote, Geocoding, TrackingHistory, Direction</typeparam>
        /// <param name="jsonContent">JSON content of the Route4Me object</param>
        /// <param name="errorMessage">Error message n cases of failure</param>
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
                            typeof(OptimizationProblem).GetProperties().Where(x => x.Name == prop.Name).FirstOrDefault()
                            .SetValue(optimization, prop.GetValue(optimizationObject));
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

            if (scheduleObject.Enabled!=null) scheduleEntity.Enabled = scheduleObject.Enabled;
            if (scheduleObject.Mode != null) scheduleEntity.Mode = scheduleObject.Mode;
            if (scheduleObject.From != null) scheduleEntity.From = scheduleObject.From;

            if (scheduleObject.Daily!=null)
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

                if (scheduleObject.Monthly.Every!=null) scheduleEntity.Monthly.Every = scheduleObject.Monthly.Every;
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
    }
}
