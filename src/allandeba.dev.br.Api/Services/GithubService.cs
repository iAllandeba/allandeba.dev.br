using allandeba.dev.br.Api.Endpoints.Github.Dto;
using allandeba.dev.br.Core.Models.Github;
using AngleSharp;

namespace allandeba.dev.br.Api.Services;

public class GithubService(ILogger<GithubService> logger)
{
    public async Task<List<GithubProject>?> GetFavoriteProjects(string user)
    {
        var repositories = await GetFavoriteRepositories(user);
        if (!repositories.Any()) return [];

        var tasks = repositories.Select(async repo =>
            await GetProjectAsync(repo));

        var projects = await Task.WhenAll(tasks);
        return projects.OfType<GithubProject>().ToList();
    }

    private async Task<GithubProject?> GetProjectAsync(string repo)
    {
        if (string.IsNullOrEmpty(repo))
        {
            logger.LogWarning($"{nameof(GetProjectAsync)}: invalid parameter {nameof(repo)} with value: {repo}");
            return null;
        }

        var url = $"https://api.github.com/repos{repo}";
        using var client = new HttpClient();
        try
        {
            client.DefaultRequestHeaders.Add("User-Agent", "allandeba.dev.br");

            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var dto = await response.Content.ReadFromJsonAsync<GithubProjectDto>();
                return dto is not null ? dto.ToGithubProject() : null;
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            return null;
        }

        return null;
    }

    private async Task<List<string>> GetFavoriteRepositories(string user)
    {
        var repositories = new List<string>();

        var url = $"https://github.com/{user}";
        var config = Configuration.Default.WithDefaultLoader();
        var context = BrowsingContext.New(config);
        var document = await context.OpenAsync(url);
        var elements = document.GetElementsByClassName("pinned-item-list-item-content");
        foreach (var element in elements)
        {
            var linkElement = element.QuerySelector("a");
            if (linkElement == null) continue;

            var repoPath = linkElement.GetAttribute("href")!;
            repositories.Add(repoPath);
        }

        return repositories;
    }
}