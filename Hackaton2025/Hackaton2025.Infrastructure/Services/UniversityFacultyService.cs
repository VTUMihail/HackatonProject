using Hackaton2025.Domain.Models.Abstractions;
using Hackaton2025.Domain.Models.Entities;
using Hackaton2025.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Hackaton2025.Infrastructure.Services;

public class UniversityFacultyService : IUniversityFacultyService
{
    private readonly IPaginator _paginator;
    private readonly ApplicationDbContext _applicationDbContext;

    public UniversityFacultyService(
        IPaginator paginator,
        ApplicationDbContext applicationDbContext)
    {
        _paginator = paginator;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<ICollection<UniversityFaculty>> GetAllAsync(
        string universityId,
        int page, 
        int pageSize, 
        CancellationToken cancellationToken)
    {
        var skip = _paginator.GetSkip(page, pageSize);
        var result = await _applicationDbContext
            .UniversityFaculties
            .Where(a => a.UniversityId == universityId)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return result;
    }
}