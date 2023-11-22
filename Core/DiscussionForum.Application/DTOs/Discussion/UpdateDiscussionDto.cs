using Microsoft.AspNetCore.Http;

namespace DiscussionForum.Application.DTOs.Discussion;

public record UpdateDiscussionDto
{
    public Guid DiscussionId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public IFormFileCollection? Files { get; set; }
}