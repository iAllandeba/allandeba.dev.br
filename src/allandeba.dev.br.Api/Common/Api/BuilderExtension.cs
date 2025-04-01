using System.Reflection;
using allandeba.dev.br.Api.Data;
using allandeba.dev.br.Api.Handlers;
using allandeba.dev.br.Api.Data.Entities;
using allandeba.dev.br.Api.Services;
using allandeba.dev.br.Core;
using allandeba.dev.br.Core.Handlers;
using Deba.Caching;
using Deba.Caching.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace allandeba.dev.br.Api.Common.Api;

public static class BuilderExtension
{
    public static void AddOptions(this WebApplicationBuilder builder)
    {

    }
    
    public static void AddHttpSettings(this WebApplicationBuilder builder)
    {
        // builder.Services.AddHttpClient<Interface, Concret>((serviceProvider, httpClient) =>
        // {
        //     var options = serviceProvider.GetRequiredService<IOptions<OptionsInAppSettings>>().Value;
        //     
        //     httpClient.BaseAddress = new Uri(options.Url ?? string.Empty);
        //     httpClient.DefaultRequestHeaders.Add("ApiKey", options.ApiKey);
        //     httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, UserAgents.Get());
        // });
    }
    
    public static void AddAppSettings(this WebApplicationBuilder builder)
    {
        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddUserSecrets(Assembly.GetExecutingAssembly(), true);
    }
    
    public static void AddConfiguration(
        this WebApplicationBuilder builder)
    {
        Configuration.ConnectionString =
            builder
                .Configuration
                .GetConnectionString("DefaultConnection")
            ?? string.Empty;
        
        Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
        Configuration.FrontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
    }

    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x => { x.CustomSchemaIds(n => n.FullName); });
    }

    public static void AddSecurity(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddIdentityCookies();

        builder.Services.AddAuthorization();
    }

    public static void AddDataContexts(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddDbContext<AppDbContext>(
                x => { x.UseNpgsql(Configuration.ConnectionString); });
        builder.Services
            .AddIdentityCore<Users>()
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddApiEndpoints();
    }

    public static void AddCrossOrigin(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(
            options => options.AddPolicy(
                ApiConfiguration.CorsPolicyName,
                policy => policy
                    .WithOrigins([
                        Configuration.BackendUrl,
                        Configuration.FrontendUrl,
                    ])
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            ));
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IGithubHandler, GithubHandler>();
        builder.Services.AddTransient<GithubService>();
        builder.Services.AddTransient<IAccountHandler, AccountHandler>();
    }

    public static void AddPackages(this WebApplicationBuilder builder)
    {
        builder.Services.AddDebaCaching(ECachingType.MemoryCache);
    }
}