using DiscussionForum.Application.DTOs.Discussion;
using FluentValidation;

namespace DiscussionForum.Application.Validators.DiscussionValidators;

public class CreateDiscussionDtoValidator:AbstractValidator<CreateDiscussionDto>
{
    public CreateDiscussionDtoValidator()
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