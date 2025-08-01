using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using stackup_quiz_api.Abstraction;
using stackup_quiz_api.Dtos;
using stackup_quiz_api.Middlewares;
using stackup_quiz_api.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddJsonOptions(jsonOptions =>
    {
        jsonOptions.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter<QuizState>());
    });
builder.Services.AddSingleton<IQuizService, QuizService>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();



var app = builder.Build();
app.UseMiddleware<CustomExceptionHandlerMiddleware>();
app.MapControllers();
app.Run();
