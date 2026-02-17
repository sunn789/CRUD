namespace CRUD.Dtos;

public class MahsolDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Ghimat { get; set; }
    public int Tedad { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime ModifyDate { get; set; }
    public int Priority { get; set; }
    public bool Active { get; set; }
}