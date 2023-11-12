using DiscussionForum.Domain.Entities.Common;

namespace DiscussionForum.Domain.Entities;

public class Community : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public List<User> AdminUsers { get; set; }
    public List<User> Members { get; set; }
    public List<Discussion>? Discussions { get; set; }
}