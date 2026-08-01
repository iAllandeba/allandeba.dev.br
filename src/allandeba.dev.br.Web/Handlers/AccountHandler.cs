using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
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
        return await ReadResponseAsync(result);
    }

    public async Task<Response<AccountResponse>> RegisterAsync(RegisterRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/account/register", request, AppJsonContext.Default.RegisterRequest);
        return await ReadResponseAsync(result);
    }

    public async Task<Response<AccountResponse>> LogoutAsync()
    {
        var emptyContent = new StringContent("{}", Encoding.UTF8, "application/json");
        var result = await _httpClient.PostAsync("v1/account/logout", emptyContent);
        return await ReadResponseAsync(result);
    }

    // The API answers with a Response body on failure too, so the body is preferred.
    // Anything else — an empty 401 from the authorization filter, a proxy error page —
    // becomes a Response carrying the HTTP status, with no message so the caller can
    // fall back to its own localized text.
    private static async Task<Response<AccountResponse>> ReadResponseAsync(HttpResponseMessage result)
    {
        try
        {
            var response = await result.Content.ReadFromJsonAsync(AppJsonContext.Default.AccountResponseResult);
            if (response is not null)
                return response;
        }
        catch (JsonException)
        {
        }

        return new Response<AccountResponse>(null, (int)result.StatusCode);
    }
}
