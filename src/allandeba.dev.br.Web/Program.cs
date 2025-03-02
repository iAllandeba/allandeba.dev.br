using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using allandeba.dev.br.Web;
using allandeba.dev.br.Web.Security;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using allandeba.dev.br.Core.Handlers;
using allandeba.dev.br.Web.Handlers;
using allandeba.dev.br.Web.Handlers.Github;
using allandeba.dev.br.Web.Services;
using Deba.Caching;
using Deba.Caching.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<CookieHandler>();

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();
builder.Services.AddScoped(x =>
    (ICookieAuthenticationStateProvider)x.GetRequiredService<AuthenticationStateProvider>());
builder.Services.AddMudServices();

builder.Services
    .AddHttpClient(Configuration.HttpClientName, opt => { opt.BaseAddress = new Uri(Configuration.BackendUrl); })
    .AddHttpMessageHandler<CookieHandler>();

builder.Services.AddSingleton<EventAggregator>();
builder.Services.AddScoped<ThemeManagerService>();
builder.Services.AddDebaCaching(ECachingType.LocalStorage);

builder.Services.AddTransient<IAccountHandler, AccountHandler>();
builder.Services.AddTransient<IGithubHandler, GithubHandler>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddLocalization();
// CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
// CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-BR");


await builder.Build().RunAsync();