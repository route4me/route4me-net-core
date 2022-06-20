using Route4MeSDK.QueryTypes;
using Route4MeSDKLibrary.DataTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting all the telematics connections.
        /// </summary>
        public void DeleteTelematicsConnection()
        {
            CreateVendorConnection(false);

            if (lsCreatedConnections.Count < 1)
            {
                System.Console.WriteLine("Cannot create a test telematics connection to delete");
                return;
            }

            var route4Me = new Route4MeManager(ActualApiKey);

            var result = route4Me.DeleteTelematicsConnection(
                apiToken,
                lsCreatedConnections[lsCreatedConnections.Count - 1].ConnectionToken,
                out var errorString);

            if (result != null && result.GetType()==typeof(TelematicsConnection))
            {
                System.Console.WriteLine($"Telematics connection <{result.Name}> removed");
            }
            else
            {
                System.Console.WriteLine($"Cannot delete telematics connection <{result.Name}>");
            }
            
        }
    }
}
