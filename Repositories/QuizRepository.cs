using stackup_quiz_api.Entities;
using stackup_quiz_api.Exceptions;
using stackup_quiz_api.Repositories.Abstraction;

namespace stackup_quiz_api.Repositories;

public class QuizRepository : IQuizRepository
{
    private Dictionary<string, Quiz> quizes = [];
    private int index = 1;
    public async ValueTask DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var quiz = await GetSingleAsync(id, cancellationToken)
            ?? throw new CustomNotFoundException("Quiz doesn't exist");
        quiz.State = QuizState.Deleted;
    }

    public ValueTask<bool> ExistsAsync(string title, CancellationToken cancellationToken = default)
        => ValueTask.FromResult(quizes.ContainsKey(title));

    public ValueTask<IEnumerable<Quiz>> GetAllAsync(CancellationToken cancellationToken = default)
        => ValueTask.FromResult(quizes.Values.Where(q => q.State != QuizState.Deleted));

    public async ValueTask<Quiz> GetSingleAsync(int id, CancellationToken cancellationToken = default)
        => await GetSingleOrDefaultAsync(id, cancellationToken)
            ?? throw new CustomNotFoundException("Quiz doesn't exist");

    public ValueTask<Quiz?> GetSingleOrDefaultAsync(int id, CancellationToken cancellationToken = default)
        => ValueTask.FromResult(quizes.Values.FirstOrDefault(q => q.Id == id && q.State != QuizState.Deleted));

    public ValueTask<Quiz> InsertAsync(Quiz quiz, CancellationToken cancellationToken = default)
    {
        if (quizes.TryAdd(quiz.Title, quiz))
        {
            quiz.Id = index++;
            return ValueTask.FromResult(quiz);
        }
        throw new CustomConflictException("Title must be unique");
    }

    public async ValueTask<Quiz> UpdateAsync(int id, Quiz quiz, CancellationToken cancellationToken = default)
    {
        var found = await GetSingleAsync(id, cancellationToken);
        quiz.Id = found.Id;
        if (found.Title != quiz.Title)
        {
            if (quizes.TryAdd(quiz.Title, quiz))
                quizes.Remove(found.Title);
            else
                throw new CustomConflictException("Title must be unique");
        }
        else
            quizes[quiz.Title] = quiz;

        return quiz;
    }
}