using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

using Route4MeSDKLibrary.DataTypes;

using Route4MeSDKUnitTest.Types;

namespace Route4MeSDKUnitTest.Tests
{
    [TestFixture]
    [Ignore("User API 4 is deprecated")]
    public class UsersGroupTests
    {
        private static string _skip;

        private static readonly string
            CApiKey = ApiKeys
                .ActualApiKey; // Creating of a user better to do with the business and higher account types --- put in the parameter an appropriate API key

        private static readonly string CApiKey1 = ApiKeys.DemoApiKey;

        private List<string> _lsMembers;

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value
        private readonly int? createdMemberID;
#pragma warning restore CS0649 // Field is never assigned to, and will always have its default value

        [OneTimeSetUp]
        [Obsolete]
        public void UserGroupInitialize()
        {
            _skip = CApiKey == CApiKey1 ? "yes" : "no";

            _lsMembers = new List<string>();

            var dispetcher = new UsersGroupTests().CreateUser("SUB_ACCOUNT_DISPATCHER", out var errorString);
            Assert.That(dispetcher, Is.InstanceOf<MemberResponseV4>(), "Cannot create dispetcher. " + errorString);

            _lsMembers.Add(dispetcher.MemberId);

            var driver = new UsersGroupTests().CreateUser("SUB_ACCOUNT_DRIVER", out errorString);
            Assert.That(driver, Is.InstanceOf<MemberResponseV4>(), "Cannot create driver. " + errorString);

            _lsMembers.Add(driver.MemberId);
        }

        [Test]
        [Obsolete]
        public void CreateUserTest()
        {
            if (_skip == "yes") return;

            var dispetcher = CreateUser("SUB_ACCOUNT_DISPATCHER", out var errorString);

            //For successful testing of an user creating, you shuld provide valid email address, otherwise you'll get error "Email is used in system"
            var rightResponse = dispetcher != null
                ? "ok"
                : errorString == "Email is used in system" ||
                  errorString == "Registration: The e-mail address is missing or invalid."
                    ? "ok"
                    : "";

            Assert.IsTrue(rightResponse == "ok", "CreateUserTest failed. " + errorString);

            _lsMembers.Add(dispetcher.MemberId);
        }

        [Obsolete]
        public MemberResponseV4 CreateUser(string memberType, out string errorString)
        {
            var route4Me = new Route4MeManager(CApiKey);

            var userFirstName = "";
            var userLastName = "";
            var userPhone = "";

            switch (memberType)
            {
                case "SUB_ACCOUNT_DISPATCHER":
                    userFirstName = "Clay";
                    userLastName = "Abraham";
                    userPhone = "571-259-5939";
                    break;
                case "SUB_ACCOUNT_DRIVER":
                    userFirstName = "Driver";
                    userLastName = "Driverson";
                    userPhone = "577-222-5555";
                    break;
            }

            var @params = new MemberParametersV4
            {
                HIDE_ROUTED_ADDRESSES = "FALSE",
                member_phone = userPhone,
                member_zipcode = "22102",
                member_email = "regression.autotests+" + DateTime.Now.ToString("yyyyMMddHHmmss") + "@gmail.com",
                HIDE_VISITED_ADDRESSES = "FALSE",
                READONLY_USER = "FALSE",
                member_type = memberType,
                date_of_birth = "2010",
                member_first_name = userFirstName,
                member_password = "123456",
                HIDE_NONFUTURE_ROUTES = "FALSE",
                member_last_name = userLastName,
                SHOW_ALL_VEHICLES = "FALSE",
                SHOW_ALL_DRIVERS = "FALSE"
            };

            var result = route4Me.CreateUser(@params, out errorString);

            return result;
        }

        [Test]
        [Obsolete]
        public void AddEditCustomDataToUserTest()
        {
            if (_skip == "yes") return;

            var route4Me = new Route4MeManager(CApiKey);

            var memberId = Convert.ToInt32(_lsMembers[_lsMembers.Count - 1]);

            var customParams = new MemberParametersV4
            {
                member_id = memberId,
                custom_data = new Dictionary<string, string> { { "Custom Key 2", "Custom Value 2" } }
            };

            var result2 = route4Me.UserUpdate(customParams, out var errorString);

            Assert.IsTrue(result2 != null, "UpdateUserTest failed. " + errorString);

            var customData = result2.CustomData;

            Assert.IsTrue(
                customData.Keys.ElementAt(0) == "Custom Key 2",
                "Custom Key is not 'Custom Key 2'");

            Assert.IsTrue(
                customData["Custom Key 2"] == "Custom Value 2",
                "Custom Value is not 'Custom Value 2'");
        }

