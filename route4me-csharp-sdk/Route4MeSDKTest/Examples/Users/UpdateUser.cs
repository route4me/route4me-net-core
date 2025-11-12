using System;

using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Update an User
        /// </summary>
        [Obsolete]
        public void UpdateUser()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateTestUser();

            int memberId = Convert.ToInt32(usersToRemove[usersToRemove.Count - 1]);

            var @params = new MemberParametersV4
            {
                MemberId = memberId,
                member_phone = "571-259-5939"
            };

            // Run the query
            MemberResponseV4 result = route4Me.UserUpdate(@params, out string errorString);

            PrintTestUsers(result, errorString);

            if (result != null && result.GetType() == typeof(MemberResponseV4))
            {
                Console.WriteLine(
                    result.MemberPhone != "571-259-5939"
                        ? "The user phone is not '571-259-5939'"
                        : "The user phone is '571-259-5939'"
                );
            }

            RemoveTestUsers();
        }
    }
}