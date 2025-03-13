using allandeba.dev.br.Api.Endpoints.Chatwoot.ChatTriggered.Models;
using allandeba.dev.br.Api.Endpoints.Chatwoot.MessageCreated.Models;
using allandeba.dev.br.Api.Handlers.Interfaces;
using allandeba.dev.br.Api.Requests.EvolutionApi;
using allandeba.dev.br.Api.Services.Interfaces;
using allandeba.dev.br.Core.Handlers;
using Deba.Caching.Interfaces;
using Deba.Caching.Models;

namespace allandeba.dev.br.Api.Services;

public class ChatwootService(IMemoryCacheService memoryCache, IEvolutionApiHandler evolutionApiHandler) : IChatwootService
{
    private async Task<bool> CanSendNotification(string webhookName, string? payloadSourceId)
    {
        var key = $"{webhookName}_{payloadSourceId}";
        
        if (!string.IsNullOrEmpty(payloadSourceId))
        {
            var existInCache = await memoryCache.GetItemAsync<string>(key);
            if (!string.IsNullOrEmpty(existInCache))
                return false;
        }
        
        await memoryCache.SetItemAsync(key, payloadSourceId, new CacheOptions(DateTime.UtcNow.AddMinutes(10)));
        return true;
    }

    private static string GetEnvironmentMessageFrom(string inboxName)
    {
        if (inboxName.Contains("hml", StringComparison.InvariantCultureIgnoreCase))
            return "homologação";

        if (inboxName.Contains("local", StringComparison.InvariantCultureIgnoreCase))
            return "desenvolvimento";

        if (inboxName.Equals("allandeba.dev.br", StringComparison.InvariantCultureIgnoreCase))
            return "produção";

        return "undefined chatwoot inbox";
    }
    
    private static string GetTemplateMessage(string message, string environment, string? sourceId) =>
        @$"{message}
Ambiente de *{environment}*
sourceId: {sourceId}";
    
    public async Task<IResult> MessageCreated(MessageCreatedPayload payload)
    {
        if (!await CanSendNotification("messageCreated", payload.Conversation?.ContactInbox?.SourceId))
            return TypedResults.Accepted("Uma notificacao ja foi encaminhada para esse mesmo Id e nao sera notificado novamente dentro do tempo de espera para cada nova notificacao");
        
        var environment = GetEnvironmentMessageFrom(payload.Inbox?.Name ?? string.Empty);
        var message = GetTemplateMessage("Uma *nova mensagem* foi encaminhada no website", environment, payload.Conversation?.ContactInbox?.SourceId);
        
        var request = new EvolutionApiRequest { Message = message, };
        var result = await evolutionApiHandler.SendText(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }

    public async Task<IResult> ChatTriggered(WidgetTriggeredPayload payload)
    {
        if (!await CanSendNotification("chatTriggered", payload.SourceId))
            return TypedResults.Accepted("Uma notificacao ja foi encaminhada para esse mesmo sourceId e nao sera notificado novamente dentro do tempo de espera para cada nova notificacao");
        
        var environment = GetEnvironmentMessageFrom(payload.Inbox?.Name ?? string.Empty);
        var message = GetTemplateMessage("O chat foi *aberto* no website", environment, payload.SourceId);
        
        var request = new EvolutionApiRequest { Message = message };
        var result = await evolutionApiHandler.SendText(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}