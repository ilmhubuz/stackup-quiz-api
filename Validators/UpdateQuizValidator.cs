using FluentValidation;
using stackup_quiz_api.Abstraction;
using stackup_quiz_api.Dtos;

namespace stackup_quiz_api.Validators;

public class UpdateQuizValidator : AbstractValidator<UpdateQuizDto>
{
    public UpdateQuizValidator(IQuizService service)
    {
        // ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(q => q.Title)
            .MinimumLength(4)
            .MaximumLength(100);

        RuleFor(q => q.Description)
            .MaximumLength(100);
        RuleFor(q => q.State)
            .IsInEnum();

        RuleFor(q => q.StartsAt)
            .NotEmpty()
            .When(a => a.EndsAt is not null);

        RuleFor(x => x.EndsAt)
            .NotEmpty()
            .When(x => x.StartsAt != null)
            .WithMessage("EndsAt is required if StartsAt is provided.");

        When(q => q.IsPrivate, () =>
        {
            RuleFor(x => x.Password)
                .NotEmpty()
                .Length(6)
                .Must(x => x.All(char.IsAsciiLetterOrDigit))
                .WithMessage("'{PropertyName}' must be alpha-numeric");
        });

        RuleFor(x => x)
            .MustAsync(async (dto, token) => await service.ExistAsync(dto.Title, token) is false)
            .WithMessage("Quiz title must be unique");

    }
}