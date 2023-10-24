using System.ComponentModel.DataAnnotations.Schema;
using DiscussionForum.Domain.Entities.Common;

namespace DiscussionForum.Domain.Entities;

public class Community : BaseEntity
{
    public string Name { get; set; }
    public Guid AdminId { get; set; }
    public User Admin { get; set; }
    public List<User>? Members { get; set; }
}