namespace Dtos;

public class GradingResultDto
{
    public int QuizId { get; set; }
    public double TotalScore { get; set; }
    public double Percent { get; set; }
    public bool Passed { get; set; }
    public List<QuestionFeedbackDto> Feedbacks { get; set; } = new();
}