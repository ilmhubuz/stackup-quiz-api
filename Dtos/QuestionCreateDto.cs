using Models;

public class QuestionCreateDto
{
    public EQuestiontype EQuestiontype { get; set; }
    public required string Prompt { get; set; }
    public int? TimeLimitSeconds { get; set; }

    // MCQ
    public Dictionary<string, string>? Options { get; set; }
    public string? AnswerKey { get; set; }

    // True/False
    public bool? CorrectBoolAnswer { get; set; }

    // ShortAnswer
    public string? CorrectShortAnswer { get; set; }
}