namespace Models;

public class McqQuestion : Question
{
    public string AnswerKey { get; set; }
    public Dictionary<string, string> Options { get; init; }

    public override double CheckAnswer(string answer, double elapsedSec)
    {
        if (!string.Equals(answer.Trim(), AnswerKey.Trim(), StringComparison.OrdinalIgnoreCase))
            return 0.0;

        if (TimeLimitSeconds.HasValue && elapsedSec > TimeLimitSeconds.Value)
            return 0.5;

        return 1.0;
    }
}