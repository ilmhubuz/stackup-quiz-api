using Dtos;
using Models;
using Repositories;

namespace Services;

public class QuizGrader(IQuizResultRepository resultRepo) : IQuizGrader
{

    public QuizResult Evaluate(Quizz quiz, string studentName, List<AnswerSubmissionDto> answers)
    {
        double totalScore = 0;

        foreach (var question in quiz.Questions)
        {
            var userAnswer = answers.FirstOrDefault(a => a.QuestionId == question.Id);
            if (userAnswer != null)
            {
                totalScore += question.CheckAnswer(userAnswer.Answer, userAnswer.ElapsedSeconds);
            }
        }

        var result = new QuizResult
        {
            QuizId = quiz.Id,
            StudentName = studentName,
            Score = totalScore,
            SubmittedAt = DateTime.Now,
            SubmittedAnswers = answers
        };

        resultRepo.SaveResult(result);
        return result;
    }

    public List<QuizResult> GetResultsByStudent(string studentName)
    {
        return resultRepo.GetResultsByStudent(studentName);
    }

}
