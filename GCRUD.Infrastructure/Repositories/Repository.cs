using GCRUD.Core.Entities;
using GCRUD.Core.Interfaces;
using GCRUD.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace GCRUD.Infrastructure.Repositories;

public class Repository<T, TId> : IRepository<T, TId> where T : ModelPaye<TId>
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task<List<T>> GetAllAsync()
    {
       return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(TId id)
    {
        return await _dbSet.FindAsync(id);
            }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() >0;
    }

    public void Update(T entity)
    {
         _dbSet.Update(entity);               }
}