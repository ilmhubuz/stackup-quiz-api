namespace Stackup.Quiz.Api.Repositories;

using Stackup.Quiz.Api.Repositories.Abstractions;
using Stackup.Quiz.Api.Entities;
using Stackup.Quiz.Api.Exceptions;

public class QuizRepository : IQuizRepository
{
    private Dictionary<string, Quiz> quizzes = [];
    private int index = 1;

    public async ValueTask DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var quiz = await GetSingleAsync(id, cancellationToken)
            ?? throw new CustomNotFoundException("Quiz not found.");

        quiz.State = QuizState.Deleted;
    }

    public ValueTask<bool> ExistsAsync(string title, CancellationToken cancellationToken = default)
        => ValueTask.FromResult(quizzes.ContainsKey(title));

    public ValueTask<IEnumerable<Quiz>> GetAllAsync(CancellationToken cancellationToken = default)
        => ValueTask.FromResult(quizzes.Values.Where(q => q.State != QuizState.Deleted));

    public async ValueTask<Quiz> GetSingleAsync(int id, CancellationToken cancellationToken = default)
        => await GetSingleOrDefaultAsync(id, cancellationToken)
            ?? throw new CustomNotFoundException($"Quiz with id '{id}' not found!");

    public ValueTask<Quiz?> GetSingleOrDefaultAsync(int id, CancellationToken cancellationToken = default)
        => ValueTask.FromResult(quizzes.Values.FirstOrDefault(q => q.Id == id && q.State != QuizState.Deleted));

    public ValueTask<Quiz> InsertAsync(Quiz quiz, CancellationToken cancellationToken = default)
    {
        if (quizzes.TryAdd(quiz.Title, quiz))
        {
            quiz.Id = index++;
            return ValueTask.FromResult(quiz);
        }

        throw new CustomConflictException("Title musb be unique");
    }

    public async ValueTask<Quiz> UpdateAsync(int id, Quiz quiz, CancellationToken cancellationToken = default)
    {
        var found = await GetSingleAsync(id, cancellationToken);
        quiz.Id = found.Id;
        
        if (found.Title != quiz.Title)
        {
            if (quizzes.TryAdd(quiz.Title, quiz))
                quizzes.Remove(found.Title);
            else
                throw new CustomConflictException("Title must be unique.");
        }
        else
            quizzes[quiz.Title] = quiz;

        return quiz;
    }
}