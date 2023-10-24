using System.Linq.Expressions;
using DiscussionForum.Domain.Entities;

namespace DiscussionForum.Application.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<bool> IsExistAsync(Expression<Func<User, bool>> expression);
}