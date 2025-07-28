using Models;

namespace Data;

public static class Datastorage
{
    private static int _quizzId = 1;
    private static int _questionId = 1;
    private static int _quizResultId = 1;

    public static int GetNextQuizzId() => _quizzId++;
    public static int GetNextQuestionId() => _questionId++;
    public static int GetNextQuizzResultId() => _quizResultId++;
    public static List<Quizz> Quizzes { get; set; } = new();
    public static List<Question> Questions { get; set; } = new();
    public static List<QuizResult> QuizResults = new();

}
