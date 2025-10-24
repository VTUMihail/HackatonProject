using Hackaton2025.Domain.Models.Abstractions;
using Hackaton2025.Domain.Models.Entities;
using Hackaton2025.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Hackaton2025.Infrastructure.Services;
public class TeacherService : ITeacherService
{
    private readonly IPaginator _paginator;
    private readonly ApplicationDbContext _applicationDbContext;

    public TeacherService(
        IPaginator paginator,
        ApplicationDbContext applicationDbContext)
    {
        _paginator = paginator;
        _applicationDbContext = applicationDbContext;
    }

    public void Add(Teacher entity)
    {
        _applicationDbContext.Teachers.Add(entity);
    }

    public void Delete(Teacher entity)
    {
        _applicationDbContext.Teachers.Remove(entity);
    }

    public async Task<ICollection<Teacher>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        var skip = _paginator.GetSkip(page, pageSize);
        var result = await _applicationDbContext
            .Teachers
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return result;
    }

    public async Task<Teacher?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var result = await _applicationDbContext.Teachers.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        return result;
    }

    public void Update(Teacher entity)
    {
        _applicationDbContext.Teachers.Update(entity);
    }
}
