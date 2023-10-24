namespace DiscussionForum.Application.DTOs.Auth;

public record LoginDto
{
    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }
}