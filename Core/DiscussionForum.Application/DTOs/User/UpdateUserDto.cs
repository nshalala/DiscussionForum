namespace DiscussionForum.Application.DTOs.User;

public record UpdateUserDto
{
    public string? Fullname { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? CurrentPassword { get; set; }
    public string? NewPassword { get; set; }
    public string? NewPasswordConfirm { get; set; }
}