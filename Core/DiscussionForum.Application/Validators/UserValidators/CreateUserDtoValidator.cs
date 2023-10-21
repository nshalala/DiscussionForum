using DiscussionForum.Application.DTOs.User;
using FluentValidation;

namespace DiscussionForum.Application.Validators.UserValidators;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(x => x.Fullname)
            .Cascade(CascadeMode.Stop)
            .MaximumLength(100)
            .Matches(@"^[A-Za-z0-9\s'-]+$")
                .WithMessage("Fullname cannot contain special characters other than hyphens and apostrophes");
        RuleFor(x => x.Username)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .MaximumLength(30)
            .Matches(@"^[\da-z_.]+$")
                .WithMessage("Username can contain lowercase letters, numbers, underscore and dot characters.");
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .EmailAddress();
        RuleFor(x => x.Password)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .MinimumLength(8);
        RuleFor(x => x.PasswordConfirm)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .MinimumLength(8);
    }
}