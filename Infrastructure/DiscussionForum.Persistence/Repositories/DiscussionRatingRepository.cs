using DiscussionForum.Application.Repositories;
using DiscussionForum.Domain.Entities;
using DiscussionForum.Persistence.Contexts;
using Microsoft.AspNetCore.Http;

namespace DiscussionForum.Persistence.Repositories;

public class DiscussionRatingRepository : Repository<DiscussionRating>, IDiscussionRatingRepository
{
    public DiscussionRatingRepository(DiscussionForumDbContext context, IHttpContextAccessor contextAccessor) : base(
        context, contextAccessor)
    {
    }
}