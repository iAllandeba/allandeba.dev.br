using Microsoft.AspNetCore.Identity;

namespace allandeba.dev.br.Api.Extensions;

public static class IdentityParsing
{
    public static string ToText(this IEnumerable<IdentityError> errors) =>
        string.Join(", ", errors.Select(x => x.Code).ToArray());
}