using System.Net.Http.Json;
using System.Text;
using allandeba.dev.br.Core.Handlers;
using allandeba.dev.br.Core.Requests.Account;
using allandeba.dev.br.Core.Responses;
using allandeba.dev.br.Core.Responses.Account;
using allandeba.dev.br.Core.Serialization;

namespace allandeba.dev.br.Web.Handlers;

public class AccountHandler : IAccountHandler
{
    private readonly HttpClient _httpClient;

    public AccountHandler(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
    }

    public async Task<Response<AccountResponse>> LoginAsync(LoginRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/account/login", request, AppJsonContext.Default.LoginRequest);
        return await result.Content.ReadFromJsonAsync(AppJsonContext.Default.AccountResponseResult) ?? throw new InvalidOperationException();
    }

    public async Task<Response<AccountResponse>> RegisterAsync(RegisterRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/account/register", request, AppJsonContext.Default.RegisterRequest);
        return await result.Content.ReadFromJsonAsync(AppJsonContext.Default.AccountResponseResult) ?? throw new InvalidOperationException();
    }

    public async Task<Response<AccountResponse>> LogoutAsync()
    {
        var emptyContent = new StringContent("{}", Encoding.UTF8, "application/json");
        var result = await _httpClient.PostAsync("v1/account/logout", emptyContent);
        return await result.Content.ReadFromJsonAsync(AppJsonContext.Default.AccountResponseResult) ?? throw new InvalidOperationException();
    }
}
