using allandeba.dev.br.Core.Requests.Account;
using allandeba.dev.br.Core.Responses;

namespace allandeba.dev.br.Core.Handlers;

public interface IAccountHandler
{
    Task<Response<string>> LoginAsync(LoginRequest request);
    Task<Response<string>> RegisterAsync(RegisterRequest request);
    Task LogoutAsync();
}