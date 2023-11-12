using DiscussionForum.Domain.Entities;
using DiscussionForum.Persistence.DbConfigurations;
using Microsoft.EntityFrameworkCore;

namespace DiscussionForum.Persistence.Contexts;

public class DiscussionForumDbContext : DbContext
{
    public DiscussionForumDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CommunityConfiguration());
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Community> Communities { get; set; }
    public DbSet<Discussion> Discussions { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<DiscussionRating> DiscussionRatings { get; set; }
    public DbSet<CommentRating> CommentRatings { get; set; }
    
}