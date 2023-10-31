namespace DiscussionForum.Application.DTOs.Community;

public record CommunityListDto
{
    public string Name { get; set; }
    public Guid AdminId { get; set; }
    public List<Guid>? MemberIds { get; set; }
}