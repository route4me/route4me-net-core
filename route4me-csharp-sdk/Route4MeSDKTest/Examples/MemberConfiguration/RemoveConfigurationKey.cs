using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Remove a Configuration Key
        /// </summary>
        public void RemoveConfigurationKey()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var @params = new MemberConfigurationParameters { ConfigKey = "My height" };

            // Run the query
            MemberConfigurationResponse result = route4Me.RemoveConfigurationKey(@params, out string errorString);

            Console.WriteLine("");

            if (result != null)
            {
                Console.WriteLine("RemoveConfigurationKey executed successfully");
                Console.WriteLine("Result: " + result.Result);
                Console.WriteLine("Affected: " + result.Affected);
                Console.WriteLine("---------------------------");
            }
            else
            {
                Console.WriteLine("UserRegistration error: {0}", errorString);
            }

        }
    }
}
