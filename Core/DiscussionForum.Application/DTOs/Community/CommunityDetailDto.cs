namespace DiscussionForum.Application.DTOs.Community;

public record CommunityDetailDto
{
    public string Name { get; set; }
    public Guid AdminId { get; set; }
}