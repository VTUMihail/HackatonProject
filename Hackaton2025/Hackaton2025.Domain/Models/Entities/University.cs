namespace Hackaton2025.Domain.Models.Entities;

public class University
{
    private University() { }
    public University(string id, string name)
    {
        Id = id;
        Name = name;
        Faculties = [];
        Teachers = [];
    }

    public string Id { get; }

    public string Name { get; }

    public ICollection<UniversityFaculty> Faculties { get; }

    public ICollection<Teacher> Teachers { get; }
}
