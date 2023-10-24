namespace DiscussionForum.Application.DTOs.User;

public record UserDetailDto
{
    public DateTime CreatedAt { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string? Fullname { get; set; }
}