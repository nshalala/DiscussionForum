using DiscussionForum.Application.DTOs.Community;

namespace DiscussionForum.Application.DTOs.Discussion;

using Domain.Entities;
public record DiscussionListDto
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public List<string>? FilePaths { get; set; }
    public CommunityDetailDto Community { get; set; }
    public int CommentCount { get; set; }
    public int Rating { get; set; }
}