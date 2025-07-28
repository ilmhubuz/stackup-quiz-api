using Data;
using Models;

namespace Repositories;

public class QuizRepository(ILogger<QuizRepository> logger) : IQuizRepository
{
    public void AddQuiz(QuizCreateDto quizDto)
    {
        ValidateQuiz(quizDto);

        var quiz = new Quizz
        {
            Id = Datastorage.GetNextQuizzId(),
            Title = quizDto.Title,
            Description = quizDto.Description,
            IsActive = quizDto.IsActive,
            Questions = quizDto.Questions.Select(CreateQuestionFromDto).ToList()
        };

        Datastorage.Quizzes.Add(quiz);
        logger.LogInformation($"Quiz added: {quiz.Title}");
    }

    public void UpdateQuiz(int quizId, QuizCreateDto quizDto)
    {
        ValidateQuiz(quizDto);

        var existingQuiz = Datastorage.Quizzes.FirstOrDefault(q => q.Id == quizId);
        if (existingQuiz == null)
            throw new Exception($"Quiz with ID {quizId} not found.");

        existingQuiz.Title = quizDto.Title;
        existingQuiz.Description = quizDto.Description;
        existingQuiz.IsActive = quizDto.IsActive;

        existingQuiz.Questions = quizDto.Questions.Select(CreateQuestionFromDto).ToList();

        logger.LogInformation($"Quiz updated: {existingQuiz.Title}");
    }

    public List<Quizz> GetAllQuizzes() => Datastorage.Quizzes;

    public Quizz? GetQuizById(int id) => Datastorage.Quizzes.FirstOrDefault(q => q.Id == id);

    public bool DeleteQuiz(int id)
    {
        var quiz = GetQuizById(id);
        if (quiz == null) return false;
        Datastorage.Quizzes.Remove(quiz);
        logger.LogInformation($"Quiz deleted: {id}");
        return true;
    }

    public bool ToggleQuizStatus(int id)
    {
        var quiz = GetQuizById(id);
        if (quiz is null) return false;
        quiz.IsActive = !quiz.IsActive;
        logger.LogInformation($"Quiz status toggled: {id}, IsActive: {quiz.IsActive}");
        return true;
    }

    public bool AddQuestionToQuiz(int quizId, QuestionCreateDto questionDto)
    {
        var quiz = GetQuizById(quizId);
        if (quiz == null) return false;
        var question = CreateQuestionFromDto(questionDto);
        quiz.Questions.Add(question);
        logger.LogInformation($"Question added to quiz {quizId}: { question.Prompt}");
        return true;
    }

    private void ValidateQuiz(QuizCreateDto quiz)
    {
        if (string.IsNullOrWhiteSpace(quiz.Title))
            throw new ArgumentException("Quiz title cannot be empty.");

        if (quiz.Questions == null || !quiz.Questions.Any())
            throw new ArgumentException("Quiz must have at least one question.");

        if (quiz.Questions.Any(q => string.IsNullOrWhiteSpace(q.Prompt)))
            throw new ArgumentException("All questions must have a prompt.");
    }

    private Question CreateQuestionFromDto(QuestionCreateDto dto)
    {
        return dto.EQuestiontype switch
        {
            EQuestiontype.McqQuestion => new McqQuestion
            {
                Id = Datastorage.GetNextQuestionId(),
                EQuestiontype = dto.EQuestiontype,
                Prompt = dto.Prompt,
                TimeLimitSeconds = dto.TimeLimitSeconds,
                Options = dto.Options!,
                AnswerKey = dto.AnswerKey!
            },
            EQuestiontype.ShortAnswerQuestion => new ShortAnswerQuestion
            {
                Id = Datastorage.GetNextQuestionId(),
                EQuestiontype = dto.EQuestiontype,
                Prompt = dto.Prompt,
                TimeLimitSeconds = dto.TimeLimitSeconds,
                CorrectAnswer = dto.CorrectShortAnswer!
            },
            EQuestiontype.TrueFalseQuestion => new TrueFalseQuestion
            {
                Id = Datastorage.GetNextQuestionId(),
                EQuestiontype = dto.EQuestiontype,
                Prompt = dto.Prompt,
                TimeLimitSeconds = dto.TimeLimitSeconds,
                CorrectAnswer = (bool)dto.CorrectBoolAnswer!
            },
            _ => throw new ArgumentException("Invalid question type")
        };
    }
}
