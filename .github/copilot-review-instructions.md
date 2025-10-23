# GitHub Copilot Review Instructions for Route4Me .NET Core SDK

## Project Overview
This is the Route4Me Route Optimization SaaS C# SDK (.NET Core framework) that provides a comprehensive API wrapper for route optimization, fleet management, and logistics operations. The SDK supports both V4 and V5 API versions with a focus on performance, reliability, and maintainability.

## Architecture & Design Patterns

### Manager Pattern
- **Base Class**: All managers inherit from `Route4MeManagerBase`
- **Naming Convention**: `{Domain}ManagerV5` (e.g., `OptimizationManagerV5`, `VehicleManagerV5`)
- **Constructor**: Always takes `string apiKey` parameter
- **API Methods**: Provide both synchronous and asynchronous versions
- **Return Pattern**: Use `out ResultResponse resultResponse` for error handling

```csharp
public class ExampleManagerV5 : Route4MeManagerBase
{
    public ExampleManagerV5(string apiKey) : base(apiKey) { }
    
    public DataType GetData(Parameters parameters, out ResultResponse resultResponse)
    {
        return GetJsonObjectFromAPI<DataType>(parameters, url, HttpMethodType.Get, out resultResponse);
    }
    
    public async Task<Tuple<DataType, ResultResponse>> GetDataAsync(Parameters parameters)
    {
        var result = await GetJsonObjectFromAPIAsync<DataType>(parameters, url, HttpMethodType.Get);
        return new Tuple<DataType, ResultResponse>(result.Item1, result.Item2);
    }
}
```

### Data Types & Serialization
- **DataContract**: All data types use `[DataContract]` and `[DataMember]` attributes
- **JSON Mapping**: Use `[DataMember(Name = "json_property_name")]` for API field mapping
- **Documentation**: Include comprehensive XML documentation for all public properties
- **Versioning**: V5 types are in `Route4MeSDK.DataTypes.V5` namespace

```csharp
[DataContract]
public class ExampleResponse
{
    /// <summary>
    /// Detailed description of the property and its purpose
    /// </summary>
    [DataMember(Name = "property_name")]
    public string PropertyName { get; set; }
}
```

### Error Handling
- **ResultResponse**: Use `ResultResponse` class for consistent error handling
- **Exception Types**: Catch specific exceptions (`HttpListenerException`, `Exception`)
- **Error Messages**: Structure errors in `Dictionary<string, string[]>` format
- **Status Codes**: Always check `response.IsSuccessStatusCode`

```csharp
catch (HttpListenerException e)
{
    resultResponse = new ResultResponse
    {
        Status = false,
        Messages = new Dictionary<string, string[]>
        {
            {"Error", new[] {e.Message}}
        }
    };
}
```

## Code Quality Standards

### Documentation Requirements
- **XML Documentation**: All public methods, properties, and classes must have comprehensive XML documentation
- **Parameter Documentation**: Document all parameters with purpose and constraints
- **Return Documentation**: Document return values and possible error conditions
- **Example Usage**: Include usage examples in complex method documentation

### Naming Conventions
- **Classes**: PascalCase (e.g., `OptimizationManagerV5`)
- **Methods**: PascalCase (e.g., `GetVehicles`, `CreateOrder`)
- **Properties**: PascalCase (e.g., `RouteId`, `MemberEmail`)
- **Parameters**: camelCase (e.g., `vehicleParams`, `optimizationParameters`)
- **Private Fields**: camelCase with underscore prefix (e.g., `_apiKey`)

### Async/Await Patterns
- **Async Methods**: Always provide async versions of public API methods
- **ConfigureAwait**: Use `.ConfigureAwait(false)` in library code
- **Return Types**: Use `Task<Tuple<T, ResultResponse>>` for async methods
- **Cancellation**: Support `CancellationToken` where appropriate

### HTTP Client Management
- **HttpClientHolder**: Use `HttpClientHolderManager.AcquireHttpClientHolder()` for HTTP client management
- **Disposal**: Always use `using` statements for HTTP client holders
- **V5 Detection**: Use `R4MeUtils.IsV5(url)` to determine API version
- **Authentication**: Pass API key to V5 endpoints via HTTP client holder

## API Integration Patterns

### URL Construction
- **Base URLs**: Use constants from `R4MEInfrastructureSettings` and `R4MEInfrastructureSettingsV5`
- **Parameter Serialization**: Use `optimizationParameters.Serialize(v5 ? null : ApiKey)`
- **Query Parameters**: Append serialized parameters to base URL

### HTTP Methods
- **GET**: For data retrieval operations
- **POST**: For creation operations
- **PUT**: For full updates
- **PATCH**: For partial updates
- **DELETE**: For deletion operations

