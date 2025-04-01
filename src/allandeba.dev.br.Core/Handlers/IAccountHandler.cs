using allandeba.dev.br.Core.Requests.Account;
using allandeba.dev.br.Core.Responses;
using allandeba.dev.br.Core.Responses.Account;

namespace allandeba.dev.br.Core.Handlers;

public interface IAccountHandler
{
    Task<Response<AccountResponse?>> LoginAsync(LoginRequest request);
    Task<Response<AccountResponse?>> RegisterAsync(RegisterRequest request);
    Task<Response<AccountResponse?>> LogoutAsync();
}