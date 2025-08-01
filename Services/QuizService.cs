using AutoMapper;
using stackup_quiz_api.Abstraction;
using stackup_quiz_api.Exceptions;
using stackup_quiz_api.Models;

namespace stackup_quiz_api.Services;

public class QuizService(IMapper mapper) : IQuizService
{
    private readonly Dictionary<string, Quiz> quizes = [];
    private IEnumerable<Quiz> existingQuizes => quizes.Values.Where(q => q.State is not QuizState.Deleted);
    private int IdIndex = 1;
    public ValueTask<Quiz> CreateQuizAsync(CreateQuiz quiz, CancellationToken cancellationToken = default)
    {
        if (quizes.ContainsKey(quiz.Title))
            throw new CustomConflictException($"Quiz title with '{quiz.Title}' already exists");
        var newQuiz = mapper.Map<Quiz>(quiz);
        newQuiz.Id = IdIndex++;
        quizes.Add(quiz.Title, newQuiz);
        return ValueTask.FromResult(newQuiz);
    }

    public ValueTask<IEnumerable<Quiz>> GetAllAsync(CancellationToken cancellationToken = default)
        => ValueTask.FromResult(existingQuizes);

    public ValueTask<Quiz?> GetSingleOrDefaultAsync(int id, CancellationToken cancellationToken = default)
        => ValueTask.FromResult(existingQuizes.FirstOrDefault(q => q.Id == id));

        
    public async ValueTask<Quiz> GetSingleAsync(int id, CancellationToken cancellationToken = default)
        => await GetSingleOrDefaultAsync(id, cancellationToken) ?? throw new CustomNotFoundException($"Quiz with id '{id}' not found");


    public async ValueTask DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var quiz = await GetSingleAsync(id, cancellationToken);
        quizes[quiz.Title] = quiz with { State = QuizState.Deleted };
    }


    public async ValueTask<Quiz> UpdateAsync(int id, UpdateQuiz quiz, CancellationToken cancellationToken = default)
    {
        var quizUpdate = await GetSingleAsync(id, cancellationToken);
        mapper.Map(quiz, quizUpdate);

        return quizUpdate;
    }
}