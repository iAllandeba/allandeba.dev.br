using allandeba.dev.br.Api.Common.Api;
using allandeba.dev.br.Api.Endpoints.Chatwoot.ChatTriggered.Models;
using allandeba.dev.br.Api.Endpoints.Chatwoot.Models;
using allandeba.dev.br.Api.Responses.EvolutionApi;
using allandeba.dev.br.Api.Services.Interfaces;
using allandeba.dev.br.Core.Handlers;
using allandeba.dev.br.Core.Responses;
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
        IChatwootService service)
    {
        return await service.ChatTriggered(payload);
    }
}