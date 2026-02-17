using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace CRUD.Dtos;

public class MahsoCreateDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    public int Ghimat { get; set; }
    public int Tedad { get; set; }
  
    public int Priority { get; set; }
    public bool Active { get; set; } = true;

}