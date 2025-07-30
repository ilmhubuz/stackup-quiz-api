using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Stackup.Quiz.Api.Dtos;
namespace Stackup.Quiz.Api.Controller;

[ApiController, Route("/api/[controller]")]
public class QuizController : ControllerBase
{
    private static Dictionary<string, QuizDto> quizes = [];
    private static IEnumerable<QuizDto> existingQuizes => quizes.Values.Where(q => q.state == Quizstate.Active);
    private static int idIndex = 1;
    [HttpPost]
    public IActionResult Create(
        [FromBody] CreateQuizDto dto)
    {
        if (quizes.TryAdd(dto.Title, new QuizDto
        {
            Title = dto.Title,
            Description = dto.Description,
            StartsAt = dto.StartsAt,
            EndsAt = dto.EndsAt,
            state = dto.state,
            IsPrivate = dto.IsPrivate,
            Password = dto.Password
        }) is false)
            return Conflict($"'{dto.Title}'sarlavhaga ega Quiz qo'shilgan ");

        idIndex++;
        return Ok(quizes[dto.Title]);
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(existingQuizes);
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var quiz = existingQuizes.FirstOrDefault(q => q.Id == id);
        if (quiz is null)
            return NotFound();

        return Ok(quiz);
    } 
    
}
