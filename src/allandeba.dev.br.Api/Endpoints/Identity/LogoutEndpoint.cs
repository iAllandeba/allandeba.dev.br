using allandeba.dev.br.Api.Common.Api;
using allandeba.dev.br.Api.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace allandeba.dev.br.Api.Endpoints.Identity;

public class LogoutEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app
            .MapPost("/logout", HandleAsync)
            .RequireAuthorization();

    private static async Task<IResult> HandleAsync(SignInManager<Users> signInManager)
    {
        await signInManager.SignOutAsync();
        return Results.Ok();
    }
}