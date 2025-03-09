namespace allandeba.dev.br.Api.Models.Environments;

public class EvolutionApiOptions
{
    public string? ApiUrl { get; init; }
    public string? ApiKey { get; init; }
    public string? ApiInstance { get; init; }
    public string? SendToNumber { get; init; }

    public bool IsInvalid =>
        string.IsNullOrEmpty(ApiUrl) ||
        string.IsNullOrEmpty(ApiKey) ||
        string.IsNullOrEmpty(ApiInstance) ||
        string.IsNullOrEmpty(SendToNumber);
}