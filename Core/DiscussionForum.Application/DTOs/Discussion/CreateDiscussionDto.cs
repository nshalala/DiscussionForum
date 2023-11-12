namespace DiscussionForum.Application.DTOs.Discussion;

public record CreateDiscussionDto
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public Guid CommunityId { get; set; }
}