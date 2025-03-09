using allandeba.dev.br.Api.Common.Api;
using allandeba.dev.br.Api.Endpoints.Github;
using allandeba.dev.br.Api.Data.Entities;
using allandeba.dev.br.Api.Endpoints.EvolutionApi;
using allandeba.dev.br.Api.Endpoints.Identity;

namespace allandeba.dev.br.Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app
            .MapGroup("");

        endpoints.MapGroup("v1/identity")
            .WithTags("Identity")
            .MapIdentityApi<Users>();

        endpoints.MapGroup("v1/identity")
            .WithTags("Identity")
            .MapEndpoint<LogoutEndpoint>()
            .MapEndpoint<GetRolesEndpoint>();

        endpoints.MapGroup("/")
            .WithTags("Health Check")
            .MapGet("/", () => new { message = "OK" });

        endpoints.MapGroup("v1/github")
            .WithTags("Github")
            .MapEndpoint<GetFavoriteProjectsEndpoint>();
        
        endpoints.MapGroup("v1/chatwoot")
            .WithTags("ChatWoot")
            .MapEndpoint<NotifyEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}