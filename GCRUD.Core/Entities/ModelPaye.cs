namespace GCRUD.Core.Entities;
public class ModelPaye<TId>
{
    public TId? Id { get; set; }
    public bool Active { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime ModifyDate { get; set; }
    public int Priority { get; set; }
}