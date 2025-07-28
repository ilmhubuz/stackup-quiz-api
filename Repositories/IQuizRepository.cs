using Models;

namespace Repositories;

public interface IQuizRepository
{
    void AddQuiz(QuizCreateDto quizDto);
    void UpdateQuiz(int quizId, QuizCreateDto quizDto);
    List<Quizz> GetAllQuizzes();
    Quizz? GetQuizById(int id);
    bool AddQuestionToQuiz(int quizId, QuestionCreateDto questionDto);
    bool DeleteQuiz(int id);
    bool ToggleQuizStatus(int id);
}
