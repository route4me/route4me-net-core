using System;
using System.Collections.Generic;

using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

using Route4MeSDKLibrary.DataTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        private List<TelematicsConnection> _lsCreatedConnections;

        private TelematicsVendors _tomtomVendor;

        /// <summary>
        /// The example refers to the process of getting all the telematics connections.
        /// </summary>
        public void CreateVendorConnection(bool removeAfterComplete = true)
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            var vendParams = new TelematicsVendorParameters { Search = "tomtom" };

            var vendors = route4Me.SearchTelematicsVendors(vendParams, out var errorString2);

            _tomtomVendor = vendors.Vendors[0];

            _lsCreatedConnections = new List<TelematicsConnection>();

            var conParams = new TelematicsConnectionParameters
            {
                VendorID = Convert.ToUInt32(_tomtomVendor.ID),
                Vendor = _tomtomVendor.Slug,
                AccountId = "12345",
                UserName = "John Doe",
                Password = "password",
                VehiclePositionRefreshRate = 60,
                Name = "Test Telematics Connection from c# SDK",
                ValidateRemoteCredentials = false
            };

            var result = route4Me.CreateTelematicsConnection(ActualApiKey, conParams, out var errorString);

            PrintExampleTelematicsVendor(result, errorString);

            if (result != null) _lsCreatedConnections.Add(result);

            if (removeAfterComplete)
            {
                DeleteTelematicsConnection();
                _lsCreatedConnections.Clear();
            }
        }
    }
}