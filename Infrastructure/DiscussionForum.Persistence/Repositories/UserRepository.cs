using System.Linq.Expressions;
using DiscussionForum.Application.Repositories;
using DiscussionForum.Domain.Entities;
using DiscussionForum.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DiscussionForum.Persistence.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(DiscussionForumDbContext context) : base(context)
    {
    }
}