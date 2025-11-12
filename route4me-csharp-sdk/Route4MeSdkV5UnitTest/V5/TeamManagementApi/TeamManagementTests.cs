using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using NUnit.Framework;

using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

using Route4MeSDKLibrary.DataTypes.V5;
using Route4MeSDKLibrary.DataTypes.V5.Internal.Requests;
using Route4MeSDKLibrary.Managers;

namespace Route4MeSdkV5UnitTest.V5.TeamManagementApi
{
    [TestFixture]
    public class TeamManagementTests
    {
        private static readonly string CApiKey = ApiKeys.ActualApiKey;
        private TeamManagementManagerV5 tm;

        private long? ownerId;
        private List<long> memberIds;
        private string testEmail, testPassword;

        [OneTimeSetUp]
        public void Setup()
        {
            tm = new TeamManagementManagerV5(CApiKey);

            memberIds = new List<long>();

            ownerId = tm.GetTeamOwner(out ResultResponse resltResponse);

            testEmail = R4MeUtils.ReadSetting("test_acc_mail");
            testPassword = R4MeUtils.ReadSetting("test_acc_psw");

            #region Create Test Member

            var memberParams = new TeamRequest()
            {
                OwnerMemberId = ownerId,
                MemberType = MemberTypes.Driver.Description(),
                MemberEmail = R4MeUtils.GenerateTestEmail(testEmail),
                NewPassword = testPassword,
                MemberFirstName = "John12",
                MemberLastName = "Doe12"
            };

            var createdMember = tm.CreateTeamMember(memberParams, out ResultResponse resltResponse2);
            Assert.That(createdMember.GetType(), Is.EqualTo(typeof(TeamResponse)));

            if ((createdMember?.MemberId ?? -1) > 0) memberIds.Add((long)createdMember.MemberId);

            #endregion

        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            if (memberIds.Count > 0)
            {
                foreach (long memberId in memberIds)
                {
                    var memmerParams = new MemberQueryParameters()
                    {
                        UserId = memberId.ToString()
                    };

                    var result = await tm.RemoveTeamMemberAsync(memmerParams);

                    Console.WriteLine($"Removed the member: {result.Item1.MemberId}");
                }

            }
        }

        [Test, Order(1)]
        public void GetTeamMembersTest()
        {
            var users = tm.GetTeamMembers(out ResultResponse resltResponse);

            Assert.That(users.GetType(), Is.EqualTo(typeof(TeamResponse[])));

        }

        [Test, Order(2)]
        public async Task GetTeamMembersAsyncTest()
        {
            var result = await tm.GetTeamMembersAsync();

            Assert.That(result.GetType(), Is.EqualTo(typeof(Tuple<TeamResponse[], ResultResponse>)));
        }

        [Test, Order(3)]
        public void GetTeamMemberByIdTest()
        {
            var parameters = new MemberQueryParameters()
            {
                UserId = memberIds[0].ToString()
            };

            var user = tm.GetTeamMemberById(parameters, out ResultResponse resltResponse);

            Assert.That(user.GetType(), Is.EqualTo(typeof(TeamResponse)));

        }

        [Test, Order(4)]
        public async Task GetTeamMemberByIdAsyncTest()
        {
            var parameters = new MemberQueryParameters()
            {
                UserId = memberIds[0].ToString()
            };

            var result = await tm.GetTeamMemberByIdAsync(parameters);

            Assert.That(result.GetType(), Is.EqualTo(typeof(Tuple<TeamResponse, ResultResponse>)));
        }

        [Test, Order(5)]
        public void CreateTeamMemberTest()
        {
            var memberParams = new TeamRequest()
            {
                OwnerMemberId = ownerId,
                MemberType = MemberTypes.Driver.Description(),
                MemberEmail = R4MeUtils.GenerateTestEmail(testEmail),
                NewPassword = testPassword,
                MemberFirstName = "John",
                MemberLastName = "Doe"
            };

            var createdMember = tm.CreateTeamMember(memberParams, out ResultResponse resltResponse);
            Assert.That(createdMember.GetType(), Is.EqualTo(typeof(TeamResponse)));

            long? newMemberId = createdMember?.MemberId ?? null;

            if (newMemberId != null) memberIds.Add((long)newMemberId);

        }

