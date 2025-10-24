using Hackaton2025.Domain.Models.Entities;

namespace Hackaton2025.Domain.Models.Abstractions;

public interface IPastProcedureJuryMemberService
{
    public void Add(PastProcedureJuryMember entity);
    public void Delete(PastProcedureJuryMember entity);
}