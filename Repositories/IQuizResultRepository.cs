using Models;

namespace Repositories;

public interface IQuizResultRepository
{
    void SaveResult(QuizResult result);
    List<QuizResult> GetResultsByStudent(string studentName);
}
