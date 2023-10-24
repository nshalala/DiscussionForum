namespace DiscussionForum.Application.DTOs.User;

public record UserListDto
{
    public DateTime CreatedAt { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string? Fullname { get; set; }
}