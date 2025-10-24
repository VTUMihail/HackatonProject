using Hackaton2025.Domain.Models.Entities;

namespace Hackaton2025.Domain.Models.Abstractions;

public interface IPastProcedureService
{
    public Task<ICollection<PastProcedure>> GetAllAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken);

    public Task<PastProcedure?> GetByIdAsync(
        string id,
        CancellationToken cancellationToken);

    public void Add(PastProcedure entity);
    public void Update(PastProcedure entity);
    public void Delete(PastProcedure entity);
}
