using System.Text.Json.Serialization;

namespace allandeba.dev.br.Core.Responses;

public class Response<TData>
{
    // Serialized as "_code" so the wire format stays compatible with clients that
    // still read the old payload. A public property is required here: the JSON source
    // generator cannot access private members, so a [JsonInclude] private field is
    // silently dropped and IsSuccess would always report true.
    [JsonPropertyName("_code")]
    public int Code { get; set; } = Configuration.DefaultStatusCode;

    public TData? Data { get; set; }
    public string? Message { get; set; }
    public string? Details { get; set; }

    [JsonIgnore]
    public bool IsSuccess =>
        Code is >= 200 and <= 299;

    // Explicit: the type has two public constructors, so leaving the choice implicit would
    // rely on the serializer preferring the parameterless one.
    [JsonConstructor]
    public Response() { }

    public Response(TData? data, int code = Configuration.DefaultStatusCode, string? message = null, string? details = null)
        => (Data, Code, Message, Details) = (data, code, message, details);
}
