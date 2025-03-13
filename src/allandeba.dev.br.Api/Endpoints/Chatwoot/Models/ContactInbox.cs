using System.Text.Json.Serialization;

namespace allandeba.dev.br.Api.Endpoints.Chatwoot.Models;

public class ContactInbox
{
    public int Id { get; set; }
    [JsonPropertyName("contact_id")] public int ContactId { get; set; }
    [JsonPropertyName("inbox_id")] public int InboxId { get; set; }
    [JsonPropertyName("source_id")] public string? SourceId { get; set; }
    [JsonPropertyName("created_at")] public DateTime CreatedAt { get; set; }
    [JsonPropertyName("updated_at")] public DateTime UpdatedAt { get; set; }
    [JsonPropertyName("hmac_verified")] public bool HmacVerified { get; set; }
}