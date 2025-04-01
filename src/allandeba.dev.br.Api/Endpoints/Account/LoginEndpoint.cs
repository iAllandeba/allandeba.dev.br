using allandeba.dev.br.Api.Common.Api;
using allandeba.dev.br.Core.Handlers;
using allandeba.dev.br.Core.Requests.Account;
using allandeba.dev.br.Core.Responses;
using allandeba.dev.br.Core.Responses.Account;
using Microsoft.AspNetCore.Mvc;

namespace allandeba.dev.br.Api.Endpoints.Account;

public class LoginEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app
            .MapPost("/login", HandleAsync);

    private static async Task<Response<AccountResponse?>> HandleAsync(
        [FromBody] LoginRequest request,
        IAccountHandler handler
    )
    {
        return await handler.LoginAsync(request);
    }
}