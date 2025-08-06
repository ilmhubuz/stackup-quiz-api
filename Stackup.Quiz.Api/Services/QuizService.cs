using Stackup.Quiz.Api.Services.Abstractions;
using Stackup.Quiz.Api.Models;
using AutoMapper;
using Stackup.Quiz.Api.Exceptions;
using Stackup.Quiz.Api.Repositories.Abstractions;

namespace Stackup.Quiz.Api.Services;

public class QuizService(IMapper mapper, IQuizRepository repository) : IQuizService
{
    public async ValueTask<Models.Quiz> CreateQuizAsync(CreateQuiz quiz, CancellationToken cancellationToken = default)
    {
        var entity = await repository.InsertAsync(mapper.Map<Entities.Quiz>(quiz), cancellationToken);
        return mapper.Map<Models.Quiz>(entity);
    }

    public async ValueTask<IEnumerable<Models.Quiz>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var enteties = await repository.GetAllAsync(cancellationToken);
        return enteties.Select(mapper.Map<Models.Quiz>);
    }

    public async ValueTask<Models.Quiz?> GetSingleOrDefaultAsync(int id, CancellationToken cancellationToken = default)
        => mapper.Map<Models.Quiz>(await repository.GetSingleOrDefaultAsync(id, cancellationToken));

    public async ValueTask<Models.Quiz> GetSingleAsync(int id, CancellationToken cancellationToken = default)
        => mapper.Map<Models.Quiz>(await repository.GetSingleAsync(id, cancellationToken));

    public ValueTask DeleteAsync(int id, CancellationToken cancellationToken = default)
        => repository.DeleteAsync(id, cancellationToken);

    public async ValueTask<Models.Quiz> UpdateAsync(int id, UpdateQuiz quiz, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetSingleAsync(id, cancellationToken);
        mapper.Map(quiz, entity);
        return mapper.Map<Models.Quiz>(await repository.UpdateAsync(entity, cancellationToken));        
    }

    public ValueTask<bool> ExistsAsync(string title, CancellationToken cancellationToken = default)
        => repository.ExistsAsync(title, cancellationToken);
}