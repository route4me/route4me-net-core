# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

This is the Route4Me Route Optimization C# SDK for .NET Core, providing a wrapper around the Route4Me RESTful API. The SDK supports both v4 (legacy) and v5 (modern) API endpoints, with v5 being the actively developed version.

## Building and Testing

### Build the solution
```bash
dotnet build route4me-csharp-sdk/Route4MeSDK.sln
```

### Run all tests
```bash
# V5 Unit Tests (primary test suite)
dotnet test route4me-csharp-sdk/Route4MeSdkV5UnitTest/Route4MeSdkV5UnitTest.csproj

# V4 Unit Tests (legacy)
dotnet test route4me-csharp-sdk/Route4MeSDKUnitTest/Route4MeSDKUnitTest.csproj

# Examples/Integration Tests
dotnet run --project route4me-csharp-sdk/Route4MeSDKTest/Route4MeSDKTest.csproj
```

### Run specific tests
```bash
# Run tests from a specific class
dotnet test route4me-csharp-sdk/Route4MeSdkV5UnitTest/Route4MeSdkV5UnitTest.csproj --filter "FullyQualifiedName~NotesTests"

# Run a single test method
dotnet test route4me-csharp-sdk/Route4MeSdkV5UnitTest/Route4MeSdkV5UnitTest.csproj --filter "FullyQualifiedName~NotesTests.CreateNoteTest"
```

### Build NuGet package
```bash
dotnet pack route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeSDKLibrary.csproj -c Release
```

## Architecture

### API Version Organization

The SDK supports two API versions with distinct authentication and organization:

**V4 API** (Legacy - Maintenance Mode):
- Base URL: `https://api.route4me.com`
- Auth: API key in query string (`?api_key=KEY`)
- Manager: `Route4MeManager.cs` (monolithic, sealed class)
- Endpoints: Defined in `R4MEInfrastructureSettings` constants

**V5 API** (Modern - Active Development):
- Base URL: `https://wh.route4me.com/modules/api/v5.0`
- Auth: Bearer token in Authorization header (`Authorization: Bearer KEY`)
- Manager: `Route4MeManagerV5.cs` (composition of domain managers)
- Endpoints: Defined in `R4MEInfrastructureSettingsV5` constants

### Manager Pattern (V5)

V5 uses a **domain-driven manager composition pattern**. Each domain (Routes, Orders, Vehicles, Notes, etc.) has its own dedicated manager:

```
Route4MeManagerV5 (facade)
├── NotesManagerV5
├── RouteManagerV5
├── OrderManagerV5
├── VehicleManagerV5
├── OptimizationManagerV5
├── AddressBookContactsManagerV5
├── TeamManagementManagerV5
├── RouteStatusManagerV5
├── SchedulesManagerV5
├── TelematicsManagerV5
├── AddressBarcodeManagerV5
├── AccountProfileManagerV5
├── OptimizationProfileManagerV5
└── PodWorkflowManagerV5
```

All V5 managers:
- Inherit from `Route4MeManagerBase` (located at `route4me-csharp-sdk/Route4MeSDKLibrary/Managers/Route4MeManagerBase.cs`)
- Provide both synchronous and asynchronous methods
- Return results via `out ResultResponse` (sync) or `Task<Tuple<T, ResultResponse>>` (async)
- Handle validation before making API calls

### File Organization

```
Route4MeSDKLibrary/                          # Main SDK library (netstandard2.0)
├── Route4MeManager.cs                       # V4 API facade
├── Route4MeManagerV5.cs                     # V5 API facade (composes domain managers)
├── Consts.cs                                # ALL API endpoints (V4 + V5) defined here
├── HttpClientHolderManager.cs               # Connection pooling (thread-safe)
├── Managers/                                # V5 domain managers
│   ├── Route4MeManagerBase.cs               # Base class for all V5 managers
│   └── [Domain]ManagerV5.cs                 # One per domain (Notes, Routes, Orders, etc.)
├── DataTypes/V5/[Domain]/                   # Response models per domain
│   └── [Entity]Resource.cs                  # Individual resources
│   └── [Entity]Collection.cs                # Paginated collections
└── QueryTypes/V5/[Domain]/                  # Request parameters per domain
    └── [Entity]Request.cs                   # Create/update requests
    └── [Entity]Parameters.cs                # Query parameters
```

### Key Architecture Patterns

**1. Dual Method Pattern**: Every operation has sync and async variants
```csharp
// Synchronous
public T Operation(Request req, out ResultResponse resultResponse)

// Asynchronous
public async Task<Tuple<T, ResultResponse>> OperationAsync(Request req)
```

**2. Error Handling**: Use `ResultResponse` instead of exceptions
```csharp
public class ResultResponse
{
    public bool Status { get; set; }  // true = success, false = error
    public Dictionary<string, string[]> Messages { get; set; }  // error details
}
```

