using FluentValidation;

namespace DiscussionForum.Application.Validators.DiscussionValidators;

public class UpdateDiscussionDtoValidator:AbstractValidator<UpdateDiscussionDto>
{
    public UpdateDiscussionDtoValidator()
    {
        RuleFor(d => d.Title)
            .NotNull()
            .NotEmpty()
            .MinimumLength(5)
            .WithMessage("Title is too short")
            .MaximumLength(200)
            .WithMessage("Title is too long");
        RuleFor(d => d.Description)
            .MaximumLength(500)
            .WithMessage("Description is too long");
    }
}