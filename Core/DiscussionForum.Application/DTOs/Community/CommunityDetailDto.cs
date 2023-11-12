namespace DiscussionForum.Application.DTOs.Community;

public record CommunityDetailDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public int MemberCount { get; set; }
    public List<Domain.Entities.User> AdminUsers { get; set; }
}