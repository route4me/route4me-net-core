using System;
using System.Collections.Generic;

using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

using Route4MeSDKLibrary.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of updating a team member's
        /// route visibility permissions:
        ///   - Hide routes scheduled for past dates (HIDE_NONFUTURE_ROUTES)
        ///   - Limit future routes visibility (limit_future_routes_visible)
        ///   - Hide future routes scheduled past N days (display_max_routes_future_days)
        /// </summary>
        public void UpdateTeamMemberPermissions()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            #region Create Member To Update

            membersToRemove = new List<TeamResponse>();
            CreateTestTeamMember();

            if (membersToRemove.Count < 1)
            {
                Console.WriteLine("Cannot create a team member to update");
                return;
            }

            var member = membersToRemove[membersToRemove.Count - 1];

            #endregion

            var queryParams = new MemberQueryParameters()
            {
                UserId = member.MemberId.ToString()
            };

            // Update route visibility permissions
            var requestParams = new TeamRequest()
            {
                HideNonFutureRoutes = true,                // Hide Routes Scheduled For Past Dates
                LimitFutureRoutesVisible = true,           // Enable hiding of future routes past N days
                DisplayMaxRoutesFutureDays = 15            // Number of Days in Advance
            };

            // Run the query
            var updatedMember = route4Me.UpdateTeamMember(
                queryParams,
                requestParams,
                out ResultResponse resultResponse);

            if (updatedMember != null)
            {
                Console.WriteLine("UpdateTeamMemberPermissions executed successfully");
                Console.WriteLine("Member: {0} {1}", updatedMember.MemberFirstName, updatedMember.MemberLastName);
                Console.WriteLine("");

                Console.WriteLine(
                    updatedMember.HideNonFutureRoutes == requestParams.HideNonFutureRoutes
                        ? "  HIDE_NONFUTURE_ROUTES updated successfully: " + updatedMember.HideNonFutureRoutes
                        : "  Cannot update HIDE_NONFUTURE_ROUTES"
                );

                Console.WriteLine(
                    updatedMember.LimitFutureRoutesVisible == requestParams.LimitFutureRoutesVisible
                        ? "  limit_future_routes_visible updated successfully: " + updatedMember.LimitFutureRoutesVisible
                        : "  Cannot update limit_future_routes_visible"
                );

                Console.WriteLine(
                    updatedMember.DisplayMaxRoutesFutureDays == requestParams.DisplayMaxRoutesFutureDays
                        ? "  display_max_routes_future_days updated successfully: " + updatedMember.DisplayMaxRoutesFutureDays
                        : "  Cannot update display_max_routes_future_days"
                );
            }
            else
            {
                PrintTeamMembers(updatedMember, resultResponse);
            }

            RemoveTestTeamMembers();
        }
    }
}