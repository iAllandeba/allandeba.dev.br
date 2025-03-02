using Microsoft.AspNetCore.Identity;

namespace allandeba.dev.br.Api.Data.Entities;

public class Users : IdentityUser<Guid>
{
    public List<IdentityRole<Guid>>? Roles { get; set; }
}