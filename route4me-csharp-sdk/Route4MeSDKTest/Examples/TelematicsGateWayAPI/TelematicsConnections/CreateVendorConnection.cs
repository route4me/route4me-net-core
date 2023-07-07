using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using Route4MeSDKLibrary.DataTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        private string firstMemberId;

        private string apiToken;

        private List<TelematicsConnection> lsCreatedConnections;

        private TelematicsVendors tomtomVendor;

        /// <summary>
        /// The example refers to the process of getting all the telematics connections.
        /// </summary>
        public void CreateVendorConnection(bool removeAfterComplete = true)
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            var vendParams = new TelematicsVendorParameters { Search = "tomtom" };

            var vendors = route4Me.SearchTelematicsVendors(vendParams, out var errorString2);

            tomtomVendor = vendors.Vendors[0];

            lsCreatedConnections = new List<TelematicsConnection>();

            var conParams = new TelematicsConnectionParameters
            {
                VendorID = Convert.ToUInt32(tomtomVendor.ID),
                Vendor = tomtomVendor.Slug,
                AccountId = "12345",
                UserName = "John Doe",
                Password = "password",
                VehiclePositionRefreshRate = 60,
                Name = "Test Telematics Connection from c# SDK",
                ValidateRemoteCredentials = false
            };

            var result = route4Me.CreateTelematicsConnection(apiToken, conParams, out var errorString);

            PrintExampleTelematicsVendor(result, errorString);

            if (result != null && result.GetType() == typeof(TelematicsConnection)) lsCreatedConnections.Add(result);

            if (removeAfterComplete)
            {
                DeleteTelematicsConnection();
                lsCreatedConnections.Clear();
            }
        }
    }
}