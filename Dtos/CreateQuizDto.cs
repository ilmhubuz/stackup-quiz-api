using System.ComponentModel.DataAnnotations;
 
namespace Stackup.Quiz.Api.Dtos;

public class CreateQuizDto
{
    [Required, MinLength(4,   ErrorMessage ="Title 4t belgidan ko'p bo'lishi kerak"), MaxLength(100)]
    public required string Title { get; set; }
    public  string? Description { get; set; }
    // [JsonConverter(typeof (JsonStringEnumConverter<Quizstate>))]
    public Quizstate state { get; set; }
    public DateTimeOffset StartsAt { get; set; }
    public DateTimeOffset? EndsAt { get; set; }
    public bool IsPrivate{ get; set; }
    public string? Password { get; set; }
}

public enum Quizstate
{
    Active,

    Disabled
}