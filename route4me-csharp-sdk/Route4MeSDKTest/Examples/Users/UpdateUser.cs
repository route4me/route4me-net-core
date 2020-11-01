using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Update an User
        /// </summary>
        public void UpdateUser()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var @params = new MemberParametersV4
            {
                member_id = 220461,
                member_phone = "571-259-5939"
            };

            // Run the query
            MemberResponseV4 result = route4Me.UserUpdate(@params, out string errorString);

            Console.WriteLine("");

            if (result != null)
            {
                Console.WriteLine("UpdateUser executed successfully");
                Console.WriteLine("status: " + result.MemberFirstName + " " + result.MemberLastName);
                Console.WriteLine("member_id: " + result.MemberId);
                Console.WriteLine("---------------------------");
            }
            else
            {
                Console.WriteLine("UpdateUser error: {0}", errorString);
            }
        }
    }
}
