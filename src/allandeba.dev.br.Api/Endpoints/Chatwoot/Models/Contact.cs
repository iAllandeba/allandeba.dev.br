namespace allandeba.dev.br.Api.Endpoints.Chatwoot.Models;

public class Contact
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Avatar { get; set; }
    public string? Type { get; set; }
    public Account? Account { get; set; }
}