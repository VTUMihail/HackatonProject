namespace Hackaton2025.Domain.Models.Entities;

public class University
{
    public University(string id, string name)
    {
        Id = id;
        Name = name;
        Faculties = [];
    }

    public string Id { get; }

    public string Name { get; }

    public ICollection<UniversityFaculty> Faculties { get; }
}
