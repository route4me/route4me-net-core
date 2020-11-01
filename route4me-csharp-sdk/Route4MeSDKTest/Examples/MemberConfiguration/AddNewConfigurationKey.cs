using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Create New Configuration Key
        /// </summary>
        public void AddNewConfigurationKey()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var @params = new MemberConfigurationParameters
            {
                ConfigKey = "destination_icon_uri",
                ConfigValue = "value"
            };

            // Run the query
            MemberConfigurationResponse result = route4Me.CreateNewConfigurationKey(@params, out string errorString);

            Console.WriteLine("");

            if (result != null)
            {
                Console.WriteLine("AddNewConfigurationKey executed successfully");
                Console.WriteLine("Result: " + result.Result);
                Console.WriteLine("Affected: " + result.Affected);
                Console.WriteLine("---------------------------");
            }
            else
            {
                Console.WriteLine("AddNewConfigurationKey error: {0}", errorString);
            }
        }
    }
}
