using Microsoft.AspNetCore.Mvc;
using Stackup.Quiz.Api.Dtos;

namespace Stackup.Quiz.Api.Controllers;

[ApiController, Route("api/[controller]")]
public class QuizController : Controller
{
    private static Dictionary<string, QuizDto> quizes = [];
    private static IEnumerable<QuizDto> existingQuizes = quizes.Values?.Where(q => q.State is not QuizState.Deleted) ?? [];
    private static int idIndex = 1;

    [HttpPost]
    public IActionResult Create(
        [FromBody] CreateQuizDto dto)
    {
        if (quizes.TryAdd(dto.Title, new QuizDto
        {
            Id = idIndex,
            Title = dto.Title,
            Description = dto.Description,
            StartsAt = dto.StartsAt,
            EndsAt = dto.EndsAt,
            State = dto.State,
            IsPrivate = dto.IsPrivate,
            Password = dto.Password
        }) is false)
            return Conflict($"'{dto.Title}' sarlavhaga ega Quiz allaqachon qo'shilgan!");

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

    [HttpPut("{id}")]
    public IActionResult UpdateById(int id, [FromBody] UpdateQuizDto dto)
    {
        var quiz = existingQuizes.FirstOrDefault(q => q.Id == id);
        if (quiz is null)
            return NotFound();

        quiz.Title = dto.Title;
        quiz.Description = dto.Description;
        quiz.StartsAt = dto.StartsAt;
        quiz.EndsAt = dto.EndsAt;
        quiz.State = dto.State;
        quiz.IsPrivate = dto.IsPrivate;
        quiz.Password = dto.Password;

        return Ok(quiz);
    }
}