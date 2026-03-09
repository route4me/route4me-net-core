using System;
using System.Collections.Generic;

using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of creating a team member
        /// with route visibility permissions:
        ///   - Hide routes scheduled for past dates (HIDE_NONFUTURE_ROUTES)
        ///   - Limit future routes visibility (limit_future_routes_visible)
        ///   - Hide future routes scheduled past N days (display_max_routes_future_days)
        /// </summary>
        public void CreateTeamMemberWithPermissions()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            long? ownerId = GetOwnerMemberId();

            if (ownerId == null) return;

            var newMemberParameters = new TeamRequest()
            {
                NewPassword = testPassword,
                MemberFirstName = "Driver",
                MemberLastName = "WithPermissions",
                MemberCompany = "Test Company",
                MemberEmail = GetTestEmail(),
                OwnerMemberId = (int)ownerId,

                // Route Visibility Permissions
                HideNonFutureRoutes = true,                // Hide Routes Scheduled For Past Dates
                LimitFutureRoutesVisible = true,           // Enable hiding of future routes past N days
                DisplayMaxRoutesFutureDays = 15            // Number of Days in Advance
            };

            newMemberParameters.SetMemberType(MemberTypes.Driver);

            // Run the query
            var member = route4Me.CreateTeamMember(newMemberParameters,
                out ResultResponse resultResponse);

            if (member != null && member.GetType() == typeof(TeamResponse))
            {
                membersToRemove.Add(member);

                Console.WriteLine("CreateTeamMemberWithPermissions executed successfully");
                Console.WriteLine("Member: {0} {1}", member.MemberFirstName, member.MemberLastName);
                Console.WriteLine("  HIDE_NONFUTURE_ROUTES:          {0}", member.HideNonFutureRoutes);
                Console.WriteLine("  limit_future_routes_visible:    {0}", member.LimitFutureRoutesVisible);
                Console.WriteLine("  display_max_routes_future_days: {0}", member.DisplayMaxRoutesFutureDays);
            }
            else
            {
                PrintTeamMembers(member, resultResponse);
            }

            RemoveTestTeamMembers();
        }
    }
}
