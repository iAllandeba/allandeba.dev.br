using System.Text.Json.Serialization;

namespace allandeba.dev.br.Core.Responses;

public class Response<TData>
{
    [JsonInclude]
    private int _code;

    [JsonConstructor]
    public Response() =>
        _code = Configuration.DefaultStatusCode;

    public Response(TData? data, int code = Configuration.DefaultStatusCode, string? message = null, string? details = null)
    {
        Data = data;
        Message = message;
        _code = code;
        Details = details;
    }

    public TData? Data { get; set; }
    public string? Message { get; set; }
    public string? Details { get; set; }

    [JsonIgnore]
    public bool IsSuccess =>
        _code is >= 200 and <= 299;
}