**3. Input Validation**: Validate BEFORE making API calls (fail fast)
```csharp
if (request == null) return ErrorResponse("Request is required");
if (string.IsNullOrWhiteSpace(request.RouteId)) return ErrorResponse("RouteId is required");
```

**4. Parameter Serialization**: Use `GenericParameters` base class with reflection
```csharp
// Inherit and add [HttpQueryMember] attributes
public class MyParameters : GenericParameters
{
    [HttpQueryMember(Name = "page")]
    public int? Page { get; set; }
}

// Serialize to query string
var queryString = parameters.Serialize(apiKey); // ?api_key=KEY&page=1
```

**5. Connection Pooling**: HttpClient instances are pooled per base URL (thread-safe via locks)

**6. JSON Serialization**: Dual support for fastJSON (legacy) and Newtonsoft.Json (modern)

## Adding New API Endpoints

When adding support for a new Route4Me API endpoint, follow this pattern:

### 1. Add endpoint constant to `Consts.cs`

```csharp
// In R4MEInfrastructureSettingsV5 class
public const string MyNewEndpoint = MainHost + "/my-resource";
public const string MyNewEndpointById = MyNewEndpoint + "/{id}";
```

### 2. Create DataTypes (response models) in `DataTypes/V5/[Domain]/`

```csharp
[DataContract]
public class MyResource
{
    [DataMember(Name = "id")]
    public string Id { get; set; }

    [DataMember(Name = "name")]
    public string Name { get; set; }
}

[DataContract]
public class MyResourceCollection
{
    [DataMember(Name = "data")]
    public MyResource[] Data { get; set; }

    [DataMember(Name = "meta")]
    public PageMeta Meta { get; set; }  // Pagination info

    [DataMember(Name = "links")]
    public PageLinks Links { get; set; }  // Pagination links
}
```

### 3. Create QueryTypes (request models) in `QueryTypes/V5/[Domain]/`

```csharp
[DataContract]
public class MyCreateRequest
{
    [DataMember(Name = "name")]
    public string Name { get; set; }

    [DataMember(Name = "description")]
    public string Description { get; set; }
}

[DataContract]
public class MyQueryParameters : GenericParameters
{
    [HttpQueryMember(Name = "page")]
    public int? Page { get; set; }

    [HttpQueryMember(Name = "per_page")]
    public int? PerPage { get; set; }
}
```

### 4. Create or update Manager in `Managers/`

```csharp
public class MyDomainManagerV5 : Route4MeManagerBase
{
    public MyDomainManagerV5(string apiKey) : base(apiKey) { }

    // CRUD operations with validation
    public MyResource Create(MyCreateRequest request, out ResultResponse resultResponse)
    {
        // Validate inputs
        if (request == null)
        {
            resultResponse = new ResultResponse
            {
                Status = false,
                Messages = new Dictionary<string, string[]>
                {
                    {"Error", new[] {"Request is required"}}
                }
            };
            return null;
        }

        // Make API call
        var result = GetJsonObjectFromAPI<MyResource>(
            new GenericParameters(),
            R4MEInfrastructureSettingsV5.MyNewEndpoint,
            HttpMethodType.Post,
            request,
            out resultResponse
        );

        return result;
    }

    // Always provide async variant
    public async Task<Tuple<MyResource, ResultResponse>> CreateAsync(MyCreateRequest request)
    {
        // Same validation as sync version
        if (request == null)
        {
            var errorResponse = new ResultResponse
            {
                Status = false,
                Messages = new Dictionary<string, string[]>
                {
                    {"Error", new[] {"Request is required"}}
                }
            };
            return new Tuple<MyResource, ResultResponse>(null, errorResponse);
        }

        // Make async API call
        var result = await GetJsonObjectFromAPIAsync<MyResource>(
            new GenericParameters(),
            R4MEInfrastructureSettingsV5.MyNewEndpoint,
            HttpMethodType.Post,
            request
        );

        return result;
    }
}
```

### 5. Add manager to `Route4MeManagerV5.cs`

```csharp
public sealed class Route4MeManagerV5
{
    // Add property for new manager
    public MyDomainManagerV5 MyDomain { get; }

    public Route4MeManagerV5(string apiKey)
    {
        // Initialize in constructor
        MyDomain = new MyDomainManagerV5(apiKey);
    }
}
```

### 6. Write tests in `Route4MeSdkV5UnitTest/V5/[Domain]/`

