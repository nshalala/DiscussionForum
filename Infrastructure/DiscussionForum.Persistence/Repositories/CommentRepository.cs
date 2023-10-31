using DiscussionForum.Application.Repositories;
using DiscussionForum.Domain.Entities;
using DiscussionForum.Persistence.Contexts;

namespace DiscussionForum.Persistence.Repositories;

public class CommentRepository : Repository<Comment>, ICommentRepository
{
    public CommentRepository(DiscussionForumDbContext context) : base(context)
    {
    }
}