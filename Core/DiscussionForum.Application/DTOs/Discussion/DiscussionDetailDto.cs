namespace DiscussionForum.Application.DTOs.Discussion;

using Domain.Entities;

public record DiscussionDetailDto
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public List<string>? FilePaths { get; set; }
    public User User { get; set; }
    public Community Community { get; set; }
    public List<Comment>? Comments { get; set; }
    public int Rating { get; set; }
}