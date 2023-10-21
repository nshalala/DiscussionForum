using System.Linq.Expressions;
using DiscussionForum.Application.Repositories;
using DiscussionForum.Domain.Entities.Common;
using DiscussionForum.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DiscussionForum.Persistence.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DiscussionForumDbContext _context;
    public Repository(DiscussionForumDbContext context)
    {
        _context = context;
    }

    public DbSet<TEntity> Table => _context.Set<TEntity>();

    public IQueryable<TEntity> GetAll(bool tracking = true)
        => tracking ? Table.AsQueryable() : Table.AsQueryable().AsNoTracking();

    public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> expression, bool tracking = true)
        => tracking ? Table.Where(expression) : Table.Where(expression).AsNoTracking();

    public async Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true)
        =>  tracking ? 
            await Table.FirstOrDefaultAsync(expression) : 
            await Table.AsNoTracking().FirstOrDefaultAsync(expression);

    public async Task<TEntity> GetByIdAsync(string id, bool tracking = true)
    {
        var query = tracking ? Table.AsQueryable() : Table.AsNoTracking();
        return await query.FirstOrDefaultAsync(e => e.Id == Guid.Parse(id));
    }

    public async Task<bool> AddAsync(TEntity entity)
    {
        EntityEntry<TEntity> entityEntry = await Table.AddAsync(entity);
        return entityEntry.State == EntityState.Added;
    }

    public bool Remove(TEntity entity)
    {
        EntityEntry<TEntity> entityEntry =  Table.Remove(entity);
        return entityEntry.State == EntityState.Deleted;
    }

    public void Update(TEntity entity)
    {
        Table.Update(entity);
    }

    public async Task<int> SaveAsync()
        => await _context.SaveChangesAsync();
}