namespace Stackup.Quiz.Api.Repositories.Abstractions;
using Stackup.Quiz.Api.Entities;

public interface IQuizRepository
{
    ValueTask<Quiz> InsertAsync(Quiz quiz, CancellationToken cancellationToken = default);
    ValueTask<IEnumerable<Quiz>> GetAllAsync(CancellationToken cancellationToken = default);
    ValueTask<Quiz?> GetSingleOrDefaultAsync(int id, CancellationToken cancellationToken = default);
    ValueTask<Quiz> GetSingleAsync(int id, CancellationToken cancellationToken = default);
    ValueTask DeleteAsync(int id, CancellationToken cancellationToken = default);
    ValueTask<Quiz> UpdateAsync(Quiz quiz, CancellationToken cancellationToken = default);
    ValueTask<bool> ExistsAsync(string title, CancellationToken cancellationToken = default);
}