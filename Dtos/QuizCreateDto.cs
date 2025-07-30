using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class QuizCreateeDto
{
    [Range(minimum:3, maximum:200)]
    public required string Title { get; set; }
    public required string Description { get; set; }
    public bool IsActivce { get; set; }

    public List<QuestionDto> Questions { get; set; } = new List<QuestionDto>();
}