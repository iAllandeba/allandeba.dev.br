using System.Text.Json.Serialization;

namespace allandeba.dev.br.Api.Endpoints.Chatwoot.Models;

public class Message
{
    public int Id { get; set; }
    public string? Content { get; set; }
    [JsonPropertyName("message_type")] public int MessageType { get; set; }
    [JsonPropertyName("created_at")] public long CreatedAt { get; set; }
    public bool Private { get; set; }
    [JsonPropertyName("source_id")] public string? SourceId { get; set; }
    [JsonPropertyName("content_type")] public string? ContentType { get; set; }
    [JsonPropertyName("content_attributes")] public object? ContentAttributes { get; set; }
    public Sender? Sender { get; set; }
    public Account? Account { get; set; }
    public Conversation? Conversation { get; set; }
    public Inbox? Inbox { get; set; }
}