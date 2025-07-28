using Dtos;
using Models;

public interface IQuizGrader
{
    QuizResult Evaluate(Quizz quiz, string studentName, List<AnswerSubmissionDto> answers);
    List<QuizResult> GetResultsByStudent(string studentName);
}