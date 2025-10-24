using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackaton2025.Domain.Models.Entities;
public class PastProcedure
{
    public PastProcedure(
        string id, 
        PastProcedureType type,
        DateTime createdAt)
    {
        Id = id;
        Type = type;
        CreatedAt = createdAt;
        Juries = [];
    }

    public string Id { get; }

    public PastProcedureType Type { get; }

    public DateTime CreatedAt { get; }

    public ICollection<PastProcedureJury> Juries { get; } = [];
}
