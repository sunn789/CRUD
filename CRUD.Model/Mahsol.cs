namespace CRUD.Model;

public class Mahsol:BaseEntity<Guid>
{
    public string Name { get; set; } = string.Empty;
    public int Ghimat { get; set; }= 0;
    public int Tedad { get; set; } = 0;
}