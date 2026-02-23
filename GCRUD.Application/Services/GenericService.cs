using AutoMapper;
using GCRUD.Core.Entities;
using GCRUD.Core.Interfaces;
using GCRUD.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;




namespace GCRUD.Application.Services
{
    public class GenericService<TDto, TCreateDto, TUpdateDto, TEntity, TId> :
         IGenericService<TDto, TCreateDto, TUpdateDto, TEntity, TId>
         where TDto : class
         where TCreateDto : class
         where TUpdateDto : class, IHasId<TId>
         where TEntity : ModelPaye<TId>, IActivatable, new()
    {
        protected readonly IRepository<TEntity, TId> _repository;
        protected readonly IMapper _mapper;

        public GenericService(IRepository<TEntity, TId> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<TDto?> GetByIdAsync(TId? id)
        {
            if (id == null) return null;
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<TDto>(entity);
        }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public virtual async Task<IEnumerable<TDto>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = await _repository.FindAsync(predicate);
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public virtual async Task<TDto> CreateAsync(TCreateDto createDto)
        {
            var entity = _mapper.Map<TEntity>(createDto);
            entity.CreateDate = DateTime.UtcNow;
            entity.ModifyDate = DateTime.UtcNow;
            entity.Active = true;
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<TDto?> UpdateAsync(TUpdateDto updateDto)
        {
            if (updateDto.Id == null)
                return null;

            var entity = await _repository.GetByIdAsync(updateDto.Id);
            if (entity == null)
                return null;

            _mapper.Map(updateDto, entity);
            entity.ModifyDate = DateTime.UtcNow;

            _repository.Update(entity);
            await _repository.SaveChangesAsync();
            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<bool> DeleteAsync(TId? id)
        {
            if (id == null) return false;
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            _repository.Delete(entity);
            return await _repository.SaveChangesAsync();
        }
        public virtual async Task<TDto?> SetActiveAsync(TId id, bool active)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return null;

            entity.Active = active;
            entity.ModifyDate = DateTime.UtcNow;

            _repository.Update(entity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<TDto>(entity);
        }
    }
}