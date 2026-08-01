using System.Text.Json;
using allandeba.dev.br.Core.Models.Account;
using allandeba.dev.br.Core.Requests.Account;
using allandeba.dev.br.Core.Responses;
using allandeba.dev.br.Core.Responses.Account;
using allandeba.dev.br.Core.Responses.Github;
using allandeba.dev.br.Web.Serialization;

namespace allandeba.dev.br.Tests.Serialization;

/// <summary>
/// The Web project deserializes with the source-generated <see cref="AppJsonContext"/>, while the
/// API serializes with reflection under the minimal-API defaults. Nothing in the compiler checks
/// that those two agree, and a mismatch fails silently: fields land as null and IsSuccess reports
/// success for a failed request. These tests pin the contract from the client side.
/// </summary>
public class AppJsonContextTests
{
    // What the API actually puts on the wire. Minimal APIs serialize with
    // JsonSerializerDefaults.Web, so every property is camelCase.
    private const string FailedLoginPayload =
        """{"_code":404,"data":null,"message":"Ocorreu um erro ao efetuar o login","details":null}""";

    private const string SuccessPayload =
        """{"_code":200,"data":null,"message":null,"details":null}""";

    [Fact]
    public void Response_ReportsFailure_ForNonSuccessCode()
    {
        var result = JsonSerializer.Deserialize(FailedLoginPayload, AppJsonContext.Default.AccountResponseResult)!;

        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.Code);
    }

    [Fact]
    public void Response_ReportsSuccess_ForSuccessCode()
    {
        var result = JsonSerializer.Deserialize(SuccessPayload, AppJsonContext.Default.AccountResponseResult)!;

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Response_ReadsMessage_FromCamelCasePayload()
    {
        var result = JsonSerializer.Deserialize(FailedLoginPayload, AppJsonContext.Default.AccountResponseResult)!;

        Assert.Equal("Ocorreu um erro ao efetuar o login", result.Message);
    }

    [Fact]
    public void Response_DefaultsToSuccess_WhenCodeIsAbsent()
    {
        // A cached client build talking to an older API must not read a missing _code as failure.
        var result = JsonSerializer.Deserialize(
            """{"data":null,"message":null,"details":null}""",
            AppJsonContext.Default.AccountResponseResult)!;

        Assert.Equal(200, result.Code);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Response_SurvivesRoundTripThroughTheApiSerializer()
    {
        // Serialize exactly as the API does, deserialize exactly as the Web client does.
        var apiOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        var payload = JsonSerializer.Serialize(
            new Response<AccountResponse>(null, 404, "erro", "detalhe"), apiOptions);

        var result = JsonSerializer.Deserialize(payload, AppJsonContext.Default.AccountResponseResult)!;

        Assert.False(result.IsSuccess);
        Assert.Equal("erro", result.Message);
        Assert.Equal("detalhe", result.Details);
    }

    [Fact]
    public void User_ReadsEmailAndClaims_FromIdentityEndpointPayload()
    {
        var user = JsonSerializer.Deserialize(
            """{"email":"allan@example.com","isEmailConfirmed":true,"claims":{"sub":"1"}}""",
            AppJsonContext.Default.User)!;

        Assert.Equal("allan@example.com", user.Email);
        Assert.True(user.IsEmailConfirmed);
        Assert.Equal("1", user.Claims["sub"]);
    }

    [Fact]
    public void RoleClaims_ReadTypeAndValue_SoAuthorizationClaimsAreNotDropped()
    {
        // CookieAuthenticationStateProvider filters out roles with an empty Type or Value,
        // so a naming mismatch here silently removes every role from the principal.
        var roles = JsonSerializer.Deserialize(
            """[{"issuer":"LOCAL","originalIssuer":"LOCAL","type":"role","value":"admin","valueType":"string"}]""",
            AppJsonContext.Default.RoleClaimArray)!;

        var role = Assert.Single(roles);
        Assert.Equal("role", role.Type);
        Assert.Equal("admin", role.Value);
        Assert.Equal("LOCAL", role.Issuer);
    }

    [Fact]
    public void GithubProjects_ReadNestedCollection()
    {
        var apiOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        var payload = JsonSerializer.Serialize(
            new Response<GithubProjectResponse>
            {
                Data = new GithubProjectResponse
                {
                    GithubProjects =
                    [
                        new() { Name = "repo", Title = "Repo", TechStack = ["C#", "Blazor"] }
                    ]
                }
            },
            apiOptions);

        var result = JsonSerializer.Deserialize(payload, AppJsonContext.Default.GithubProjectResult)!;

        Assert.True(result.IsSuccess);
        var project = Assert.Single(result.Data!.GithubProjects!);
        Assert.Equal("repo", project.Name);
        Assert.Equal(["C#", "Blazor"], project.TechStack);
    }

    [Theory]
    [InlineData("email")]
    [InlineData("password")]
    public void LoginRequest_SerializesCamelCase_SoTheApiBindsIt(string expectedProperty)
    {
        var json = JsonSerializer.Serialize(
            new LoginRequest { Email = "a@b.com", Password = "secret" },
            AppJsonContext.Default.LoginRequest);

        Assert.Contains($"\"{expectedProperty}\":", json);
    }

    [Fact]
    public void RegisterRequest_RoundTripsThroughTheApiDeserializer()
    {
        var json = JsonSerializer.Serialize(
            new RegisterRequest { Email = "a@b.com", Password = "secret" },
            AppJsonContext.Default.RegisterRequest);

        var apiOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        var bound = JsonSerializer.Deserialize<RegisterRequest>(json, apiOptions)!;

        Assert.Equal("a@b.com", bound.Email);
        Assert.Equal("secret", bound.Password);
    }
}
