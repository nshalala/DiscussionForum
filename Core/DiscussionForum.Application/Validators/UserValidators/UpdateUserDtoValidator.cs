using DiscussionForum.Application.DTOs.User;
using FluentValidation;

namespace DiscussionForum.Application.Validators.UserValidators;

public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserDtoValidator()
    {
        RuleFor(x => x.Username)
            .Cascade(CascadeMode.Stop)
            .MaximumLength(30)
            .Matches(@"^[\w\d_.]+$")
                .WithMessage("Username can contain letters, numbers, underscore and dot characters.");
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .EmailAddress();
        RuleFor(x => x.NewPassword)
            .Cascade(CascadeMode.Stop)
            .Matches(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
                .WithMessage("The New Password must contain at least one letter, one digit, one special character (@, $, !, %, *, ?, or &) and be at least 8 characters long.");
        RuleFor(x => x.NewPasswordConfirm)
            .Cascade(CascadeMode.Stop)
            .Matches(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
                .WithMessage("The New Password Confirm must contain at least one letter, one digit, one special character (@, $, !, %, *, ?, or &) and be at least 8 characters long.");
    }
}