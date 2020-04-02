# Changelog
All notable changes to this project will be documented in this file.

## [1.0.0.4] - 2020-03-30

### Added

- The method [UpdateAddressBookContact](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManager.cs#L2308) - for updating the Address Book Contact with data with containg null values.
- The method [UpdateAddressBookContact](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManager.cs#L2326) - for updating the Address Book Contact by sending modified address book contact object.
- The class [ReadOnlyAttribute](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/ReadOnlyAttribute.cs) - for creating custom attribute ReadOnlyAttribute to distinguish the object properties while updating.
- The response class [VehicleV4CreateResponse](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/VehicleV4CreateResponse.cs)) for new vehicle creating process using the endpoint /api.v4/vehicle.php.
- The helper class [DataContractResolver](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataContractResolver.cs) - for assigning the null values to the properties.
- The class [Route4MeDynamicClass](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/Route4MeDynamicClass.cs)) - for creating a dynamic class from an existing class by copying only specified properties.
- The method [UpdateRoute(DataObjectRoute route, DataObjectRoute initialRoute, out string errorString)](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManager.cs#L376) - for updating a route by sending the initial route and cloned-modified route. The algorithm compares the routes, finds modified properties and updates them in the Route4Me database.
- The method [UpdateOptimizationDestination](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManager.cs#L973) to the Route4MeManager class for updating an optimization destination by sending changed Address object.
- The method [AddOptimizationDestinations](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManager.cs#L1839) to the Route4MeManager class for adding the optimization destinations by sending array of the Address objectS.
- The class [Utils](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs)). Modified the methods:
    1. The method [ObjectDeepClone<T>(T obj)](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs#L232)) - for clonning a Route4Me object.
	2. The method [GetPropertiesWithDifferentValues(object modifiedObject, object initialObject, out string errorString, bool excludeReadonly = true)](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs#L266)) - compares initial and modified Route4Me objects and returns a list of the modified properties.
	3. The method [IsPropertyDictionary](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs#L384)) - checks if a Route4Me object property is a dictionary type.
	4. The method [IsPropertyObject](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs#L399)) - checks if a Route4Me object property is a nested object type.
	5. The method [IsPropertyArray](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs#L421)) - checks if a Route4Me object property is an array type.
	6. The method [IsDictionariesEqual](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs#L434)) - checks if the dictionaries are equal.
	7. The method [CheckIfPropertyHasIgnoreAttribute](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs#L480)) - checks if a Route4Me object property has the attribute IgnoreDataMemberAttribute.
	8. The method [CheckIfPropertyHasReadOnlyAttribute](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs#L492)) - checks if a Route4Me object property has the attribute ReadOnllyAttribute.
	9. The method [ReadObjectNew<T>](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs#L59)) - for reading Route4Me object from a JSON string.
	10. The method [GetPropertyPositions<T>](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs#L510)) - for getting positions of the Route4Me object properties.
	11. The method [OrderPropertiesByPosition<T>](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs#L529)) - for the ordering of a property names list by the positions in the object.
	12. The method [ToObject<TValue>](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs#L562)) - for converting anonymous type object to a specified Route4Me object type.


### Changed

- The class [Address](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/Address.cs) - added the custom attribute ReadOnlyAttribute to some properties.
- The enumaration types [Enum](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/Enums.cs): the enumeration types cahnged according to current state of the web application.
- In the class [VehicleV4Response](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/VehicleV4Response.cs) :
   1. Removed the property [IsDeleted].
   2. Removed the property [CreatedTime] - there is the property [TimestampAdded](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/VehicleV4Response.cs#L72) instead.
   3. Removed the property [TimestampRemoved].
   3. The class [VehicleV4Response](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/VehicleV4Response.cs) - all the properties type changed to the string.
- In the class AddressBookContact.cs changed the property [AddressCustomData](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/AddressBookContact.cs#L165) - filters wrong data type for this property.
- In the AddressBookContact.cs changed type of the properties from object type to:
	1. AddressCube -> double?
	2. AddressPieces -> int?
	3. AddressReferenceNo -> string
	4. AddressRevenue -> double?
	5. AddressWeight -> double?
	6. AddressCustomerPo -> string
	
- The method [GetUserLocations](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManager.cs#L1099) - changed returning object type from Dictionary<string,UserLocation> to UserLocation[].
- In the Utils.cs changed the method [SerializeObjectToJson](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs#L94) - for asuring to get correct serialization: if serialization with the DataContractJsonSerializer is failing, serialization with the SerializeObjectToJson will be done.
- The class [Route](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/Route.cs) - added the custom attribute ReadOnlyAttribute to some properties.
- The class [OptimizationParameters](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/OptimizationParameters.cs) - added he properties:
    1. The property [StartDate ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/OptimizationParameters.cs#L80);
	2. The property [EndDate](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/OptimizationParameters.cs#L84).
- The class [RouteParametersQuery](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/RouteParametersQuery.cs) - added he properties:
    1. The property [StartDate ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/RouteParametersQuery.cs#L66);
	2. The property [EndDate](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/RouteParametersQuery.cs#L70).





