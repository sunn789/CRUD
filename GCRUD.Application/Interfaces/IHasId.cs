using System;
using System.Collections.Generic;
using System.Text;

namespace GCRUD.Application.Interfaces
{
    public interface IHasId<TId>
    {
        TId? Id { get; set; }
    }
}
