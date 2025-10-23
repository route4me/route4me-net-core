using System.Linq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Route4MeSDKLibrary.DataTypes.V5.OptimizationProfiles;
using Route4MeSDKLibrary.Managers;
using System.Threading.Tasks;
using System;

namespace Route4MeSdkV5UnitTest.V5.OptimizationProfiles
{
    [TestFixture]
    public class OptimizationProfileManagerV5Tests
    {
        private OptimizationProfileManagerV5 _manager;
        private string _testProfileId;

        [SetUp]
        public void Setup()
        {
            _manager = new OptimizationProfileManagerV5(ApiKeys.ActualApiKey);
        }

        #region Get Profiles Tests

        [Test]
        public void GetOptimizationProfiles_ReturnsValidResponse()
        {
            // Arrange & Act
            var result = _manager.GetOptimizationProfiles(out var error);
            
            // Assert
            Assert.IsNull(error, "Should not return error");
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsNotNull(result.Items, "Items should not be null");
            Assert.Greater(result.Items.Length, 0, "Should return at least one profile");
            
            // Verify first profile has required properties
            var firstProfile = result.Items[0];
            Assert.IsNotEmpty(firstProfile.OptimizationProfileId, "Profile should have ID");
            Assert.IsNotEmpty(firstProfile.ProfileName, "Profile should have name");
        }
        
        [Test]
        public async Task GetOptimizationProfilesAsync_ReturnsValidResponse()
        {
            // Arrange & Act
            var result = await _manager.GetOptimizationProfilesAsync();
            
            // Assert
            Assert.IsNull(result.Item2, "Should not return error");
            Assert.IsNotNull(result.Item1, "Result should not be null");
            Assert.IsNotNull(result.Item1.Items, "Items should not be null");
            Assert.Greater(result.Item1.Items.Length, 0, "Should return at least one profile");
        }
        
        [Test]
        public void GetOptimizationProfiles_WithInvalidApiKey_ReturnsError()
        {
            // Arrange
            var invalidManager = new OptimizationProfileManagerV5("invalid_key");
            
            // Act
            var result = invalidManager.GetOptimizationProfiles(out var error);
            
            // Assert
            Assert.IsNotNull(error, "Should return error with invalid API key");
            Assert.IsNull(result, "Should not return result on error");
        }
        
        #endregion

        #region Get Profile By ID Tests

        [Test]
        public void GetOptimizationProfileById_WithValidId_ReturnsProfile()
        {
            // Arrange - First get a profile ID
            var profiles = _manager.GetOptimizationProfiles(out _);
            var profileId = profiles.Items[0].OptimizationProfileId;
            
            // Act
            var result = _manager.GetOptimizationProfileById(profileId, out var error);
            
            // Assert
            Assert.IsNull(error, "Should not return error");
            Assert.IsNotNull(result, "Result should not be null");
            Assert.AreEqual(profileId, result.OptimizationProfileId, "Should return correct profile");
        }
        
        [Test]
        public async Task GetOptimizationProfileByIdAsync_WithValidId_ReturnsProfile()
        {
            // Arrange - First get a profile ID
            var profiles = await _manager.GetOptimizationProfilesAsync();
            var profileId = profiles.Item1.Items[0].OptimizationProfileId;
            
            // Act
            var result = await _manager.GetOptimizationProfileByIdAsync(profileId);
            
            // Assert
            Assert.IsNull(result.Item2, "Should not return error");
            Assert.IsNotNull(result.Item1, "Result should not be null");
            Assert.AreEqual(profileId, result.Item1.OptimizationProfileId, "Should return correct profile");
        }
        
        [Test]
        public void GetOptimizationProfileById_WithInvalidId_ReturnsError()
        {
            // Arrange
            var invalidId = "invalid-profile-id";
            
            // Act
            var result = _manager.GetOptimizationProfileById(invalidId, out var error);
            
            // Assert
            Assert.IsNotNull(error, "Should return error with invalid ID");
            Assert.IsNull(result, "Should not return result on error");
        }
        
        #endregion

        #region Create Profile Tests

