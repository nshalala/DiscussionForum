using System.Security.Principal;
using DiscussionForum.Domain.Entities.Common;
using DiscussionForum.Domain.Enums;

namespace DiscussionForum.Domain.Entities;

public class DiscussionRating : BaseEntity
{
    public Rates Rate { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid DiscussionId { get; set; }
    public Discussion Discussion { get; set; }
}