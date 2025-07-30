namespace Models;

public class QuizMeta
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsActivce { get; set; }

    public List<Question> Questions { get; set; }
}