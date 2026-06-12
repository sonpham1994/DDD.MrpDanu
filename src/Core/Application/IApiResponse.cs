namespace Application;

/// <summary>
/// Marker for an API payload that is returned wrapped as <c>ApiResponse&lt;T&gt;</c>.
/// Types implementing this are picked up automatically by the JSON source-generator
/// code-gen tool (tools/JsonSerializerContextGenerator) - no manual [JsonSerializable] needed.
/// </summary>
public interface IApiResponse;

/// <summary>
/// Marker for an API payload that is returned wrapped as <c>ApiResponse&lt;IReadOnlyList&lt;T&gt;&gt;</c>
/// (a collection endpoint). Picked up automatically by the code-gen tool.
/// A type may implement both markers if it is returned both ways.
/// </summary>
public interface IApiCollectionResponse;
