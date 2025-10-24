using Hackaton2025.Domain.Models.ValueObjects;

namespace Hackaton2025.Domain.Models.Entities;
public class Teacher
{
    public Teacher(
        string id,
        TeacherTitle title,
        FullName fullName,
        string universityId,
        University? university,
        string universityFactultyId,
        UniversityFaculty? universityFaculty,
        decimal distance,
        DateTime secondLastJuryMemberDate,
        DateTime lastJuryMemberDate)
    {
        Id = id;
        Title = title;
        FullName = fullName;
        UniversityId = universityId;
        University = university;
        UniversityFactultyId = universityFactultyId;
        UniversityFaculty = universityFaculty;
        Distance = distance;
        SecondLastJuryMemberDate = secondLastJuryMemberDate;
        LastJuryMemberDate = lastJuryMemberDate;
    }

    public string Id { get; }

    public TeacherTitle Title { get; private set; }

    public FullName FullName { get; }

    public string UniversityId { get; }

    public University? University { get; }

    public string UniversityFactultyId { get; }

    public UniversityFaculty? UniversityFaculty { get; }

    public decimal Distance { get; }

    public DateTime SecondLastJuryMemberDate { get; private set; }

    public DateTime LastJuryMemberDate { get; private set; }

    public void AddLastJuryMemberDate(DateTime lastJuryMemberDate)
    {
        SecondLastJuryMemberDate = LastJuryMemberDate;
        LastJuryMemberDate = lastJuryMemberDate;
    }
}
