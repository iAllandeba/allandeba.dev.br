namespace allandeba.dev.br.Core.Requests.Github;

public class GetGithubProjectRequest : Request
{
    public string User { get; set; } = string.Empty;
}