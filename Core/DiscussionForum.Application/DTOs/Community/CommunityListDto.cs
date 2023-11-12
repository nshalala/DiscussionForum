namespace DiscussionForum.Application.DTOs.Community;

public record CommunityListDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public int MemberCount { get; set; }
}