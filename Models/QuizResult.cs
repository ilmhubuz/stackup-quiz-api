using Dtos;

namespace Models;

public class QuizResult
{
    public int Id { get; set; }
    public int QuizId { get; set; }
    public required string StudentName { get; set; }
    public double Score { get; set; }
    public DateTime SubmittedAt { get; set; }

    public List<AnswerSubmissionDto> SubmittedAnswers { get; set; } = new();
}