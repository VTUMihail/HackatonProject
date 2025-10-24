using Hackaton2025.Domain.Models.Entities;

namespace Hackaton2025.Domain.Models.Abstractions;

public interface IPastProcedureJuryService
{
    public void Add(PastProcedureJury entity);
    public void Delete(PastProcedureJury entity);
}