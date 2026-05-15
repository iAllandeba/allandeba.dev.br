using allandeba.dev.br.Api.Endpoints.Github.Dto;
using allandeba.dev.br.Core.Models.Github;
using AngleSharp;

namespace allandeba.dev.br.Api.Services;

public class GithubService(ILogger<GithubService> logger, HttpClient httpClient)
{
    public async Task<List<GithubProject>?> GetFavoriteProjects(string user)
    {
        var repositories = await GetFavoriteRepositories(user);
        if (!repositories.Any()) return [];

        var tasks = repositories.Select(GetProjectAsync);

        var projects = await Task.WhenAll(tasks);
        return projects.OfType<GithubProject>().ToList();
    }

    private async Task<GithubProject?> GetProjectAsync(string repo)
    {
        if (string.IsNullOrEmpty(repo))
        {
            logger.LogWarning("{Method}: invalid parameter {Param} with value: {Value}", nameof(GetProjectAsync), nameof(repo), repo);
            return null;
        }

        var url = $"https://api.github.com/repos{repo}";
        GithubProject? project = null;
        try
        {
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var dto = await response.Content.ReadFromJsonAsync<GithubProjectDto>();
                if (dto is not null)
                    project = dto.ToGithubProject();
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
        }

        project ??= BuildMinimalProject(repo);

        var metadata = await GetPortfolioMetadataAsync(project.FullName) ?? PortfolioFallbacks.Default;
        ApplyMetadata(project, metadata);
        return project;
    }

    private async Task<PortfolioMetadataDto?> GetPortfolioMetadataAsync(string fullName)
    {
        var url = $"https://raw.githubusercontent.com/{fullName}/HEAD/portfolio.json";
        try
        {
            var response = await httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<PortfolioMetadataDto>();
        }
        catch (Exception e)
        {
            logger.LogWarning(e, "{Method}: failed to fetch portfolio.json for {FullName}", nameof(GetPortfolioMetadataAsync), fullName);
            return null;
        }
    }

    private static GithubProject BuildMinimalProject(string repoPath)
    {
        var fullName = repoPath.TrimStart('/');
        var name = fullName.Contains('/') ? fullName.Split('/')[1] : fullName;
        return new GithubProject
        {
            Name = name,
            FullName = fullName,
            GithubUrl = $"https://github.com/{fullName}",
        };
    }

    private static void ApplyMetadata(GithubProject project, PortfolioMetadataDto metadata)
    {
        if (!string.IsNullOrEmpty(metadata.Title))
            project.Title = metadata.Title;
        if (!string.IsNullOrEmpty(metadata.Impact))
            project.Impact = metadata.Impact;
        if (metadata.TechStack?.Count > 0)
            project.TechStack = metadata.TechStack;
        if (!string.IsNullOrEmpty(metadata.Diff?.Before))
            project.DiffBefore = metadata.Diff.Before;
        if (metadata.Diff?.Added?.Count > 0)
            project.DiffAdded = metadata.Diff.Added;
    }

    private async Task<List<string>> GetFavoriteRepositories(string user)
    {
        var repositories = new List<string>();
        try
        {
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
        }
        catch (Exception e)
        {
            logger.LogError(e, "{Method}: failed to fetch pinned repositories for {User}", nameof(GetFavoriteRepositories), user);
        }

        return repositories;
    }
}
