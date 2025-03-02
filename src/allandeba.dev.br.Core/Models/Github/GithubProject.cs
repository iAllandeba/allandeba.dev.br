using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;

namespace allandeba.dev.br.Core.Models.Github;

public class GithubProject
{
    public string Name { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string GithubUrl { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    [JsonIgnore] public MarkupString MarkDown => new MarkupString(Description);
    [JsonIgnore] public bool HasWebsite => !string.IsNullOrEmpty(Website);
}