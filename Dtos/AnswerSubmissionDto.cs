namespace Dtos;

public class QuizSubmissionDto
{
    public required string StudentName { get; set; }
    public List<AnswerSubmissionDto> Answers { get; set; } = new();
}

public class AnswerSubmissionDto
{
    public int QuestionId { get; set; }
    public string Answer { get; set; } = string.Empty;
    public double ElapsedSeconds { get; set; }
}