namespace Dtos;

public class QuestionFeedbackDto
{
    public int QuestionId { get; set; }
    public bool IsCorrect { get; set; }
    public double Score { get; set; }
    public string? Message { get; set; }
}
