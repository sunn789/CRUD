
using GCRUD.Core.Entities;
using System.Linq.Expressions;
namespace GCRUD.Core.Interfaces;

public interface IRepository<TEntity, TId> where TEntity : ModelPaye<TId>
{
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(TId id);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<bool> SaveChangesAsync();
}