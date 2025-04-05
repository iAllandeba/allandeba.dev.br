using allandeba.dev.br.Api.Data;
using allandeba.dev.br.Core;
using Microsoft.EntityFrameworkCore;

namespace allandeba.dev.br.Api.Common.Api;

public static class AppExtension
{
    public static void ConfigureDevEnvironment(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapSwagger().RequireAuthorization();
    }

    public static void UseSecurity(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
    
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (!context.Database.CanConnect())
            throw new Exception($"Não foi possivel se conectar com o banco de dados!\nArquivo está configurado: {!string.IsNullOrEmpty(Configuration.ConnectionString)}");
        
        if (context.Database.GetPendingMigrations().Any())
            context.Database.Migrate();
    }
}