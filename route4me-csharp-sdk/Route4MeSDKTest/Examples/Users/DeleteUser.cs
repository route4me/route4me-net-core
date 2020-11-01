using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Remove User
        /// </summary>
        public void DeleteUser()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var @params = new MemberParametersV4 { member_id = 147824 };

            // Run the query
            bool result = route4Me.UserDelete(@params, out string errorString);

            Console.WriteLine("");

            if (result)
            {
                Console.WriteLine("DeleteUser executed successfully");
                Console.WriteLine("---------------------------");
            }
            else
            {
                Console.WriteLine("DeleteUser error: {0}", errorString);
            }
        }
    }
}
