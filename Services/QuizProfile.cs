using AutoMapper;
using stackup_quiz_api.Dtos;
using stackup_quiz_api.Models;

namespace stackup_quiz_api.Services;

public class QuizProfile : Profile
{
    public QuizProfile()
    {
        CreateMap<CreateQuizDto, CreateQuiz>();
        CreateMap<UpdateQuizDto, UpdateQuiz>();
        CreateMap<QuizDto, Quiz>();
        CreateMap<Dtos.QuizState, Models.QuizState>();

        CreateMap<CreateQuiz, Quiz>();
        CreateMap<Quiz, QuizDto>();
        CreateMap<UpdateQuiz, Quiz>();
    }
}