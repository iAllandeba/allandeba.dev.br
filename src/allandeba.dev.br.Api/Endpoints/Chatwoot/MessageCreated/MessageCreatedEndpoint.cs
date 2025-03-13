using allandeba.dev.br.Api.Common.Api;
using allandeba.dev.br.Api.Endpoints.Chatwoot.MessageCreated.Models;
using allandeba.dev.br.Api.Endpoints.Chatwoot.Models;
using allandeba.dev.br.Core.Handlers;
using allandeba.dev.br.Core.Requests.EvolutionApi;
using allandeba.dev.br.Core.Responses;
using allandeba.dev.br.Core.Responses.EvolutionApi;
using Deba.Caching.Interfaces;
using Deba.Caching.Models;
using Microsoft.AspNetCore.Mvc;

namespace allandeba.dev.br.Api.Endpoints.Chatwoot.MessageCreated;

public class MessageCreatedEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/messageCreated", HandleAsync)
            .WithName("Notifica que alguem enviou uma mensagem no chat no site: Post")
            .WithSummary("Notifica no whatsapp quando alguem enviar uma mensagem no chat no website")
            .WithDescription("Notifica no whatsapp quando alguem enviar uma mensagem no chat no website")
            .WithOrder(2)
            .Produces<Response<EvolutionApiResponse?>>();
    
    private static async Task<IResult> HandleAsync(
        [FromBody] MessageCreatedPaylod payload,
        IEvolutionApiHandler handler,
        IMemoryCacheService memoryCache)
    {
        if (!await CanSendNotification(memoryCache, payload.Conversation?.ContactInbox?.SourceId))
            return TypedResults.Accepted("Uma notificação já foi encaminhada para esse mesmo Id e não será notificado novamente dentro do tempo de espera para cada nova notificação");
        
        var environment = GetEnvironmentMessageFrom(payload.Inbox);
        var request = new EvolutionApiRequest
        {
            Message = @$"Uma *nova mensagem* foi encaminhada no website
Ambiente de *{environment}*
id: {payload.Id}
sourceId: {payload.Conversation?.ContactInbox?.SourceId}"
        };
        
        var result = await handler.SendText(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }

    private static async Task<bool> CanSendNotification(IMemoryCacheService memoryCache, string? payloadSourceId)
    {
        var key = $"messageCreated_{payloadSourceId}";
        
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