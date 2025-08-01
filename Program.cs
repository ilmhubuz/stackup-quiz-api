using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using stackup_quiz_api;
using stackup_quiz_api.Abstraction;
using stackup_quiz_api.Dtos;
using stackup_quiz_api.Middlewares;
using stackup_quiz_api.Services;
using stackup_quiz_api.Validators;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddFluentValidationAsyncAutoValidation()
    .AddJsonOptions(jsonOptions =>
    {
        jsonOptions.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter<QuizState>());
    });
builder.Services.AddSingleton<IQuizService, QuizService>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IValidator<CreateQuizDto>, CreateQuizValidator>();
builder.Services.AddScoped<IValidator<UpdateQuizDto>, UpdateQuizValidator>();

// assemblydagi barcha IValidatorni implement qilganlarni add qilib qoyadi
// builder.Services.AddValidatorsFromAssemblyContaining<Program>(); 



var app = builder.Build();
app.UseMiddleware<CustomExceptionHandlerMiddleware>();
app.MapControllers();
app.Run();
