namespace Hackaton2025.Domain.Models.Entities;

public class UniversityFaculty
{
    public UniversityFaculty(string id, string universityId, string name)
    {
        Id = id;
        UniversityId = universityId;
        Name = name;
    }

    public string Id { get; }

    public string UniversityId { get; }

    public string Name { get; }
}
