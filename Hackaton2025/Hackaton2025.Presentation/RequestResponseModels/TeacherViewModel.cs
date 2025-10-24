using Hackaton2025.Domain.Models.Entities;
using Hackaton2025.Domain.Models.ValueObjects;

public class TeacherViewModel
{
    public string Id { get; set; }

    public TeacherTitle Title { get; set; }

    public FullName FullName { get; set; }

    public string UniversityId { get; set; }

    public string UniversityFactultyId { get; set; }

    public decimal Distance { get; set; }

    public DateTime? SecondLastJuryMemberDate { get; set; }

    public DateTime? LastJuryMemberDate { get; set; }
}