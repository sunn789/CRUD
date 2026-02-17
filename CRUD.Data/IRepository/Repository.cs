using CRUD.Data.Contract;
using CRUD.Model;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Data.Irepository;

public class GRepository<T, TId> : IGRepository<T, TId> where T : BaseEntity<TId>
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;
    public GRepository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        entity.CreateDate = DateTime.UtcNow;
        entity.ModifyDate = DateTime.UtcNow;
        await _dbSet.AddAsync(entity);

    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<bool> ExistsAsync(TId id)
    {
        return await _dbSet.AnyAsync(c => c.Id!.Equals(id));
    }

    public async Task<IEnumerable<T?>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetMahsolByIdAsync(TId id)
    {
        return await _dbSet.FindAsync(id);
    }

    public Task<T?> GetMahsolhaByIdAsync(TId id)
    {
        return _dbSet.FirstOrDefaultAsync(c => c.Id!.Equals(id) && c.Active);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    public async Task<bool> SoftDeleteAsync(TId id)
    {
        var entity = await GetMahsolByIdAsync(id);
        if (entity == null)
            return false;

        entity.Active = false;
        entity.ModifyDate = DateTime.UtcNow;
        _dbSet.Update(entity);
        await SaveChangesAsync();
        return true;
    }
    
    public void Update(T entity)
    {
        entity.ModifyDate = DateTime.UtcNow;
        _dbSet.Update(entity);
    }
}