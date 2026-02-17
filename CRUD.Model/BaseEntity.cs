using System.Collections.Concurrent;

namespace CRUD.Model;

public class BaseEntity<TId>
{
    public TId? Id { get; set; }
    public bool Active { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime ModifyDate { get; set; }
    public int Priority { get; set; }
}