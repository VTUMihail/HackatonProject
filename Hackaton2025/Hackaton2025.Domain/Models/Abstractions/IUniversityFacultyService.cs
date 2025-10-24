using Hackaton2025.Domain.Models.Entities;

namespace Hackaton2025.Domain.Models.Abstractions;

public interface IUniversityFacultyService
{
    public Task<ICollection<UniversityFaculty>> GetAllAsync(
        string universityId, 
        int page, 
        int pageSize, 
        CancellationToken cancellationToken);
}