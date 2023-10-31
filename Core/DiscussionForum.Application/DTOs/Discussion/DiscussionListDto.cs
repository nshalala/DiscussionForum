namespace DiscussionForum.Application.Validators.DiscussionValidators;

public record DiscussionListDto
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public Guid UserId { get; set; }
    public Guid? CommunityId { get; set; }
    public int Rating { get; set; }
}