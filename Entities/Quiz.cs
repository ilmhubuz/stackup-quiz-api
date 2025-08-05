namespace stackup_quiz_api.Entities;

public class Quiz
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public QuizState State { get; set; }
    public DateTimeOffset? StartsAt { get; set; }
    public DateTimeOffset? EndsAt { get; set; }
    public bool IsPrivate { get; set; }
    public string? Password { get; set; }
}

public enum QuizState
{
    Active,
    Disabled,
    Deleted
}