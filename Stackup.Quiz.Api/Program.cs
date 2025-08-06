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
using Stackup.Quiz.Api.Repositories.Abstractions;
using Stackup.Quiz.Api.Repositories;
using Stackup.Quiz.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
            options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            options.SerializerSettings.DateFormatString = "yyyy-MM-dd / HH:mm:ss";
    })
    .AddFluentValidationAsyncAutoValidation()
    .AddJsonOptions(jsonOptions =>
    {
        jsonOptions.JsonSerializerOptions.AllowTrailingCommas = true;
        jsonOptions.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter<QuizState>());
    });

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<IValidator<CreateQuizDto>, CreateQuizDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateQuizDto>, UpdateQuizDtoValidator>();

builder.Services.AddDbContext<QuizContext>(options => options
    .UseNpgsql(builder.Configuration.GetConnectionString("Quiz"))
    .UseSnakeCaseNamingConvention());

var app = builder.Build();

app.UseMiddleware<CustomExceptionHandlerMiddleware>();
app.MapControllers();

app.Run();
