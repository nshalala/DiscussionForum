namespace DiscussionForum.Application.DTOs.User;

public record RegisterUserDto
{
    public string? Fullname { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PasswordConfirm { get; set; }
}