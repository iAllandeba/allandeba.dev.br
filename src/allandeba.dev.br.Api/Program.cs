using allandeba.dev.br.Api;
using allandeba.dev.br.Api.Common.Api;
using allandeba.dev.br.Api.Endpoints;
using Deba.Caching;
using Deba.Caching.Models;

var builder = WebApplication.CreateBuilder(args);
builder.AddAppSettings();
builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();
builder.Services.AddDebaCaching(ECachingType.MemoryCache);

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.UseCors(ApiConfiguration.CorsPolicyName);
app.UseSecurity();
app.MapEndpoints();

app.Run();