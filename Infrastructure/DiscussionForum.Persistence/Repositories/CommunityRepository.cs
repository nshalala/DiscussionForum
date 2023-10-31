using DiscussionForum.Application.Repositories;
using DiscussionForum.Domain.Entities;
using DiscussionForum.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DiscussionForum.Persistence.Repositories;

public class CommunityRepository : Repository<Community>, ICommunityRepository
{
    public CommunityRepository(DiscussionForumDbContext context) : base(context)
    {
    }
}