using System.Linq.Expressions;
using DiscussionForum.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace DiscussionForum.Application.Repositories;

public interface IRepository<TEntity> where TEntity:BaseEntity
{
    DbSet<TEntity> Table { get; }
    
    IQueryable<TEntity> GetAll(int skip, int take ,bool tracking = true,  params string[] includes);
    IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> expression, bool tracking = true,  params string[] includes);
    Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true,  params string[] includes);
    Task<TEntity> GetByIdAsync(Guid id, bool tracking = true,  params string[] includes);
    Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> expression);
    Guid GetUserId();

    Task<bool> AddAsync(TEntity entity);
    bool Remove(TEntity entity);
    void Update(TEntity entity);
    Task<int> SaveAsync();
}