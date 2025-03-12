using System.Text.Json.Serialization;

namespace allandeba.dev.br.Api.Endpoints.Chatwoot.Models;

public class Browser
{
    [JsonPropertyName("browser_name")] public string? BrowserName { get; set; }
    [JsonPropertyName("browser_version")] public string? BrowserVersion { get; set; }
    [JsonPropertyName("device_name")] public string? DeviceName { get; set; }
    [JsonPropertyName("platform_name")] public string? PlatformName { get; set; }
    [JsonPropertyName("platform_version")] public string? PlatformVersion { get; set; }
}