using CRUD.Data.Contract;
using CRUD.Dtos;
using CRUD.Model;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class MahsolController : ControllerBase
{
    private readonly IGRepository<Mahsol, Guid> _repository;
    public MahsolController(IGRepository<Mahsol, Guid> repository)
    {
        _repository = repository;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<MahsolDto>>> GetAll()
    {
        var mahsol = await _repository.GetAllAsync();

        var Mahsolresult = mahsol
         .Where(x => x is not null)
            .Select(x => new MahsolDto
            {
                Id = x!.Id,
                Active = x.Active,
                Name = x.Name,
                CreateDate = x.CreateDate,
                Ghimat = x.Ghimat,
                ModifyDate = x.ModifyDate,
                Priority = x.Priority,
                Tedad = x.Tedad
            });
        return Ok(Mahsolresult);
    }


    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MahsolDto>> GetById(Guid id)
    {
        var item = await _repository.GetMahsolByIdAsync(id);
        if (item is null) return NotFound();

        return Ok(new MahsolDto
        {
            Id = item.Id,
            Name = item.Name,
            Ghimat = item.Ghimat,
            Tedad = item.Tedad,
            CreateDate = item.CreateDate,
            ModifyDate = item.ModifyDate,
            Priority = item.Priority,
            Active = item.Active
        });
    }

    [HttpPost]
    public async Task<ActionResult<MahsolDto>> CreateMahsole([FromBody] MahsoCreateDto dto)
    {
        var item = new Mahsol
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Ghimat = dto.Ghimat,
            Tedad = dto.Tedad,
            Active = dto.Active,
            Priority = dto.Priority,
        };
        await _repository.AddAsync(item);
        await _repository.SaveChangesAsync();

        return Ok(new { id = item.Id });
    }
    [HttpPut]
    public async Task<IActionResult> UpdateMahsol([FromBody] MahsolDto dto)
    {
        var item = await _repository.GetMahsolByIdAsync(dto.Id);
        if (item is null) return NotFound();

        item.Name = dto.Name;
        item.Ghimat = dto.Ghimat;
        item.Tedad = dto.Tedad;
        item.Active = dto.Active;
        item.Priority = dto.Priority;
        item.ModifyDate = DateTime.UtcNow;

        _repository.Update(item);
        await _repository.SaveChangesAsync();

        return NoContent();
    }
    [HttpPatch("deactivate/{id:guid}")]
    public async Task<IActionResult> Deactivate(Guid id, [FromBody] MahsolDto dto)
    {
        var item = await _repository.GetMahsolByIdAsync(id);
        if (item is null) return NotFound();
        item.Active = false;
        item.ModifyDate = DateTime.UtcNow;
        item.Name = dto.Name;
        item.Ghimat = dto.Ghimat;
        item.Tedad = dto.Tedad;
        item.Priority = dto.Priority;

        _repository.Update(item);
        await _repository.SaveChangesAsync();

        return Ok(new { message = "mahsol Gheyr-faal shod" });
    }
    [HttpPatch("activate/{id:guid}")]
    public async Task<IActionResult> Activate(Guid id, [FromBody] MahsolDto dto)
    {
        var item = await _repository.GetMahsolByIdAsync(id);
        if (item is null) return NotFound();

        item.Active = true;
        item.ModifyDate = DateTime.UtcNow;
        item.Name = dto.Name;
        item.Ghimat = dto.Ghimat;
        item.Tedad = dto.Tedad;
        item.Priority = dto.Priority;

        _repository.Update(item);
        await _repository.SaveChangesAsync();

        return Ok(new { message = "mahsol faal shod" });
    }




    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteMahsol(Guid id)
    {
        var item = await _repository.GetMahsolByIdAsync(id);
        if (item is null) return NotFound();

        _repository.Delete(item);
        await _repository.SaveChangesAsync();

        return NoContent();
    }

}

