

using GCRUD.Core.Interfaces;

namespace GCRUD.Core.Entities;

public class Mahsol:ModelPaye<Guid>, IActivatable
{
    public string Name { get; set; } = string.Empty;
    public int Ghimat { get; set; }= 0;
    public int Tedad { get; set; } = 0;
}