using System.Text.Json.Serialization;

namespace allandeba.dev.br.Api.Endpoints.Chatwoot.Models;

public class EventInfo
{
    [JsonPropertyName("initiated_at")] public DateTime InitiatedAt { get; set; }
    public string? Referer { get; set; }
    public string? WidgetLanguage { get; set; }
    public string? BrowserLanguage { get; set; }
    public Browser? Browser { get; set; }
}