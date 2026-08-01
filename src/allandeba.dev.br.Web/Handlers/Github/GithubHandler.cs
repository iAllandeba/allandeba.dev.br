using System.Net.Http.Json;
using allandeba.dev.br.Core.Handlers;
using allandeba.dev.br.Core.Requests.Github;
using allandeba.dev.br.Core.Responses;
using allandeba.dev.br.Core.Responses.Github;
using allandeba.dev.br.Core.Serialization;

namespace allandeba.dev.br.Web.Handlers.Github;

public class GithubHandler : IGithubHandler
{
    private readonly HttpClient _httpClient;
    public GithubHandler(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
    }

    public async Task<Response<GithubProjectResponse>> GetFavoriteProjectsAsync(GetGithubProjectRequest request)
    {
        var uriBuilder = new UriBuilder(_httpClient.BaseAddress!)
        {
            Path = "/v1/github",
            Query = $"githubUser={Uri.EscapeDataString(request.User)}"
        };

        try
        {
            // The API answers with a Response body on failure too, so the body is read
            // regardless of the status code to keep the message it carries.
            var httpResponse = await _httpClient.GetAsync(uriBuilder.ToString());
            var response = await httpResponse.Content.ReadFromJsonAsync(AppJsonContext.Default.GithubProjectResult);

            return response ?? new Response<GithubProjectResponse>(
                null, (int)httpResponse.StatusCode, "Não foi possível obter os projetos");
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e);
            return new Response<GithubProjectResponse>(null, 500, "Não foi possível obter os projetos");
        }
    }
}
