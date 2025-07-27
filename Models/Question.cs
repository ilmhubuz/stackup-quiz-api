namespace Models;

public class Question
{
    public int Id { get; set; }
    public EQuestiontype EQuestiontype { get; set; }
    public string Prompt { get; set; }
    public int? TimeLimitSeconds { get; set; }

    abstract double CheckAnswer(string answer, double elapsedSec);
}