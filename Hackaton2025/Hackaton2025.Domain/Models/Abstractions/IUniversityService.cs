using Hackaton2025.Domain.Models.Entities;

namespace Hackaton2025.Domain.Models.Abstractions;
public interface IUniversityService
{
    public Task<ICollection<University>> GetAllAsync(
        int page, 
        int pageSize, 
        CancellationToken cancellationToken);
}
