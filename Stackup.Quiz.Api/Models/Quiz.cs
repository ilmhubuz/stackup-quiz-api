namespace Stackup.Quiz.Api.Models;

public record Quiz(
    string Title,
    string? Description,
    DateTimeOffset? StartsAt,
    DateTimeOffset? EndsAt,
    QuizState State,
    bool IsPrivate,
    string? Password)
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public int Id { get; set; }   
}