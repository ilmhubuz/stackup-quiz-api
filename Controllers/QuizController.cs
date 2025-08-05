
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using stackup_quiz_api.Abstraction;
using stackup_quiz_api.Dtos;
using stackup_quiz_api.Models;

namespace stackup_quiz_api.Controllers;

[ApiController, Route("api/[controller]")]
public class QuizController(
    IQuizService quizService,
    IMapper mapper) : Controller
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateQuizDto dto, CancellationToken abortionToken=default)
    {
        var model = mapper.Map<CreateQuiz>(dto);
        var quiz = await quizService.CreateQuizAsync(model, abortionToken);
        return Ok(mapper.Map<QuizDto>(quiz));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken abortionToken)
    {
        var quizes = await quizService.GetAllAsync(abortionToken);
        return Ok(quizes.Select(mapper.Map<QuizDto>));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken abortionToken)
    {
        var quiz = await quizService.GetSingleAsync(id, abortionToken);
        return Ok(mapper.Map<QuizDto>(quiz));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateById(int id, [FromBody] UpdateQuizDto dto, CancellationToken abortionToken)
    {
        var quiz = await quizService.UpdateAsync(id, mapper.Map<UpdateQuiz>(dto),abortionToken);
        return Ok(mapper.Map<QuizDto>(quiz));
    }

}
