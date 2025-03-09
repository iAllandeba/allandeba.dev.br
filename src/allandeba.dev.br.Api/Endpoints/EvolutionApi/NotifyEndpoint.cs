using allandeba.dev.br.Api.Common.Api;
using allandeba.dev.br.Core.Handlers;
using allandeba.dev.br.Core.Requests.ChatWoot;
using allandeba.dev.br.Core.Responses;
using allandeba.dev.br.Core.Responses.ChatWoot;

namespace allandeba.dev.br.Api.Endpoints.EvolutionApi;

public class NotifyEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/notify", HandleAsync)
            .WithName("Notify new message: Post")
            .WithSummary("Notifica no whatsapp que uma nova mensagem foi enviada no site")
            .WithDescription("Notifica no whatsapp que uma nova mensagem foi enviada no site")
            .WithOrder(1)
            .Produces<Response<ChatWootResponse?>>();
    
    private static async Task<IResult> HandleAsync(
        IChatWootHandler handler)
    {
        var result = await handler.Notify(new ChatWootRequest());
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}