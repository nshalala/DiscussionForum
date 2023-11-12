using DiscussionForum.Domain.Enums;

namespace DiscussionForum.Application.DTOs.Discussion;

public record RateDiscussionDto
{
    public Rates Rate { get; set; }
    public Guid DiscussionId { get; set; }
}