using allandeba.dev.br.Api;
using allandeba.dev.br.Api.Common.Api;
using allandeba.dev.br.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.AddAppSettings();
builder.AddOptions();
builder.AddConfiguration();
builder.AddServices();
builder.AddHttpSettings();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddPackages();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.ConfigureDevEnvironment();
    app.ApplyMigrations();
}

app.UseCors(ApiConfiguration.CorsPolicyName);
app.UseSecurity();
app.MapEndpoints();

app.Run();