        [Test]
        [Obsolete]
        public void GetUserByIdTest()
        {
            if (_skip == "yes") return;

            var route4Me = new Route4MeManager(CApiKey);

            var memberID = Convert.ToInt32(_lsMembers[0]);
            var @params = new MemberParametersV4 { MemberId = memberID };

            // Run the query
            var result = route4Me.GetUserById(@params, out var errorString);

            Assert.IsNotNull(result, "GetUserByIdTest. " + errorString);
        }

        [Test]
        [Obsolete]
        public void GetUsersTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var parameters = new GenericParameters();

            // Run the query
            var dataObjects = route4Me.GetUsers(parameters, out var errorString);

            Assert.That(dataObjects, Is.InstanceOf<GetUsersResponse>(), "GetUsersTest failed. " + errorString);
        }

        [Test]
        [Obsolete]
        public void UpdateUserTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            Console.WriteLine("createdMemberID -> " + createdMemberID);

            var @params = new MemberParametersV4
            {
                member_id = createdMemberID != null
                    ? createdMemberID
                    : Convert.ToInt32(_lsMembers[_lsMembers.Count - 1]),
                member_phone = "571-259-5939"
            };

            // Run the query
            var result = route4Me.UserUpdate(@params, out var errorString);

            Assert.IsNotNull(result, "UpdateUserTest failed. " + errorString);
        }

        [Test]
        public void UserAuthenticationTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var @params = new MemberParameters
            {
                StrEmail = "aaaaaaaa@gmail.com",
                StrPassword = "11111111111",
                Format = "json"
            };

            // Run the query
            var result = route4Me.UserAuthentication(@params, out var errorString);

            // result is always non null object, but in case of 
            // successful autentication object properties have non nul values
            Assert.IsNotNull(result, "UserAuthenticationTest failed. " + errorString);
        }

        [Test]
        public void UserRegistrationTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var @params = new MemberParameters
            {
                StrEmail = "thewelco@gmail.com",
                StrPassword_1 = "11111111",
                StrPassword_2 = "11111111",
                StrFirstName = "Olman",
                StrLastName = "Progman",
                StrIndustry = "Transportation",
                Format = "json",
                ChkTerms = 1,
                DeviceType = "web",
                Plan = "free",
                MemberType = 5
            };

            // Run the query
            var result = route4Me.UserRegistration(@params, out var errorString);

            // result is always non null object, but in case of 
            // successful autentication object property Status=true
            Assert.IsNotNull(result, "UserRegistrationTest failed. " + errorString);
        }

        [Test]
        public void ValidateSessionTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var @params = new MemberParameters
            {
                SessionGuid = "ad9001f33ed6875b5f0e75bce52cbc34",
                MemberId = 1,
                Format = "json"
            };

            // Run the query
            var result = route4Me.ValidateSession(@params, out var errorString);

            // result is always non null object, but in case of 
            // successful autentication object properties have non nul values
            Assert.IsNotNull(result, "ValidateSessionTest failed. " + errorString);
        }

        [Test]
        [Obsolete]
        public void DeleteUserTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var @params = new MemberParametersV4
            {
                MemberId = Convert.ToInt32(_lsMembers[_lsMembers.Count - 1])
            };

            // Run the query
            var result = route4Me.UserDelete(@params, out var errorString);

            Assert.IsNotNull(result, "DeleteUserTest failed. " + errorString);

            _lsMembers.RemoveAt(_lsMembers.Count - 1);
        }

        [OneTimeTearDown]
        [Obsolete]
        public void UsersGroupCleanup()
        {
            var route4Me = new Route4MeManager(CApiKey);
            var parameters = new MemberParametersV4();

            bool result;

            foreach (var memberId in _lsMembers)
            {
                parameters.MemberId = Convert.ToInt32(memberId);
                result = route4Me.UserDelete(parameters, out var errorString);
            }
        }
    }
}