```csharp
[TestFixture]
public class MyDomainTests
{
    private MyDomainManagerV5 _manager;
    private string _apiKey;

    [SetUp]
    public void Setup()
    {
        _apiKey = TestDataRepository.GetApiKey();
        _manager = new MyDomainManagerV5(_apiKey);
    }

    [Test]
    public void CreateTest()
    {
        var request = new MyCreateRequest
        {
            Name = "Test",
            Description = "Test description"
        };

        var result = _manager.Create(request, out var resultResponse);

        Assert.IsTrue(resultResponse.Status);
        Assert.IsNotNull(result);
        Assert.AreEqual("Test", result.Name);
    }

    [Test]
    public async Task CreateAsyncTest()
    {
        var request = new MyCreateRequest
        {
            Name = "Test Async",
            Description = "Test async description"
        };

        var (result, resultResponse) = await _manager.CreateAsync(request);

        Assert.IsTrue(resultResponse.Status);
        Assert.IsNotNull(result);
        Assert.AreEqual("Test Async", result.Name);
    }

    [Test]
    public void CreateValidationTest()
    {
        var result = _manager.Create(null, out var resultResponse);

        Assert.IsFalse(resultResponse.Status);
        Assert.IsNull(result);
        Assert.IsTrue(resultResponse.Messages.ContainsKey("Error"));
    }
}
```

### 7. Create example in `Route4MeSDKTest/Examples/API5/[Domain]/`

```csharp
public class MyDomainSample
{
    private readonly string _apiKey;

    public MyDomainSample()
    {
        _apiKey = ConfigurationManager.AppSettings["actualApiKey"];
    }

    public void RunExamples()
    {
        var manager = new MyDomainManagerV5(_apiKey);

        // Create example
        Console.WriteLine("Creating resource...");
        var createRequest = new MyCreateRequest
        {
            Name = "Example",
            Description = "Example description"
        };
        var created = manager.Create(createRequest, out var createResponse);

        if (createResponse.Status)
        {
            Console.WriteLine($"Created: {created.Id}");
        }
        else
        {
            Console.WriteLine($"Error: {string.Join(", ", createResponse.Messages.SelectMany(m => m.Value))}");
        }
    }
}
```

## Naming Conventions

| Element | Pattern | Example |
|---------|---------|---------|
| V5 Manager | `[Domain]ManagerV5` | `NotesManagerV5` |
| Response DTO | `[Entity]Resource` or `[Entity]Collection` | `RouteNoteResource`, `RouteNoteCollection` |
| Create Request | `[Entity]StoreRequest` | `NoteStoreRequest` |
| Update Request | `[Entity]UpdateRequest` | `NoteUpdateRequest` |
| Query Parameters | `[Entity]Parameters` | `RouteNotesParameters` |
| Endpoint Constant | PascalCase in `Consts.cs` | `R4MEInfrastructureSettingsV5.Notes` |

## Authentication Configuration

API keys are stored in `appsettings.json` files:
- `Route4MeSDKTest/appsettings.json` (examples)
- `Route4MeSDKUnitTest/appsettings.json` (V4 tests)
- `Route4MeSdkV5UnitTest/appsettings.json` (V5 tests)

**IMPORTANT**: The `appsettings.json` files contain demo API keys. When testing with production data, update these files with valid API keys. These files are tracked in git, so never commit real API keys.

## Versioning and Releases

- Version format: `MAJOR.MINOR.PATCH.BUILD` (e.g., `7.12.1.0`)
- Update version in: `Route4MeSDKLibrary/Route4MeSDKLibrary.csproj` (lines 19, 21, 22)
- Document changes in: `route4me-csharp-sdk/CHANGELOG.md`
- The SDK is published to NuGet as `Route4MeSDKLibrary`
- Assembly is strong-named (signed with `r4m_csharp_sdk.snk`)

## Important Implementation Notes

### Thread Safety
- All managers are thread-safe
- `HttpClientHolderManager` uses locks for connection pooling
- Avoid capturing context in async methods (use `ConfigureAwait(false)`)

### HTTP Method Usage
The SDK supports all REST verbs via `HttpMethodType` enum:
- `Get` - Read operations
- `Post` - Create operations
- `Put` - Full update operations
- `Patch` - Partial update operations (Content-Type: application/json-patch+json)
- `Delete` - Delete operations (can include request body)

### URL Parameter Substitution
For endpoints with path parameters (e.g., `/notes/{note_id}`), the SDK automatically:
1. Replaces `{param_name}` in URL with actual values
2. Removes the parameter from query string
3. Example: `R4MEInfrastructureSettingsV5.NoteById` → `/notes/12345`

### Pagination Handling
V5 API responses include standardized pagination:
```csharp
public class PageMeta
{
    [DataMember(Name = "total")]
    public int? Total { get; set; }

    [DataMember(Name = "per_page")]
    public int? PerPage { get; set; }

    [DataMember(Name = "current_page")]
    public int? CurrentPage { get; set; }

    [DataMember(Name = "last_page")]
    public int? LastPage { get; set; }
}
```

### Long-Running Operations
For async operations (e.g., optimization), the SDK:
1. Extracts job ID from `Location` header
2. Returns `Tuple<T, ResultResponse, string>` (result, response, jobId)
3. Use job ID to poll for completion

## API Endpoint Reference

All endpoints are defined in `route4me-csharp-sdk/Route4MeSDKLibrary/Consts.cs`:
- V4: `R4MEInfrastructureSettings` class (40+ constants)
- V5: `R4MEInfrastructureSettingsV5` class (60+ constants)

When adding new endpoints, always add the constant to this file first.
