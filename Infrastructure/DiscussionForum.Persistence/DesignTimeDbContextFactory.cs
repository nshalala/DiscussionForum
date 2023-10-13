using DiscussionForum.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DiscussionForum.Persistence;

public class DesignTimeDbContextFactory:IDesignTimeDbContextFactory<DiscussionForumDbContext>
{
    public DiscussionForumDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<DiscussionForumDbContext> dbContextOptionsBuilder = new();
        dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);
        return new(dbContextOptionsBuilder.Options);
    }
}