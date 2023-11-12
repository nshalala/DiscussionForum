using DiscussionForum.Application.Repositories;
using DiscussionForum.Domain.Entities;
using DiscussionForum.Persistence.Contexts;
using Microsoft.AspNetCore.Http;

namespace DiscussionForum.Persistence.Repositories;

public class DiscussionRepository : Repository<Discussion>, IDiscussionRepository
{
    public DiscussionRepository(DiscussionForumDbContext context, IHttpContextAccessor contextAccessor) : base(context, contextAccessor)
    {
    }
}