using AutoMapper;
using stackup_quiz_api.Abstraction;
using stackup_quiz_api.Models;
using stackup_quiz_api.Repositories.Abstraction;


namespace stackup_quiz_api.Services;

public class QuizService(
    IQuizRepository repository,
    IMapper mapper) : IQuizService
{
    public async ValueTask<Quiz> CreateQuizAsync(CreateQuiz quiz, CancellationToken cancellationToken = default)
    {
        var entity = await repository.InsertAsync(mapper.Map<Entities.Quiz>(quiz), cancellationToken);
        return mapper.Map<Quiz>(entity);
    }

    public async ValueTask<IEnumerable<Quiz>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await repository.GetAllAsync(cancellationToken);
        return entities.Select(mapper.Map<Quiz>);
    }

    public async ValueTask<Quiz?> GetSingleOrDefaultAsync(int id, CancellationToken cancellationToken = default)
        => mapper.Map<Quiz>(await repository.GetSingleOrDefaultAsync(id, cancellationToken));


    public async ValueTask<Quiz> GetSingleAsync(int id, CancellationToken cancellationToken = default)
        => mapper.Map<Quiz>(await repository.GetSingleAsync(id, cancellationToken));

    public ValueTask DeleteAsync(int id, CancellationToken cancellationToken = default)
        => repository.DeleteAsync(id, cancellationToken);


    public async ValueTask<Quiz> UpdateAsync(int id, UpdateQuiz quiz, CancellationToken cancellationToken = default)
        => mapper.Map<Quiz>(await repository.UpdateAsync(id, mapper.Map<Entities.Quiz>(quiz), cancellationToken));
    public ValueTask<bool> ExistAsync(string title, CancellationToken cancellationToken = default)
        => repository.ExistsAsync(title, cancellationToken);
}