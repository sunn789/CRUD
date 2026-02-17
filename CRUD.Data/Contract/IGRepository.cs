using CRUD.Model;
namespace CRUD.Data.Contract;

public interface IGRepository<T, TId> where T : BaseEntity<TId>
{
    Task<T?> GetMahsolhaByIdAsync(TId id);
    Task<IEnumerable<T?>> GetAllAsync();
    Task<T?> GetMahsolByIdAsync(TId id);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
     Task<bool> SoftDeleteAsync(TId id);
    Task<bool> ExistsAsync(TId id);
    Task SaveChangesAsync();

}