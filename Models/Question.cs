namespace Models;

public abstract class Question
{
    public int Id { get; set; }
    public EQuestiontype EQuestiontype { get; set; }
    public required string Prompt { get; set; }
    public int? TimeLimitSeconds { get; set; }

    public abstract double CheckAnswer(string answer, double elapsedSec);
}