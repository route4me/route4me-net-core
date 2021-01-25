# Changelog
All notable changes to this project will be documented in this file.

## [1.0.1.1] - 2021-01-21

The project was migrated to the version .net core 3.1.

### Added

The file [AddressBookContact.cs: ](https://github.com/route4me/route4me-net-core/blob/master/Route4MeDB/ApplicationCore/Entities/AddressBookContactAggregate/AddressBookContact.cs)   
– **Properties**: MemberId(int?), InRouteCount(int?), VisitedCount(int?), LastVisitedTimestamp(long?),  LastRoutedTimestamp(long?)
	
The file [Address.cs: ](https://github.com/route4me/route4me-net-core/blob/master/Route4MeDB/ApplicationCore/Entities/RouteAddressAggregate/Address.cs)  
 **Properties**: UduDistanceToNextDestination(double?), WaitTimeToNextDestination(long?), BundleCount(BundledItemResponse[]), BundleItems(string), Pickup(string), Dropoff(string), Joint(int?), Tags(string[])

The file [AddressManifest.cs: ](https://github.com/route4me/route4me-net-core/blob/master/Route4MeDB/ApplicationCore/Entities/RouteAddressAggregate/AddressManifest.cs)   
– **Properties**: ScheduledArrivalTimeTs(long?), ScheduledDepartureTimeTs(long?), UduRunningDistance(double?).

The file [OptimizationProblem.cs: ](https://github.com/route4me/route4me-net-core/blob/master/Route4MeDB/ApplicationCore/Entities/RouteAggregate/OptimizationProblem.cs)   
– **Properties**: SmartOptimizationId(string), OptimizationErrors(string[]).

The file [Route.cs : ](https://github.com/route4me/route4me-net-core/blob/master/Route4MeDB/ApplicationCore/Entities/RouteAggregate/Route.cs)   
– **Properties**: RouteWeight(double?), RouteCube(double?), RoutePieces(int?), OriginalRoute(Route), BundleItems(BundledItemResponse[]).

The file [RouteParameters.cs : ](https://github.com/route4me/route4me-net-core/blob/master/Route4MeDB/ApplicationCore/Entities/RouteAggregate/RouteParameters.cs)   
– **Properties**: TargetDuration(double?), TargetDistance(double?), WaitingTime(double?), IgnoreTw(bool?), Slowdowns(SlowdownParams), is_dynamic_start_time(bool), Bundling(AddressBundling). 

The file [Enum.cs : ](https://github.com/route4me/route4me-net-core/blob/master/Route4MeDB/ApplicationCore/Enum.cs)   
– **Enumerations**:  AddressBundlingMode, AddressBundlingMergeMode, AddressBundlingFirstItemMode, AddressBundlingAdditionalItemsMode.

The file [DirectionStep.cs: ](https://github.com/route4me/route4me-net-core/blob/master/Route4MeDB/ApplicationCore/Entities/RouteAggregate/DirectionStep.cs)   
– **Property**: UduDistance(double?).

The file [BundledItemResponse.cs : ](https://github.com/route4me/route4me-net-core/blob/master/Route4MeDB/ApplicationCore/Entities/RouteAddressAggregate/BundledItemResponse.cs)   
– **Class**: BundledItemResponse.

The file [AddressBundling.cs : ](https://github.com/route4me/route4me-net-core/blob/master/Route4MeDB/ApplicationCore/Entities/RouteAggregate/AddressBundling.cs)   
– **Class**: AddressBundling.

The file [RouteAdvancedConstraints.cs : ](https://github.com/route4me/route4me-net-core/blob/master/Route4MeDB/ApplicationCore/Entities/RouteAggregate/RouteAdvancedConstraints.cs)   
– **Class**: RouteAdvancedConstraints.

The file [ServiceTimeRulesClass.cs : ](https://github.com/route4me/route4me-net-core/blob/master/Route4MeDB/ApplicationCore/Entities/RouteAggregate/ServiceTimeRulesClass.cs)   
– **Class**: ServiceTimeRulesClass.

The file [SlowdownParams.cs : ](https://github.com/route4me/route4me-net-core/blob/master/Route4MeDB/ApplicationCore/Entities/RouteAggregate/SlowdownParams.cs)   
– **Class**: SlowdownParams.


	
### Changed

The file [AddressBookContact.cs: ](https://github.com/route4me/route4me-net-core/blob/master/Route4MeDB/ApplicationCore/Entities/AddressBookContactAggregate/AddressBookContact.cs)
– Type int? replaced with long? for the property: ServiceTime.
– Type float replaced with double? for the properties: AddressCube, AddressRevenue, AddressWeight.
– Type int replaced with int? for the property: AddressPieces.
	
The file [Address.cs: ](https://github.com/route4me/route4me-net-core/blob/master/Route4MeDB/ApplicationCore/Entities/RouteAddressAggregate/Address.cs)  
– Type int replaced with int? for the property: MemberId.
– Type int? replaced with long? for the properties: DriveTimeToNextDestination, AbnormalTrafficTimeToNextDestination, UncongestedTimeToNextDestination, TrafficTimeToNextDestination, Time.
– The property name geofence_detected_visited_timestamp changed to GeofenceDetectedVisitedTimestamp.
– The property name geofence_detected_departed_timestamp changed to GeofenceDetectedDepartedTimestamp.
– The property name geofence_detected_service_time changed to GeofenceDetectedService_time.
– The property name geofence_detected_visited_lat changed to GeofenceDetectedVisitedLat.
– The property name geofence_detected_visited_lng changed to GeofenceDetectedVisitedLng.
– The property name geofence_detected_departed_lat changed to GeofenceDetectedDepartedLat.
– The property name geofence_detected_departed_lng changed to GeofenceDetectedDepartedLng.

The file [RouteParameters.cs : ](https://github.com/route4me/route4me-net-core/blob/master/Route4MeDB/ApplicationCore/Entities/RouteAggregate/RouteParameters.cs)   
– Type int? replaced with long? for the property: Ip.

The file [RouteVehicle.cs : ](https://github.com/route4me/route4me-net-core/blob/master/Route4MeDB/ApplicationCore/Entities/RouteAggregate/RouteVehicle.cs)   
– All the properties' types were set to the string.
	




