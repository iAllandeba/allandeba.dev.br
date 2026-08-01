using System.Text.RegularExpressions;
using allandeba.dev.br.Api.Services;
using allandeba.dev.br.Core.Handlers;
using allandeba.dev.br.Core.Models.Github;
using allandeba.dev.br.Core.Requests.Github;
using allandeba.dev.br.Core.Responses;
using allandeba.dev.br.Core.Responses.Github;
using Deba.Caching.Interfaces;
using Deba.Caching.Models;

namespace allandeba.dev.br.Api.Handlers;

public partial class GithubHandler(GithubService githubService, IMemoryCacheService memoryCache) : IGithubHandler
{
    private static readonly TimeSpan _cacheDuration = TimeSpan.FromDays(7);

    // The endpoint is anonymous and the user name becomes both a cache key and a scraped URL,
    // so anything that is not a real github handle is rejected before it can reach either.
    [GeneratedRegex(@"^[a-zA-Z0-9](?:[a-zA-Z0-9]|-(?=[a-zA-Z0-9])){0,38}$")]
    private static partial Regex GithubUserRegex();

    public async Task<Response<GithubProjectResponse>> GetFavoriteProjectsAsync(GetGithubProjectRequest request)
    {
        var user = request.User;
        if (!GithubUserRegex().IsMatch(user))
            return new Response<GithubProjectResponse>(null, 400, "Usuário do github inválido");

        try
        {
            var projects = await GetCachedProjectsAsync(user);

            return projects is null
                ? new Response<GithubProjectResponse>(null, 404, "Projetos não encontrados")
                : new Response<GithubProjectResponse> { Data = new() { GithubProjects = projects } };
        }
        catch
        {
            return new Response<GithubProjectResponse>(null, 500, "Não foi possível recuperar os projetos");
        }
    }

    private async Task<List<GithubProject>?> GetCachedProjectsAsync(string user)
    {
        var cacheKey = $"projects_{user}";

        var cachedProjects = await memoryCache.GetItemAsync<List<GithubProject>>(cacheKey);
        if (cachedProjects is not null)
            return cachedProjects;

        var projects = await githubService.GetFavoriteProjects(user);

        // Empty results are not cached: a name that matches the format but owns no pinned
        // repositories would otherwise pin an entry for a week on every lookup.
        if (projects is { Count: > 0 })
            await memoryCache.SetItemAsync(cacheKey, projects, new CacheOptions(DateTime.UtcNow.Add(_cacheDuration)));

        return projects;
    }
}
