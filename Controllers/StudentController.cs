
using Microsoft.AspNetCore.Mvc;
using Services;
using Repositories;
using Dtos;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController(
        IQuizRepository quizRepo,
        IQuizGrader grader) : ControllerBase
    {

        [HttpGet("quizzes")]
        public IActionResult GetActiveQuizzes()
        {
            var quizzes = quizRepo.GetAllQuizzes().Where(q => q.IsActive).ToList();
            return Ok(quizzes);
        }

        [HttpGet("quizzes/{id}")]
        public IActionResult GetQuizById(int id)
        {
            var quiz = quizRepo.GetQuizById(id);
            if (quiz == null || !quiz.IsActive)
                return NotFound();
            return Ok(quiz);
        }

        [HttpPost("quizzes/{id}/submit")]
        public IActionResult SubmitAnswers(int id, [FromBody] QuizSubmissionDto submission)
        {
            var quiz = quizRepo.GetQuizById(id);
            if (quiz == null || !quiz.IsActive)
                return NotFound("Quiz not found or inactive.");

            var result = grader.Evaluate(quiz, submission.StudentName, submission.Answers);
            return Ok(result);
        }


        [HttpGet("results/{studentName}")]
        public IActionResult GetStudentResults(string studentName)
        {
            var results = grader.GetResultsByStudent(studentName);
            var dto = results.Select(r => new QuizResultDto
            {
                QuizId = r.QuizId,
                StudentName = r.StudentName,
                Score = r.Score,
                SubmittedAt = r.SubmittedAt
            }).ToList();

            return Ok(dto);
        }

    }
}
