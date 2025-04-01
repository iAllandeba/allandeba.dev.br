using allandeba.dev.br.Api.Common.Api;
using allandeba.dev.br.Core.Handlers;
using allandeba.dev.br.Core.Responses;
using allandeba.dev.br.Core.Responses.Account;

namespace allandeba.dev.br.Api.Endpoints.Account;

public class LogoutEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app
            .MapPost("/logout", HandleAsync)
            .RequireAuthorization();

    private static async Task<Response<AccountResponse?>> HandleAsync(IAccountHandler handler)
    {
        return await handler.LogoutAsync();
    }
}