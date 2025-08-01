using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Stackup.Quiz.Api.Dtos;

public class UpdateQuizDto
{
    [Required, MinLength(4, ErrorMessage = "{PropertyName} 3 ta belgidan ko'p bo'lishi kerak!"), MaxLength(100)]
    public required string Title { get; set; }
    public string? Description { get; set; }
    public QuizState State { get; set; }
    public DateTimeOffset? StartsAt { get; set; }
    public DateTimeOffset? EndsAt { get; set; }
    public bool IsPrivate { get; set; }
    public string? Password { get; set; }
}