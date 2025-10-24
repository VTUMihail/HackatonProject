using Hackaton2025.Domain.Models.Entities;

public class TeacherViewModel
{
    public string Id { get; set; }

    public TeacherTitle Title { get; set; }

    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string UniversityId { get; set; }

    public string UniversityFactultyId { get; set; }

    public decimal Distance { get; set; }

    public DateTime? SecondLastJuryMemberDate { get; set; }

    public DateTime? LastJuryMemberDate { get; set; }
}