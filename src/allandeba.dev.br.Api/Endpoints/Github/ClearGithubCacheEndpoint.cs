using allandeba.dev.br.Api.Common.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace allandeba.dev.br.Api.Endpoints.Github;

public class ClearGithubCacheEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/cache", HandleAsync)
            .WithName("Github: Clear Cache")
            .WithSummary("Força o reset do cache de projetos do GitHub")
            .WithOrder(2)
            .Produces(204);

    private static IResult HandleAsync(
        IMemoryCache memoryCache,
        [FromQuery] string githubUser)
    {
        memoryCache.Remove($"projects_{githubUser}");
        return TypedResults.NoContent();
    }
}
