using allandeba.dev.br.Api.Endpoints.Chatwoot.ChatTriggered.Models;
using allandeba.dev.br.Api.Endpoints.Chatwoot.MessageCreated.Models;

namespace allandeba.dev.br.Api.Services.Interfaces;

public interface IChatwootService
{
    Task<IResult> MessageCreated(MessageCreatedPayload payload);
    Task<IResult> ChatTriggered(WidgetTriggeredPayload payload);
}