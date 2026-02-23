using GCRUD.Api.Models;
using GCRUD.Application.Interfaces;
using GCRUD.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GCRUD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenericController<TDto, TCreateDto, TUpdateDto, TEntity, TId> : ControllerBase
        where TDto : class
        where TCreateDto : class
        where TUpdateDto : class, IHasId<TId>
        where TEntity : GCRUD.Core.Entities.ModelPaye<TId>, new()
    {
        protected readonly IGenericService<TDto, TCreateDto, TUpdateDto, TEntity, TId> _service;

        public GenericController(IGenericService<TDto, TCreateDto, TUpdateDto, TEntity, TId> service)
        {
            _service = service;
        }

        [HttpGet]
        public virtual async Task<ActionResult<ApiResponse<IEnumerable<TDto>>>> GetAll()
        {
            try
            {
                var data = await _service.GetAllAsync();
                return Ok(ApiResponse<IEnumerable<TDto>>.Success(data, "Data fetched successfully"));
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<IEnumerable<TDto>>.Error($"Error fetching data: {ex.Message}"));
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<ApiResponse<TDto>>> GetById([FromRoute] TId id)
        {
            try
            {
                var data = await _service.GetByIdAsync(id);

                if (data is null)
                    return Ok(ApiResponse<TDto>.Error("Item not found"));

                return Ok(ApiResponse<TDto>.Success(data, "Item fetched successfully"));
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<TDto>.Error($"Error fetching item: {ex.Message}"));
            }
        }

        [HttpPost]
        public virtual async Task<ActionResult<ApiResponse<TDto>>> Create([FromBody] TCreateDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(ApiResponse<TDto>.Error("Invalid request body"));

                var created = await _service.CreateAsync(dto);
                return Ok(ApiResponse<TDto>.Success(created, "Item created successfully"));
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<TDto>.Error($"Error creating item: {ex.Message}"));
            }
        }

        [HttpPut]
        public virtual async Task<ActionResult<ApiResponse<TDto>>> Update([FromBody] TUpdateDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(ApiResponse<TDto>.Error("Invalid request body"));

                var updated = await _service.UpdateAsync(dto);

                if (updated is null)
                    return Ok(ApiResponse<TDto>.Error("Item not found or invalid Id"));

                return Ok(ApiResponse<TDto>.Success(updated, "Item updated successfully"));
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<TDto>.Error($"Error updating item: {ex.Message}"));
            }
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<ApiResponse<object>>> Delete([FromRoute] TId id)
        {
            try
            {
                var deleted = await _service.DeleteAsync(id);

                if (!deleted)
                    return Ok(ApiResponse<object>.Error("Item not found or delete failed"));

                return Ok(ApiResponse<object>.Success(null, "Item deleted successfully"));
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<object>.Error($"Error deleting item: {ex.Message}"));
            }
        }
    
    [HttpPatch("activate/{id}")]
        public virtual async Task<ActionResult<ApiResponse<TDto>>> Activate([FromRoute] TId id)
        {
            try
            {
                var result = await _service.SetActiveAsync(id, true);
                if (result is null)
                    return Ok(ApiResponse<TDto>.Error("Item not found"));

                return Ok(ApiResponse<TDto>.Success(result, "Item activated successfully"));
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<TDto>.Error($"Error activating item: {ex.Message}"));
            }
        }

        [HttpPatch("deactivate/{id}")]
        public virtual async Task<ActionResult<ApiResponse<TDto>>> Deactivate([FromRoute] TId id)
        {
            try
            {
                var result = await _service.SetActiveAsync(id, false);
                if (result is null)
                    return Ok(ApiResponse<TDto>.Error("Item not found"));

                return Ok(ApiResponse<TDto>.Success(result, "Item deactivated successfully"));
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<TDto>.Error($"Error deactivating item: {ex.Message}"));
            }
        }
    }
}