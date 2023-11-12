using DiscussionForum.Domain.Entities.Common;

namespace DiscussionForum.Domain.Entities;

public class Comment : BaseEntity
{
    public string Content { get; set; }
    public List<CommentRating>? CommentRatings { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }
    public Discussion Discussion { get; set; }
    public Guid DiscussionId { get; set; }
    public Comment? Parent { get; set; }
    public Guid? ParentId { get; set; }
    public List<Comment>? Children { get; set; }
}