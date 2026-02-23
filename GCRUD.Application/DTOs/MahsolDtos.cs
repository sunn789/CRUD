
using GCRUD.Application.Interfaces;


namespace GCRUD.Application.DTOs;


public class MahsolDto :IHasId<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Ghimat { get; set; }= 0;
    public int Tedad { get; set; } = 0;
}
public class CreateMahsolDto
{
    public string Name { get; set; } = string.Empty;
    public int Ghimat { get; set; }= 0;
    public int Tedad { get; set; } = 0;
}
public class UpdateMahsolDto : IHasId<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Ghimat { get; set; }= 0;
    public int Tedad { get; set; } = 0;

}
