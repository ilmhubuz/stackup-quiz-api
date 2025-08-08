using Microsoft.EntityFrameworkCore;
namespace Stackup.Quiz.Api.Data;

public class AutoApplyMigrationsService(
    ILogger<AutoApplyMigrationsService> logger,
    IConfiguration configuration,
    IServiceScopeFactory scopeFactory
    ) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        if(configuration.GetValue("MigrateDatabase", false))
        {
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IQuizContext>();

            logger.LogInformation("Attempting to migrate database...");
            await context.Database.MigrateAsync(cancellationToken);
            logger.LogInformation("{service} completed successfully.", nameof(AutoApplyMigrationsService));
        }    
    }
}