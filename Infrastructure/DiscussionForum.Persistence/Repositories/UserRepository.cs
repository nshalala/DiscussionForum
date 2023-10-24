using System.Linq.Expressions;
using DiscussionForum.Application.Repositories;
using DiscussionForum.Domain.Entities;
using DiscussionForum.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DiscussionForum.Persistence.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly DiscussionForumDbContext _context;

    public UserRepository(DiscussionForumDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> IsExistAsync(Expression<Func<User, bool>> expression)
    {
        return await _context.Users.AnyAsync(expression);
    }
}