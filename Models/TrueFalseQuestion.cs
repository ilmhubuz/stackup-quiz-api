namespace Models;

public class TrueFalseQuestion : Question
{
    public bool CorrectAnswer { get; set; }

    public override double CheckAnswer(string answer, double _) =>
        bool.TryParse(answer, out var val) && val == CorrectAnswer ? 1 : 0;
}