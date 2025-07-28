using Data;
using Models;

namespace Repositories;

public class QuizResultRepository(ILogger<QuizResultRepository> logger) : IQuizResultRepository
{
    public void SaveResult(QuizResult result)
    {
        result.Id = Datastorage.GetNextQuizzResultId();
        Datastorage.QuizResults.Add(result);
        logger.LogInformation($"Quiz result saved: {result.StudentName}, Score: {result.Score}");
    }

    public List<QuizResult> GetResultsByStudent(string studentName)
    {
        logger .LogInformation($"Fetching results for student: {studentName}");
        if (string.IsNullOrWhiteSpace(studentName))
        {
            logger.LogWarning("Student name is null or empty");
            return new List<QuizResult>();
        }

        return Datastorage.QuizResults.Where(r => r.StudentName == studentName).ToList();
    }
}
