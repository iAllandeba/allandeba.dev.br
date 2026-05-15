using allandeba.dev.br.Api.Endpoints.Github.Dto;

namespace allandeba.dev.br.Api.Services;

internal static class PortfolioFallbacks
{
    internal static readonly PortfolioMetadataDto Default = new()
    {
        Impact = "↑ Projeto em desenvolvimento ativo",
        TechStack = ["..."],
        Diff = new PortfolioDiffDto
        {
            Before = "sem portfolio.json configurado",
            Added = ["adicione portfolio.json na raiz do repositório"]
        }
    };
}
