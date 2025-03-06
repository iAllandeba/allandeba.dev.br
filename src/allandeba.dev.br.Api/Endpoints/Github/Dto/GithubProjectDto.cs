using allandeba.dev.br.Core.Models.Github;

namespace allandeba.dev.br.Api.Endpoints.Github.Dto;

public sealed class GithubProjectDto
{
    public string Name { get; set; } = string.Empty;
    public string Full_Name { get; set; } = string.Empty;
    public string Html_Url { get; set; } = string.Empty;
    public string Homepage { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public GithubProject ToGithubProject()
    {
        return new GithubProject
        {
            Name = this.Name,
            FullName = this.Full_Name,
            GithubUrl = this.Html_Url,
            Website = this.Homepage,
            Description = this.Description,
        };
    }
}