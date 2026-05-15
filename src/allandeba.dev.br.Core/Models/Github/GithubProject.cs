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

    public string Title { get; set; } = string.Empty;
    public string Impact { get; set; } = string.Empty;
    public List<string> TechStack { get; set; } = [];
    public string DiffBefore { get; set; } = string.Empty;
    public List<string> DiffAdded { get; set; } = [];

    [JsonIgnore] public MarkupString MarkDown => new MarkupString(Description);
    [JsonIgnore] public bool HasWebsite => !string.IsNullOrEmpty(Website);
    [JsonIgnore] public string DisplayTitle => !string.IsNullOrEmpty(Title) ? Title : Name;
    [JsonIgnore] public bool HasImpact => !string.IsNullOrEmpty(Impact);
    [JsonIgnore] public bool HasTechStack => TechStack.Count > 0;
    [JsonIgnore] public string TechStackDisplay => string.Join(" · ", TechStack);
    [JsonIgnore] public bool HasDiff => !string.IsNullOrEmpty(DiffBefore);
}