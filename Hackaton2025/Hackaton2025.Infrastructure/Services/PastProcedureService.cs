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

    private PastProcedureJury CreateJury(
     PastProcedureType type,
     int minProf,
     int minForeign,
     int amount)
    {
        var forbiddenTeacherIds = _applicationDbContext
            .PastProcedures
            .OrderByDescending(a => a.CreatedAt)
            .Select(a => a.Juries.SelectMany(a => a.JuryMembers.Select(a => a.TeacherId)))
            .First()
            .GroupBy(id => id)
            .Where(g => g.Count() == 2)
            .Select(g => g.Key)
            .ToList();

        var professors = _applicationDbContext
            .Teachers
            .Where(t => !forbiddenTeacherIds.Contains(t.Id))
            .OrderBy(t => t.Distance)
            .Where(t => t.Title == TeacherTitle.Professor)
            .Take(minProf)
            .ToList();

        forbiddenTeacherIds.AddRange(professors.Select(a => a.Id));
        var foreigns = _applicationDbContext
            .Teachers
            .OrderBy(t => t.Distance)
            .Include(t => t.University)
            .Where(t => !forbiddenTeacherIds.Contains(t.Id) && t.University!.Name != "VTU")
            .Take(minForeign)
            .ToList();

        forbiddenTeacherIds.AddRange(foreigns.Select(a => a.Id));

        var remainingCount = amount - (professors.Count + foreigns.Count);
        var remaining = _applicationDbContext
            .Teachers
            .OrderBy(t => t.Distance)
            .Include(t => t.University)
            .Where(t => !forbiddenTeacherIds.Contains(t.Id))
            .Take(remainingCount)
            .ToList();

        var selectedMembers = professors
            .Union(foreigns)
            .Union(remaining)
            .ToList();

        var jury = new PastProcedureJury(Guid.NewGuid().ToString(), type, DateTime.Now);

        foreach (var member in selectedMembers)
        {
            jury.JuryMembers.Add(new PastProcedureJuryMember(jury.Id, member.Id));
            member.AddLastJuryMemberDate(DateTime.Now);
        }

        _applicationDbContext.SaveChanges();

        return jury;
    }

    public void Add(PastProcedure entity)
    {
        PastProcedureJury jury;

        switch (entity.Type)
        {
            case PastProcedureType.Doctor:
                jury = CreateJury(entity.Type, 1, 3, 5);
                break;

            case PastProcedureType.DoctorOfScience:
                jury = CreateJury(entity.Type, 3, 4, 7);
                break;

            case PastProcedureType.AssociateProfessor:
                jury = CreateJury(entity.Type, 3, 3, 7);
                break;

            case PastProcedureType.Professor:
                jury = CreateJury(entity.Type, 4, 3, 7);
                break;

            default:
                throw new InvalidOperationException("Непознат тип процедура");
        }

        entity.Juries.Add(jury);

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
        //switch (type)
        //{
        //    case PastProcedureType.Doctor:
        //        AddDoctorateJuryAsync(cancellationToken);

        //}

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

    private async Task<PastProcedureJury> AddDoctorateJuryAsync(CancellationToken cancellationToken)
    {
        var professor = await _applicationDbContext
            .Teachers
            .OrderBy(a => a.Distance)
            .FirstOrDefaultAsync(a => a.Title == TeacherTitle.Professor, cancellationToken);

        //var foreigns = await _applicationDbContext
        //    .Teachers
        //    .OrderBy(a => a.Distance)
        //    .Where(a => a.Title == TeacherTitle.Professor);

        return null;
    }

}
