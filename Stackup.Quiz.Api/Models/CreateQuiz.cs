namespace Stackup.Quiz.Api.Models;

public record CreateQuiz(
    string Title,
    string? Description,
    DateTimeOffset? StartsAt,
    DateTimeOffset? EndsAt,
    QuizState State,
    bool IsPrivate,
    string? Password);