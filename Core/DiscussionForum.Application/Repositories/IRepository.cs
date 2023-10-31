using System.Linq.Expressions;
using DiscussionForum.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace DiscussionForum.Application.Repositories;

public interface IRepository<TEntity> where TEntity:BaseEntity
{
    DbSet<TEntity> Table { get; }
    
    IQueryable<TEntity> GetAll(int skip, int take ,bool tracking = true);
    IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> expression, bool tracking = true);
    Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true);
    Task<TEntity> GetByIdAsync(Guid id, bool tracking = true);
    Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> expression);

    Task<bool> AddAsync(TEntity entity);
    bool Remove(TEntity entity);
    void Update(TEntity entity);
    Task<int> SaveAsync();
}