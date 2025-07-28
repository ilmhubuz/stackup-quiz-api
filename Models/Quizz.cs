namespace Models;

public class Quizz
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public bool IsActive { get; set; }

    public required List<Question> Questions { get; set; } = new();
}