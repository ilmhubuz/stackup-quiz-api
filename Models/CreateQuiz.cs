namespace stackup_quiz_api.Models;

public record CreateQuiz(
    string Title,
    string? Description,
    QuizState State,
    DateTimeOffset? StartsAt,
    DateTimeOffset? EndsAt,
    bool IsPrivate,
    string? Password );
