using allandeba.dev.br.Api.Common.Api;
using allandeba.dev.br.Api.Endpoints.Chatwoot.MessageCreated.Models;
using allandeba.dev.br.Api.Endpoints.Chatwoot.Models;
using allandeba.dev.br.Api.Responses.EvolutionApi;
using allandeba.dev.br.Api.Services.Interfaces;
using allandeba.dev.br.Core.Handlers;
using allandeba.dev.br.Core.Responses;
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
        [FromBody] MessageCreatedPayload payload,
        IChatwootService service)
    {
        return await service.MessageCreated(payload);
    }
}