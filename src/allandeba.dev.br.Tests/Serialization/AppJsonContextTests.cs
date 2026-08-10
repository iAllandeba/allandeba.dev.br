using System.Text.Json;
using allandeba.dev.br.Core.Models.Account;
using allandeba.dev.br.Core.Requests.Account;
using allandeba.dev.br.Core.Responses;
using allandeba.dev.br.Core.Responses.Account;
using allandeba.dev.br.Core.Responses.Github;

namespace allandeba.dev.br.Tests.Serialization;

/// <summary>
/// Validates the JSON contract shared between the Web client and the API.
/// Serialization and deserialization use the default Web JSON options with
/// reflection because trimming and AOT are not enabled in this project.
/// </summary>
public class AppJsonContextTests
{
    private static readonly JsonSerializerOptions JsonOptions =
        new(JsonSerializerDefaults.Web);

    private const string FailedLoginPayload =
        """{"_code":404,"data":null,"message":"Ocorreu um erro ao efetuar o login","details":null}""";

    private const string SuccessPayload =
        """{"_code":200,"data":null,"message":null,"details":null}""";

    [Fact]
    public void Response_ReportsFailure_ForNonSuccessCode()
    {
        var result = JsonSerializer.Deserialize<Response<AccountResponse>>(
            FailedLoginPayload,
            JsonOptions)!;

        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.Code);
    }

    [Fact]
    public void Response_ReportsSuccess_ForSuccessCode()
    {
        var result = JsonSerializer.Deserialize<Response<AccountResponse>>(
            SuccessPayload,
            JsonOptions)!;

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Response_ReadsMessage_FromCamelCasePayload()
    {
        var result = JsonSerializer.Deserialize<Response<AccountResponse>>(
            FailedLoginPayload,
            JsonOptions)!;

        Assert.Equal("Ocorreu um erro ao efetuar o login", result.Message);
    }

    [Fact]
    public void Response_DefaultsToSuccess_WhenCodeIsAbsent()
    {
        var result = JsonSerializer.Deserialize<Response<AccountResponse>>(
            """{"data":null,"message":null,"details":null}""",
            JsonOptions)!;

        Assert.Equal(200, result.Code);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Response_SurvivesRoundTripThroughTheApiSerializer()
    {
        var payload = JsonSerializer.Serialize(
            new Response<AccountResponse>(null, 404, "erro", "detalhe"),
            JsonOptions);

        var result = JsonSerializer.Deserialize<Response<AccountResponse>>(
            payload,
            JsonOptions)!;

        Assert.False(result.IsSuccess);
        Assert.Equal("erro", result.Message);
        Assert.Equal("detalhe", result.Details);
    }

    [Fact]
    public void User_ReadsEmailAndClaims_FromIdentityEndpointPayload()
    {
        var user = JsonSerializer.Deserialize<User>(
            """{"email":"test@example.com","isEmailConfirmed":true,"claims":{"sub":"1"}}""",
            JsonOptions)!;

        Assert.Equal("test@example.com", user.Email);
        Assert.True(user.IsEmailConfirmed);
        Assert.Equal("1", user.Claims["sub"]);
    }

    [Fact]
    public void RoleClaims_ReadTypeAndValue_SoAuthorizationClaimsAreNotDropped()
    {
        var roles = JsonSerializer.Deserialize<RoleClaim[]>(
            """[{"issuer":"LOCAL","originalIssuer":"LOCAL","type":"role","value":"admin","valueType":"string"}]""",
            JsonOptions)!;

        var role = Assert.Single(roles);

        Assert.Equal("role", role.Type);
        Assert.Equal("admin", role.Value);
        Assert.Equal("LOCAL", role.Issuer);
    }

    [Fact]
    public void Response_WritesCodeAsUnderscoreCode_SoTheApiStillReadsIt()
    {
        var json = JsonSerializer.Serialize(
            new Response<AccountResponse>(null, 404, "erro"),
            JsonOptions);

        Assert.Contains("\"_code\":404", json);
        Assert.DoesNotContain("\"code\":", json);
    }

    [Fact]
    public void Response_OmitsIsSuccess_BecauseItIsDerived()
    {
        var json = JsonSerializer.Serialize(
            new Response<AccountResponse>(),
            JsonOptions);

        Assert.DoesNotContain("isSuccess", json, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void GithubProjects_ReportFailure_ForTheApiErrorPayload()
    {
        var result = JsonSerializer.Deserialize<Response<GithubProjectResponse>>(
            """{"_code":400,"data":null,"message":"Usuário do github inválido","details":null}""",
            JsonOptions)!;

        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.Code);
        Assert.Equal("Usuário do github inválido", result.Message);
        Assert.Null(result.Data);
    }

    [Fact]
    public void GithubProjects_ReadNestedCollection()
    {
        var payload = JsonSerializer.Serialize(
            new Response<GithubProjectResponse>
            {
                Data = new GithubProjectResponse
                {
                    GithubProjects =
                    [
                        new()
                        {
                            Name = "repo",
                            Title = "Repo",
                            TechStack = ["C#", "Blazor"]
                        }
                    ]
                }
            },
            JsonOptions);

        var result = JsonSerializer.Deserialize<Response<GithubProjectResponse>>(
            payload,
            JsonOptions)!;

        Assert.True(result.IsSuccess);

        var project = Assert.Single(result.Data!.GithubProjects!);

        Assert.Equal("repo", project.Name);
        Assert.Equal(["C#", "Blazor"], project.TechStack);
    }

    [Theory]
    [InlineData("email")]
    [InlineData("password")]
    public void LoginRequest_SerializesCamelCase_SoTheApiBindsIt(
        string expectedProperty)
    {
        var json = JsonSerializer.Serialize(
            new LoginRequest
            {
                Email = "a@b.com",
                Password = "secret"
            },
            JsonOptions);

        Assert.Contains($"\"{expectedProperty}\":", json);
    }

    [Fact]
    public void RegisterRequest_RoundTripsThroughTheApiDeserializer()
    {
        var json = JsonSerializer.Serialize(
            new RegisterRequest
            {
                Email = "a@b.com",
                Password = "secret"
            },
            JsonOptions);

        var bound = JsonSerializer.Deserialize<RegisterRequest>(
            json,
            JsonOptions)!;

        Assert.Equal("a@b.com", bound.Email);
        Assert.Equal("secret", bound.Password);
    }
}