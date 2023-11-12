namespace DiscussionForum.Application.DTOs.Comment;
using Domain.Entities;
public record CommentListDto
{
    public string Content { get; set; }
    public User User { get; set; }
    public int Rating { get; set; }
    public List<CommentListDto>? Children { get; set; }
}