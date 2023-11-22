using DiscussionForum.Application.Repositories;
using DiscussionForum.Domain.Entities;
using DiscussionForum.Persistence.Contexts;
using Microsoft.AspNetCore.Http;

namespace DiscussionForum.Persistence.Repositories;

public class CommentRatingRepository : Repository<CommentRating>, ICommentRatingRepository
{
    public CommentRatingRepository(DiscussionForumDbContext context, IHttpContextAccessor contextAccessor) : base(
        context, contextAccessor)
    {
    }
}