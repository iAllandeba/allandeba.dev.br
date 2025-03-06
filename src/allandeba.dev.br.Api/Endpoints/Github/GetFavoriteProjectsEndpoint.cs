using System.Security.Claims;
using allandeba.dev.br.Api.Common.Api;
using allandeba.dev.br.Core.Handlers;
using allandeba.dev.br.Core.Requests.Github;
using allandeba.dev.br.Core.Responses;
using allandeba.dev.br.Core.Responses.Github;
using Microsoft.AspNetCore.Mvc;

namespace allandeba.dev.br.Api.Endpoints.Github;

public class GetFavoriteProjectsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/", HandleAsync)
        .WithName("Github: Get")
        .WithSummary("Retorna os projetos salvos como favoritos no github")
        .WithDescription("Retorna os projetos salvos como favoritos no github")
        .WithOrder(1)
        .Produces<Response<GithubProjectResponse?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IGithubHandler handler,
        [FromQuery] string githubUser)
    {
        var request = new GetGithubProjectRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            User = githubUser,
        };

        var result = await handler.GetFavoriteProjectsAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}