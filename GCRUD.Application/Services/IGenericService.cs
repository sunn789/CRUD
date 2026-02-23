using GCRUD.Application.Interfaces;
using GCRUD.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;


namespace GCRUD.Application.Services
{
    public interface IGenericService<TDto, TCreateDto, TUpdateDto, TEntity, TId>
         where TDto : class
         where TCreateDto : class
         where TUpdateDto : class, IHasId<TId>   
         where TEntity : ModelPaye<TId>
    {
        Task<TDto?> GetByIdAsync(TId? id);
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<IEnumerable<TDto>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TDto> CreateAsync(TCreateDto createDto);
        Task<TDto?> UpdateAsync(TUpdateDto updateDto);
        Task<bool> DeleteAsync(TId? id);
        Task<TDto?> SetActiveAsync(TId id, bool active);
    }
}
