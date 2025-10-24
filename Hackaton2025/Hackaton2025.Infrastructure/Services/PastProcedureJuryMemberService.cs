using Hackaton2025.Domain.Models.Abstractions;
using Hackaton2025.Domain.Models.Entities;

namespace Hackaton2025.Infrastructure.Services;

public class PastProcedureJuryMemberService : IPastProcedureJuryMemberService
{
    private readonly ApplicationDbContext _applicationDbContext;

    public PastProcedureJuryMemberService(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public void Add(PastProcedureJuryMember entity)
    {
        _applicationDbContext.PastProcedureJuryMembers.Add(entity);
    }

    public void Delete(PastProcedureJuryMember entity)
    {
        _applicationDbContext.PastProcedureJuryMembers.Remove(entity);
    }
}
