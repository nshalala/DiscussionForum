using DiscussionForum.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiscussionForum.Persistence.Contexts;

public class DiscussionForumDbContext : DbContext
{
    public DiscussionForumDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}