using Hackaton2025.Domain.Models.Abstractions;
using Hackaton2025.Domain.Models.Entities;
using Hackaton2025.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Hackaton2025.Infrastructure.Services;

public class UniversityService : IUniversityService
{
    private readonly IPaginator _paginator;
    private readonly ApplicationDbContext _applicationDbContext;

    public UniversityService(
        IPaginator paginator,
        ApplicationDbContext applicationDbContext)
    {
        _paginator = paginator;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<ICollection<University>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        var skip = _paginator.GetSkip(page, pageSize);
        var result = await _applicationDbContext
            .Universities
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return result;
    }
}
