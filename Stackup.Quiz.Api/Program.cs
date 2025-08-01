using Microsoft.AspNetCore.Builder;
using System.Text.Json.Serialization;
using Stackup.Quiz.Api.Dtos;
using Stackup.Quiz.Api.Services.Abstractions;
using Stackup.Quiz.Api.Services;
using Stackup.Quiz.Api.Middlewares;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(jsonOptions =>
    {
        jsonOptions.JsonSerializerOptions.AllowTrailingCommas = true;
        jsonOptions.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter<QuizState>());
    });

builder.Services.AddSingleton<IQuizService, QuizService>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

app.UseMiddleware<CustomExceptionHandlerMiddleware>();
app.MapControllers();

app.Run();
