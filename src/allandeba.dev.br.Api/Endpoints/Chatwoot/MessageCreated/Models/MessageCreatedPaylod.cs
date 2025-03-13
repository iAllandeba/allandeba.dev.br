using System.Text.Json.Serialization;
using allandeba.dev.br.Api.Endpoints.Chatwoot.Models;

namespace allandeba.dev.br.Api.Endpoints.Chatwoot.MessageCreated.Models;

public class MessageCreatedPaylod
{
    public int Id { get; set; }
    public Inbox? Inbox { get; set; }
    public Conversation? Conversation { get; set; }
    public string? Event { get; set; }
}