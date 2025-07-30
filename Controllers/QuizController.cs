using Microsoft.AspNetCore.Mvc;

namespace stackup_quiz_api.Controllers;

[Route("api/[controller]")]
class QuizController : Controller
{
    public IActionResult Test() => Ok();
}
