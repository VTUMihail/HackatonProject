namespace Hackaton2025.Domain.Models.Entities;

public class University
{
    public University() { }
    public University(string id, string name)
    {
        Id = id;
        Name = name;
        Faculties = [];
        Teachers = [];
    }

    public string Id { get; set; }

    public string Name { get; set; }

    public ICollection<UniversityFaculty> Faculties { get; }

    public ICollection<Teacher> Teachers { get; }
}