### Content Handling
- **JSON Content**: Use `StringContent` with `application/json` content type
- **PATCH Content**: Use `application/json-patch+json` for PATCH operations
- **Stream Processing**: Handle both `StreamContent` and other content types

## Testing & Examples

### Test Structure
- **Example Classes**: Use `Route4MeExamples` partial class for examples
- **Test Data**: Use `ActualApiKey` and `DemoApiKey` from configuration
- **Cleanup**: Always clean up test data using removal lists
- **CRUD Operations**: Follow Create-Read-Update-Delete pattern in examples

### Configuration
- **API Keys**: Store in `appsettings.json` with `actualApiKey` and `demoApiKey`
- **Environment**: Support both production and demo environments
- **Settings**: Use `R4MeUtils.ReadSetting()` for configuration access

## Performance Considerations

### HTTP Client Reuse
- **Connection Pooling**: Leverage `HttpClientHolderManager` for connection reuse
- **Timeout Handling**: Implement appropriate timeout values
- **Retry Logic**: Consider implementing retry mechanisms for transient failures

### Memory Management
- **Disposal**: Properly dispose of HTTP clients and streams
- **Large Responses**: Handle large response streams efficiently
- **Object Pooling**: Consider object pooling for frequently created objects

## Security Guidelines

### API Key Management
- **Secure Storage**: Never hardcode API keys in source code
- **Environment Variables**: Use configuration files or environment variables
- **Key Rotation**: Support API key rotation without code changes

### Data Validation
- **Input Validation**: Validate all input parameters
- **Sanitization**: Sanitize user inputs before API calls
- **Error Information**: Avoid exposing sensitive information in error messages

## Version Compatibility

### V4 vs V5 APIs
- **Version Detection**: Use `R4MeUtils.IsV5(url)` for version-specific logic
- **Backward Compatibility**: Maintain V4 support while adding V5 features
- **Namespace Separation**: Keep V5 types in separate namespaces
- **Manager Separation**: Use separate manager classes for V4 and V5

### .NET Standard
- **Target Framework**: Maintain `netstandard2.0` compatibility
- **Dependencies**: Use compatible package versions
- **Cross-Platform**: Ensure cross-platform compatibility

## Code Review Checklist

### Before Submitting
- [ ] All public methods have XML documentation
- [ ] Both sync and async versions provided for API methods
- [ ] Proper error handling with `ResultResponse`
- [ ] HTTP client properly disposed
- [ ] API version detection implemented correctly
- [ ] Test examples provided for new functionality
- [ ] Configuration properly externalized
- [ ] No hardcoded values or API keys

### Code Quality
- [ ] Follows established naming conventions
- [ ] Uses appropriate design patterns
- [ ] Handles exceptions properly
- [ ] Implements proper disposal patterns
- [ ] Uses async/await correctly
- [ ] Includes comprehensive error messages
- [ ] Maintains backward compatibility

### Testing
- [ ] Example code provided
- [ ] Test data cleanup implemented
- [ ] Both success and error scenarios covered
- [ ] Configuration properly set up
- [ ] Documentation matches implementation

## Common Pitfalls to Avoid

1. **Hardcoded API Keys**: Never commit API keys to source control
2. **Missing Async Methods**: Always provide async versions of public API methods
3. **Improper Disposal**: Always dispose of HTTP clients and streams
4. **Generic Exception Handling**: Catch specific exception types, not just `Exception`
5. **Missing Documentation**: All public APIs must be documented
6. **Version Mixing**: Don't mix V4 and V5 logic in the same method
7. **Memory Leaks**: Ensure proper disposal of disposable resources
8. **Inconsistent Error Handling**: Use `ResultResponse` consistently

## Dependencies & Packages

### Core Dependencies
- `Newtonsoft.Json` (13.0.2) - JSON serialization
- `Microsoft.Extensions.Configuration.*` - Configuration management
- `System.Collections.Immutable` - Immutable collections
- `System.ComponentModel.Annotations` - Data annotations

### Development Dependencies
- `CsvHelper` - CSV file processing
- `fastJSON` - Alternative JSON processing
- `SocketIoClientDotNet.Standard` - WebSocket support

## Release & Versioning

### Version Management
- **Semantic Versioning**: Follow semantic versioning principles
- **Changelog**: Maintain detailed changelog in `CHANGELOG.md`
- **Assembly Signing**: Use strong name signing with `r4m_csharp_sdk.snk`
- **Package Metadata**: Keep package metadata up to date

### Distribution
- **NuGet Package**: Publish to NuGet with proper metadata
- **GitHub Releases**: Tag releases in GitHub
- **Documentation**: Update README and examples for new features

---

*This document should be reviewed and updated as the project evolves. All team members should familiarize themselves with these guidelines before contributing to the codebase.*
