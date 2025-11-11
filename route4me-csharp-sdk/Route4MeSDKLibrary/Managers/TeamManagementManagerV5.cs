using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;
using Route4MeSDKLibrary.DataTypes.V5;
using Route4MeSDKLibrary.DataTypes.V5.Internal.Requests;
using Route4MeSDKLibrary.QueryTypes.V5;

namespace Route4MeSDKLibrary.Managers
{
    public class TeamManagementManagerV5 : Route4MeManagerBase
    {
        public TeamManagementManagerV5(string apiKey) : base(apiKey)
        {
        }

        /// <summary>
        ///     Retrieve all existing sub-users associated with the Member's account.
        /// </summary>
        /// <param name="parameters">Query parameters for filtering team members (optional)</param>
        /// <param name="failResponse">Failing response</param>
        /// <returns>An array of the TeamResponseV5 type objects</returns>
        public TeamResponse[] GetTeamMembers(TeamUsersQueryParameters parameters, out ResultResponse failResponse)
        {
            var queryParameters = parameters ?? new TeamUsersQueryParameters();

            var result = GetJsonObjectFromAPI<TeamResponse[]>(queryParameters,
                R4MEInfrastructureSettingsV5.TeamUsers,
                HttpMethodType.Get,
                out failResponse);

            return result;
        }
        

        /// <summary>
        ///     Retrieve all existing sub-users associated with the Member's account.
        /// </summary>
        /// <param name="failResponse">Failing response</param>
        /// <returns>An array of the TeamResponseV5 type objects</returns>
        public TeamResponse[] GetTeamMembers(out ResultResponse failResponse)
        {
            return GetTeamMembers(null, out failResponse);
        }

        /// <summary>
        ///     Retrieve all existing sub-users associated with the Member's account.
        /// </summary>
        /// <param name="parameters">Query parameters for filtering team members (optional)</param>
        /// <returns>An array of the TeamResponseV5 type objects</returns>
        public Task<Tuple<TeamResponse[], ResultResponse>> GetTeamMembersAsync(TeamUsersQueryParameters parameters = null)
        {
            var queryParameters = parameters ?? new TeamUsersQueryParameters();

            return GetJsonObjectFromAPIAsync<TeamResponse[]>(queryParameters,
                R4MEInfrastructureSettingsV5.TeamUsers,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Retrieve a team member by the parameter UserId
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Retrieved team member</returns>
        public TeamResponse GetTeamMemberById(MemberQueryParameters parameters,
            out ResultResponse resultResponse)
        {
            if (parameters?.UserId == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The UserId parameter is not specified"}}
                    }
                };

                return null;
            }

            var result = GetJsonObjectFromAPI<TeamResponse>(parameters,
                R4MEInfrastructureSettingsV5.TeamUsers + "/" + parameters.UserId,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Retrieve a team member by the parameter UserId
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <returns>Retrieved team member</returns>
        public Task<Tuple<TeamResponse, ResultResponse>> GetTeamMemberByIdAsync(MemberQueryParameters parameters)
        {
            if (parameters?.UserId == null)
            {
                return Task.FromResult(new Tuple<TeamResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The UserId parameter is not specified"}}
                    }
                }));
            }

            return GetJsonObjectFromAPIAsync<TeamResponse>(parameters,
                R4MEInfrastructureSettingsV5.TeamUsers + "/" + parameters.UserId,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Creates new team member (sub-user) in the user's account
        /// </summary>
        /// <param name="memberParams">An object of the type MemberParametersV4</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Created team member</returns>
        public TeamResponse CreateTeamMember(TeamRequest memberParams,
            out ResultResponse resultResponse)
        {
            if (!memberParams.ValidateMemberCreateRequest(out var error0))
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {error0}}
                    }
                };

