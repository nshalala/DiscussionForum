namespace DiscussionForum.Application.DTOs.Comment;

public class CommentDetailDto
{
    public string Content { get; set; }
    public int Rating { get; set; }
    public Guid UserId { get; set; }
    public Guid DiscussionId { get; set; }
    public Guid? ParentId { get; set; }
    public List<Domain.Entities.Comment>? Children { get; set; }
}