        [Test]
        public void CreateOptimizationProfile_WithValidData_CreatesProfile()
        {
            // Arrange
            var request = new OptimizationProfileCreateRequest
            {
                ProfileName = $"Test Profile {Guid.NewGuid()}",
                IsDefault = false,
                OptimizationType = "multi",
                RouteName = "Test Route"
            };
            
            // Act
            var result = _manager.CreateOptimizationProfile(request, out var error);
            
            // Assert
            Assert.IsNull(error, "Should not return error");
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsNotEmpty(result.OptimizationProfileId, "Created profile should have ID");
            Assert.AreEqual(request.ProfileName, result.ProfileName, "Should have correct name");
            
            // Store for cleanup
            _testProfileId = result.OptimizationProfileId;
        }
        
        [Test]
        public async Task CreateOptimizationProfileAsync_WithValidData_CreatesProfile()
        {
            // Arrange
            var request = new OptimizationProfileCreateRequest
            {
                ProfileName = $"Test Profile Async {Guid.NewGuid()}",
                IsDefault = false,
                OptimizationType = "multi"
            };
            
            // Act
            var result = await _manager.CreateOptimizationProfileAsync(request);
            
            // Assert
            Assert.IsNull(result.Item2, "Should not return error");
            Assert.IsNotNull(result.Item1, "Result should not be null");
            Assert.IsNotEmpty(result.Item1.OptimizationProfileId, "Created profile should have ID");
        }
        
