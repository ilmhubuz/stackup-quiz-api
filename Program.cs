<<<<<<< HEAD
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
var app = builder.Build();


app.UseHttpsRedirection();

=======
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Stackup.Quiz.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions( JsonOptions =>
{
    JsonOptions.JsonSerializerOptions.AllowTrailingCommas = true;
    JsonOptions.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
    JsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

>>>>>>> 79025f1 (CreateQuiz endpoint qo‘shildi)
app.MapControllers();

app.Run();

<<<<<<< HEAD

=======
>>>>>>> 79025f1 (CreateQuiz endpoint qo‘shildi)
