using DiscussionForum.Domain.Entities;

namespace DiscussionForum.Application.DTOs.User;

public record UserDetailDto
{
    public DateTime CreatedAt { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string? Fullname { get; set; }
    public int DiscussionsCount { get; set; }
    public int CommentsCount { get; set; }
    public List<Discussion>? Discussions { get; set; }
    public List<Domain.Entities.Comment> Comments { get; set; }
}