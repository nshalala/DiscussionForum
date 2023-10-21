using DiscussionForum.Application.Repositories;
using DiscussionForum.Domain.Entities;
using DiscussionForum.Persistence.Contexts;

namespace DiscussionForum.Persistence.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(DiscussionForumDbContext context) : base(context)
    {
        
    }
}