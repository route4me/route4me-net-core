using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get User By ID
        /// </summary>
        public void GetUserById()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var @params = new MemberParametersV4 { member_id = 45844 };

            // Run the query
            MemberResponseV4 result = route4Me.GetUserById(@params, out string errorString);

            Console.WriteLine("");

            if (result != null)
            {
                Console.WriteLine("GetUserById executed successfully");
                Console.WriteLine("User: " + result.MemberFirstName + " " + result.MemberLastName);
                Console.WriteLine("member_id: " + result.MemberId);
                Console.WriteLine("---------------------------");
            }
            else
            {
                Console.WriteLine("GetUserById error: {0}", errorString);
            }
        }
    }
}
