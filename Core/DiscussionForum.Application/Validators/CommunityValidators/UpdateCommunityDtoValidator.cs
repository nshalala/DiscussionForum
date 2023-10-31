using DiscussionForum.Application.DTOs.Community;
using FluentValidation;

namespace DiscussionForum.Application.Validators.CommunityValidators;

public class UpdateCommunityDtoValidator:AbstractValidator<UpdateCommunityDto>
{
    public UpdateCommunityDtoValidator()
    {
        RuleFor(c => c.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(30);
    }
}