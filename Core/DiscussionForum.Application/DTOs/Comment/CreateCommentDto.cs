namespace DiscussionForum.Application.DTOs.Comment;

public record CreateCommentDto
{
    public string Content { get; set; }
    public Guid DiscussionId { get; set; }
    public Guid? ParentId { get; set; }
}