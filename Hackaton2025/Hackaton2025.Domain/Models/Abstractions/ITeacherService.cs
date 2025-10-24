using Hackaton2025.Domain.Models.Entities;

namespace Hackaton2025.Domain.Models.Abstractions;

public interface ITeacherService
{
    public Task<ICollection<Teacher>> GetAllAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken);

    public Task<Teacher?> GetByIdAsync(
        string id,
        CancellationToken cancellationToken);

    public void Add(Teacher entity);
    public void Update(Teacher entity);
    public void Delete(Teacher entity);
}
