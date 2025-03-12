using System.Text.Json.Serialization;

namespace allandeba.dev.br.Api.Endpoints.Chatwoot.Models;

public class WidgetTriggeredPayload
{
    public int Id { get; set; }
    public Contact? Contact { get; set; }
    public Inbox? Inbox { get; set; }
    public Account? Account { get; set; }
    [JsonPropertyName("current_conversation")] public Conversation? CurrentConversation { get; set; }
    [JsonPropertyName("source_id")] public string? SourceId { get; set; }
    public string? Event { get; set; }
    public EventInfo? EventInfo { get; set; }
    public string? Referer { get; set; }
    [JsonPropertyName("widget_language")] public string? WidgetLanguage { get; set; }
    [JsonPropertyName("browser_language")] public string? BrowserLanguage { get; set; }
    public Browser? Browser { get; set; }
}