        [Test]
        public void CreateOptimizationProfile_WithNullRequest_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                _manager.CreateOptimizationProfile(null, out _));
        }
        
        [Test]
        public void CreateOptimizationProfile_WithEmptyName_ReturnsError()
        {
            // Arrange
            var request = new OptimizationProfileCreateRequest
            {
                ProfileName = "", // Empty name should fail
                IsDefault = false
            };
            
            // Act
            var result = _manager.CreateOptimizationProfile(request, out var error);
            
            // Assert
            Assert.IsNotNull(error, "Should return error with empty name");
        }
        
        #endregion

        #region Update Profile Tests

        [Test]
        public void UpdateOptimizationProfile_WithValidData_UpdatesProfile()
        {
            // Arrange - First create a profile
            var createRequest = new OptimizationProfileCreateRequest
            {
                ProfileName = $"Update Test Profile {Guid.NewGuid()}",
                IsDefault = false,
                OptimizationType = "multi"
            };
            var created = _manager.CreateOptimizationProfile(createRequest, out _);
            var profileId = created.OptimizationProfileId;
            
            // Arrange - Update request
            var updateRequest = new OptimizationProfileCreateRequest
            {
                ProfileName = $"Updated Profile {Guid.NewGuid()}",
                IsDefault = false,
                OptimizationType = "multi",
                RouteName = "Updated Route"
            };
            
            // Act
            var result = _manager.UpdateOptimizationProfile(profileId, updateRequest, out var error);
            
            // Assert
            Assert.IsNull(error, "Should not return error");
            Assert.IsNotNull(result, "Result should not be null");
            Assert.AreEqual(updateRequest.ProfileName, result.ProfileName, "Should have updated name");
            Assert.AreEqual(updateRequest.OptimizationType, result.OptimizationType, "Should have updated type");
            
            // Store for cleanup
            _testProfileId = profileId;
        }
        
        [Test]
        public async Task UpdateOptimizationProfileAsync_WithValidData_UpdatesProfile()
        {
            // Arrange - First create a profile
            var createRequest = new OptimizationProfileCreateRequest
            {
                ProfileName = $"Update Async Test Profile {Guid.NewGuid()}",
                IsDefault = false,
                OptimizationType = "multi"
            };
            var created = await _manager.CreateOptimizationProfileAsync(createRequest);
            var profileId = created.Item1.OptimizationProfileId;
            
            // Arrange - Update request
            var updateRequest = new OptimizationProfileCreateRequest
            {
                ProfileName = $"Updated Async Profile {Guid.NewGuid()}",
                IsDefault = false,
                OptimizationType = "multi"
            };
            
            // Act
            var result = await _manager.UpdateOptimizationProfileAsync(profileId, updateRequest);
            
            // Assert
            Assert.IsNull(result.Item2, "Should not return error");
            Assert.IsNotNull(result.Item1, "Result should not be null");
            Assert.AreEqual(updateRequest.ProfileName, result.Item1.ProfileName, "Should have updated name");
        }
        
        [Test]
        public void UpdateOptimizationProfile_WithInvalidId_ReturnsError()
        {
            // Arrange
            var invalidId = "invalid-profile-id";
            var request = new OptimizationProfileCreateRequest
            {
                ProfileName = "Test Update",
                IsDefault = false
            };
            
            // Act
            var result = _manager.UpdateOptimizationProfile(invalidId, request, out var error);
            
            // Assert
            Assert.IsNotNull(error, "Should return error with invalid ID");
            Assert.IsNull(result, "Should not return result on error");
        }
        
        #endregion

        #region Delete Profile Tests

        [Test]
        public void DeleteOptimizationProfile_WithValidId_DeletesProfile()
        {
            // Arrange - First create a profile
            var createRequest = new OptimizationProfileCreateRequest
            {
                ProfileName = $"Delete Test Profile {Guid.NewGuid()}",
                IsDefault = false,
                OptimizationType = "multi"
            };
            var created = _manager.CreateOptimizationProfile(createRequest, out _);
            var profileId = created.OptimizationProfileId;
            
            // Act
            var result = _manager.DeleteOptimizationProfile(profileId, out var error);
            
            // Assert
            Assert.IsNull(error, "Should not return error");
            Assert.IsTrue(result, "Should return true for successful deletion");
        }
        
        [Test]
        public async Task DeleteOptimizationProfileAsync_WithValidId_DeletesProfile()
        {
            // Arrange - First create a profile
            var createRequest = new OptimizationProfileCreateRequest
            {
                ProfileName = $"Delete Async Test Profile {Guid.NewGuid()}",
                IsDefault = false,
                OptimizationType = "multi"
            };
            var created = await _manager.CreateOptimizationProfileAsync(createRequest);
            var profileId = created.Item1.OptimizationProfileId;
            
            // Act
            var result = await _manager.DeleteOptimizationProfileAsync(profileId);
            
            // Assert
            Assert.IsNull(result.Item2, "Should not return error");
            Assert.IsTrue(result.Item1, "Should return true for successful deletion");
        }
        
        [Test]
        public void DeleteOptimizationProfile_WithInvalidId_ReturnsError()
        {
            // Arrange
            var invalidId = "invalid-profile-id";
            
            // Act
            var result = _manager.DeleteOptimizationProfile(invalidId, out var error);
            
            // Assert
            Assert.IsNotNull(error, "Should return error with invalid ID");
            Assert.IsFalse(result, "Should return false for failed deletion");
        }
        
        #endregion

        #region Save Entities Tests (Legacy)

        [Test]
        public void SaveEntities_WithValidData_CreatesProfile()
        {
            // Arrange
            var testData = new JObject { ["append_date_to_route_name"] = true };
            var request = new OptimizationProfileSaveEntities
            {
                Items = new[]
                {
                    new OptimizationProfileSaveEntitiesItem
                    {
                        Guid = Guid.NewGuid().ToString(),
                        Parts = new[]
                        {
                            new OptimizationProfileSaveEntitiesItemPart
                            {
                                Guid = "pav",
                                Data = testData
                            }
                        }
                    }
                }
            };
            
            // Act
            var result = _manager.SaveEntities(request, out var error);
            
            // Assert
            Assert.IsNull(error, "Should not return error");
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsNotNull(result.Items, "Items should not be null");
            Assert.AreEqual(1, result.Items.Length, "Should return one item");
            Assert.IsNotEmpty(result.Items[0].Id, "Created item should have ID");
            
            // Store for cleanup
            _testProfileId = result.Items[0].Id;
        }
        
        [Test]
        public async Task SaveEntitiesAsync_WithValidData_CreatesProfile()
        {
            // Arrange
            var testData = new JObject { ["append_date_to_route_name"] = true };
            var request = new OptimizationProfileSaveEntities
            {
                Items = new[]
                {
                    new OptimizationProfileSaveEntitiesItem
                    {
                        Guid = Guid.NewGuid().ToString(),
                        Parts = new[]
                        {
                            new OptimizationProfileSaveEntitiesItemPart
                            {
                                Guid = "pav",
                                Data = testData
                            }
                        }
                    }
                }
            };
            
            // Act
            var result = await _manager.SaveEntitiesAsync(request);
            
            // Assert
            Assert.IsNull(result.Item2, "Should not return error");
            Assert.IsNotNull(result.Item1, "Result should not be null");
            Assert.IsNotNull(result.Item1.Items, "Items should not be null");
        }
        
        [Test]
        public void SaveEntities_WithNullRequest_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                _manager.SaveEntities(null, out _));
        }
        
        [Test]
        public void SaveEntities_WithEmptyItems_ReturnsError()
        {
            // Arrange
            var request = new OptimizationProfileSaveEntities { Items = new OptimizationProfileSaveEntitiesItem[0] };
            
            // Act
            var result = _manager.SaveEntities(request, out var error);
            
            // Assert
            Assert.IsNotNull(error, "Should return error with empty items");
        }
        
        #endregion

        #region Delete Entities Tests (Legacy)

        [Test]
        public void DeleteEntities_WithValidId_DeletesProfile()
        {
            // Arrange - First create a profile
            var createData = new JObject { ["test"] = true };
            var createRequest = new OptimizationProfileSaveEntities
            {
                Items = new[]
                {
                    new OptimizationProfileSaveEntitiesItem
                    {
                        Guid = Guid.NewGuid().ToString(),
                        Parts = new[]
                        {
                            new OptimizationProfileSaveEntitiesItemPart
                            {
                                Guid = "pav",
                                Data = createData
                            }
                        }
                    }
                }
            };
            var created = _manager.SaveEntities(createRequest, out _);
            var createdId = created.Items[0].Id;
            
            // Act - Delete it
            var deleteRequest = new OptimizationProfileDeleteEntitiesRequest
            {
                Items = new[] { new OptimizationProfileDeleteEntitiesRequestItem { Id = createdId } }
            };
            var result = _manager.DeleteEntities(deleteRequest, out var error);
            
            // Assert
            Assert.IsNull(error, "Should not return error");
            Assert.IsNotNull(result, "Result should not be null");
        }
        
        [Test]
        public async Task DeleteEntitiesAsync_WithValidId_DeletesProfile()
        {
            // Arrange - First create a profile
            var createData = new JObject { ["test"] = true };
            var createRequest = new OptimizationProfileSaveEntities
            {
                Items = new[]
                {
                    new OptimizationProfileSaveEntitiesItem
                    {
                        Guid = Guid.NewGuid().ToString(),
                        Parts = new[]
                        {
                            new OptimizationProfileSaveEntitiesItemPart
                            {
                                Guid = "pav",
                                Data = createData
                            }
                        }
                    }
                }
            };
            var created = await _manager.SaveEntitiesAsync(createRequest);
            var createdId = created.Item1.Items[0].Id;
            
            // Act - Delete it
            var deleteRequest = new OptimizationProfileDeleteEntitiesRequest
            {
                Items = new[] { new OptimizationProfileDeleteEntitiesRequestItem { Id = createdId } }
            };
            var result = await _manager.DeleteEntitiesAsync(deleteRequest);
            
            // Assert
            Assert.IsNull(result.Item2, "Should not return error");
            Assert.IsNotNull(result.Item1, "Result should not be null");
        }
        
        [Test]
        public void DeleteEntities_WithInvalidId_ReturnsError()
        {
            // Arrange
            var request = new OptimizationProfileDeleteEntitiesRequest
            {
                Items = new[] { new OptimizationProfileDeleteEntitiesRequestItem { Id = "invalid-id" } }
            };
            
            // Act
            var result = _manager.DeleteEntities(request, out var error);
            
            // Assert
            Assert.IsNotNull(error, "Should return error with invalid ID");
        }
        
        #endregion

        [TearDown]
        public void Cleanup()
        {
            // Clean up any test data created
            if (!string.IsNullOrEmpty(_testProfileId))
            {
                try
                {
                    _manager.DeleteOptimizationProfile(_testProfileId, out _);
                }
                catch
                {
                    // Ignore cleanup errors
                }
            }
        }
    }
}