using System.Linq.Expressions;
using System.Security.Claims;
using DiscussionForum.Application.Repositories;
using DiscussionForum.Domain.Entities.Common;
using DiscussionForum.Persistence.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DiscussionForum.Persistence.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DiscussionForumDbContext _context;
    private readonly IHttpContextAccessor _contextAccessor;


    public Repository(DiscussionForumDbContext context, IHttpContextAccessor contextAccessor)
    {
        _context = context;
        _contextAccessor = contextAccessor;
    }

    public DbSet<TEntity> Table => _context.Set<TEntity>();

    public IQueryable<TEntity> GetAll(int skip, int take, bool tracking = true,  params string[] includes)
    {
        var query = Table.Skip(skip).Take(take);
        query = _getIncludes(query, includes);
        return tracking ? query : query.AsNoTracking();
    }

    public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> expression, bool tracking = true,
        params string[] includes)
    {
        var query = tracking ? Table.Where(expression) : Table.Where(expression).AsNoTracking();
        return _getIncludes(query, includes);
    }

    public async Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true,
        params string[] includes)
    {
        var query = _getIncludes(Table, includes);
        return  tracking ? await query.FirstOrDefaultAsync(expression) : await query.AsNoTracking().FirstOrDefaultAsync(expression);
    }

    public async Task<TEntity> GetByIdAsync(Guid id, bool tracking = true,  params string[] includes)
    {
        var query = tracking ? Table.AsQueryable() : Table.AsNoTracking();
        query = _getIncludes(query, includes);
        return await query.FirstOrDefaultAsync(e => e.Id == id);
    }


    public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await Table.AnyAsync(expression);
    }

    public Guid GetUserId()
    {
        var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Sid)?.Value;
        if (userId == null)
            throw new Exception("Log in");
        return Guid.Parse(userId);
    }

    public async Task<bool> AddAsync(TEntity entity)
    {
        EntityEntry<TEntity> entityEntry = await Table.AddAsync(entity);
        return entityEntry.State == EntityState.Added;
    }

    public bool Remove(TEntity entity)
    {
        EntityEntry<TEntity> entityEntry = Table.Remove(entity);
        return entityEntry.State == EntityState.Deleted;
    }

    public void Update(TEntity entity)
    {
        Table.Update(entity);
    }

    public async Task<int> SaveAsync()
        => await _context.SaveChangesAsync();
    
    IQueryable<TEntity> _getIncludes(IQueryable<TEntity> query, params string[] includes)
    {
        return includes.Aggregate(query, (current, item) => current.Include(item));
    }
}