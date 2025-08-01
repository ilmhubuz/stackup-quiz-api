using FluentValidation;
using stackup_quiz_api.Dtos;

namespace stackup_quiz_api.Validators;

public class CreateQuizValidator : AbstractValidator<CreateQuizDto>
{
    public CreateQuizValidator()
    {
        ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(q => q.Title)
            .MinimumLength(4)
            .MaximumLength(100);

        RuleFor(q => q.Description)
            .MaximumLength(100);

        RuleFor(q => q.State)
            .NotEmpty()
            .WithMessage("State is required");
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

    }
}
