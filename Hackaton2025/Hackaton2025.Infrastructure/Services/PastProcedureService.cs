using Hackaton2025.Domain.Models.Abstractions;
using Hackaton2025.Domain.Models.Entities;
using Hackaton2025.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Hackaton2025.Infrastructure.Services;

public class PastProcedureService : IPastProcedureService
{
    private readonly IPaginator _paginator;
    private readonly ApplicationDbContext _applicationDbContext;

    public PastProcedureService(
        IPaginator paginator,
        ApplicationDbContext applicationDbContext)
    {
        _paginator = paginator;
        _applicationDbContext = applicationDbContext;
    }

    public void Add(PastProcedure entity)
    {
        _applicationDbContext.PastProcedures.Add(entity);
    }

    public void Delete(PastProcedure entity)
    {
        _applicationDbContext.PastProcedures.Remove(entity);
    }

    public async Task<ICollection<PastProcedure>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        var skip = _paginator.GetSkip(page, pageSize);
        var result = await _applicationDbContext
            .PastProcedures
            .Include(a => a.JuryMembers)
            .ThenInclude(a => a.Teacher)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return result;
    }

    public async Task<PastProcedure?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var result = await _applicationDbContext
            .PastProcedures
            .Include(a => a.JuryMembers)
            .ThenInclude(a => a.Teacher)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        return result;
    }

    public void Update(PastProcedure entity)
    {
        _applicationDbContext.PastProcedures.Update(entity);
    }
}
