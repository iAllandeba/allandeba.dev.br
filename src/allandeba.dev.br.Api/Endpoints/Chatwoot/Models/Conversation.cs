using System.Text.Json.Serialization;

namespace allandeba.dev.br.Api.Endpoints.Chatwoot.Models;

public class Conversation
{
    public int Id { get; set; }
    [JsonPropertyName("inbox_id")] public int InboxId { get; set; }
    [JsonPropertyName("can_reply")] public bool CanReply { get; set; }
    public string? Channel { get; set; }
    public ContactInbox? ContactInbox { get; set; }
    public List<Message>? Messages { get; set; }
    public Meta? Meta { get; set; }
    public string? Status { get; set; }
    [JsonPropertyName("unread_count")] public int UnreadCount { get; set; }
    [JsonPropertyName("agent_last_seen_at")] public long AgentLastSeenAt { get; set; }
    [JsonPropertyName("contact_last_seen_at")] public long ContactLastSeenAt { get; set; }
    [JsonPropertyName("account_id")] public int AccountId { get; set; }
}