        [Test, Order(6)]
        public async Task CreateTeamMemberAsyncTest()
        {
            var memberParams = new TeamRequest()
            {
                OwnerMemberId = ownerId,
                MemberType = MemberTypes.Dispatcher.Description(),
                MemberEmail = R4MeUtils.GenerateTestEmail(testEmail),
                NewPassword = testPassword,
                MemberFirstName = "John2",
                MemberLastName = "Doe2"
            };

            var result = await tm.CreateTeamMemberAsync(memberParams);
            Assert.That(result.GetType(), Is.EqualTo(typeof(Tuple<TeamResponse, ResultResponse>)));

            long? newMemberId = result?.Item1?.MemberId ?? null;

            if (newMemberId != null) memberIds.Add((long)newMemberId);
        }

        [Test, Order(7)]
        public void BulkCreateTeamMembersTest()
        {
            var emails = new List<string>()
            {
                R4MeUtils.GenerateTestEmail(testEmail),
                R4MeUtils.GenerateTestEmail(testEmail)
            };

            var memberParams = new List<TeamRequest>()
            {
                new TeamRequest()
                {
                    OwnerMemberId = ownerId,
                    MemberType = MemberTypes.Dispatcher.Description(),
                    MemberEmail = emails[0],
                    NewPassword = testPassword,
                    MemberFirstName = "John3",
                    MemberLastName = "Doe3"
                },
                new TeamRequest()
                {
                    OwnerMemberId = ownerId,
                    MemberType = MemberTypes.Dispatcher.Description(),
                    MemberEmail = emails[1],
                    NewPassword = testPassword,
                    MemberFirstName = "John4",
                    MemberLastName = "Doe4"
                }
            };

            Conflicts conflicts = Conflicts.Skip;

            var result = tm.BulkCreateTeamMembers(memberParams.ToArray(),
                conflicts,
                out ResultResponse resultResponse);

            Assert.IsNotNull(result);
            Assert.AreEqual(202, result.Code);

            Thread.Sleep(4000);

            List<long?> createdMemberIds = tm.GetUserIdsByEmails(emails, out ResultResponse resultResponse1);

            if ((createdMemberIds?.Count ?? 0) > 0)
                memberIds.AddRange(createdMemberIds.Select(x => (long)x));

        }

        [Test, Order(8)]
        public async Task BulkCreateTeamMembersAsyncTest()
        {
            var emails = new List<string>()
            {
                R4MeUtils.GenerateTestEmail(testEmail),
                R4MeUtils.GenerateTestEmail(testEmail)
            };

            var memberParams = new List<TeamRequest>()
            {
                new TeamRequest()
                {
                    OwnerMemberId = ownerId,
                    MemberType = MemberTypes.Dispatcher.Description(),
                    MemberEmail = emails[0],
                    NewPassword = testPassword,
                    MemberFirstName = "John5",
                    MemberLastName = "Doe5"
                },
                new TeamRequest()
                {
                    OwnerMemberId = ownerId,
                    MemberType = MemberTypes.Dispatcher.Description(),
                    MemberEmail = emails[1],
                    NewPassword = testPassword,
                    MemberFirstName = "John6",
                    MemberLastName = "Doe6"
                }
            };

            Conflicts conflicts = Conflicts.Skip;

            var result = await tm.BulkCreateTeamMembersAsync(memberParams.ToArray(),
                conflicts);
            Assert.That(result.GetType(), Is.EqualTo(typeof(Tuple<ResultResponse, ResultResponse>)));

            Thread.Sleep(4000);

            List<long?> createdMemberIds = tm.GetUserIdsByEmails(emails, out ResultResponse resultResponse1);

            if ((createdMemberIds?.Count ?? 0) > 0)
                memberIds.AddRange(createdMemberIds.Select(x => (long)x));
        }

        [Test, Order(9)]
        public void RemoveTeamMemberTest()
        {
            var parameters = new MemberQueryParameters()
            {
                UserId = memberIds[memberIds.Count - 1].ToString()
            };

            var removedMember = tm.RemoveTeamMember(parameters, out ResultResponse resultResponse);

            Assert.That(removedMember.GetType(), Is.EqualTo(typeof(TeamResponse)));
            Assert.IsTrue(removedMember.MemberEmail.EndsWith(".deleted"));

            memberIds.RemoveAt(memberIds.Count - 1);
        }

