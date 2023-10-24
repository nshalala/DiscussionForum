using DiscussionForum.Domain.Entities.Common;

namespace DiscussionForum.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string HashedPassword { get; set; }
    public string Salt { get; set; }
    public string? Fullname { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpires { get; set; }

    public List<Community>? CommunitiesAsAdmin { get; set; }
    public List<Community>? CommunitiesAsMember { get; set; }
}