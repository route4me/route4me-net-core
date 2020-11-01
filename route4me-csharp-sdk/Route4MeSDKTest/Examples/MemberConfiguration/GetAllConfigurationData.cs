using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get All Configuration Data
        /// </summary>
        public void GetAllConfigurationData()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var @params = new MemberConfigurationParameters();

            // Run the query
            MemberConfigurationDataResponse result = route4Me.GetConfigurationData(@params, out string errorString);

            Console.WriteLine("");

            if (result != null)
            {
                Console.WriteLine("GetAllConfigurationData executed successfully");
                Console.WriteLine("Result: " + result.Result);
                foreach (MemberConfigurationData mc_data in result.Data)
                {
                    Console.WriteLine("member_id= " + mc_data.MemberId);
                    Console.WriteLine("config_key= " + mc_data.ConfigKey);
                    Console.WriteLine("config_value= " + mc_data.ConfigValue);
                    Console.WriteLine("---------------------------");
                }
            }
            else
            {
                Console.WriteLine("GetAllConfigurationData error: {0}", errorString);
            }
        }
    }
}
