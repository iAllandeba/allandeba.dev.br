using allandeba.dev.br.Api.Common.Api;
using allandeba.dev.br.Api.Endpoints.Chatwoot.ChatTriggered.Models;
using allandeba.dev.br.Api.Endpoints.Chatwoot.Models;
using allandeba.dev.br.Core.Handlers;
using allandeba.dev.br.Core.Requests.EvolutionApi;
using allandeba.dev.br.Core.Responses;
using allandeba.dev.br.Core.Responses.EvolutionApi;
using Deba.Caching.Interfaces;
using Deba.Caching.Models;
using Microsoft.AspNetCore.Mvc;

namespace allandeba.dev.br.Api.Endpoints.Chatwoot.ChatTriggered;

public class ChatTriggeredEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/chatTriggered", HandleAsync)
            .WithName("Notifica que alguem abriu o chat no site: Post")
            .WithSummary("Notifica no whatsapp quando alguem abrir o chat no website")
            .WithDescription("Notifica no whatsapp quando alguem abrir o chat no website")
            .WithOrder(1)
            .Produces<Response<EvolutionApiResponse?>>();
    
    private static async Task<IResult> HandleAsync(
        [FromBody] WidgetTriggeredPayload payload,
        IEvolutionApiHandler handler,
        IMemoryCacheService memoryCache)
    {
        if (!await CanSendNotification(memoryCache, payload.SourceId))
            return TypedResults.Accepted("Uma notificação já foi encaminhada para esse mesmo sourceId e não será notificado novamente dentro do tempo de espera para cada nova notificação");
        
        var environment = GetEnvironmentMessageFrom(payload.Inbox);
        var request = new EvolutionApiRequest
        {
            Message = @$"O chat foi *aberto* no website
Ambiente de *{environment}*
id: {payload.Id}
sourceId: {payload.SourceId}"
        };
        
        var result = await handler.SendText(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }

    private static async Task<bool> CanSendNotification(IMemoryCacheService memoryCache, string? payloadSourceId)
    {
        var key = $"chatTriggered_{payloadSourceId}";
        
        if (!string.IsNullOrEmpty(payloadSourceId))
        {
            var existInCache = await memoryCache.GetItemAsync<string>(key);
            if (!string.IsNullOrEmpty(existInCache))
                return false;
        }
        
        await memoryCache.SetItemAsync(key, payloadSourceId, new CacheOptions(DateTime.UtcNow.AddMinutes(10)));
        return true;
    }

    private static string GetEnvironmentMessageFrom(Inbox? payloadInbox)
    {
        var inboxName = payloadInbox?.Name ?? string.Empty;
        if (inboxName.Contains("hml", StringComparison.InvariantCultureIgnoreCase))
            return "homologação";

        if (inboxName.Contains("local", StringComparison.InvariantCultureIgnoreCase))
            return "desenvolvimento";

        if (inboxName.Equals("allandeba.dev.br", StringComparison.InvariantCultureIgnoreCase))
            return "produção";

        return "undefined chatwoot inbox";
    }
}