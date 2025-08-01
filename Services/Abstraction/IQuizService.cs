using stackup_quiz_api.Models;

namespace stackup_quiz_api.Abstraction;

public interface IQuizService
{
    ValueTask<Quiz> CreateQuizAsync(CreateQuiz quiz,CancellationToken cancellationToken);
    ValueTask<IEnumerable<Quiz>> GetAllAsync(CancellationToken cancellationToken=default);
    ValueTask<Quiz?> GetSingleOrDefaultAsync(int id, CancellationToken cancellationToken=default);
    ValueTask<Quiz> GetSingleAsync(int id, CancellationToken cancellationToken=default);
    ValueTask DeleteAsync(int id, CancellationToken cancellationToken=default);
    ValueTask<Quiz> UpdateAsync(int id,UpdateQuiz quiz, CancellationToken cancellationToken=default);
}