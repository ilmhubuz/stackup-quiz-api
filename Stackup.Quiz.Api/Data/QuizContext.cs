using Microsoft.EntityFrameworkCore;

namespace Stackup.Quiz.Api.Data;

using Stackup.Quiz.Api.Entities;

public class QuizContext(DbContextOptions<QuizContext> options) 
: DbContext(options), IQuizContext 
{
    public DbSet<Quiz> Quizzes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(QuizContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyTimeStampChnages();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyTimeStampChnages()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if(entry is { State: EntityState.Added, Entity: IHasTimeStamp added })
            {
                added.CreatedAt = DateTimeOffset.UtcNow;
                added.UpdatedAt = DateTimeOffset.UtcNow;
            }
            else if (entry is { State: EntityState.Modified, Entity: IHasTimeStamp updated })
                updated.UpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}