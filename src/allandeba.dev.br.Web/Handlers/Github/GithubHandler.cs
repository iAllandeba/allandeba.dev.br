using System.Net.Http.Json;
using allandeba.dev.br.Core.Handlers;
using allandeba.dev.br.Core.Requests.Github;
using allandeba.dev.br.Core.Responses;
using allandeba.dev.br.Core.Responses.Github;

namespace allandeba.dev.br.Web.Handlers.Github;

public class GithubHandler : IGithubHandler
{
    private readonly HttpClient _httpClient;
    public GithubHandler(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
    }

    public async Task<Response<GithubProjectResponse?>> GetFavoriteProjectsAsync(GetGithubProjectRequest request)
    {
        var uriBuilder = new UriBuilder(_httpClient.BaseAddress!)
        {
            Path = "/v1/github",
            Query = $"githubUser={request.User}"
        };

        try
        {
            return await _httpClient.GetFromJsonAsync<Response<GithubProjectResponse?>>(uriBuilder.ToString())
                ?? new Response<GithubProjectResponse?>(null, 400, "Não foi possível obter o projeto");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<GithubProjectResponse?>(null, 500, e.Message);
        }
    }
}