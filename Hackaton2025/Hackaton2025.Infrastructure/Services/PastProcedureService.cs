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
            .Include(a => a.Juries)
            .ThenInclude(a => a.JuryMembers)
            .ThenInclude(a => a.Teacher)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return result;
    }

    public async Task<ICollection<PastProcedure>> GetAllAsync(
        PastProcedureType type,
        int page, 
        int pageSize, 
        CancellationToken cancellationToken)
    {
        switch (type)
        {
            case PastProcedureType.Doctor:
                AddDoctorateJuryAsync();
            case PastProcedureType.DoctorOfScience:

            case PastProcedureType.AssociateProfessor:

            case PastProcedureType.Professor:

        }

        var skip = _paginator.GetSkip(page, pageSize);
        var result = await _applicationDbContext
            .PastProcedures
            .Include(a => a.Juries)
            .ThenInclude(a => a.JuryMembers)
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
            .Include(a => a.Juries)
            .ThenInclude(a => a.JuryMembers)
            .ThenInclude(a => a.Teacher)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        return result;
    }

    public void Update(PastProcedure entity)
    {
        _applicationDbContext.PastProcedures.Update(entity);
    }

    private Task<PastProcedureJury> AddDoctorateJuryAsync(CancellationToken cancellationToken)
    {
        var professor = _applicationDbContext
            .Teachers
            .OrderBy(a => a.Distance)
            .FirstOrDefaultAsync(a => a.Title == TeacherTitle.Professor, cancellationToken);

        var professor = _applicationDbContext
            .Teachers
            .OrderBy(a => a.Distance)
            .FirstOrDefaultAsync(a => a.Title == TeacherTitle.Professor, cancellationToken);
    }
}
