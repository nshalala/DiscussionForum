using System.Reflection.Metadata.Ecma335;

namespace DiscussionForum.Application.DTOs.Community;

public record CreateCommunityDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
}