using DiscussionForum.Domain.Entities;
using FluentValidation;

namespace DiscussionForum.Application.Validators.CommentValidators;

public class CreateCommentDtoValidators : AbstractValidator<Comment>
{
    public CreateCommentDtoValidators()
    {
        RuleFor(c => c.Content)
            .NotNull()
            .NotEmpty()
            .MaximumLength(500);
    }
}