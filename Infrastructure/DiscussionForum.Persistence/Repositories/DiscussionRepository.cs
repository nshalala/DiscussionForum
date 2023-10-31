using DiscussionForum.Application.Repositories;
using DiscussionForum.Domain.Entities;
using DiscussionForum.Persistence.Contexts;

namespace DiscussionForum.Persistence.Repositories;

public class DiscussionRepository : Repository<Discussion>, IDiscussionRepository
{
    public DiscussionRepository(DiscussionForumDbContext context) : base(context)
    {
    }
}