
using Microsoft.AspNetCore.Mvc;
using stackup_quiz_api.Dtos;

namespace stackup_quiz_api.Controllers;

[ApiController, Route("api/[controller]")]
public class QuizController : Controller
{
    private static Dictionary<string, QuizDto> quizes = [];
    private static IEnumerable<QuizDto> existingQuiz = quizes.Values.Where(p => p.State != QuizState.Deleted);
    public static int IdIndex = 1;
    [HttpPost]
    public IActionResult Create([FromBody] CreateQuizDto dto)
    {
        if (quizes.TryAdd(dto.Title, new QuizDto
        {
            Id = IdIndex,
            Title = dto.Title,
            Description = dto.Description,
            State = dto.State,
            StartsAt = dto.StartsAt,
            EndsAt = dto.EndsAt,
            IsPrivate = dto.IsPrivate,
            Password = dto.Password
        }) is false)
            return Conflict($"'{dto.Title}' sarlavhaga ega quiz allaqachon qo'shilgan");
        IdIndex++;
        return Ok(quizes[dto.Title]);
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(existingQuiz);

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var quiz = existingQuiz.FirstOrDefault(p => p.Id == id);
        if (quiz is null)
            return NotFound();
        return Ok(quiz);
    }

    [HttpPut("{id}")]
    public IActionResult GetById(int id, [FromBody] UpdateQuizDto dto)
    {
        var quiz = existingQuiz.FirstOrDefault(p => p.Id == id);
        if (quiz is null)
            return NotFound();

        quiz.Title = dto.Title;
        quiz.Description = dto.Description;
        quiz.State = dto.State;
        quiz.StartsAt = dto.StartsAt;
        quiz.EndsAt = dto.EndsAt;
        quiz.IsPrivate = dto.IsPrivate;
        quiz.Password = dto.Password;
        
        return Ok(quiz);
    }
    
}
