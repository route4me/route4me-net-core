using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of updating a telematics connection.
        /// </summary>
        public void UpdateTelematicsConnection()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var conParams = new TelematicsConnectionParameters
            {
                VendorID = Convert.ToUInt32(tomtomVendor.ID),
                AccountId = "12345",
                UserName = "John Doe",
                Password = "password",
                VehiclePositionRefreshRate = 60,
                Name = "Test Telematics Connection from c# SDK",
                ValidateRemoteCredentials = false
            };

            // Run the query
            var vendors = route4Me.UpdateTelematicsConnection(
                                            "apiToken", 
                                            "connectionToken", 
                                            conParams,
                                            out string errorString);

            PrintExampleTelematicsVendor(vendors, errorString);
        }
    }
}
