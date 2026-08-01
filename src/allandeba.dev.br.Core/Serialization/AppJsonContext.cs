using System.Text.Json;
using System.Text.Json.Serialization;
using allandeba.dev.br.Core.Models.Account;
using allandeba.dev.br.Core.Requests.Account;
using allandeba.dev.br.Core.Responses;
using allandeba.dev.br.Core.Responses.Account;
using allandeba.dev.br.Core.Responses.Github;

namespace allandeba.dev.br.Core.Serialization;

// Lives in Core, next to the contracts it describes, so both sides of the wire can share it
// and the tests can pin it without referencing the Blazor app.
// JsonSerializerDefaults.Web mirrors what the API serializes with (camelCase, case-insensitive)
// and what the HttpClient JSON extensions used before this context existed. Without it the
// generated contract is PascalCase and case-sensitive, so every response field reads back null.
[JsonSourceGenerationOptions(JsonSerializerDefaults.Web)]
[JsonSerializable(typeof(LoginRequest))]
[JsonSerializable(typeof(RegisterRequest))]
[JsonSerializable(typeof(Response<AccountResponse>), TypeInfoPropertyName = "AccountResponseResult")]
[JsonSerializable(typeof(Response<GithubProjectResponse>), TypeInfoPropertyName = "GithubProjectResult")]
[JsonSerializable(typeof(User))]
[JsonSerializable(typeof(RoleClaim[]))]
public partial class AppJsonContext : JsonSerializerContext;
