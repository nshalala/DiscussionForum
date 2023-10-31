using DiscussionForum.Domain.Entities.Common;

namespace DiscussionForum.Domain.Entities;

public class Discussion : BaseEntity
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid? CommunityId { get; set; }
    public Community? Community { get; set; }
    public int Rating { get; set; }
    public List<Comment>? Comments { get; set; }
}