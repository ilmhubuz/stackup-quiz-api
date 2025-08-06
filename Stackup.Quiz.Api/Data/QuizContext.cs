using Microsoft.EntityFrameworkCore;

namespace Stackup.Quiz.Api.Data;

using Stackup.Quiz.Api.Entities;

public class QuizContext(DbContextOptions<QuizContext> options) : DbContext(options)
{
    public DbSet<Quiz> Quizzes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(QuizContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}