namespace allandeba.dev.br.Api.Endpoints.Github.Dto;

public sealed class PortfolioMetadataDto
{
    public string? Title { get; set; }
    public string? Impact { get; set; }
    public List<string>? TechStack { get; set; }
    public PortfolioDiffDto? Diff { get; set; }
}

public sealed class PortfolioDiffDto
{
    public string? Before { get; set; }
    public List<string>? Added { get; set; }
}
