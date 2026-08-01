using allandeba.dev.br.Api.Services;
using allandeba.dev.br.Core.Handlers;
using allandeba.dev.br.Core.Requests.Github;
using allandeba.dev.br.Core.Responses;
using allandeba.dev.br.Core.Responses.Github;
using Deba.Caching.Interfaces;
using Deba.Caching.Models;

namespace allandeba.dev.br.Api.Handlers;

public class GithubHandler(GithubService githubService, IMemoryCacheService memoryCache) : IGithubHandler
{
    public async Task<Response<GithubProjectResponse>> GetFavoriteProjectsAsync(GetGithubProjectRequest request)
    {
        try
        {
            var user = request.User;
            var cacheOptions = new CacheOptions(DateTime.UtcNow.AddDays(7));
            var projects = await memoryCache.GetOrSetAsync($"projects_{user}", () => githubService.GetFavoriteProjects(user), cacheOptions);

            return projects is null
                ? new Response<GithubProjectResponse>(null, 404, "Projetos não encontrados")
                : new Response<GithubProjectResponse> { Data = new() { GithubProjects = projects } };
        }
        catch
        {
            return new Response<GithubProjectResponse>(null, 500, "Não foi possível recuperar os projetos");
        }
    }
}