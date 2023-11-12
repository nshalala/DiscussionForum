using DiscussionForum.Domain.Entities.Common;
using DiscussionForum.Domain.Enums;

namespace DiscussionForum.Domain.Entities;

public class CommentRating : BaseEntity
{
    public Rates Rate { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid CommentId { get; set; }
    public Comment Comment { get; set; }
}