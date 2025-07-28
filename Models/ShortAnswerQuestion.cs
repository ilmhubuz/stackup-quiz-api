namespace Models;

public class ShortAnswerQuestion : Question
{
    public required string CorrectAnswer { get; set; }

    public override double CheckAnswer(string answer, double elapsedSec)
    {
        if (!string.Equals(answer.Trim(), CorrectAnswer.Trim(), StringComparison.OrdinalIgnoreCase))
            return 0.0;

        if (TimeLimitSeconds.HasValue && elapsedSec > TimeLimitSeconds.Value)
            return 0.5;

        return 1.0;
    }
}