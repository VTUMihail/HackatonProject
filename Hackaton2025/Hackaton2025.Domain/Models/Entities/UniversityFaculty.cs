namespace Hackaton2025.Domain.Models.Entities;

public class UniversityFaculty
{
    private UniversityFaculty() { }
    public UniversityFaculty(string id, string universityId, string name)
    {
        Id = id;
        UniversityId = universityId;
        Name = name;
    }

    public string Id { get; set; }

    public string UniversityId { get; set; }

    public string Name { get; set;}
}
