using Models;

namespace Dtos;

public class QuestionDto
{
    public EQuestiontype EQuestiontype { get; set; }
    public required string Prompt { get; set; }
    public int? TimeLimitSeconds { get; set; }
}