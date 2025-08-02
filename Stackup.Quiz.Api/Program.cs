using Microsoft.AspNetCore.Builder;
using System.Text.Json.Serialization;
using Stackup.Quiz.Api.Dtos;
using Stackup.Quiz.Api.Services.Abstractions;
using Stackup.Quiz.Api.Services;
using Stackup.Quiz.Api.Middlewares;
using FluentValidation;
using Stackup.Quiz.Api.Validators;
using Stackup.Quiz.Api;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddFluentValidationAsyncAutoValidation()
    .AddJsonOptions(jsonOptions =>
    {
        jsonOptions.JsonSerializerOptions.AllowTrailingCommas = true;
        jsonOptions.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter<QuizState>());
    });

builder.Services.AddSingleton<IQuizService, QuizService>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IValidator<CreateQuizDto>, CreateQuizDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateQuizDto>, UpdateQuizDtoValidator>();

var app = builder.Build();

app.UseMiddleware<CustomExceptionHandlerMiddleware>();
app.MapControllers();

app.Run();
