using System.ComponentModel.DataAnnotations;

namespace Stackup.Quiz.Api.Dtos;

public class QuizDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public  string? Description { get; set; }
    // [JsonConverter(typeof (JsonStringEnumConverter<Quizstate>))]
    public Quizstate state { get; set; }
    public DateTimeOffset StartsAt { get; set; }
    public DateTimeOffset? EndsAt { get; set; }
    public bool IsPrivate{ get; set; }
    public string? Password { get; set; }
    public object Value { get; internal set; }
}
