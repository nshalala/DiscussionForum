namespace DiscussionForum.Application.DTOs.Comment;
using Domain.Entities;
public record ChildCommentListDto
{
    public string Content { get; set; }
    public User User { get; set; }
    public int Rating { get; set; }
}