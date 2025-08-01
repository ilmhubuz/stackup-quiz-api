using FluentValidation;
using stackup_quiz_api.Dtos;

namespace stackup_quiz_api.Validators;

public class CreateQuizValidator : AbstractValidator<CreateQuizDto>
{
    public CreateQuizValidator()
    {
        RuleFor(q => q.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(100).WithMessage("Title must be at most 100 characters");

        RuleFor(q => q.Description)
            .NotEmpty().WithMessage("Description is required");

        RuleFor(q => q.State)
            .NotEmpty().WithMessage("State is required");

        RuleFor(x => x.EndsAt)
            .NotNull()
            .When(x => x.StartsAt != null)
            .WithMessage("EndsAt is required if StartsAt is provided.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .When(x => x.IsPrivate)
            .WithMessage("Password is required when quiz is private.");


    }
}