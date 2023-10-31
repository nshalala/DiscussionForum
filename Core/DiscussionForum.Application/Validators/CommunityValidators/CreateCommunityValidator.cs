using DiscussionForum.Application.DTOs.Community;
using FluentValidation;

namespace DiscussionForum.Application.Validators.CommunityValidators;

public class CreateCommunityValidator:AbstractValidator<CreateCommunityDto>
{
    public CreateCommunityValidator()
    {
        RuleFor(c => c.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(30);
    }    
}