using DiscussionForum.Domain.Enums;

namespace DiscussionForum.Application.DTOs.Comment;

public record RateCommentDto
{
    public Rates Rate { get; set; }
    public Guid CommentId { get; set; }
}