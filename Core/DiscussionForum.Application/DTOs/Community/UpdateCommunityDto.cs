namespace DiscussionForum.Application.DTOs.Community;

public record UpdateCommunityDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}