using NUnit.Framework;
using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDKLibrary.DataTypes.V5.Customers;
using Route4MeSDKLibrary.Managers;
using Route4MeSDKLibrary.QueryTypes.V5.Customers;
using Route4MeSdkV5UnitTest.V5;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Route4MeSDKUnitTest.V5
{
    [TestFixture]
    public class CustomersTests
    {
        private CustomerManagerV5 _customerManager;
        private string _createdCustomerId;

        [SetUp]
        public void Setup()
        {
            _customerManager = new CustomerManagerV5(ApiKeys.ActualApiKey);
            _createdCustomerId = null;
        }

        [TearDown]
        public async Task Cleanup()
        {
            if (!string.IsNullOrEmpty(_createdCustomerId))
            {
                try
                {
                    await _customerManager.DeleteCustomerAsync(new CustomerIdParameters
                    {
                        CustomerId = _createdCustomerId
                    });
                }
                catch (Exception ex)
                {
                    Assert.Fail($"Cleanup failed: {ex.Message}");
                }
            }
        }

        [Test]
        public void CreateCustomerTest()
        {
            var createRequest = new StoreRequest
            {
                Name = "Test Customer " + Guid.NewGuid().ToString("N").Substring(0, 6),
                ExternalId = "EXT-" + Guid.NewGuid().ToString("N").Substring(0, 8),
                Status = 1,
                Currency = "USD",
                AccountablePersonId = GetAccountablePersonId()
            };

            var result = _customerManager.CreateCustomer(createRequest, out var response);
            _createdCustomerId = result.CustomerId;

            Assert.NotNull(result, GetErrorMessage(response));
            Assert.That(string.IsNullOrEmpty(result.CustomerId), Is.False);
        }

        [Test]
        public async Task CreateCustomerAsyncTest()
        {
            var createRequest = new StoreRequest
            {
                Name = "Async Customer " + Guid.NewGuid().ToString("N").Substring(0, 6),
                ExternalId = "EXT-" + Guid.NewGuid().ToString("N").Substring(0, 8),
                Status = 1,
                Currency = "USD",
                AccountablePersonId = GetAccountablePersonId()
            };

            var result = await _customerManager.CreateCustomerAsync(createRequest);
            _createdCustomerId = result.Item1.CustomerId;

            Assert.NotNull(result.Item1, GetErrorMessage(result.Item2));
            Assert.That(string.IsNullOrEmpty(result.Item1.CustomerId), Is.False);
        }

        [Test]
        public void GetCustomerByIdTest()
        {
            var created = _customerManager.CreateCustomer(new StoreRequest
            {
                Name = "GetTest Customer " + (new Random()).Next(),
                Status = 1,
                Currency = "USD",
                AccountablePersonId = GetAccountablePersonId()
            }, out var response);

            Assert.NotNull(created, GetErrorMessage(response));
            _createdCustomerId = created.CustomerId;

            var parameters = new CustomerIdParameters { CustomerId = created.CustomerId };
            var result = _customerManager.GetCustomerById(parameters, out var getResponse);

            Assert.NotNull(result, GetErrorMessage(getResponse));
            Assert.That(result.CustomerId, Is.EqualTo(created.CustomerId));
        }

        [Test]
        public async Task GetCustomerByIdAsyncTest()
        {
            var created = await _customerManager.CreateCustomerAsync(new StoreRequest
            {
                Name = "GetAsyncTest Customer " + new Random().Next(),
                Status = 1,
                Currency = "USD",
                AccountablePersonId = GetAccountablePersonId()
            });

            Assert.NotNull(created.Item1, GetErrorMessage(created.Item2));
            _createdCustomerId = created.Item1.CustomerId;

            var parameters = new CustomerIdParameters { CustomerId = created.Item1.CustomerId };
            var result = await _customerManager.GetCustomerByIdAsync(parameters);

            Assert.NotNull(result.Item1, GetErrorMessage(result.Item2));
            Assert.That(result.Item1.CustomerId, Is.EqualTo(created.Item1.CustomerId));
        }

        [Test]
        public void UpdateCustomerTest()
        {
            var created = _customerManager.CreateCustomer(new StoreRequest
            {
                Name = "UpdateTest Customer " + new Random().Next(),
                Status = 1,
                Currency = "USD",
                AccountablePersonId = GetAccountablePersonId()
            }, out var response);

            Assert.NotNull(created, GetErrorMessage(response));
            _createdCustomerId = created.CustomerId;

            var updateParams = new UpdateCustomerParameters
            {
                CustomerId = created.CustomerId,
                Name = "Updated Name " + DateTime.Now.ToString("HHmmss"),
                Status = 1,
                AccountablePersonId = GetAccountablePersonId()
            };

            var updated = _customerManager.UpdateCustomer(updateParams, out var updateResponse);

            Assert.NotNull(updated, GetErrorMessage(updateResponse));
            Assert.That(updated.Name, Is.EqualTo(updateParams.Name));
        }

        [Test]
        public async Task UpdateCustomerAsyncTest()
        {
            var created = await _customerManager.CreateCustomerAsync(new StoreRequest
            {
                Name = "UpdateAsync Customer " + (new Random()).Next(),
                Status = 1,
                Currency = "USD",
                AccountablePersonId = GetAccountablePersonId()
            });

            Assert.NotNull(created.Item1, GetErrorMessage(created.Item2));
            _createdCustomerId = created.Item1.CustomerId;

            var updateParams = new UpdateCustomerParameters
            {
                CustomerId = created.Item1.CustomerId,
                Name = "Updated Async " + DateTime.Now.ToString("HHmmss"),
                Status = 1,
                AccountablePersonId = GetAccountablePersonId()
            };

            var updated = await _customerManager.UpdateCustomerAsync(updateParams);

            Assert.NotNull(updated.Item1, GetErrorMessage(updated.Item2));
            Assert.That(updated.Item1.Name, Is.EqualTo(updateParams.Name));
        }

        [Test]
        public void GetCustomersListTest()
        {
            var listParams = new CustomerListParameters()
            {
                Page = 1,
                PerPage = 10
            };

            var result = _customerManager.GetCustomersList(listParams, out var response);

            Assert.NotNull(result, GetErrorMessage(response));
            Assert.That(result.Items, Is.Not.Null);
        }

        [Test]
        public async Task GetCustomersListAsyncTest()
        {
            var listParams = new CustomerListParameters()
            {
                Page = 1,
                PerPage = 10
            };

            var result = await _customerManager.GetCustomersListAsync(listParams);

            Assert.NotNull(result.Item1, GetErrorMessage(result.Item2));
            Assert.That(result.Item1.Items, Is.Not.Null);
        }

        [Test]
        public void DeleteCustomerTest()
        {
            var created = _customerManager.CreateCustomer(new StoreRequest
            {
                Name = "DeleteTest Customer " + new Random().Next(),
                Status = 1,
                Currency = "USD",
                AccountablePersonId = GetAccountablePersonId()
            }, out var response);

            Assert.NotNull(created, GetErrorMessage(response));
            _createdCustomerId = created.CustomerId;

            var parameters = new CustomerIdParameters { CustomerId = created.CustomerId };
            var deleted = _customerManager.DeleteCustomer(parameters, out var deleteResponse);

            Assert.NotNull(deleted, GetErrorMessage(deleteResponse));
            Assert.That(deleted.Status, Is.True);

            _createdCustomerId = null;
        }

        [Test]
        public async Task DeleteCustomerAsyncTest()
        {
            var created = await _customerManager.CreateCustomerAsync(new StoreRequest
            {
                Name = "DeleteAsync Customer " + (new Random()).Next(),
                Status = 1,
                Currency = "USD",
                AccountablePersonId = GetAccountablePersonId()
            });

            Assert.NotNull(created.Item1, GetErrorMessage(created.Item2));
            var deleteResult = await _customerManager.DeleteCustomerAsync(new CustomerIdParameters
            {
                CustomerId = created.Item1.CustomerId
            });

            Assert.NotNull(deleteResult.Item1, GetErrorMessage(deleteResult.Item2));
            Assert.That(deleteResult.Item1.Status, Is.True);

            _createdCustomerId = null;
        }

        private long GetAccountablePersonId()
        {
            var teamManager = new Route4MeManagerV5(ApiKeys.ActualApiKey);
            var members = teamManager.GetTeamMembers(out _);
            var accountablePersonId = members?.FirstOrDefault()?.MemberId ?? 0;

            if (accountablePersonId == 0)
            {
                Assert.Fail("Cannot retrieve a valid team member for AccountablePersonId.");
            }

            return accountablePersonId;
        }
         
        private string GetErrorMessage(ResultResponse response)
        {
            if (response == null)
            {
                return "Unknown error (null response)";
            }

            if (response.Messages != null && response.Messages.Any())
            {
                return string.Join("; ",
                    response.Messages.Select(kv => $"{kv.Key}: {string.Join(", ", kv.Value)}"));
            }

            return $"Code: {response.Code}, ExitCode: {response.ExitCode}";
        }
    }
}
