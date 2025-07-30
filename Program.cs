using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using stackup_quiz_api.Dtos;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddJsonOptions(jsonOptions =>
    {
        jsonOptions.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter<QuizState>());
    });

var app = builder.Build();
app.MapControllers();
app.Run();
