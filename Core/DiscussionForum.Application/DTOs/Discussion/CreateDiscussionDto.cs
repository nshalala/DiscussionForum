namespace DiscussionForum.Application.Validators.DiscussionValidators;

public record CreateDiscussionDto
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public Guid? CommunityId { get; set; }
}