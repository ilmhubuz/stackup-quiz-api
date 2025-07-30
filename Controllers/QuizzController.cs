using Microsoft.AspNetCore.Mvc;

namespace Controllers;


[ApiController]
[Route("[controller]")]
public class QuizzController : ControllerBase
{

    [HttpGet("[action]")]
    public IActionResult GetAll()
    {
        return Ok("Everything is OK!");
    }
}
