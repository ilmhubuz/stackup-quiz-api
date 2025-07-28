using Extensions;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizzesController : ControllerBase
{
    private readonly ILogger<QuizRepository> _logger;
    private readonly IQuizRepository _repo;

    public QuizzesController(ILogger<QuizRepository> logger, IQuizRepository repo)
    {
        _logger = logger;
        _repo = repo;
    }

    [HttpPost]
    public IActionResult CreateQuiz([FromBody] ICollection<QuizCreateDto> quizDtos)
    {
        try
        {
            foreach (var quizDto in quizDtos)
            {
                _repo.AddQuiz(quizDto);
                _logger.LogInformation("Quiz created: {Title}", quizDto.Title);
            }

            return Ok(new { message = "Quiz created successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateQuiz(int id, [FromBody] QuizCreateDto quizDto)
    {
        try
        {
            _repo.UpdateQuiz(id, quizDto);
            return Ok(new { message = "Quiz successfully updated." });
        }
        catch (Exception ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_repo.GetAllQuizzes());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var quiz = _repo.GetQuizById(id);
        return quiz == null ? NotFound() : Ok(quiz);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var result = _repo.DeleteQuiz(id);
        return result ? Ok(new { message = "Quiz deleted." }) : NotFound();
    }

    [HttpPatch("{id}/status")]
    public IActionResult ToggleStatus(int id)
    {
        var result = _repo.ToggleQuizStatus(id);
        return result ? Ok(new { message = "Quiz status toggled." }) : NotFound();
    }

    [HttpPost("{id}/questions")]
    public IActionResult AddQuestion(int id, [FromBody] QuestionCreateDto questionDto)
    {
        try
        {
            var result = _repo.AddQuestionToQuiz(id, questionDto);
            return result ? Ok(new { message = "Question added." }) : NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("question-types")]
    public IActionResult GetQuestionTypes()
    {
        try
        {
            var questionTypes = EnumExtensions.GetQuestionTypes<EQuestiontype>();
            return Ok(questionTypes);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
