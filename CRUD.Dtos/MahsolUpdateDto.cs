using System.ComponentModel.DataAnnotations;

namespace CRUD.Dtos;

public class MahsolUpdateDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    public int Ghimat { get; set; }

    public int Tedad { get; set; }

    public bool Active { get; set; }
    public int Priority { get; set; }
}
