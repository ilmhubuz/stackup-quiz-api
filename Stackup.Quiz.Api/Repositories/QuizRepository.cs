namespace Stackup.Quiz.Api.Repositories;

using Stackup.Quiz.Api.Repositories.Abstractions;
using Stackup.Quiz.Api.Entities;
using Stackup.Quiz.Api.Exceptions;
using Stackup.Quiz.Api.Data;
using Npgsql;
using Microsoft.EntityFrameworkCore;

public class QuizRepository(IQuizContext context) : IQuizRepository
{
    public async ValueTask DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var effectedRows = await context.Quizzes
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
        if (effectedRows < 1)
            throw new CustomNotFoundException($"Quiz with id {id} not found");
    }

    public async ValueTask<bool> ExistsAsync(string title, CancellationToken cancellationToken = default)
        => await context.Quizzes.AnyAsync(x => x.Title == title && x.State != QuizState.Deleted, cancellationToken);

    public async ValueTask<IEnumerable<Quiz>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Quizzes.Where(x => x.State != QuizState.Deleted).ToListAsync(cancellationToken);

    public async ValueTask<Quiz> GetSingleAsync(int id, CancellationToken cancellationToken = default)
        => await GetSingleOrDefaultAsync(id, cancellationToken)
            ?? throw new CustomNotFoundException($"Quiz with id '{id}' not found!");

    public async ValueTask<Quiz?> GetSingleOrDefaultAsync(int id, CancellationToken cancellationToken = default)
         => await context.Quizzes
        .Where(q => q.Id == id && q.State != QuizState.Deleted)
        .FirstOrDefaultAsync(cancellationToken);
        //=> await context.Quizzes.FindAsync([ id ], cancellationToken);

    public async ValueTask<Quiz> InsertAsync(Quiz quiz, CancellationToken cancellationToken = default)
    {
        try
        {
            var entry = context.Quizzes.Add(quiz);
            await context.SaveChangesAsync(cancellationToken);

            return entry.Entity;
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: "23505" }) // Unique violation
        {
            throw new CustomConflictException("Title must be unique.");
        }
    }

    public async ValueTask<Quiz> UpdateAsync(Quiz quiz, CancellationToken cancellationToken = default)
    {
        try
        {
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: "23505" }) // Unique violation
        {
            throw new CustomConflictException("Title must be unique.");
        }

        return quiz;
    }
}