        [Test, Order(10)]
        public async Task RemoveTeamMemberAsyncTest()
        {
            var parameters = new MemberQueryParameters()
            {
                UserId = memberIds[memberIds.Count - 1].ToString()
            };

            var result = await tm.RemoveTeamMemberAsync(parameters);
            Assert.That(result.GetType(), Is.EqualTo(typeof(Tuple<TeamResponse, ResultResponse>)));
            Assert.IsTrue(result.Item1.MemberEmail.EndsWith(".deleted"));

            memberIds.RemoveAt(memberIds.Count - 1);
        }

        [Test, Order(11)]
        public void UpdateTeamMemberTest()
        {
            MemberQueryParameters queryParams = new MemberQueryParameters()
            {
                UserId = memberIds[memberIds.Count - 1].ToString()
            };

            TeamRequest payload = new TeamRequest()
            {
                MemberType = MemberTypes.Analyst.Description(),
                MemberFirstName = "John Renamed",
                MemberLastName = "Doe Renamed"
            };

            var updatedMember = tm.UpdateTeamMember(queryParams,
                payload,
                out ResultResponse resultResponse);

            Assert.That(updatedMember.GetType(), Is.EqualTo(typeof(TeamResponse)));
            Assert.AreEqual("John Renamed", updatedMember.MemberFirstName);
            Assert.AreEqual("Doe Renamed", updatedMember.MemberLastName);
        }

        [Test, Order(12)]
        public async Task UpdateTeamMemberAsyncTest()
        {
            MemberQueryParameters queryParams = new MemberQueryParameters()
            {
                UserId = memberIds[0].ToString()
            };

            TeamRequest payload = new TeamRequest()
            {
                MemberType = MemberTypes.Analyst.Description(),
                MemberFirstName = "John Renamed",
                MemberLastName = "Doe Renamed"
            };

            var result = await tm.UpdateTeamMemberAsync(queryParams,
                payload);

            Assert.That(result.GetType(), Is.EqualTo(typeof(Tuple<TeamResponse, ResultResponse>)));
            Assert.AreEqual("John Renamed", result.Item1.MemberFirstName);
            Assert.AreEqual("Doe Renamed", result.Item1.MemberLastName);
        }

        [Test, Order(13)]
        public void AddSkillsToDriverTest()
        {
            var queryParams = new MemberQueryParameters()
            {
                UserId = memberIds[memberIds.Count - 1].ToString()
            };

            Console.WriteLine($"AddSkillsToDriverTest UserId: {queryParams.UserId}");

            var skills = new List<string>()
            {
                "Class A CDL",
                "Class B CDL",
                "Forklift"
            };

            var skilledDriver = tm.AddSkillsToDriver(queryParams,
                skills.ToArray(),
                out ResultResponse resultResponse);

            Assert.That(skilledDriver.GetType(), Is.EqualTo(typeof(TeamResponse)),
                    String.Join(", ", (resultResponse?.Messages?.Select(x => x.Key + ": " + x.Value).ToList() ?? new List<string>()))
                    );


            #region Get and Check Updated User

            var parameters = new MemberQueryParameters()
            {
                UserId = memberIds[memberIds.Count - 1].ToString()
            };

            var user = tm.GetTeamMemberById(parameters, out ResultResponse resltResponse);

            Assert.IsTrue(user.CustomData.ContainsKey("driver_skills"));
            Assert.AreEqual(String.Join(",", skills), user.CustomData["driver_skills"]);

            #endregion

        }

        [Test, Order(14)]
        public async Task AddSkillsToDriverAsyncTest()
        {
            var queryParams = new MemberQueryParameters()
            {
                UserId = memberIds[0].ToString()
            };

            Console.WriteLine($"AddSkillsToDriverTest UserId: {queryParams.UserId}");

            var skills = new List<string>()
            {
                "Skid Steer Loader",
                "Independent Contractor"
            };

            var result = await tm.AddSkillsToDriverAsync(queryParams, skills.ToArray());

            Assert.That(result.GetType(), Is.EqualTo(typeof(Tuple<TeamResponse, ResultResponse>)));

            #region Get and Check Updated User

            var parameters = new MemberQueryParameters()
            {
                UserId = memberIds[0].ToString()
            };

            var user = tm.GetTeamMemberById(parameters, out ResultResponse resltResponse);

            Assert.IsTrue(user.CustomData.ContainsKey("driver_skills"));
            Assert.AreEqual(String.Join(",", skills), user.CustomData["driver_skills"]);

            #endregion

        }
    }
}