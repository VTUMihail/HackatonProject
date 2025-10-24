using Hackaton2025.Domain.Models.Abstractions;
using Hackaton2025.Domain.Models.Entities;

namespace Hackaton2025.Infrastructure.Services;

public class PastProcedureJuryService : IPastProcedureJuryService
{
    private readonly ApplicationDbContext _applicationDbContext;

    public PastProcedureJuryService(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public void Add(PastProcedureJury entity)
    {
        _applicationDbContext.PastProcedureJuries.Add(entity);
    }

    public void Delete(PastProcedureJury entity)
    {
        _applicationDbContext.PastProcedureJuries.Remove(entity);
    }
}