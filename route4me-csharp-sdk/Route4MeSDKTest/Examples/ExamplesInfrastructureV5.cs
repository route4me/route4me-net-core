using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;
using Route4MeSDKLibrary.DataTypes.V5;
using Route4MeSDKLibrary.DataTypes.V5.Orders;
using static Route4MeSDK.Route4MeManagerV5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public List<TeamResponse> membersToRemove = new List<TeamResponse>();

        private const string testPassword = "pSw1_2_3_4@";

        private string testRatingId;

        List<int> vehicleProfilesToRemove = new List<int>();
        List<int> vehicleCapacityProfilesToRemove = new List<int>();

        public AddressBookContact contact51, contact52, contactToRemoveV5;

        DataObject dataObjectSD10Stops_V5;
        string SD10Stops_optimization_problem_id_V5;

        DataObjectRoute SD10Stops_route_V5;
        string SD10Stops_route_id_V5;

        #region Team Management

        private string GetTestEmail()
        {
            return "test" + DateTime.Now.ToString("yyMMddHHmmss") + "+evgenysoloshenko@gmail.com";
        }

        private void PrintTeamMembers(object result, ResultResponse resultResponse)
        {
            Console.WriteLine("");

            string testName = (new StackTrace()).GetFrame(1).GetMethod().Name;
            testName = testName != null ? testName : "";

            if (result != null)
            {
                Console.WriteLine(testName + " executed successfully");

                if (result.GetType() == typeof(TeamResponse))
                {
                    var member = (TeamResponse)result;
                    Console.WriteLine("Member: {0}", member.MemberFirstName + " " + member.MemberLastName);
                }
                else if (result.GetType() == typeof(TeamResponse[]))
                {
                    var members = (TeamResponse[])result;

                    foreach (var member in members)
                    {
                        Console.WriteLine("Member: {0}", member.MemberFirstName + " " + member.MemberLastName);
                    }
                }
                else
                {
                    Console.WriteLine(testName + ": unknown response type");
                }
            }
            else
            {
                PrintFailResponse(resultResponse, testName);
            }
        }

        private void PrintFailResponse(ResultResponse resultResponse, string testName)
        {
            Console.WriteLine(testName + " failed:");
            Console.WriteLine("Status: {0}", resultResponse?.Status.ToString() ?? "");
            Console.WriteLine("Status code: {0}", resultResponse?.Code.ToString() ?? "");
            Console.WriteLine("Exit code: {0}", resultResponse?.ExitCode.ToString() ?? "");

            if ((resultResponse?.Messages?.Count ?? 0) > 0)
            {
                foreach (var message in resultResponse.Messages)
                {
                    if (message.Key != null && (message.Value?.Length ?? 0) > 0)
                    {
                        Console.WriteLine("");
                        Console.WriteLine((message.Key ?? "") + ":");

                        foreach (var msg in message.Value)
                        {
                            Console.WriteLine("    " + msg);
                        }
                    }
                }
            }
        }

        private TeamResponse GetRandomTeamMember()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            // Run the query
            var members = route4Me.GetTeamMembers(out ResultResponse errorResponse);

            if (members == null) return null;

            Random rnd = new Random();
            int i = rnd.Next(0, members.Count() - 1);

            return members[i];
        }

        private long? GetOwnerMemberId(string anotherApiKey = null)
        {
            var route4Me = new Route4MeManagerV5(anotherApiKey==null ? ActualApiKey : anotherApiKey);

            var members = route4Me.GetTeamMembers(out ResultResponse errorResponse);

            var memberParams = new TeamRequest();

            var ownerMemberId = members
                .Where(x => memberParams.GetMemberType(x.MemberType) == MemberTypes.AccountOwner)
                .FirstOrDefault()
                ?.MemberId ?? null;

            if (ownerMemberId == null)
            {
                Console.WriteLine("Cannot retrieve the team owner - cannot create a member.");
            }

            return ownerMemberId;
        }

        private void CreateTestTeamMember()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            long? ownerId = GetOwnerMemberId();

            if (ownerId == null) return;

            var newMemberParameters = new TeamRequest()
            {
                NewPassword = testPassword,
                MemberFirstName = "John",
                MemberLastName = "Doe",
                MemberCompany = "Test Member To Remove",
                MemberEmail = GetTestEmail(),
                OwnerMemberId = (int)ownerId
            };

            newMemberParameters.SetMemberType(MemberTypes.Driver);

            TeamResponse member = route4Me.CreateTeamMember(newMemberParameters,
                                                            out ResultResponse resultResponse);

            if (member != null && member.GetType() == typeof(TeamResponse)) membersToRemove.Add(member);
        }

        private void RemoveTestTeamMembers()
        {
            if ((membersToRemove?.Count ?? 0) < 1) return;

            var route4Me = new Route4MeManagerV5(ActualApiKey);

            foreach (var member in membersToRemove)
            {
                var memberParams = new MemberQueryParameters()
                {
                    UserId = member.MemberId.ToString()
                };

                var removedMember = route4Me.RemoveTeamMember(memberParams,
                                                out ResultResponse resultResponse);

                Console.WriteLine(
                    (removedMember?.MemberEmail?.Contains(".deleted") ?? false)
                    ? String.Format("A test member {0} removed succsessfully", removedMember.MemberId)
                    : String.Format("Cannot remove a test member {0}", removedMember.MemberId)
                );
            }
        }

        #endregion

        #region Driver Review

        private void PrintDriverReview(object result, ResultResponse resultResponse)
        {
            Console.WriteLine("");

            string testName = (new StackTrace()).GetFrame(1).GetMethod().Name;
            testName = testName != null ? testName : "";

            if (result != null)
            {
                Console.WriteLine(testName + " executed successfully");

                if (result.GetType() == typeof(DriverReview))
                {
                    var review = (DriverReview)result;

                    Console.WriteLine(
                            "Tracking number: {0}, Rating: {1}, Review: {2}",
                            review.TrackingNumber, review.Rating, review.Review
                        );
                }
                else if (result.GetType() == typeof(DriverReviewsResponse))
                {
                    var reviewResponse = (DriverReviewsResponse)result;

                    foreach (var review in reviewResponse.Data)
                    {
                        Console.WriteLine(
                            "Tracking number: {0}, Rating: {1}, Review: {2}",
                            review.TrackingNumber, review.Rating, review.Review
                        );
                    }
                }
                else
                {
                    Console.WriteLine("Unexcepted response type");
                }
            }
            else
            {
                PrintFailResponse(resultResponse, testName);
            }
        }

        private void CreateTestDriverReview()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var newDriverReview = new DriverReview()
            {
                TrackingNumber = "NDRK0M1V", // TO DO: take this value from generated test route later.
                Rating = 4,
                Review = "Test Review"
            };

            var driverReview = route4Me.CreateDriverReview(newDriverReview,
                                                          out ResultResponse resultResponse);

            PrintDriverReview(driverReview, resultResponse);

            testRatingId = driverReview?.Data?.RatingId ?? null;
        }

        private DriverReview GetLastDriverReview()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var queryParameters = new DriverReviewParameters()
            {
                Page = 1,
                PerPage = 20
            };

            var reviewList = route4Me.GetDriverReviewList(queryParameters,
                                                          out ResultResponse resultResponse);

            return (reviewList?.Data?.Length ?? 0) > 1
                ? reviewList.Data[1]
                : null;
        }

        private DriverReview GetDriverReviewById(string ratingId)
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var queryParameters = new DriverReviewParameters()
            {
                RatingId = ratingId
            };

            var review = route4Me.GetDriverReviewById(queryParameters,
                                                          out ResultResponse resultResponse);

            return review?.Data ?? null;
        }

        #endregion

        #region Vehicles V5

        private void PrintTestVehcilesV5(object result, ResultResponse resultResponse)
        {
            Console.WriteLine("");

            string testName = (new StackTrace()).GetFrame(1).GetMethod().Name;
            testName = testName != null ? testName : "";

            if (result != null)
            {
                Console.WriteLine(testName + " executed successfully");

                if (result.GetType() == typeof(Vehicle))
                {
                    var vehicle = (Vehicle)result;

                    Console.WriteLine(
                        "Vehicle ID: {0}, Alias: {1}",
                        vehicle.VehicleId,
                        vehicle.VehicleAlias
                    );
                }
                else if (result.GetType() == typeof(VehicleBase))
                {
                    var vehicle = (VehicleBase)result;

                    Console.WriteLine(
                        "Vehicle ID: {0}, Alias: {1}",
                        vehicle.VehicleId,
                        vehicle.VehicleAlias
                    );
                }
                else if (result.GetType() == typeof(VehicleOrderResponse))
                {
                    var vehicleOrder = (VehicleOrderResponse)result;

                    Console.WriteLine(
                        "Vehicle ID: {0}, Order: {1}",
                        vehicleOrder.VehicleId,
                        vehicleOrder.OrderId
                    );
                }
                else if (result.GetType() == typeof(VehiclesResponse))
                {
                    var vehicles = ((VehiclesResponse)result).Data;

                    foreach (var vehicle in vehicles)
                    {
                        Console.WriteLine(
                        "Vehicle ID: {0}, Alias: {1}",
                        vehicle.VehicleId,
                        vehicle.VehicleAlias
                        );
                    }
                }
                else if (result.GetType() == typeof(VehiclesResults))
                {
                    var vehicles = ((VehiclesResults)result).Results;

                    if ((vehicles?.Length ?? 0)>0)
                    {
                        foreach (var vehicle in vehicles)
                        {
                            Console.WriteLine(
                            "Vehicle ID: {0}, Alias: {1}",
                            vehicle.VehicleId,
                            vehicle.VehicleAlias
                            );
                        }
                    }
                }
                else if (result.GetType() == typeof(Vehicle[]))
                {
                    var vehicles = (Vehicle[])result;

                    foreach (var vehicle in vehicles)
                    {
                        Console.WriteLine(
                        "Vehicle ID: {0}, Alias: {1}",
                        vehicle.VehicleId,
                        vehicle.VehicleAlias
                        );
                    }
                }
                else
                {
                    Console.WriteLine(testName + ": unknown response type");
                }
            }
            else
            {
                PrintFailResponse(resultResponse, testName);
            }
        }

        private void PrintTestVehcileProfilesV5(object result, ResultResponse resultResponse)
        {
            Console.WriteLine("");

            string testName = (new StackTrace()).GetFrame(1).GetMethod().Name;
            testName = testName != null ? testName : "";

            if (result != null)
            {
                Console.WriteLine(testName + " executed successfully");

                if (result.GetType() == typeof(VehicleProfile))
                {
                    var vehicleProfile = (VehicleProfile)result;

                    Console.WriteLine(
                        "Vehicle profile ID: {0}, Name: {1}",
                        vehicleProfile.VehicleProfileId,
                        vehicleProfile.Name
                    );
                }
                else if (result.GetType() == typeof(VehicleProfilesResponse))
                {
                    var vehicleProfileResponse = (VehicleProfilesResponse)result;

                    Console.WriteLine($"URL to the paginated list: {vehicleProfileResponse.FirstPageUrl}");
                    Console.WriteLine($"Tota; number in the list: {vehicleProfileResponse.Total}");
                }
                else
                {
                    PrintFailResponse(resultResponse, testName);
                }
            }
            
        }

        private void PrintTestVehcileCapacityProfilesV5(object result, ResultResponse resultResponse)
        {
            Console.WriteLine("");

            string testName = (new StackTrace()).GetFrame(1).GetMethod().Name;
            testName = testName != null ? testName : "";

            if (result != null)
            {
                Console.WriteLine(testName + " executed successfully");

                if (result.GetType() == typeof(VehicleCapacityProfileResponse))
                {
                    var vehicleCapacityProfile = (VehicleCapacityProfileResponse)result;

                    Console.WriteLine(
                        "Vehicle capacity profile ID: {0}, Name: {1}",
                        vehicleCapacityProfile.Data.VehicleCapacityProfileId,
                        vehicleCapacityProfile.Data.Name
                    );
                }
                else if (result.GetType() == typeof(VehicleCapacityProfilesResponse))
                {
                    var vehicleProfiles = (VehicleCapacityProfilesResponse)result;

                    Console.WriteLine($"URL to the paginated list: {vehicleProfiles.Links.First}");
                    Console.WriteLine($"Total number in the list: {vehicleProfiles.Data.Length}");
                }
                else
                {
                    PrintFailResponse(resultResponse, testName);
                }
            }
        }


        private void RemoveTestVehiclesV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            // Run the query
            if ((vehiclesToRemove?.Count ?? 0) < 1) return;

            foreach (var vehicleId in vehiclesToRemove)
            {
                var result = route4Me.DeleteVehicle(vehicleId, out ResultResponse resultResponse);

                Console.WriteLine(
                    (result != null && result.GetType() == typeof(Vehicle))
                    ? String.Format("The vehicle {0} removed successfully.", vehicleId)
                    : String.Format("Cannot remove the vehicle {0}.", vehicleId)
                );
            }
        }

        private void RemoveTestVehicleProfilesV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            // Run the query
            if ((vehicleProfilesToRemove?.Count ?? 0) < 1) return;

            foreach (var vehicleProfileId in vehicleProfilesToRemove)
            {
                var profileParams = new VehicleProfileParameters()
                {
                    VehicleProfileId = (long)vehicleProfileId
                };

                var result = route4Me.DeleteVehicleProfile(profileParams, out ResultResponse resultResponse);

                Console.WriteLine(
                    (result != null && resultResponse==null)
                    ? String.Format("The vehicle profile {0} removed successfully.", vehicleProfileId)
                    : String.Format("Cannot remove the vehicle profile {0}.", vehicleProfileId)
                );
            }
        }

        private void RemoveTestVehicleCapacityProfilesV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            // Run the query
            if ((vehicleCapacityProfilesToRemove?.Count ?? 0) < 1) return;

            foreach (var vehicleCapacityProfileId in vehicleCapacityProfilesToRemove)
            {
                var capacityProfileParams = new VehicleCapacityProfileParameters()
                {
                    VehicleCapacityProfileId = (long)vehicleCapacityProfileId
                };

                var result = route4Me.DeleteVehicleCapacityProfile(capacityProfileParams, out ResultResponse resultResponse);

                Console.WriteLine(
                    (result != null && resultResponse == null)
                    ? String.Format("The vehicle profile {0} removed successfully.", vehicleCapacityProfileId)
                    : String.Format("Cannot remove the vehicle profile {0}.", vehicleCapacityProfileId)
                );
            }
        }

        #endregion

        #region Address Book Contacts

        public void CreateTestContactsV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var contact = new AddressBookContact()
            {
                FirstName = "Test FirstName " + (new Random()).Next().ToString(),
                Address1 = "Test Address1 " + (new Random()).Next().ToString(),
                AddressStopType = AddressStopType.Delivery.Description(),
                CachedLat = 38.024654,
                CachedLng = -77.338814,
                EligibleDepot = true
            };

            // Run the query
            contact51 = route4Me.AddAddressBookContact(contact, out ResultResponse resultResponse);

            Assert.IsNotNull(
                contact51, "AddAddressBookContactsTest failed. Code = " + resultResponse?.Code ?? "Undefined");

            int location1 = contact51.AddressId != null ? Convert.ToInt32(contact51.AddressId) : -1;

            ContactsToRemove = new List<string>();

            if (location1 > 0) ContactsToRemove.Add(location1.ToString());

            var dCustom = new Dictionary<string, string>()
            {
                {"FirstFieldName1", "FirstFieldValue1"},
                {"FirstFieldName2", "FirstFieldValue2"}
            };

            contact = new AddressBookContact()
            {
                FirstName = "Test FirstName " + (new Random()).Next().ToString(),
                Address1 = "Test Address1 " + (new Random()).Next().ToString(),
                AddressStopType = AddressStopType.PickUp.Description(),
                CachedLat = 38.024654,
                CachedLng = -77.338814,
                AddressCustomData = dCustom,
                EligibleDepot = true
            };

            contact52 = route4Me.AddAddressBookContact(contact, out resultResponse);

            Assert.IsNotNull(contact52, "AddAddressBookContactsTest failed. Code = " + (resultResponse?.Code ?? 0));

            int location2 = contact52.AddressId != null ? Convert.ToInt32(contact52.AddressId) : -1;

            if (location2 > 0) ContactsToRemove.Add(location2.ToString());

            var contactParams = new AddressBookContact()
            {
                FirstName = "Test FirstName Rem" + (new Random()).Next().ToString(),
                Address1 = "Test Address1 Rem " + (new Random()).Next().ToString(),
                CachedLat = 38.02466,
                CachedLng = -77.33882
            };

            contactToRemoveV5 = route4Me.AddAddressBookContact(contactParams, out resultResponse);

            if (contactToRemove != null && contactToRemove.GetType() == typeof(AddressBookContact))
                ContactsToRemove.Add(contactToRemove.AddressId.ToString());
        }

        #endregion

        private bool RunOptimizationSingleDriverRoute10StopsV5(string routeName = null)
        {
            var r4mm = new Route4MeManagerV5(ActualApiKey);

            // Prepare the addresses
            Address[] addresses = new Address[]
            {
            #region Addresses

            new Address() { AddressString = "151 Arbor Way Milledgeville GA 31061",
                            //indicate that this is a departure stop
                            //single depot routes can only have one departure depot 
                            IsDepot = true, 
                        
                            //required coordinates for every departure and stop on the route
                            Latitude = 33.132675170898,
                            Longitude = -83.244743347168,
                        
                            //the expected time on site, in seconds. this value is incorporated into the optimization engine
                            //it also adjusts the estimated and dynamic eta's for a route
                            Time = 0, 
                        
                        
                            //input as many custom fields as needed, custom data is passed through to mobile devices and to the manifest
                            CustomFields = new Dictionary<string, string>() {{"color", "red"}, {"size", "huge"}}
            },

            new Address() { AddressString = "230 Arbor Way Milledgeville GA 31061",
                            Latitude = 33.129695892334,
                            Longitude = -83.24577331543,
                            Time = 0 },

            new Address() { AddressString = "148 Bass Rd NE Milledgeville GA 31061",
                            Latitude = 33.143497,
                            Longitude = -83.224487,
                            Time = 0 },

            new Address() { AddressString = "117 Bill Johnson Rd NE Milledgeville GA 31061",
                            Latitude = 33.141784667969,
                            Longitude = -83.237518310547,
                            Time = 0 },

            new Address() { AddressString = "119 Bill Johnson Rd NE Milledgeville GA 31061",
                            Latitude = 33.141086578369,
                            Longitude = -83.238258361816,
                            Time = 0 },

            new Address() { AddressString =  "131 Bill Johnson Rd NE Milledgeville GA 31061",
                            Latitude = 33.142036437988,
                            Longitude = -83.238845825195,
                            Time = 0 },

            new Address() { AddressString =  "138 Bill Johnson Rd NE Milledgeville GA 31061",
                            Latitude = 33.14307,
                            Longitude = -83.239334,
                            Time = 0 },

            new Address() { AddressString =  "139 Bill Johnson Rd NE Milledgeville GA 31061",
                            Latitude = 33.142734527588,
                            Longitude = -83.237442016602,
                            Time = 0 },

            new Address() { AddressString =  "145 Bill Johnson Rd NE Milledgeville GA 31061",
                            Latitude = 33.143871307373,
                            Longitude = -83.237342834473,
                            Time = 0 },

            new Address() { AddressString =  "221 Blake Cir Milledgeville GA 31061",
                            Latitude = 33.081462860107,
                            Longitude = -83.208511352539,
                            Time = 0 }

            #endregion
            };

            // Set parameters
            var parameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.TSP,
                RouteName =
                    routeName == null
                    ? "SD Route 10 Stops Test " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    : routeName,
                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                RouteTime = 60 * 60 * 7,
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description()
            };

            var optimizationParameters = new OptimizationParameters()
            {
                Addresses = addresses,
                Parameters = parameters
            };

            // Run the query

            try
            {
                dataObjectSD10Stops_V5 = r4mm.RunOptimization(optimizationParameters, out ResultResponse resultResponse);

                SD10Stops_optimization_problem_id_V5 = dataObjectSD10Stops_V5.OptimizationProblemId;
                SD10Stops_route_V5 = (dataObjectSD10Stops_V5 != null && dataObjectSD10Stops_V5.Routes != null && dataObjectSD10Stops_V5.Routes.Length > 0) ? dataObjectSD10Stops_V5.Routes[0] : null;
                SD10Stops_route_id_V5 = (SD10Stops_route_V5 != null) ? SD10Stops_route_V5.RouteID : null;

                if (dataObjectSD10Stops_V5 != null && dataObjectSD10Stops_V5.Routes != null && dataObjectSD10Stops_V5.Routes.Length > 0)
                {
                    SD10Stops_route_V5 = dataObjectSD10Stops_V5.Routes[0];
                }
                else
                {
                    SD10Stops_route_V5 = null;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Single Driver Route 10 Stops generation failed... " + ex.Message);
                return false;
            }
        }

        #region Orders History

        public void PrintTestOrders(object result)
        {
            Console.WriteLine("");

            string testName = (new StackTrace()).GetFrame(1).GetMethod().Name;
            testName = testName != null ? testName : "";

            if (result != null)
            {
                if (result.GetType()==typeof(OrderHistoryResponse))
                {
                    var historyResponse = (OrderHistoryResponse)result;

                    if ((historyResponse.Results?.Length ?? 0)>0)
                    {
                        foreach (var h1 in historyResponse.Results)
                        {
                            Console.WriteLine($"Order ID: {h1.OrderId}");
                            Console.WriteLine($"Address1: {h1.OrderModel.Address1}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Cannot find records in the orders' history");
                    }
                }
                else if (result.GetType() == typeof(ArchiveOrdersResponse))
                {
                    var archiveResponse = (ArchiveOrdersResponse)result;

                    if ((archiveResponse.Items?.Length ?? 0) > 0)
                    {
                        foreach (var a1 in archiveResponse.Items)
                        {
                            Console.WriteLine($"Address 1: {a1.Address1}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Cannot find records in the archive's response");
                    }
                }
                else
                {
                    Console.WriteLine("Unknown data type");
                }

            }
            else
            {
                Console.WriteLine("The result is null");
            }
        }

        #endregion
    }
}
