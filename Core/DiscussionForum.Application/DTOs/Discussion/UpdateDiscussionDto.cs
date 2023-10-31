namespace DiscussionForum.Application.Validators.DiscussionValidators;

public record UpdateDiscussionDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public Guid UserId { get; set; }
}