                return null;
            }

            return GetJsonObjectFromAPI<TeamResponse>(
                memberParams,
                R4MEInfrastructureSettingsV5.TeamUsers,
                HttpMethodType.Post,
                out resultResponse);
        }

        /// <summary>
        ///     Creates new team member (sub-user) in the user's account
        /// </summary>
        /// <param name="memberParams">An object of the type MemberParametersV4</param>
        /// <returns>Created team member</returns>
        public Task<Tuple<TeamResponse, ResultResponse>> CreateTeamMemberAsync(TeamRequest memberParams)
        {
            if (!memberParams.ValidateMemberCreateRequest(out var error0))
            {
                return Task.FromResult(new Tuple<TeamResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {error0}}
                    }
                }));
            }

            return GetJsonObjectFromAPIAsync<TeamResponse>(
                memberParams,
                R4MEInfrastructureSettingsV5.TeamUsers,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Bulk create the team members
        ///     TO DO: there is no response from the function.
        /// </summary>
        /// <param name="membersParams"></param>
        /// <param name="conflicts">Conflict resolving rule</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns></returns>
        public ResultResponse BulkCreateTeamMembers(TeamRequest[] membersParams, Conflicts conflicts, out ResultResponse resultResponse)
        {
            resultResponse = default;

            if (membersParams == null || (membersParams.Length == 0))
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The array of the user parameters is empty"}}
                    }
                };

                return null;
            }

            foreach (var memberParams in membersParams)
                if (!memberParams.ValidateMemberCreateRequest(out var error0))
                {
                    resultResponse = new ResultResponse
                    {
                        Status = false,
                        Messages = new Dictionary<string, string[]>
                        {
                            {"Error", new[] {error0}}
                        }
                    };

                    return null;
                }

            var newMemberParams = new BulkMembersRequest
            {
                Users = membersParams,
                Conflicts = conflicts.Description()
            };

            var result = GetJsonObjectFromAPI<ResultResponse>(
                newMemberParams,
                R4MEInfrastructureSettingsV5.TeamUsersBulkCreate,
                HttpMethodType.Post,
                out resultResponse);

            return new ResultResponse()
            {
                Code = resultResponse == null ? 202 : 0
            };
        }

        /// <summary>
        ///     Bulk create the team members
        ///     TO DO: there is no response from the function.
        /// </summary>
        /// <param name="membersParams"></param>
        /// <param name="conflicts">Conflict resolving rule</param>
        /// <returns></returns>
        public Task<Tuple<ResultResponse, ResultResponse>> BulkCreateTeamMembersAsync(TeamRequest[] membersParams, Conflicts conflicts)
        {
            if (membersParams == null || membersParams.Length == 0)
            {
                return Task.FromResult(new Tuple<ResultResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The array of the user parameters is empty"}}
                    }
                }));
            }

            foreach (var memberParams in membersParams)
            {
                if (!memberParams.ValidateMemberCreateRequest(out var error0))
                {
                    return Task.FromResult(new Tuple<ResultResponse, ResultResponse>(null, new ResultResponse
                    {
                        Status = false,
                        Messages = new Dictionary<string, string[]>
                        {
                            {"Error", new[] {error0}}
                        }
                    }));
                }
            }

            var newMemberParams = new BulkMembersRequest
            {
                Users = membersParams,
                Conflicts = conflicts.Description()
            };

            return GetJsonObjectFromAPIAsync<ResultResponse>(
                newMemberParams,
                R4MEInfrastructureSettingsV5.TeamUsersBulkCreate,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Removes a team member (sub-user) from the user's account.
        /// </summary>
        /// <param name="parameters">An object of the type MemberParametersV4 containg the parameter UserId</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Removed team member</returns>
        public TeamResponse RemoveTeamMember(MemberQueryParameters parameters,
            out ResultResponse resultResponse)
        {
            if (parameters?.UserId == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The UserId parameter is not specified"}}
                    }
                };

                return null;
            }

            var response = GetJsonObjectFromAPI<TeamResponse>(
                parameters,
                R4MEInfrastructureSettingsV5.TeamUsers + "/" + parameters.UserId,
                HttpMethodType.Delete,
                out resultResponse);

            return response;
        }

        /// <summary>
        ///     Removes a team member (sub-user) from the user's account.
        /// </summary>
        /// <param name="parameters">An object of the type MemberParametersV4 containg the parameter UserId</param>
        /// <returns>Removed team member</returns>
        public Task<Tuple<TeamResponse, ResultResponse>> RemoveTeamMemberAsync(MemberQueryParameters parameters)
        {
            if (parameters?.UserId == null)
            {
                return Task.FromResult(new Tuple<TeamResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The UserId parameter is not specified"}}
                    }
                }));
            }

            return GetJsonObjectFromAPIAsync<TeamResponse>(
                parameters,
                R4MEInfrastructureSettingsV5.TeamUsers + "/" + parameters.UserId,
                HttpMethodType.Delete);
        }

        /// <summary>
        ///     Update a team member
        /// </summary>
        /// <param name="queryParameters">Member query parameters</param>
        /// <param name="requestPayload">Member request parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Updated team member</returns>
        public TeamResponse UpdateTeamMember(MemberQueryParameters queryParameters,
            TeamRequest requestPayload,
            out ResultResponse resultResponse)
        {
            if (queryParameters?.UserId == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The UserId parameter is not specified"}}
                    }
                };

                return null;
            }

            if (requestPayload == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The team request object is empty"}}
                    }
                };

                return null;
            }

            var response = GetJsonObjectFromAPI<TeamResponse>(
                requestPayload,
                R4MEInfrastructureSettingsV5.TeamUsers + "/" + queryParameters.UserId,
                HttpMethodType.Patch,
                out resultResponse);

            return response;
        }

        /// <summary>
        ///     Update a team member
        /// </summary>
        /// <param name="queryParameters">Member query parameters</param>
        /// <param name="requestPayload">Member request parameters</param>
        /// <returns>Updated team member</returns>
        public Task<Tuple<TeamResponse, ResultResponse>> UpdateTeamMemberAsync(MemberQueryParameters queryParameters, TeamRequest requestPayload)
        {
            if (queryParameters?.UserId == null)
            {
                return Task.FromResult(new Tuple<TeamResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The UserId parameter is not specified"}}
                    }
                }));
            }

            if (requestPayload == null)
            {
                return Task.FromResult(new Tuple<TeamResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The team request object is empty"}}
                    }
                }));
            }

            return GetJsonObjectFromAPIAsync<TeamResponse>(
                requestPayload,
                R4MEInfrastructureSettingsV5.TeamUsers + "/" + queryParameters.UserId,
                HttpMethodType.Patch);
        }

        /// <summary>
        ///     Add an array of skills to the driver.
        /// </summary>
        /// <param name="queryParameters">Query parameters</param>
        /// <param name="skills">An array of the driver skills</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Updated team member</returns>
        public TeamResponse AddSkillsToDriver(MemberQueryParameters queryParameters,
            string[] skills,
            out ResultResponse resultResponse)
        {
            if (queryParameters?.UserId == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The UserId parameter is not specified"}}
                    }
                };

                return null;
            }

            if (skills == null || skills.Length == 0)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The driver skills array is empty."}}
                    }
                };

                return null;
            }

            #region Prepare Request From Driver Skills

            var driverSkills = new Dictionary<string, string> { { "driver_skills", string.Join(",", skills) } };

            var requestPayload = new TeamRequest
            {
                CustomData = driverSkills
            };

            #endregion

            try
            {
                var response = GetJsonObjectFromAPI<TeamResponse>(
                requestPayload,
                R4MEInfrastructureSettingsV5.TeamUsers + "/" + queryParameters.UserId,
                HttpMethodType.Patch,
                out resultResponse);

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                resultResponse = new ResultResponse()
                {
                    Code = -1,
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] { ex.Message }}
                    }

                };

                return null;
            }

            
        }

        /// <summary>
        ///     Add an array of skills to the driver.
        /// </summary>
        /// <param name="queryParameters">Query parameters</param>
        /// <param name="skills">An array of the driver skills</param>
        /// <returns>Updated team member</returns>
        public Task<Tuple<TeamResponse, ResultResponse>> AddSkillsToDriverAsync(MemberQueryParameters queryParameters, string[] skills)
        {
            if (queryParameters?.UserId == null)
            {
                return Task.FromResult(new Tuple<TeamResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The UserId parameter is not specified"}}
                    }
                }));
            }

            if (skills == null || skills.Length == 0)
            {
                return Task.FromResult(new Tuple<TeamResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The driver skills array is empty."}}
                    }
                }));
            }

            #region Prepare Request From Driver Skills

            var driverSkills = new Dictionary<string, string> { { "driver_skills", string.Join(",", skills) } };

            var requestPayload = new TeamRequest
            {
                CustomData = driverSkills
            };

            #endregion

            return GetJsonObjectFromAPIAsync<TeamResponse>(
                requestPayload,
                R4MEInfrastructureSettingsV5.TeamUsers + "/" + queryParameters.UserId,
                HttpMethodType.Patch);
        }

        /// <summary>
        /// Returns an ID of the team owner.
        /// </summary>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Team owner ID</returns>
        public long? GetTeamOwner(out ResultResponse resultResponse)
        {
            var members = GetTeamMembers(out resultResponse);

            if ((members?.Length ?? 0)>0)
            {
                var ownerMember = members.ToList().Where(x => x.MemberType=="PRIMARY_ACCOUNT").FirstOrDefault();
                
                if (ownerMember != null) return ownerMember.MemberId;
            }

            return null;
        }

        public Task<Tuple<long?, ResultResponse>> GetTeamOwnerAsync()
        {
            var resultResponse = new ResultResponse();

            return Task.Run<Tuple<long?, ResultResponse>>(async () =>
            {
                var membersResult = await GetTeamMembersAsync();

                if ((membersResult?.Item1.Length ?? 0) > 0)
                {
                    var ownerMember = membersResult?.Item1.ToList().Where(x => x.MemberType == "PRIMARY_ACCOUNT").FirstOrDefault();

                    if (ownerMember != null) 
                        return new Tuple<long?, ResultResponse>(ownerMember.MemberId, resultResponse);
                }

                resultResponse = new ResultResponse()
                {
                    Status = false,
                    ExitCode = 404,
                    Messages = new Dictionary<string, string[]>
                {
                    {"Error", new[] {"Can not retrieve the team members."}}
                }
                };

                return new Tuple<long?, ResultResponse>(null, resultResponse);

            });

        }

        /// <summary>
        ///     Get user IDs by their email addresses using the API query parameter.
        ///     This method makes optimized API calls using the query parameter instead of fetching all members.
        /// </summary>
        /// <param name="emails">List of email addresses to search for</param>
        /// <param name="resultResponse">Response object containing error details if the operation fails</param>
        /// <returns>List of member IDs matching the provided emails, or null if no members found</returns>
        public List<long?> GetUserIdsByEmails(List<string> emails, out ResultResponse resultResponse)
        {
            resultResponse = new ResultResponse { Status = true };

            // Handle null or empty email list
            if (emails == null || emails.Count == 0)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The emails list is null or empty"}}
                    }
                };
                return null;
            }

            var foundMemberIds = new List<long?>();
            var allMembers = new List<TeamResponse>();

            foreach (var email in emails.Distinct())
            {
                var queryParams = new TeamUsersQueryParameters
                {
                    Query = email
                };

                var members = GetTeamMembers(queryParams, out var tempResponse);

                if (!tempResponse.Status)
                {
                    resultResponse = tempResponse;
                    continue;
                }

                if (members != null && members.Length > 0)
                {
                    var exactMatches = members.Where(m =>
                        string.Equals(m.MemberEmail, email, StringComparison.OrdinalIgnoreCase));
                    allMembers.AddRange(exactMatches);
                }
            }

            if (allMembers.Count > 0)
            {
                foundMemberIds = allMembers
                    .Select(x => x.MemberId)
                    .Distinct()
                    .ToList();
            }

            return foundMemberIds.Count > 0 ? foundMemberIds : null;
        }

        /// <summary>
        ///     Asynchronously get user IDs by their email addresses using the API query parameter.
        ///     This method makes optimized API calls using the query parameter instead of fetching all members.
        /// </summary>
        /// <param name="emails">List of email addresses to search for</param>
        /// <returns>Tuple containing list of member IDs and result response</returns>
        public async Task<Tuple<List<long?>, ResultResponse>> GetUserIdsByEmailsAsync(List<string> emails)
        {
            var resultResponse = new ResultResponse { Status = true };

            if (emails == null || emails.Count == 0)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The emails list is null or empty"}}
                    }
                };
                return new Tuple<List<long?>, ResultResponse>(null, resultResponse);
            }

            var foundMemberIds = new List<long?>();
            var allMembers = new List<TeamResponse>();

            foreach (var email in emails.Distinct())
            {
                var queryParams = new TeamUsersQueryParameters
                {
                    Query = email
                };

                var membersResult = await GetTeamMembersAsync(queryParams);

                if (!membersResult.Item2.Status)
                {
                    resultResponse = membersResult.Item2;
                    continue;
                }

                if (membersResult.Item1 != null && membersResult.Item1.Length > 0)
                {
                    var exactMatches = membersResult.Item1.Where(m =>
                        string.Equals(m.MemberEmail, email, StringComparison.OrdinalIgnoreCase));
                    allMembers.AddRange(exactMatches);
                }
            }

            if (allMembers.Count > 0)
            {
                foundMemberIds = allMembers
                    .Select(x => x.MemberId)
                    .Distinct()
                    .ToList();
            }

            return new Tuple<List<long?>, ResultResponse>(
                foundMemberIds.Count > 0 ? foundMemberIds : null,
                resultResponse
            );
        }
    }
}
